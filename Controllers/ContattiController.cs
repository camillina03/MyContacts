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

            if (!contatti.Any())
            {
                return NotFound("Nessun contatto trovato");
            }

            return Ok(contatti);
        }

        [HttpGet("{Mail}")]
        public ActionResult<Contatto> GetContatto(string Mail)
        {
            var contatto = _dbManager.GetContattoEsistente(Mail);

            if (contatto== null)
            {
                return NotFound("Nessun contatto trovato");
            }

            return Ok(contatto);
        }

        [HttpPost]
        public ActionResult PostContatto([FromBody]Contatto nuovoContatto)
        {
            try
            {
                _dbManager.Contatto.Add(nuovoContatto);
                _dbManager.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                return StatusCode(500, "Si è verificato un errore durante l'elaborazione della richiesta.");
            }

            return Ok(nuovoContatto);
        }

        [HttpPut("{Mail}")]
        public ActionResult UpdateContatto(String Mail, [FromBody] Contatto contattoModificato)
        {
            if (Mail != contattoModificato.Mail)
            {
                var isContattoEsistente = _dbManager.IsContattoConStessaMailEsistente($"{Mail}");
                if (isContattoEsistente) return BadRequest("Errore! stai provando ad inserire una mail già in uso.");
            }

            var contattoEsistente = _dbManager.GetContattoEsistente($"{Mail}");

            if (contattoEsistente== null)
            {
                return NotFound("Il contatto selezionato non esiste");
            }

            contattoEsistente.Nome = contattoModificato.Nome;
            contattoEsistente.Cognome = contattoModificato.Cognome;
            contattoEsistente.Sesso = contattoModificato.Sesso;
            contattoEsistente.Mail = contattoModificato.Mail;
            contattoEsistente.Telefono = contattoModificato.Telefono;
            contattoEsistente.Città = contattoModificato.Città;
            contattoEsistente.DataDiNascita = contattoModificato.DataDiNascita;

            try
            {
                var update= _dbManager.Update<Contatto>(contattoEsistente);
                var save= _dbManager.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                return StatusCode(500, "Si è verificato un errore durante l'elaborazione della richiesta.");
            }
            return Ok(contattoEsistente);
                        
        }

        [HttpDelete("{Mail}")]
        public ActionResult DeleteContatto(String Mail)
        {
            var IsContattoEliminato = _dbManager.DeleteContatto($"{Mail}");

            if (!IsContattoEliminato)
            {
                return BadRequest("Non è stato possibile eliminare il contatto selezionato.Controllare che il contatto esista.");
            }

            return Ok();

        }

        



    }
}
