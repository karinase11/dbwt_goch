using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

using PasswordSecurity;

namespace Lecker.Models
{
    public class Login
    {

        public string form_username{ get; set; }
        public string form_password { get; set; }
        public Boolean form_stayOnline { get; set; }
        public string role { get; set; }


        public string algo { get; set; }
        public int stretch { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        public int feID { get; set; }
        public string email { get; set; }

        private List<string> emailList { get; set; }


        string passw;



        public Login()
        {
            algo = "";
            stretch = 0;
            hash = "";
            salt = "";
            feID = -1;
            email = "";
            emailList = new List<string>(); 
        }

        public void getUserData()
        {
            var dbconString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            SqlConnection dbCon = new SqlConnection(dbconString);

        
            if (form_username != null && form_password != null)
            {
                string zusatz_where = form_username;
                passw = form_password;



                dbCon.Open();
                var query = "SELECT [FH-Angehörige].ID, [FH-Angehörige].[E-Mail],[FE-Nutzer].Algo, [FE-Nutzer].Stretch, [FE-Nutzer].[Hash], [FE-Nutzer].Salt FROM [FE-Nutzer] INNER JOIN [FH-Angehörige] ON [FE-Nutzer].ID = [FH-Angehörige].ID WHERE [FH-Angehörige].[E-Mail] ='" + zusatz_where + "'";
                SqlCommand sqlcmd = new SqlCommand(query, dbCon);
                SqlDataReader reader = sqlcmd.ExecuteReader();



                while (reader.Read())
                {
                    feID = reader.GetInt32(0);
                    emailList.Add(reader.GetString(1).ToString());
                    algo = reader.GetString(2);
                    stretch = reader.GetInt32(3);
                    hash = reader.GetString(4);
                    salt = reader.GetString(5);
                }

                dbCon.Close();

            }
            
        }


        public Boolean isUserOK()
        {
            if (emailList.Count > 0)
            {

                email = emailList[0];
                return true;
            }
            
            
            return false;
        }

        public Boolean isPWOK()
        {

            Boolean userOK = isUserOK();
            Boolean passwOK;
            if (userOK && algo == "sha1")
            {

                var correctHash = algo + ':' + stretch + ':' + "18" + ':' + salt + ":" + hash;

                if (passwOK = PasswordStorage.VerifyPassword(passw, correctHash))
                {
                    return true;
                }


            }

            
            return false;

        }

    }
}