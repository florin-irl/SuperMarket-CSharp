using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.DataAccessLayer
{
    public static class DALHelper
    {
        private static readonly string _connectionString = "Data Source=DESKTOP-IFEPT2A;Initial Catalog=dbHypermarket;Integrated Security=SSPI";


        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                return connection;
            }
        }
    }
}
