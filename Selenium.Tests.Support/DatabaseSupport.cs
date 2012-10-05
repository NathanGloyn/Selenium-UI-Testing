using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Management.Smo;

namespace Selenium.Tests.Support
{
    /// <summary>
    /// Class provides functionality to help developer perform unit testing on data access code
    /// </summary>
    public class DatabaseSupport : IDisposable
    {
        private SqlConnection _sqlConnection;
        private Server _targetServer;
        private bool _disposed = false;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="connectionString">standard connection string for db to interact with</param>
        public DatabaseSupport(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _targetServer = new Server(_sqlConnection.DataSource);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DatabaseSupport()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates a new empty database on the server if one of that name doesn't already exist
        /// </summary>
        /// <param name="name">Name of the database to create</param>
        public void CreateDB(string name)
        {
            if (!_targetServer.Databases.Contains(name))
            {
                var toCreate = new Microsoft.SqlServer.Management.Smo.Database(_targetServer, name);
                toCreate.Create();
            }
        }

        /// <summary>
        /// Drops a database on the server
        /// </summary>
        /// <param name="name">name of the database to drop</param>
        public void DropDB(string name)
        {
            if (_targetServer.Databases.Contains(name))
            {
                _targetServer.KillAllProcesses(_sqlConnection.Database);
                _targetServer.KillDatabase(name);
            }
            else
            {
                throw new InvalidOperationException("Unable to drop the dabase as unable to find a database called " + name);
            }
        }

        /// <summary>
        /// Method creates\recreates db
        /// </summary>
        /// <param name="createScriptPath">Path to the <b>directory</b> that contains the script(s) for creating database</param>
        /// <remarks>The db is only created\recreated if the script file(s) or db object have changed since last created.
        /// <para>If the a series of scripts is being used then a file Order.txt needs to be provided that simply holds 
        /// a list of the files in the order they are to be run</para></remarks>
        public void RecreateDbSchema(string createScriptPath)
        {
            //	1 - DB does not exist --> recreate, run file(s)
            //	2 - DB exists, but file(s) date  > schema --> file(s) more recent, therefore run it
            //	otherwise the DB exists, but file(s) date < schema --> schema has not changed, do nothing.
            DateTime dateLastSchemaFileUpdate = GetLastFileUpdate(createScriptPath);
            DateTime dateLastDbUpdate = GetLastSchemaChange();

            if (dateLastDbUpdate >= DateTime.MaxValue)
            {
                RecreateDbWithData(createScriptPath);
            }
            else if (dateLastSchemaFileUpdate > dateLastDbUpdate)
            {
                RecreateDbWithData(createScriptPath);
            }

        }

        /// <summary>
        /// Runs a specified script against a target db
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        public void RunScript(string scriptPath)
        {
            RunScript(scriptPath, false, null);
        }

        /// <summary>
        /// Runs a specified script against a target db
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="parameters">Parameters to use within the script</param>
        public void RunScript(string scriptPath, Dictionary<string, string> parameters)
        {
            RunScript(scriptPath, false, parameters);
        }

        /// <summary>
        /// Runs a specified script against a target db 
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="returnResults">Flag indicating we expect to be return results from executing the script</param>
        /// <returns>Dataset containing data returned from executing the script</returns>
        public DataSet RunScript(string scriptPath, bool returnResults)
        {
            return RunScript(scriptPath, returnResults, null);
        }

        /// <summary>
        /// Runs a specified script against a target db
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="returnResults">Flag indicating we expect to be return results from executing the script</param>
        /// <param name="parameters">Parameters to use within the script</param>
        /// <returns>Dataset containing data returned from executing the script</returns>
        public DataSet RunScript(string scriptPath, bool returnResults, Dictionary<string, string> parameters)
        {
            DataSet results = null;
            string script = LoadScript(scriptPath);

            // If parameters provided then alter the script accordingly
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    script = script.Replace(parameter.Key, parameter.Value);
                }
            }

            if (returnResults)
            {
                results = _targetServer.ConnectionContext.ExecuteWithResults(script);
            }
            else
            {
                _targetServer.ConnectionContext.ExecuteNonQuery(script);
            }

            return results;
        }

        /// <summary>
        /// Method to dispose of objects used 
        /// </summary>
        /// <param name="disposing">Flag to indicate we are currenlty disposing the object</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_sqlConnection.State != ConnectionState.Closed)
                        _sqlConnection.Close();
                    _disposed = true;
                }

            }
        }

        /// <summary>
        /// Recreates the db with base data
        /// </summary>
        private void RecreateDbWithData(string createScriptPath)
        {


            if (Directory.GetFiles(createScriptPath).Length > 1)
            {

                // ensure that any processes using the db are killed so that the db can be dropped
                // if the creation script wants to
                _targetServer.KillAllProcesses(_sqlConnection.Database);

                // running multiple files to create db
                // open Order.txt to find files to run and the order
                using (StreamReader sr = new StreamReader(Path.Combine(createScriptPath, "Order.txt")))
                {
                    while (sr.Peek() >= 0)
                    {
                        RunScript(Path.Combine(createScriptPath, sr.ReadLine()));
                    }
                }

            }
            else
            {
                // running single file to create db
                string[] file = Directory.GetFiles(createScriptPath);
                RunScript(file[0]);
            }

            //hold off until path has been updated:
            DateTime dateLastDBUpdate = GetLastSchemaChange();
            while (dateLastDBUpdate == DateTime.MaxValue)
            {
                Console.WriteLine("Waiting for new DB schema change to take affect");
                System.Threading.Thread.Sleep(1000);
                dateLastDBUpdate = GetLastSchemaChange();
            }
        }

        /// <summary>
        /// Get the data of the last schema change
        /// </summary>
        /// <returns>DateTime the database schema was last changed.</returns>
        private DateTime GetLastSchemaChange()
        {

            string sql = "select max(crDate) from sysobjects";
            DateTime lastUpdate = DateTime.MaxValue;
            using (SqlCommand cmd = new SqlCommand(sql, _sqlConnection))
            {

                try
                {
                    this._sqlConnection.Open();
                    lastUpdate = (DateTime)cmd.ExecuteScalar();
                }
                catch
                {
                    // We need catch here so that when used in conjunction with unit testing the 
                    // unit test doesn't fail at this point.
                }
                finally
                {
                    this._sqlConnection.Close();
                    SqlConnection.ClearPool(_sqlConnection);
                }
            }

            return lastUpdate;
        }

        /// <summary>
        /// Get the last write time of specified file
        /// </summary>
        /// <param name="createScriptPath">Path to directory where create script(s) are held.</param>
        /// <returns>DateTime newest file in directory was written to.</returns>
        private static DateTime GetLastFileUpdate(string createScriptPath)
        {
            DirectoryInfo di = new DirectoryInfo(createScriptPath);

            FileInfo[] files = di.GetFiles();

            DateTime lastWriteTime = DateTime.MinValue;

            // Go through all files in the directory and find the latest time a file
            // has been written to
            foreach (FileInfo fi in files)
            {
                if (fi.LastWriteTime > lastWriteTime) lastWriteTime = fi.LastWriteTime;
            }

            return lastWriteTime;
        }

        /// <summary>
        /// Loads the path with the data provided
        /// </summary>
        private static string LoadScript(string path)
        {
            string script;

            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException("No path has been provided for the path. Unable to load.");

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    script = sr.ReadToEnd();
                }
            }
            catch (IOException)
            {
                throw new IOException("Error occured whilst trying to LoadScript");
            }

            return script;
        }
    }

}
