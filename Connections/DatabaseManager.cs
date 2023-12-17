using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {
        //public DbSet<Contatto> Contatto { get; set; }

        //public DbSet<Contatto> Contatto { get; set; }
        string nomeServer = "MyContacts.dbo";
        string dbName = "Contatto";

        //private string connectionString= "Server=NomeServer;Database=NomeDatabase;User Id=NomeUtente;Password=Password;";
        private string connectionString = "Data Source=DESKTOP-ANLK1IU\\SQLEXPRESS;Database=MyContacts;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        //User ID=DESKTOP-ANLK1IU\\Camilla
        public DatabaseManager()
        {

        }

        public List<string> GetAllContatti()
        {
            List<string> results = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    string query = "SELECT * FROM Contatto";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

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


