// ***********************************************************************
// Assembly         : MPT.Excel
// Author           : Mark
// Created          : 12-16-2016
//
// Last Modified By : Mark
// Last Modified On : 01-24-2017
// ***********************************************************************
// <copyright file="ExcelHelper.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;      //Microsoft Excel 14 object in references-> COM tab
    
    using MPT.Processor;

    /// <summary>
    /// Class with simplified methods for doing common tasks in Excel.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class ExcelFile : IDisposable
    {
        /// <summary>
        /// The process name.
        /// </summary>
        private const string PROCESS_NAME = "EXCEL";

        /// <summary>
        /// The Excel application.
        /// </summary>
        private Excel.Application _xlApp;

        /// <summary>
        /// The current Excel workbook.
        /// </summary>
        private Excel.Workbook _xlWorkbook;

        /// <summary>
        /// The current Excel worksheet.
        /// </summary>
        private Excel._Worksheet _xlWorksheet;

        /// <summary>
        /// The current Excel range.
        /// </summary>
        private Excel.Range _xlRange;

        /// <summary>
        /// True: The Excel object is attached to an existing process.
        /// </summary>
        private bool _attachedToProcess;

        /// <summary>
        /// True: When the Excel object is closed, close the file/application as well.
        /// </summary>
        private bool _totalShutdown;

        /// <summary>
        /// The path to the Excel file.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; private set; }

        /// <summary>
        /// The worksheets in the Excel file.
        /// </summary>
        /// <value>The worksheets.</value>
        public List<Excel._Worksheet> Worksheets { get; private set; } = new List<Excel._Worksheet>();

        /// <summary>
        /// The ranges in the Excel file.
        /// </summary>
        /// <value>The ranges.</value>
        public List<Excel.Range> Ranges { get; private set; } = new List<Excel.Range>();

        /// <summary>
        /// The named ranges in the Excel file.
        /// </summary>
        /// <value>The names.</value>
        public List<Excel.Name> Names { get; private set; } = new List<Excel.Name>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelFile" /> class.
        /// </summary>
        public ExcelFile() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelFile"/> class. <para/> 
        /// If a process already exists from a file started at the file path, this process is attached to. <para/>
        /// Otherwise, a new process is started from the file.
        /// </summary>
        /// <param name="path">The file path.</param>
        public ExcelFile(string path)
        {
            Path = path;
            string fileName = System.IO.Path.GetFileName(path);
            if (ProcessorLibrary.ProcessExists(PROCESS_NAME, fileName))
            {
                AttachToProcess(fileName);
                _attachedToProcess = true;
            }
            else
            {
                Load(path);
            }
        }

        /// <summary>
        /// Loads the Excel file at the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public void Load(string path)
        {
            Path = path;
            Dispose();

            //Create COM Objects. Create a COM object for everything that is referenced
            _xlApp = new Excel.Application();
            Initialize(openWorkbook:true);
        }

        /// <summary>
        /// Attaches to the process started using the file of the provided name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void AttachToProcess(string fileName)
        {
            int processId = ProcessorLibrary.GetProcessID(PROCESS_NAME, fileName);
            if (processId == 0) return;
            _xlApp = ExcelInteropService.GetExcelInterop(processId);
            Initialize(openWorkbook: false);
        }

        /// <summary>
        /// Initializes the specified open workbook.
        /// </summary>
        /// <param name="openWorkbook">if set to <c>true</c> [open workbook].</param>
        private void Initialize(bool openWorkbook)
        {
            if (_xlApp == null) { return; }

            if (openWorkbook)
            {
                _xlWorkbook = ExcelWrapper.AppWorkbooksOpen(_xlApp, Path);
            }
            else
            {
                string fileName = System.IO.Path.GetFileName(Path);
                foreach (Excel.Workbook workbook in _xlApp.Workbooks)
                {
                    if (workbook.Name == fileName)
                    {
                        _xlWorkbook = workbook;
                        break;
                    }
                }
            }
                
            if (_xlWorkbook == null) { return; }
            foreach (Excel._Worksheet worksheet in _xlWorkbook.Sheets)
            {
                Worksheets.Add(worksheet);
            }

            foreach (Excel.Name name in _xlWorkbook.Names)
            {
                Names.Add(name);
            }
        }



        /// <summary>
        /// The values within a 1-dimensional range (a single column or single row).
        /// </summary>
        /// <param name="rangeName">Name of the range.</param>
        /// <param name="includeNull">If set to <c>true</c>, includes empty entries.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> RangeValues1D(string rangeName, 
            bool includeNull = false,
            string worksheet = "")
        {
            List<string> rangeValues1D = new List<string>();
            if (_xlWorkbook == null) { return rangeValues1D; }

                _xlRange = getRangeByName(rangeName, worksheet);
            if (_xlRange == null) return rangeValues1D;

            if (_xlRange.Count > 1)
            {
                rangeValues1D = convertToStringList(_xlRange.Cells.Value, includeNull);
            }
            else
            {
                rangeValues1D.Add((string)_xlRange.Cells.Value);
            }

            return rangeValues1D;
        }



        /// <summary>
        /// Returns the cell values within rows beneath the header range name, or within the range column names.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="rangeNames">The range names.</param>
        /// <returns>List&lt;List&lt;System.String&gt;&gt;.</returns>
        public List<List<string>> RangeValuesBelowHeaderAndCorrespondingColumns(string headerRangeName,
            params string[] rangeNames)
        {
            return RangeValuesBelowHeaderAndCorrespondingColumns(headerRangeName, false, 0, "", rangeNames);
        }

        /// <summary>
        /// Returns the cell values within rows beneath the header range name, or within the range column names.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="includeNull">If set to <c>true</c>, includes empty entries.</param>
        /// <param name="numberOfRows">Maximum number of rows to read. 
        /// If this is not set, then reading will stop at the first empty cell.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <param name="rangeNames">The range names.</param>
        /// <returns>List&lt;List&lt;System.String&gt;&gt;.</returns>
        public List<List<string>> RangeValuesBelowHeaderAndCorrespondingColumns(string headerRangeName,
            bool includeNull = false,
            int numberOfRows = 0,
            string worksheet = "", 
            params string[] rangeNames)
        {
            List<List<string>> totalList = new List<List<string>>();
            List<string> rangeValuesBelowHeader = new List<string>();
            if (_xlWorkbook == null) { return totalList; }

            totalList.Add(rangeValuesBelowHeader);
            for (int i = 0; i < rangeNames.Length; i++)
            {
                totalList.Add(new List<string>());
            }


            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return totalList;

            int rowOffset = 1;
            string value;
            do
            {
                value = getValue(_xlRange, rowOffset);
                if (recordValue(value, includeNull))
                {
                    rangeValuesBelowHeader.Add(value);
                    string[] correspondingValues = CorrespondingColumnValues(rowOffset, rangeNames, worksheet);
                    for (int i = 0; i < rangeNames.Length; i++)
                    {
                        totalList[i + 1].Add(correspondingValues[i]);
                    }
                }
                rowOffset++;
            } while (continueOffset(value, rowOffset, numberOfRows));
          
            return totalList;
        }

        /// <summary>
        /// Returns the cell values within columns beside the header range name, or within the range column names.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="rangeNames">The range names.</param>
        /// <returns>List&lt;List&lt;System.String&gt;&gt;.</returns>
        public List<List<string>> RangeValuesBesideHeaderAndCorrespondingRows(string headerRangeName,
            params string[] rangeNames)
        {
            return RangeValuesBesideHeaderAndCorrespondingRows(headerRangeName, false, 0, "", rangeNames);
        }

        /// <summary>
        /// Returns the cell values within columns beside the header range name, or within the range column names.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="includeNull">If set to <c>true</c>, includes empty entries.</param>
        /// <param name="numberOfColumns">Maximum number of columns to read. 
        /// If this is not set, then reading will stop at the first empty cell.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <param name="rangeNames">The range names.</param>
        /// <returns>List&lt;List&lt;System.String&gt;&gt;.</returns>
        public List<List<string>> RangeValuesBesideHeaderAndCorrespondingRows(string headerRangeName,
            bool includeNull = false,
            int numberOfColumns = 0,
            string worksheet = "",
            params string[] rangeNames)
        {
            List<List<string>> totalList = new List<List<string>>();
            List<string> rangeValuesBesideHeader = new List<string>();
            if (_xlWorkbook == null) { return totalList; }

            totalList.Add(rangeValuesBesideHeader);
            for (int i = 0; i < rangeNames.Length; i++)
            {
                totalList.Add(new List<string>());
            }


            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return totalList;

            int columnOffset = 1;
            string value;
            do
            {
                value = getValue(_xlRange, 0, columnOffset);
                if (recordValue(value, includeNull))
                {
                    rangeValuesBesideHeader.Add(value);
                    string[] correspondingValues = CorrespondingRowValues(columnOffset, rangeNames, worksheet);
                    for (int i = 0; i < rangeNames.Length; i++)
                    {
                        totalList[i + 1].Add(correspondingValues[i]);
                    }
                }
                columnOffset++;
            } while (continueOffset(value, columnOffset, numberOfColumns));

            return totalList;
        }


        /// <summary>
        /// Returns the cell values within rows in the column offset based on the header ranges specified.
        /// </summary>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="rangeNames">The range names of the columns offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>System.String[].</returns>
        public string[] CorrespondingColumnValues(int rowOffset, 
            string[] rangeNames,
            string worksheet = "")
        {
            string[] values = new string[rangeNames.Length];
            if (_xlWorkbook == null) { return values; }
            for (int i = 0; i < rangeNames.Length; i++)
            {
                Excel.Range xlRange = getRangeByName(rangeNames[i], worksheet);
                if (xlRange != null && xlRange.Count == 1)
                {
                    values[i] = getValue(xlRange, rowOffset);
                }
                else
                {
                    values[i] = "";
                }
            }
            return values;
        }

        /// <summary>
        /// Returns the cell values within columns in the row offset based on the header ranges specified.
        /// </summary>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="rangeNames">The range names of the rows offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>System.String[].</returns>
        public string[] CorrespondingRowValues(int columnOffset,
            string[] rangeNames,
            string worksheet = "")
        {
            string[] values = new string[rangeNames.Length];
            if (_xlWorkbook == null) { return values; }
            for (int i = 0; i < rangeNames.Length; i++)
            {
                Excel.Range xlRange = getRangeByName(rangeNames[i], worksheet);
                if (xlRange != null && xlRange.Count == 1)
                {
                    values[i] = getValue(xlRange, 0, columnOffset);
                }
                else
                {
                    values[i] = "";
                }
            }
            return values;
        }


        /// <summary>
        /// Returns the cell values within rows beneath the header range.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="includeNull">If set to <c>true</c>, includes empty entries.</param>
        /// <param name="numberOfRows">Maximum number of rows to read. 
        /// If this is not set, then reading will stop at the first empty cell.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> RangeValuesBelowHeader(string headerRangeName,
            bool includeNull = false,
            int numberOfRows = 0,
            string worksheet = "")
        {
            List<string> rangeValuesBelowHeader = new List<string>();
            if (_xlWorkbook == null) { return rangeValuesBelowHeader; }

            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return rangeValuesBelowHeader;
            
            int rowOffset = 1;
            string value;
            do
            {
                value = getValue(_xlRange, rowOffset);
                if (recordValue(value, includeNull))
                {
                    rangeValuesBelowHeader.Add(value);
                }
                rowOffset++;
            } while (continueOffset(value, rowOffset, numberOfRows));

            return rangeValuesBelowHeader;
        }

      

        /// <summary>
        /// Returns the cell values within columns beside the header range.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="includeNull">If set to <c>true</c>, includes empty entries.</param>
        /// <param name="numberOfColumns">Maximum number of columns to read. 
        /// If this is not set, then reading will stop at the first empty cell.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> RangeValuesBesideHeader(string headerRangeName,
            bool includeNull = false,
            int numberOfColumns = 0,
            string worksheet = "")
        {
            List<string> rangeValuesBesideHeader = new List<string>();
            if (_xlWorkbook == null) { return rangeValuesBesideHeader; }

            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return rangeValuesBesideHeader;

            int columnOffset = 1;
            string value;
            do
            {
                value = getValue(_xlRange, 0, columnOffset);
                if (recordValue(value, includeNull))
                {
                    rangeValuesBesideHeader.Add(value);
                }
                columnOffset++;
            } while (continueOffset(value, columnOffset, numberOfColumns));

            return rangeValuesBesideHeader;
        }


        /// <summary>
        /// True: The value exists on the same row as the range name. <para />
        /// It is assumed that the ranges refer only to single cells.
        /// </summary>
        /// <param name="rangeNamesvalues">The range names and their values for single cells.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ExistOnSameRow(Dictionary<string, string> rangeNamesvalues,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return false; }
            int rowOffset = 0;
            string value = "";
            do
            {
                rowOffset++;
                bool matchingRow = true;
                foreach (string rangeName in rangeNamesvalues.Keys)
                {
                    _xlRange = getRangeByName(rangeName, worksheet);
                    // Only do this to ranges that are single cells.
                    if (_xlRange == null || _xlRange.Count > 1) return false;

                    if (string.Compare(
                            rangeNamesvalues[rangeName],
                            getValue(_xlRange, rowOffset),
                            StringComparison.OrdinalIgnoreCase) == 0) { continue; }

                    matchingRow = false;
                    break;
                }
                if (matchingRow) { return true; }
            } while (!string.IsNullOrEmpty(value));

            return false;
        }

        /// <summary>
        /// True: The value exists on the same column as the range name. <para />
        /// It is assumed that the ranges refer only to single cells.
        /// </summary>
        /// <param name="rangeNamesvalues">The range names and their values for single cells.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ExistOnSameColumn(Dictionary<string, string> rangeNamesvalues,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return false; }
            int columnOffset = 0;
            string value = "";
            do
            {
                columnOffset++;
                bool matchingRow = true;
                foreach (string rangeName in rangeNamesvalues.Keys)
                {
                    _xlRange = getRangeByName(rangeName, worksheet);
                    // Only do this to ranges that are single cells.
                    if (_xlRange == null || _xlRange.Count > 1) return false;

                    if (string.Compare(
                            rangeNamesvalues[rangeName],
                            getValue(_xlRange, 0, columnOffset), 
                            StringComparison.OrdinalIgnoreCase) == 0) { continue; }

                    matchingRow = false;
                    break;
                }
                if (matchingRow) { return true; }
            } while (!string.IsNullOrEmpty(value));

            return false;
        }



        /// <summary>
        /// Gets the row offset of the last filled cell.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>System.Int32.</returns>
        public int GetRowOffsetLastFilled(string headerRangeName,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return 0; }
            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return 0;

            int rowOffset = 0;
            string value;
            do
            {
                rowOffset++;
                value = getValue(_xlRange, rowOffset);
            } while (!string.IsNullOrEmpty(value));

            return (rowOffset - 1);
        }

        /// <summary>
        /// Gets the column offset of the last filled cell.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>System.Int32.</returns>
        public int GetColumnOffsetLastFilled(string headerRangeName,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return 0; }
            _xlRange = getRangeByName(headerRangeName, worksheet);
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return 0;

            int columnOffset = 0;
            string value;
            do
            {
                columnOffset++;
                value = getValue(_xlRange, 0, columnOffset);
            } while (!string.IsNullOrEmpty(value));

            return (columnOffset - 1);
        }




        /// <summary>
        /// Clears formulas, values and formatting from the row range.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void ClearRow(string headerRangeName, 
            int rowOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[rowOffset].EntireRow;
            ExcelWrapper.RangeClear(_xlRange);
        }

        /// <summary>
        /// Clears formulas and values from the row.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void ClearRowContents(string headerRangeName, 
            int rowOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[rowOffset].EntireRow;
            ExcelWrapper.RangeClearContents(_xlRange);
        }

        /// <summary>
        /// Clears formulas, values and formatting from the column range.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void ClearColumn(string headerRangeName,
            int columnOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[0, columnOffset].EntireRow;
            ExcelWrapper.RangeClear(_xlRange);
        }

        /// <summary>
        /// Clears formulas and values from the column.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void ClearColumnContents(string headerRangeName,
            int columnOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[0, columnOffset].EntireRow;
            ExcelWrapper.RangeClearContents(_xlRange);
        }



        /// <summary>
        /// Duplicates the row down by the specified offset.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void DuplicateRowDown(string headerRangeName, 
            int rowOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[rowOffset].EntireRow;
            ExcelWrapper.RangeCopyEntireRowAttempts(_xlRange, rowCopyOffset: 1);
        }

        /// <summary>
        /// Duplicates the row down by the specified offset.
        /// </summary>
        /// <param name="headerRangeName">Name of the header range.</param>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void DuplicateColumnOver(string headerRangeName,
            int columnOffset,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(headerRangeName, worksheet).Offset[0, columnOffset].EntireColumn;
            ExcelWrapper.RangeCopyEntireColumnAttempts(_xlRange, columnCopyOffset: 1);
        }


        /// <summary>
        /// Writes the values within the provided offset from the corresponding single cell range value.
        /// </summary>
        /// <param name="rangeNamesvalues">Name of the single cell range and corresponding value.</param>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void WriteValues(Dictionary<string, string> rangeNamesvalues, 
            int rowOffset = 0, 
            int columnOffset = 0,
           string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            foreach (string rangeName in rangeNamesvalues.Keys)
            {
                WriteValue(rangeName, rangeNamesvalues[rangeName], rowOffset, columnOffset, worksheet);
            }
        }

        /// <summary>
        /// Writes the value in the cell at the specified offset.
        /// </summary>
        /// <param name="rangeName">Name of the single cell range.</param>
        /// <param name="rangeValue">The range value.</param>
        /// <param name="rowOffset">The row offset from the header.</param>
        /// <param name="columnOffset">The column offset from the header.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        public void WriteValue(string rangeName, 
            string rangeValue, 
            int rowOffset = 0, 
            int columnOffset = 0,
            string worksheet = "")
        {
            if (_xlWorkbook == null) { return; }
            _xlRange = getRangeByName(rangeName, worksheet).Offset[rowOffset, columnOffset].Cells;
            // Only do this to ranges that are single cells.
            if (_xlRange == null || _xlRange.Count > 1) return;

            //xlRange.Value = rangeValue;
            ExcelWrapper.RangeWriteValue(_xlRange, rangeValue);
        }

        /// <summary>
        /// Saves the Excel file under the specified name.
        /// </summary>
        /// <param name="saveAsName">Name to save the file as. If this is left empty, the file is saved over itself.</param>
        public void Save(string saveAsName = "")
        {
            if (_xlWorkbook == null) { return; }

            if(string.IsNullOrEmpty(saveAsName))
            {
                _xlWorkbook.Save();
            }
            else
            {
                _xlWorkbook.SaveAs(saveAsName);
            }
        }


        /// <summary>
        /// Sets Excel to activate the tab specified by name.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        public void SetTab(string tabName)
        {
            _xlWorksheet = getWorksheetByName(tabName);
            _xlWorksheet.Activate();
        }

        /// <summary>
        /// Sets the program to do a total shutdown. <para />
        /// If not, then the program is released but left open.
        /// </summary>
        public void SetTotalShutdown()
        {
            _totalShutdown = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {            
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            if (_xlRange != null) { Marshal.ReleaseComObject(_xlRange); }
            if (_xlWorksheet != null) { Marshal.ReleaseComObject(_xlWorksheet); }

            foreach (Excel._Worksheet worksheet in Worksheets)
            {
                Marshal.ReleaseComObject(worksheet);
            }

            foreach (Excel.Name name in Names)
            {
                Marshal.ReleaseComObject(name);
            }

            foreach (Excel.Range range in Ranges)
            {
                Marshal.ReleaseComObject(range);
            }

            if (_totalShutdown)
            {
                Marshal.ReleaseComObject(_xlWorkbook);
                quitAndRelease();
            }
            else if (_attachedToProcess && !_totalShutdown)
            {
                _attachedToProcess = false;
                _xlWorkbook = null;
                _xlApp = null;
            }
            else
            {
                //close and release
                if (_xlWorkbook != null)
                {
                    _xlWorkbook.Close();
                    Marshal.ReleaseComObject(_xlWorkbook);
                }
                quitAndRelease();
            }
        }

        /// <summary>
        /// Quits and release the application.
        /// </summary>
        private void quitAndRelease()
        {
            if (_xlApp != null)
            {
                _xlApp.Quit();
                Marshal.ReleaseComObject(_xlApp);
            }
        }

        /// <summary>
        /// Specifies if the value is to be recorded.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="includeNull">if set to <c>true</c> null or empty values will be recorded..</param>
        /// <returns><c>true</c> if the value should be recorded, <c>false</c> otherwise.</returns>
        private static bool recordValue(string value, 
            bool includeNull)
        {
            return !string.IsNullOrEmpty(value) || includeNull;
        }

        /// <summary>
        /// Returns the range value as a string.
        /// </summary>
        /// <param name="xlRange">The Excel range, assumed to be a single cell.</param>
        /// <param name="rowOffset">The row offset.</param>
        /// <param name="columnOffset">The column offset.</param>
        /// <returns>System.String.</returns>
        private static string getValue(Excel.Range xlRange, 
            int rowOffset = 0, 
            int columnOffset = 0)
        {
            string value = Convert.ToString(xlRange.Offset[rowOffset, columnOffset].Cells.Value);
            return value;
        }

        /// <summary>
        /// Continues the offset loop for reading rows or columns if criteria are met.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The current offset.</param>
        /// <param name="maxOffset">The maximum offset.
        /// If less than one, continuation will terminate upon the first empty cell.</param>
        /// <returns><c>true</c> if the offset loops should continue, <c>false</c> otherwise.</returns>
        private static bool continueOffset(string value, 
            int offset, 
            int maxOffset)
        {
            if (maxOffset < 1)
            {
                return !string.IsNullOrEmpty(value);
            }
            return offset <= maxOffset;
        }

        /// <summary>
        /// Converts the array to a string list.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="includeNull">if set to <c>true</c>, empty values will be included in the list.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private List<string> convertToStringList(Array values, 
            bool includeNull = false)
        {

            // create a new string array
            List<string> stringList = new List<string>();

            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(i, 1) == null && includeNull)
                    stringList.Add("");
                else if (values.GetValue(i, 1) != null)
                    stringList.Add(values.GetValue(i, 1).ToString());
            }
            
            return stringList;
        }


        /// <summary>
        /// Gets the the worksheet specified by name.
        /// </summary>
        /// <param name="worksheetName">Name of the worksheet.</param>
        /// <returns>Excel._Worksheet.</returns>
        private Excel._Worksheet getWorksheetByName(string worksheetName)
        {
            foreach (Excel._Worksheet worksheet in Worksheets)
            {
                if (string.Compare(
                        worksheet.Name, 
                        worksheetName, 
                        StringComparison.Ordinal) == 0)
                {
                    return worksheet;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the Excel range by the provided range name.
        /// </summary>
        /// <param name="rangeName">Name of the range.</param>
        /// <param name="worksheetName">Worksheet (by name) that contains the range name.</param>
        /// <returns>Excel.Range.</returns>
        private Excel.Range getRangeByName(string rangeName,
            string worksheetName = "")
        {
            if (!string.IsNullOrEmpty(worksheetName))
            {
                _xlWorksheet = getWorksheetByName(worksheetName);
                return getRangeByLocalName(rangeName, _xlWorksheet);
            }

            foreach (Excel.Name name in Names)
            {
                if (string.Compare(
                        name.Name, 
                        rangeName, 
                        StringComparison.Ordinal) == 0)
                {
                    return name.RefersToRange;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the Excel range by the provided local range name.
        /// </summary>
        /// <param name="rangeName">Name of the range.</param>
        /// <param name="worksheet">Worksheet that contains the range name.</param>
        /// <returns>Excel.Range.</returns>
        private Excel.Range getRangeByLocalName(string rangeName,
            Excel._Worksheet worksheet)
        {
            foreach (Excel.Name name in worksheet.Names)
            {
                if (string.Compare(
                            name.Name, 
                            rangeName, 
                            StringComparison.Ordinal) == 0)
                {
                    return name.RefersToRange;
                }
            }
            return null;
        }
    }
}
