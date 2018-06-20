// ***********************************************************************
// Assembly         : MPT.Excel
// Author           : Mark
// Created          : 12-16-2016
//
// Last Modified By : Mark
// Last Modified On : 01-24-2017
// ***********************************************************************
// <copyright file="ExcelWrapper.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.Excel
{
    using System;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;      //Microsoft Excel 14 object in references-> COM tab

    using MPT.Coding;

    /// <summary>
    /// This class is used for interacting more smoothly with Excel.
    /// </summary>
    public static class ExcelWrapper
    {
        // Regarding COMException swallowed:

        /// <summary>
        /// The VBA_E_IGNORE error that is returned whenever an object model call is invoked while the property browser is suspended. <para /> 
        /// This can occur when clicking around in Excel when the API is called.<para /> 
        /// See: https://social.msdn.microsoft.com/Forums/vstudio/en-US/9168f9f2-e5bc-4535-8d7d-4e374ab8ff09/hresult-800ac472-from-set-operations-in-excel?forum=vsto
        /// </summary>
        private const string VBA_E_IGNORE = "0x800AC472";
        // HRESULT: 0x800AC472 

        /// <summary>
        /// The object required error that is sometimes returned.<para /> 
        /// This can occur when closing Excel while the API is still being called.<para /> 
        /// See: https://social.technet.microsoft.com/Forums/office/en-US/43041fcd-2f67-43d4-913b-5681c584ab67/keep-getting-error-message-exception-from-hresult-0x800a01a8-when-opening-spreadsheets-on?forum=officeitpro
        /// </summary>
        private const string OBJECT_REQUIRED = "0x800A01A8";
        // HRESULT: 0x800A01A8

        /// <summary>
        /// Opens the application workbook.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="path">The path to the file to open.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        /// <returns>Excel.Workbook.</returns>
        public static Excel.Workbook AppWorkbooksOpen(Excel.Application application, string path, int attempts = 10000, int intervalMS = 100)
        {
            return Retry.NotNull(() => Try(() => application.Workbooks.Open(path)), new TimeSpan(intervalMS), attempts);
        }


        /// <summary>
        /// Clears formulas, values and formatting from the range.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        public static void RangeClear(Excel.Range range, int attempts = 10000, int intervalMS = 100)
        {
            Retry.DoTrue(() => TryBool(() => range.Clear()), new TimeSpan(intervalMS), attempts);
        }


        /// <summary>
        /// Clears formulas and values from the range.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        public static void RangeClearContents(Excel.Range range, int attempts = 10000, int intervalMS = 100)
        {
            Retry.DoTrue(() => TryBool(() => range.ClearContents()), new TimeSpan(intervalMS), attempts);
        }

        /// <summary>
        /// Copies the entire row specified by the range.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="rowCopyOffset">The row copy offset.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        public static void RangeCopyEntireRowAttempts(Excel.Range range, int rowCopyOffset = 0, int attempts = 10000, int intervalMS = 100)
        {
            Excel.Range rangeNext = Retry.NotNull(() => Try(() => range.Offset[rowCopyOffset].EntireRow), new TimeSpan(intervalMS), attempts);
            Retry.DoTrue(() => TryBool(() => range.Copy(rangeNext)), new TimeSpan(intervalMS), attempts);
        }

        /// <summary>
        /// Copies the entire column specified by the range.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="columnCopyOffset">The column copy offset.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        public static void RangeCopyEntireColumnAttempts(Excel.Range range, int columnCopyOffset = 0, int attempts = 10000, int intervalMS = 100)
        {
            Excel.Range rangeNext = Retry.NotNull(() => Try(() => range.Offset[0, columnCopyOffset].EntireColumn), new TimeSpan(intervalMS), attempts);
            Retry.DoTrue(() => TryBool(() => range.Copy(rangeNext)), new TimeSpan(intervalMS), attempts);
        }


        /// <summary>
        /// Writes the value to the provided range.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="attempts">The number of attempts.</param>
        /// <param name="intervalMS">The interval between attempts. [ms].</param>
        public static void RangeWriteValue(Excel.Range range, string value, int attempts = 10000, int intervalMS = 100)
        {
            Retry.DoTrue(() => TryBool(() => assignValue(range, value)), new TimeSpan(intervalMS), attempts);
        }

        /// <summary>
        /// Assigns the value.
        /// </summary>
        /// <param name="range">The Excel range.</param>
        /// <param name="value">The value to write.</param>
        private static void assignValue(Excel.Range range, string value)
        {
            range.Value = value;
        }



        /// <summary>
        /// Tries the specified action.
        /// This ignores particular exceptions that are known to occur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns>T.</returns>
        public static T Try<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (COMException eCom)
            {
                if (eCom.Message.Contains(VBA_E_IGNORE))
                {
                    Console.WriteLine(eCom.Message);
                    return default(T);
                }
                if(eCom.Message.Contains(OBJECT_REQUIRED))
                {
                    throw eCom;
                }
                throw eCom;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Tries the action and returns true if successful.
        /// This ignores particular exceptions that are known to occur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if succesful in the action, <c>false</c> otherwise.</returns>
        public static bool TryBool<T>(Func<T> action)
        {
            try
            {
                action();
                return true;
            }
            catch (COMException eCom)
            {
                if (eCom.Message.Contains(VBA_E_IGNORE))
                {
                    Console.WriteLine(eCom.Message);
                    return false;
                }
                else if (eCom.Message.Contains(OBJECT_REQUIRED))
                {
                    throw eCom;
                }
                throw eCom;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Tries the action and returns true if successful.
        /// This ignores particular exceptions that are known to occur.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool TryBool(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (COMException eCom)
            {
                if (eCom.Message.Contains(VBA_E_IGNORE))
                {
                    Console.WriteLine(eCom.Message);
                    return false;
                }
                else if (eCom.Message.Contains(OBJECT_REQUIRED))
                {
                    throw eCom;
                }
                throw eCom;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
