using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadingView
{
    public partial class FarmerLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string farmerUsername;

        DAL.DataAccessLayer encrypt = new DAL.DataAccessLayer();//instance of the Data Access Layer class that retrieves the method to hash the user password

        /*Linking the app with the database*/
        static string connString = "Data Source=lab000000\\SQLEXPRESS;Initial Catalog=FarmCentral-DB;Integrated Security=SSPI;";
        SqlConnection dbConn = new SqlConnection(connString);
        SqlCommand dbComm = new SqlCommand();
        SqlDataAdapter dbAdapter = new SqlDataAdapter();

        protected void btnLoginF_Click(object sender, EventArgs e)
        {
            dbConn.Open();
            string login = "SELECT * FROM Farmer WHERE Username= '" + txtUsernameFL.Text + "' and Password= '" + encrypt.EncryptData(txtPasswordFL.Text)/*hashing password*/ + "'";
            dbComm = new SqlCommand(login, dbConn);
            SqlDataReader dr = dbComm.ExecuteReader();

            if (dr.Read() == true)
            {
                /*If the password and username entered by the user match the password and username they used to register, the user will be redirected to the main application*/

                farmerUsername = txtUsernameFL.Text;
                Response.Redirect("Products.aspx");
            }
            else
            {
                /*Else If the password and username entered by the user does not match the password and username they used to register, this message will be displayed*/

                Response.Write("Invalid Username or Password, Please Try Again");

                /*The following empty textbox variables clears the data right after the registered has been a success*/
                txtUsernameFL.Text = "";
                txtPasswordFL.Text = "";
                txtUsernameFL.Focus();
            }

            dbConn.Close();
        }
    }
    
}