using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

using PasswordSecurity;


namespace Lecker.Models
{
    public class Register
    {

        public string form_username;
        public string form_password;
        public string form_passwordAgain;
        public string form_role;
        public string form_email;
        public string form_studiengang;
        public string form_buero;
        public string form_fachbereich;

        public Register()
        {
            form_username = "";

        }


        public Boolean sendSQL(string s)
        {

            var dbconString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            SqlConnection dbCon = new SqlConnection(dbconString);




            dbCon.Open();
            var query = s;
            SqlCommand sqlcmd = new SqlCommand(query, dbCon);
            SqlDataReader reader = sqlcmd.ExecuteReader();



            dbCon.Close();

            return true;
        }

          public void registerUser()
           {

               // returns algorithm:iterations:hashSize:salt:hash
               List<string> hash = generateHash(form_password);

              string  query;
              query = "INSERT INTO [FE-Nutzer] (Aktiv, Stretch, Algo, [Hash], Salt, [BE-Nutzer_ID]) VALUES(0," + hash[1] + ",'" + hash[0] + "','" + hash[4] + "','" + hash[3] + "',3)";
              sendSQL(query);

              query = "INSERT INTO [FH-Angehörige](Name, Fachbereich, [E-Mail],ID) VALUES ('" + form_username + "'," + form_fachbereich + ", '" + form_email + "',(SELECT TOP 1 [FE-Nutzer].ID FROM [FE-Nutzer] ORDER BY [FE-Nutzer].ID DESC))";
              sendSQL(query);

              // if STUDENT
              if (form_role.Equals("Student") ){

              query = "INSERT INTO Student([MA-Nummer], Studiengang, [FH-Angehörige_ID]) VALUES (3062368, '"+ form_studiengang +"', (SELECT TOP 1 [FH-Angehörige].ID FROM [FH-Angehörige] ORDER BY [FH-Angehörige].ID DESC))";
              sendSQL(query);
              }  
              //if Mitarbeiter
              if (form_role.Equals("Mitarbeiter") ){
              query = "INSERT INTO Mitarbeiter ([MA-Nummer], [Büro], [FH-Angehörige_ID]) VALUES (10001, '"+form_buero+"', (SELECT TOP 1 [FH-Angehörige].ID FROM [FH-Angehörige] ORDER BY [FH-Angehörige].ID DESC))";
              sendSQL(query);
              }
               
          } 


                public List<string> generateHash(string password){

                    string hash = PasswordStorage.CreateHash(password);
                    
                    List<string> hashlist = new List<string>();
                    
                   String[] hashsplit = hash.Split(':');

                    foreach(var value in hashsplit )
                        hashlist.Add(value);
                    
                    return hashlist;
                }
           


    }
}