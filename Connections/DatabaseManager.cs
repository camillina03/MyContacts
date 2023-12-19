using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MyContacts.Entities;
using System.Data;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {

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
                                var telefono = "";
                                var città = "";
                                var dataDiNascita = DateTime.MinValue;
                                var nome = reader.GetString(reader.GetOrdinal("Nome"));
                                var cognome = reader.GetString(reader.GetOrdinal("Cognome"));
                                var sesso = reader.GetString(reader.GetOrdinal("Sesso"));
                                var mail = reader.GetString(reader.GetOrdinal("Mail"));
                                if (!reader.IsDBNull("Telefono")) {  telefono = reader.GetString(reader.GetOrdinal("Telefono")); }
                                if (!reader.IsDBNull("Città")) {  città = reader.GetString(reader.GetOrdinal("Città")); }
                                if (!reader.IsDBNull("DataDiNascita")) {  dataDiNascita = reader.GetDateTime(reader.GetOrdinal("DataDiNascita")); }


                                var contatto = new Contatto()
                                {
                                    Nome = nome,
                                    Cognome = cognome,
                                    Mail = mail,
                                    Sesso = FromStringToGender(sesso),
                                    Telefono = telefono,
                                    Città = città,
                                    DataDiNascita = dataDiNascita
                                };

                                contatti.Add(contatto);
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

        public Gender FromStringToGender(string stringGender)
        {
            if (stringGender == "M") return Gender.M;
            else return Gender.F;
        }
    }
}


