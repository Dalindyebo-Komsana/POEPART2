using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadingView
{
    public partial class Employee : Page
    {
        /*Linking the app with the database*/
        static string connString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmCentral-DB;Integrated Security=SSPI;";
        SqlConnection dbConn = new SqlConnection(connString);
        SqlCommand dbComm = new SqlCommand();
        SqlDataAdapter dbAdapter = new SqlDataAdapter();
        DataTable dt;

        public string employeeUsername;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)//If statements that prevents the duplication of an account
            {
                string connString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmCentral-DB;Integrated Security=SSPI;";
                SqlConnection dbConn = new SqlConnection(connString);
                dbConn.Open();
                string userCount = "SELECT COUNT(*) FROM Farmer WHERE Username='" + txtUsernameFR.Text + "'";
                SqlCommand dbComm = new SqlCommand(userCount, dbConn);
                int tempVar = Convert.ToInt32(dbComm.ExecuteScalar().ToString());
                if (tempVar == 1)
                {
                    Response.Write("User Account Already Exists.");
                }
                dbConn.Close();
            }
        }


        DAL.DataAccessLayer encrypt = new DAL.DataAccessLayer();//instance of the Data Access Layer class that retrieves the method to hash the user password

        /*Linking the app with the database*/
        BusinessLogicLayer bll = new BusinessLogicLayer();//instance of the Bussiness Logic Layer class

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == string.Empty && txtNumber.Text.Trim() == string.Empty && txtAddress.Text.Trim() == string.Empty && txtUsernameFR.Text.Trim() == string.Empty && txtPasswordFR.Text.Trim() == string.Empty && txtConfCodeF.Text.Trim() == string.Empty)
            {
                Response.Write("Please enter the required information on all empty spaces");//Displaying message for empty text fields
            }
            else
            {
                /*If the password is the same as the confirm password, then the username and password will be inserted in the database*/
                if (txtPasswordFR.Text == txtConfCodeF.Text)
                {

                    dbConn.Open();
                    string register = "INSERT INTO Farmer VALUES ('" + txtName.Text + "','" + txtNumber.Text + "','" + txtAddress.Text + "','" + txtUsernameFR.Text + "','" + encrypt.EncryptData(txtPasswordFR.Text) +/*hashing password*/ "')";
                    dbComm = new SqlCommand(register, dbConn);
                    dbComm.ExecuteNonQuery();
                    dbConn.Close();
                    /*The following empty textbox variables clears the data right after the registered has been a success*/
                    txtName.Text = "";
                    txtNumber.Text = "";
                    txtAddress.Text = "";
                    txtUsernameFR.Text = "";
                    txtPasswordFR.Text = "";

                    Response.Write("A New Farmer Has Been Added!");

                }
                /*Else If the password is not the same as the confirm password, then the username and password will not be inserted in the database, as a result this message below will display */
                else
                {

                    Response.Write("Passwords Do Not Match, Please Re-Enter.");
                    /*The following empty textbox variables clears the data right after the registered has been a success*/
                    txtName.Text = "";
                    txtNumber.Text = "";
                    txtAddress.Text = "";
                    txtUsernameFR.Text = "";
                    txtUsernameFR.Focus();
                }
            }
        }

        protected void btnDisplayEmployee_click(object sender, EventArgs e)
        {

        }
    }
}