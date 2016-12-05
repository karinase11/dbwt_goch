using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;



namespace Lecker.Models
{
    public class Details
    {

       public List<int> productID {get; set;}
       public List<string> productName { get; set; }
       public List<string> productdescription { get; set; }
       public List<int> imageID { get; set; }
       public List<SqlMoney> productPrice { get; set; }



        public Details()
        {
            productID = new List<int>();
            productName = new List<string>();
            productdescription = new List<String>();
            imageID = new List<int>();
            productPrice = new List<SqlMoney>();
        }



        public void getProductDetails(int? product)
        {

            
            var dbconString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            SqlConnection dbCon = new SqlConnection(dbconString);

            dbCon.Open();
            var query = "SELECT Produkt.ID, Produkt.Name, Produkt.Beschreibung, Produkt.Bild_ID, Preis.Preis FROM Produkt INNER JOIN PREIS ON Preis.Produkt_ID = Produkt.ID WHERE Produkt.ID=" + product;
            SqlCommand sqlcmd = new SqlCommand(query, dbCon);
            
            SqlDataReader reader = sqlcmd.ExecuteReader();
            

            while (reader.Read())
            {
                productID.Add(reader.GetInt32(0));
                productName.Add(reader.GetString(1).ToString());
                productdescription.Add(reader.GetString(2).ToString());
                imageID.Add(reader.GetInt32(3));
                productPrice.Add(reader.GetSqlMoney(4));
            }

            dbCon.Close();



        }


        

    }
}