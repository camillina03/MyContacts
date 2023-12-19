using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Connections;
using MyContacts.Entities;

namespace MyContacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContattiController : ControllerBase
    {
        private readonly DatabaseManager _dbManager;

        public ContattiController(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contatto>> GetContatti()
        {
            var contatti = _dbManager.GetAllContatti();

            if (contatti.Count == 0)
            {
                return NotFound("Nessun contatto trovato");
            }

            return Ok(contatti);
        }

        [HttpPost]
        public ActionResult PostContatto([FromBody]Contatto nuovoContatto)
        {

            _dbManager.Contatto.Add(nuovoContatto);
            _dbManager.SaveChanges();

            return Ok(nuovoContatto);
        }

        [HttpPut("{Mail}")]
        public ActionResult UpdateContatto(String Mail, [FromBody] Contatto contattoModificato)
        {
            var contattoEsistente = _dbManager.Contatto.FirstOrDefault(x => x.Mail == Mail);

            if (contattoEsistente == null)
            {
                return NotFound(); 
            }

         
            contattoEsistente.Nome = contattoModificato.Nome;
            contattoEsistente.Cognome = contattoModificato.Cognome;
            contattoEsistente.Sesso = contattoModificato.Sesso;
            contattoEsistente.Mail = contattoModificato.Mail;
            contattoEsistente.Telefono = contattoModificato.Telefono;
            contattoEsistente.Città = contattoModificato.Città;
            contattoEsistente.DataDiNascita = contattoModificato.DataDiNascita;


            _dbManager.SaveChanges();

            return Ok(contattoEsistente);
        }




    }
}
