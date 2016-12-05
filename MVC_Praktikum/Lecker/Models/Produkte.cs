using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;

namespace Lecker.Models
{
    public class Produkte
    {

        public List<string> productName { get; set; }
        public List<string> productkategorie { get; set; }
        public List<int> productID { get; set; }


        public Produkte()
        {
            productName = new List<string>();
            productkategorie = new List<String>();
            productID = new List<int>();
        }



        public void getProdukte(int? kategorie)
        {

            string zusatz_where; 

            if (kategorie == null)
            {
                zusatz_where = "";
            }
            else
            {
                zusatz_where = " WHERE Produkt.Kategorie_ID = " + kategorie;
            } 

            
            var dbconString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            SqlConnection dbCon = new SqlConnection(dbconString);

            dbCon.Open();
            var query = "SELECT Produkt.Name AS Produktname, Kategorie.Bezeichnung AS Produktkategorie,Produkt.ID AS pid FROM Produkt INNER JOIN Kategorie ON Produkt.Kategorie_ID = Kategorie.ID" + zusatz_where;
            SqlCommand sqlcmd = new SqlCommand(query, dbCon);
            SqlDataReader reader = sqlcmd.ExecuteReader();
            

            while (reader.Read())
            {
                productName.Add(reader.GetString(0).ToString());
                productkategorie.Add(reader.GetString(1).ToString());
                productID.Add(reader.GetInt32(2));
            }

            


            dbCon.Close();

        }
    }
}