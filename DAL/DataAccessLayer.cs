using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DataAccessLayer
    {
        /*Linking the app with the database*/
        static string connectionString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmCentral-DB;Integrated Security=SSPI;";
        SqlConnection dbConnection = new SqlConnection(connectionString);
        SqlCommand command;
        SqlCommand dbComm;
        SqlDataAdapter dbAdapter;
        DataTable dt;

        
        public int InsertProduct(Product products)
        {
            
            dbConnection.Open();

            string query = @"INSERT INTO Products (ProductName, ProductDescription, ProductDateSupplied, ProductType, Username)
                             VALUES (@ProductName, @ProductDescription, @ProductDateSupplied, @ProductType, @Username)";


            command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@ProductName", products.ProductName);
            command.Parameters.AddWithValue("@ProductDescription", products.ProductDescription);
            command.Parameters.AddWithValue("@ProductDateSupplied", products.ProductDateSupplied);
            command.Parameters.AddWithValue("@ProductType", products.ProductType);
            command.Parameters.AddWithValue("@Username", products.Username);

            int x = command.ExecuteNonQuery();
            dbConnection.Close();
            return x;
        }
        

        /*Hashing the password*/
        public string EncryptData(string password)
        {
            // Adopted here: http://www.dotnetfunda.com/forums/show/16822/decrypt-password-in-aspnet

            string ms = string.Empty;
            byte[] encode = new byte[password.Length]; //md.password.Length
            encode = Encoding.UTF8.GetBytes(password);//md.password
            ms = Convert.ToBase64String(encode);
            return ms;
        }
    }
}

