// ***********************************************************************
// Assembly         : MPT.Excel
// Author           : Mark
// Created          : 12-16-2016
//
// Last Modified By : Mark
// Last Modified On : 01-24-2017
// ***********************************************************************
// <copyright file="ExcelInteropService.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.Excel
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using Excel = Microsoft.Office.Interop.Excel;      //Microsoft Excel 14 object in references-> COM tab

    /// <summary>
    /// Used to start a new Excel process or attach to an existing one.
    /// </summary>
    public class ExcelInteropService
    {
        /// <summary>
        /// The excel class name.
        /// </summary>
        private const string EXCEL_CLASS_NAME = "EXCEL7";

        /// <summary>
        /// The dw objectid.
        /// </summary>
        private const uint DW_OBJECTID = 0xFFFFFFF0;

        /// <summary>
        /// The rrid.
        /// </summary>
        private static Guid rrid = new Guid("{00020400-0000-0000-C000-000000000046}");

        /// <summary>
        /// Delegate EnumChildCallback.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private delegate bool EnumChildCallback(int hwnd, ref int lParam);

        /// <summary>
        /// Accessible object from window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="dwObjectID">The dw object identifier.</param>
        /// <param name="riid">The riid.</param>
        /// <param name="ptr">The PTR.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("Oleacc.dll")]
        private static extern int AccessibleObjectFromWindow(int hwnd, uint dwObjectID, byte[] riid, ref Excel.Window ptr);

        /// <summary>
        /// The child windows enumeration value.
        /// </summary>
        /// <param name="hWndParent">The h WND parent.</param>
        /// <param name="lpEnumFunc">The lp enum function.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("User32.dll")]
        private static extern bool EnumChildWindows(int hWndParent, EnumChildCallback lpEnumFunc, ref int lParam);

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="nMaxCount">The n maximum count.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("User32.dll")]
        private static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Gets the excel interop by either attaching to the process or starting a new one, bsed on the process ID.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <returns>Excel.Application.</returns>
        public static Excel.Application GetExcelInterop(int? processId = null)
        {
            var p = processId.HasValue ? Process.GetProcessById(processId.Value) : Process.Start("excel.exe");
            try
            {
                return new ExcelInteropService().SearchExcelInterop(p);
            }
            catch (Exception)
            {
                Debug.Assert(p != null, "p != null");
                return GetExcelInterop(p.Id);
            }
        }

        /// <summary>
        /// The enumerable child function.
        /// </summary>
        /// <param name="hwndChild">The child window Id.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool EnumChildFunc(int hwndChild, ref int lParam)
        {
            var buf = new StringBuilder(128);
            GetClassName(hwndChild, buf, 128);
            if (buf.ToString() == EXCEL_CLASS_NAME) { lParam = hwndChild; return false; }
            return true;
        }

        /// <summary>
        /// Searches for the the excel interop from the provided process.
        /// </summary>
        /// <param name="p">The process.</param>
        /// <returns>Excel.Application.</returns>
        /// <exception cref="System.Exception">
        /// Excel Main Window Not Found
        /// or
        /// Excel Child Window Not Found
        /// or
        /// Accessible Object Not FOund
        /// </exception>
        private Excel.Application SearchExcelInterop(Process p)
        {
            Excel.Window ptr = null;
            int hwnd = 0;

            int hWndParent = (int)p.MainWindowHandle;
            if (hWndParent == 0) throw new Exception("Excel Main Window Not Found");

            EnumChildWindows(hWndParent, EnumChildFunc, ref hwnd);
            if (hwnd == 0) throw new Exception("Excel Child Window Not Found");

            int hr = AccessibleObjectFromWindow(hwnd, DW_OBJECTID, rrid.ToByteArray(), ref ptr);
            if (hr < 0) throw new Exception("Accessible Object Not FOund");

            return ptr.Application;
        }
    }
}
