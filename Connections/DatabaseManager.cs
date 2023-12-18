using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MyContacts.Entities;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {
        //public DbSet<Contatto> Contatto { get; set; }

        //public DbSet<Contatto> Contatto { get; set; }
        string nomeServer = "MyContacts.dbo";
        string dbName = "Contatto";

        private string connectionString = "Data Source=DESKTOP-ANLK1IU\\SQLEXPRESS;Database=MyContacts;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public DatabaseManager(DbContextOptions options) : base(options)
        {
        }

        public List<Contatto> GetAllContatti()
        {
            var contatti = new List<Contatto>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {

                    var query = "SELECT * FROM Contatto";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt32(0);
                                var nome = reader.GetString(1);

                                //contatti.Add($"ID: {id}, Nome: {nome}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }

            return contatti;
        }

        public DbSet<Contatto> Contatto { get; set; }

    }
}


