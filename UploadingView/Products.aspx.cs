using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

namespace UploadingView
{
    public partial class Products : Page
    {
        /*Linking the app with the database*/
        static string connString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmCentral-DB;Integrated Security=SSPI;";
        SqlConnection dbConn = new SqlConnection(connString);
        SqlCommand dbComm = new SqlCommand();
        SqlDataAdapter dbAdapter = new SqlDataAdapter();
        DataTable dt;

        public string farmerUsername;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public DataTable GetModule()//A method to get the module data from the database to the app
        {
            dbConn.Open();

            string query = "SELECT ProductName, ProductDescription, ProductDateSupplied, ProductType, Username" +
                "FROM Products WHERE Username = @Username";//SQL Statement that displays product data 

            dbComm = new SqlCommand(query, dbConn);

            dbComm.Parameters.AddWithValue("@Username", lblDisplayFarmer.Text);

            dbAdapter = new SqlDataAdapter(dbComm);

            dt = new DataTable();

            dbAdapter.Fill(dt);
            dbConn.Close();
            return dt;
        }

        public void GetDropDownList()//method to load a module to a dropdown list(combo box)
        {
            dbConn.Open();
            string getModule = "SELECT p.ProductName FROM Products p, Farmer f WHERE f.Username = p.Username AND p.Username = @Username"; //SQL Query that links the "tbl_Module" table to the "tbl_User" table
            dbComm = new SqlCommand(getModule, dbConn);

            dbComm.Parameters.AddWithValue("@Username", lblDisplayFarmer.Text);
            SqlDataReader dr = dbComm.ExecuteReader();
            while (dr.Read())
            {
                ddlProductInfo.Items.Add(dr["ProductName"].ToString());
            }
            dbConn.Close();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == string.Empty && txtDesc.Text.Trim() == string.Empty && txtType.Text.Trim() == string.Empty && txtDateSupplied.Text.Trim() == string.Empty)
            {
                Response.Write("Please fill in all the required information");//Displaying message for empty text fields
            }
            else
            {
                try
                {
                    Product products = new Product();//creating an instance of the "Products" class

                    /*The following is assigning the text fields of the products to the module data properties*/
                    products.ProductName = txtProductName.Text;
                    products.ProductDescription = txtDesc.Text;
                    products.ProductDateSupplied = txtDateSupplied.Text;
                    products.ProductType = txtType.Text;
                    products.Username = lblDisplayFarmer.Text;

                    dbConn.Open();
                    string insertQuery = "INSERT INTO Products (ProductName, ProductDescription, ProductType, ProductDateSupplied, Username)" +
                        "VALUES(@ProductName, @ProductDescription, @ProductDateSupplied, @ProductType, @Username)";//SQL statement that inserts the product data to the database
                    dbComm = new SqlCommand(insertQuery, dbConn);
                    dbComm.Parameters.AddWithValue("@ProductName", products.ProductName);
                    dbComm.Parameters.AddWithValue("@ProductDescription", products.ProductDescription);
                    dbComm.Parameters.AddWithValue("@ProductDateSupplied", products.ProductDateSupplied);
                    dbComm.Parameters.AddWithValue("@ProductType", products.ProductType);
                    dbComm.Parameters.AddWithValue("@Username", products.Username);

                    int x = dbComm.ExecuteNonQuery();
                    //message confirming that a product has been added
                    Response.Write("Product Has Been Added.");
                    dbConn.Close();

                    /*The following empty textbox variables clears the data right after the a product has been successfully added*/
                    txtProductName.Text = "";
                    txtDesc.Text = "";
                    txtDateSupplied.Text = "";
                    txtType.Text = "";


                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

            

        }
        protected void txtDisplay_Load(object sender, EventArgs e)
        {
            lblDisplayFarmer.Text = FarmerLogin.farmerUsername;
            GetDropDownList();

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void txtDateSupplied_TextChanged(object sender, EventArgs e)
        {

        }
    }
}