using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using IO = System.IO;

namespace MPT.Batch
{
    /// <summary>
    /// Contains routines for writing, running, and removing batch files.
    /// </summary>
    public class BatchFile
    {
        #region Fields

        private readonly string _defaultWorkingDirectory = Assembly.GetExecutingAssembly().Location;

        #endregion


        #region Properties
        /// <summary>
        /// Path to the batch file.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// True: A file will be deleted after a run so that a new one is created on next write, or no batch file is left after a run.
        /// </summary>
        public bool DeleteAfterRun { get; set; } = true;

        /// <summary>
        /// True: If a filename already exists at the path specified, it will be deleted so that a new one is created.
        /// </summary>
        /// <value><c>true</c> if [replace existing batch]; otherwise, <c>false</c>.</value>
        public bool ReplaceExistingBatch { get; set; } = true;

        /// <summary>
        /// Wait until batch process has exited before deleting the batch file. (Currently does not seem to work).
        /// </summary>
        public bool WaitForExit { get; set; } = true;

        /// <summary>
        /// True: Batch run will not be visible from a command console window.
        /// </summary>
        public bool ConsoleIsVisible { get; set; } = true;

        /// <summary>
        /// True: Console window will close after the batch run.
        /// </summary>
        public bool CloseAfterRun { get; set; } = true;


        private string _workingDirectory;

        /// <summary>
        /// The initial directory for the process to be started.
        /// If not specified, the assembly location will be used.
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                return string.IsNullOrEmpty(_workingDirectory) ? _defaultWorkingDirectory : _workingDirectory;
            }

            set
            {
                _workingDirectory = value;
            }
        }
        #endregion


        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public BatchFile(string path)
        {
            Path = path;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes all files relating to a supplied list of extensions using a batch file.
        /// </summary>
        /// <param name="directoryPath">Path to the directory containing the files to be deleted.</param>
        /// <param name="fileExtensionList">List of the file extensions of which all files will be deleted.</param>
        /// <remarks></remarks>
        public void DeleteFiles(string directoryPath,
                                List<string> fileExtensionList)
        {
            string commandLine = fileExtensionList.Aggregate("", (current, fileExtension) => 
                        current + ("FOR /R " + IO.Path.PathSeparator + directoryPath + IO.Path.PathSeparator + " %%G IN (" + fileExtension + ") DO del " + 
                        IO.Path.PathSeparator + "%%G" + (char) 34 + "\n"));
            
            if (CloseAfterRun) { commandLine += commandLine + "exit";}

            Write(commandLine);

            Run();
        }

        
        /// <summary>
        /// Writes the specified command string to a new batch file. 
        /// If no lines exist, this begins a new batch file.
        /// </summary>
        /// <param name="value">String to write to the batch file.</param>
        /// <remarks></remarks>
        public void Write(string value)
        {
            if(ReplaceExistingBatch && IO.File.Exists(Path)) {IO.File.Delete(Path);}
            Append(value);
        }



        /// <summary>
        /// Appends the specified command line to a batch file. 
        /// If no lines exist, this begins a new batch file.
        /// </summary>
        /// <param name="value">String to write to append to the batch file.</param>
        /// <remarks></remarks>
        public void Append(string value)
        {
            using (IO.StreamWriter objWriter = new IO.StreamWriter(Path, true))
            {
                objWriter.WriteLine(value);
            }
        }

        /// <summary>
        /// Runs batch file then deletes batch file if specified.
        /// </summary>
        /// <remarks></remarks>
        public void Run()
        {
            Process batchProcess = new Process
            {
                StartInfo =
                {
                    FileName = Path,
                    WorkingDirectory = WorkingDirectory,
                    CreateNoWindow = !ConsoleIsVisible,
                    WindowStyle = ConsoleIsVisible ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden
                }
            };
            
            batchProcess.Start();

            if (WaitForExit) {batchProcess.WaitForExit();}

        // Deletes Batch File
            System.Threading.Thread.Sleep(1000);
            if (DeleteAfterRun) {IO.File.Delete(Path);}
        }
        #endregion
    }
}
