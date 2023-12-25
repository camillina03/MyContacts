using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MyContacts.Entities;
using System.Data;

namespace MyContacts.Connections
{
    public class DatabaseManager : DbContext
    {

        private string connectionString = "Data Source=DESKTOP-ANLK1IU\\SQLEXPRESS;Database=MyContacts;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        #region contatti e annessi
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
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }
            
            return contatti;
        }

        public Contatto GetContattoEsistente(string mail)
        {
            
            var contattoEsistente = new Contatto()
            {
                Nome = "",
                Cognome = "",
                Mail = "",
                Sesso = 0

            };

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    //non serve controllare che mail sia != null perche lo fa gia il controllo di validazione 
                    var query = $"SELECT * FROM Contatto WHERE Mail= '{mail}'";

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
                                if (!reader.IsDBNull("Telefono")) { telefono = reader.GetString(reader.GetOrdinal("Telefono")); }
                                if (!reader.IsDBNull("Città")) { città = reader.GetString(reader.GetOrdinal("Città")); }
                                if (!reader.IsDBNull("DataDiNascita")) { dataDiNascita = reader.GetDateTime(reader.GetOrdinal("DataDiNascita")); }


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

                                contattoEsistente = contatto;

                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }

            return contattoEsistente;
        }

        public bool DeleteContatto(string mail)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    var query = $"DELETE FROM Contatto WHERE Mail= '{mail}'";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                    return false;
                }
                              
            }
        }

        public bool IsContattoConStessaMailEsistente(string mail)
        {
            bool isContattoEsistente = false;

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    //non serve controllare che mail sia != null perche lo fa gia il controllo di validazione 
                    var query = $"SELECT COUNT (*) FROM Contatto WHERE Mail= '{mail}'";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            isContattoEsistente = true;
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }

            return isContattoEsistente;
        }
        
        public DbSet<Contatto> Contatto { get; set; }

        public Gender FromStringToGender(string stringGender)
        {
            if (stringGender == "M") return Gender.M;
            else return Gender.F;
        }

        #endregion

        #region città
        public List<Città> GetAllCittà()
        {
            var città = new List<Città>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    var query = "SELECT * FROM Città";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var nome = reader.GetString(reader.GetOrdinal("Nome"));

                                var _città = new Città()
                                {
                                    Nome = nome
                                };

                                città.Add(_città);
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }

            return città;
        }

        //public Città GetCittà(string nome)
        //{

        //    var città = new Città()
        //    {
        //        Nome = ""
        //    };

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            //non serve controllare che mail sia != null perche lo fa gia il controllo di validazione 
        //            var query = $"SELECT * FROM Città WHERE Nome= '{nome}'";

        //            using (var command = new SqlCommand(query, connection))
        //            {
        //                connection.Open();

        //                using (var reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
                                
        //                        var _nome = reader.GetString(reader.GetOrdinal("Nome"));
                                
        //                        var _città = new Città()
        //                        {
        //                            Nome = _nome
        //                        };

        //                        città = _città;
        //                    }
        //                }
        //                connection.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Errore: " + ex.Message);
        //        }
        //    }
        //    return città;
        //}

        #endregion
    }
}


