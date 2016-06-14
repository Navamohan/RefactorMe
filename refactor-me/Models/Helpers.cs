using System.Data.SqlClient;
using System.Web;

namespace ProductManagement.Models
{
    /// <summary>
    /// Helper class to do basic CRUD operations
    /// </summary>
    public class Helpers
    {
        //Need to move this connection string to web configuration
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        /// <summary>
        /// Helper method to return new sql connection
        /// </summary>
        /// <returns>New SqlConnection</returns>
        public static SqlConnection NewConnection()
        {
            var connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
            return new SqlConnection(connstr);
        }

        /// <summary>
        /// Common method to Execute Non Query
        /// </summary>
        /// <param name="sqlStatement">Sql statement</param>
        public static void ExecuteNonQuery(string sqlStatement)
        {
            var conn = NewConnection();
            conn.Open();
            var cmd = new SqlCommand(sqlStatement, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute sql select statement
        /// </summary>
        /// <param name="selectStatement">Select statement</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string selectStatement)
        {
            var conn = NewConnection();
            var cmd = new SqlCommand(selectStatement, conn);
            conn.Open();

            return cmd.ExecuteReader();
        }
    }
}