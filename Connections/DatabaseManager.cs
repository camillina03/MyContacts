using MyContacts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {
        //public DbSet<Contatto> Contatto { get; set; }
        string nomeServer = "MyContacts.dbo";
        string dbName = "Contatto";
        
        //private string connectionString= "Server=NomeServer;Database=NomeDatabase;User Id=NomeUtente;Password=Password;";
        private string connectionString= "Server=DESKTOP-ANLK1IU\\SQLEXPRESS;Database=MyContacts;Integrated Security=True;";
        public DatabaseManager()
        {

        }

        public List<string> GetContattoData()
        {
            List<string> results = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Contatto";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nome = reader.GetString(1);

                                results.Add($"ID: {id}, Nome: {nome}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    results.Add("Errore: " + ex.Message);
                }
            }

            return results;
        }
    }
}


