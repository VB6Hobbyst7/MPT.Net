// ***********************************************************************
// Assembly         : MPT.Processor
// Author           : Mark Thomas
// Created          : 12-16-2016
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="ProcessorLibrary.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Diagnostics;
using System.Linq;

namespace MPT.Processor
{
    /// <summary>
    /// Library for various methods used in regard to processes.
    /// </summary>
    public static class ProcessorLibrary
    {
        // TODO: For both process methods, confirm process is from specified path, if given.
        // See: http://stackoverflow.com/questions/5497064/c-how-to-get-the-full-path-of-running-process

        /// <summary>
        /// Determines if the process with the corresponding filename exists.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="fileName">Name of the file used by the process.</param>
        /// <returns><c>true</c> if the process with the corresponding filename exists, <c>false</c> otherwise.</returns>
        public static bool ProcessExists(string processName, 
            string fileName)
        {
            if (!ProcessIsRunning(processName)) return false;
            Process[] processes = Process.GetProcesses();
            return processes.Any(process => 
                process.ProcessName.ToString() == processName && 
                process.MainWindowTitle.Contains(fileName));
        }

        /// <summary>
        /// Gets the process identifier with the corresponding filename in use.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="fileName">Name of the file used by the process.</param>
        /// <returns>System.Int32.</returns>
        public static int GetProcessID(string processName, 
            string fileName)
        {
            if (!ProcessIsRunning(processName)) return 0;
            Process[] processes = Process.GetProcesses();
            return (from process in processes
                    where process.ProcessName.ToString() == processName && 
                          process.MainWindowTitle.Contains(fileName)
                    select process.Id).FirstOrDefault();
        }

        /// <summary>
        /// Processes is running.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns><c>true</c> if the process is running, <c>false</c> otherwise.</returns>
        public static bool ProcessIsRunning(string processName)
        {
            return (Process.GetProcessesByName(processName).Length != 0);
        }
    }
}
