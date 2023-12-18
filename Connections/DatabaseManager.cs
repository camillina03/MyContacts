using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MyContacts.Entities;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {

        private string connectionString = "Data Source=lOCALDB\\SQLEXPRESS;Database=MyContacts;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

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
                                //TODO : INSERIRE COSTRUTTORE PER CONTATTO 
                                //var contatto = new Contatto();

                                //contatto.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                                //// Aggiungi altre proprietà del contatto come nell'esempio sopra

                                //contatti.Add(contatto);
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


