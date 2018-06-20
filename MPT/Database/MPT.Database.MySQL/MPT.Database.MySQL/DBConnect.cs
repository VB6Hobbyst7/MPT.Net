using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace MPT.Database.MySQL
{
    // See: https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    /// <summary>
    /// Class that handles basic connection and manipulation of a database.
    /// </summary>
    public class DbConnect
    {
        private MySqlConnection _connection;
        private readonly string _server;
        private readonly string _database;
        private readonly string _uid;
        private readonly string _password;

        // TODO: Make methods to Write SQL scripts rather than execute them

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnect"/> class.
        /// </summary>
        /// <param name="server">The server name.</param>
        /// <param name="database">The database name.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="password">The password.</param>
        public DbConnect(string server,
            string database,
            string userId,
            string password)
        {
            _server = server;
            _database = database;
            _uid = userId;
            _password = password;

            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            string connectionString = "SERVER=" + _server + ";" + "DATABASE=" + _database + ";" + 
                                        "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";

            _connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <returns><c>true</c> if the connection is opened, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">
        /// Cannot connect to server.  Contact administrator
        /// or
        /// Invalid username/password, please try again
        /// or
        /// </exception>
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                    return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator", ex);

                    case 1045:
                        throw new Exception("Invalid username/password, please try again", ex);
                    default:
                        throw new Exception($"Error {ex.Number} occurred.", ex);
                }
            }
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }




        /// <summary>
        /// Inserts the tuples into the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="values">The header/value pairs.</param>
        public void Insert(string tableName,
            List<KeyValuePair<string, string>> values)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return;
            if (values == null || values.Count == 0) return;

            // Create query
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            foreach (KeyValuePair<string, string> valueSet in values)
            {
                sbHeader.Append("`").Append(valueSet.Key).Append("`,");
                sbValue.Append("'").Append(valueSet.Value).Append("',");
            }

            // Assign values and trim final comma
            string tupleHeaders = sbHeader.ToString();
            tupleHeaders = tupleHeaders.Substring(0, tupleHeaders.Length - 1);
            string tupleValues = sbValue.ToString();
            tupleValues = tupleHeaders.Substring(0, tupleValues.Length - 1);

            // "INSERT INTO tableinfo (name, age) VALUES ('John Smith', '33')"
            string command = $"INSERT INTO `{tableName}` ({tupleHeaders}) VALUES ({tupleValues})";
            ExecuteNonQuery(command);
        }

        /// <summary>
        /// Updates the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="setValues">The values to set.</param>
        /// <param name="whereClause">The 'where clause' to determine the row(s) to update.</param>
        public void Update(string tableName,
            List<KeyValuePair<string, string>> setValues,
            string whereClause = "")
        {
            if (string.IsNullOrWhiteSpace(tableName)) return;
            if (setValues == null || setValues.Count == 0) return;
            if (string.IsNullOrWhiteSpace(whereClause)) return;

            // Create query
            StringBuilder sbSet = new StringBuilder();
            foreach (KeyValuePair<string, string> valueSet in setValues)
            {
                sbSet.Append("`").Append(valueSet.Key).Append("`=").Append("'").Append(valueSet.Value).Append("', ");
            }

            // Assign values and trim final comma
            string setClause = sbSet.ToString();
            setClause = setClause.Substring(0, setClause.Length - 1).TrimEnd();

            // "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'"
            string command = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";
            ExecuteNonQuery(command);
        }

        /// <summary>
        /// Deletes a row in the specified table based on criteria.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="whereClause">The 'where clause' used to determine the row(s) to delete.</param>
        public void Delete(string tableName,
            string whereClause = "")
        {
            if (string.IsNullOrWhiteSpace(tableName)) return;
            if (string.IsNullOrWhiteSpace(whereClause)) return;

            // "DELETE FROM tableinfo WHERE name='John Smith'"
            string command = $"DELETE FROM `{tableName}` WHERE {whereClause}";
            ExecuteNonQuery(command);
        }

        /// <summary>
        /// Executes the non-query command.
        /// Returns the number of rows affected.
        /// </summary>
        /// <param name="command">The command, written in MySQL.</param>
        public int ExecuteNonQuery(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return 0;
            if (!OpenConnection()) return 0;
            MySqlCommand cmd = new MySqlCommand(command, _connection);
            int rowsAffected = cmd.ExecuteNonQuery();
            CloseConnection();
            return rowsAffected;
        }



        /// <summary>
        /// Selects rows from the specified table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="selectQuery">The selection query.</param>
        /// <param name="additionalClause">The additional clause to refine the selection.</param>
        /// <param name="includeHeaders">True: The first entry in each array will contain the header name.</param>
        /// <returns>List&lt;System.String&gt;[].</returns>
        public List<string>[] Select(string tableName,
            string selectQuery = "*",
            string additionalClause = "",
            bool includeHeaders = false)
        {
            List<string>[] list = new List<string>[0];
            if (string.IsNullOrWhiteSpace(tableName)) return list;

            string query = $"SELECT {selectQuery} FROM {tableName}";
            if (!string.IsNullOrEmpty(additionalClause)) query += " " + additionalClause;
            
            //Open connection
            if (!OpenConnection()) return list;
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            using (MySqlDataReader dataReader = cmd.ExecuteReader())
            {
                //Create a list to store the result
                int fieldCount = dataReader.FieldCount;
                list = new List<string>[fieldCount];
                
                // Write Headers
                if (includeHeaders)
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        list[i].Add(dataReader.GetName(i));
                    }
                }

                // Write values
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        list[i].Add(dataReader[i].ToString());
                    }
                }
            }
            CloseConnection();
                
            return list;
        }


        /// <summary>
        /// Counts the specified query in the table specified.
        /// Returns -1 if unsuccessful.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="countQuery">The count query.
        /// Default is to count all rows.</param>
        /// <param name="additionalClause">The additional clause for refining the count.</param>
        /// <returns>System.Int32.</returns>
        public int Count(string tableName, 
            string countQuery = "*",
            string additionalClause = "")
        {
            int count = -1;
            if (string.IsNullOrWhiteSpace(tableName)) return count;

            // "SELECT Count(*) FROM tableinfo"
            string query = $"SELECT Count({countQuery}) FROM `{tableName}`";
            if (!string.IsNullOrEmpty(additionalClause)) query += " " + additionalClause;

            if (!OpenConnection()) return count;
            MySqlCommand cmd = new MySqlCommand(query, _connection);

            //ExecuteScalar will return one value
            count = int.Parse(cmd.ExecuteScalar() + "");
            CloseConnection();

            return count;
        }



        /// <summary>
        /// Backs up the MySQL database at the specified path root.
        /// If successful, the complete path to the file is returned.
        /// </summary>
        /// <param name="pathRoot">The path root.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="IOException">Error , unable to backup!</exception>
        public string Backup(string pathRoot = "")
        {
            try
            {
                DateTime time = DateTime.Now;
                int year = time.Year;
                int month = time.Month;
                int day = time.Day;
                int hour = time.Hour;
                int minute = time.Minute;
                int second = time.Second;
                int millisecond = time.Millisecond;
                if (string.IsNullOrWhiteSpace(pathRoot)) pathRoot = "C:\\";

                //Save file with the current date as a filename
                string fileName = 
                    $"MySqlBackup_{_database}_{year}-{month}-{day}-{hour}-{minute}-{second}-{millisecond}.sql";
                string path = Path.Combine(pathRoot, fileName);
                
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "mysqldump",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    Arguments = $@"-u{_uid} -p{_password} -h{_server} {_database}",
                    UseShellExecute = false
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return string.Empty;
                    string output = process.StandardOutput.ReadToEnd();
                    using (StreamWriter file = new StreamWriter(path))
                    {
                        file.WriteLine(output);
                        process.WaitForExit();
                    }
                }
                return path;
            }
            catch (IOException ex)
            {
                throw new IOException("Error , unable to backup!", ex);
            }
        }

        /// <summary>
        /// Restores the specified database file at the path provided.
        /// </summary>
        /// <param name="pathDatabase">The path to the database.</param>
        /// <exception cref="IOException">Error , unable to restore!</exception>
        public void Restore(string pathDatabase)
        {
            if (!File.Exists(pathDatabase)) return;
            try
            {
                string input;
                using (StreamReader file = new StreamReader(pathDatabase))
                {
                    input = file.ReadToEnd();
                }
                if (string.IsNullOrWhiteSpace(input)) return;

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "mysql",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false,
                    Arguments = $@"-u{_uid} -p{_password} -h{_server} {_database}",
                    UseShellExecute = false
                };

                using (Process process = Process.Start(psi))
                {
                    if (process == null) return;
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.Close();
                    process.WaitForExit();
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Error , unable to restore!", ex);
            }
        }
    }
}
