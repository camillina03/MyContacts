using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Connections;

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
        public ActionResult<IEnumerable<string>> GetContatti()
        {
            var contatti = _dbManager.GetAllContatti();

            if (contatti.Count == 0)
            {
                return NotFound("Nessun contatto trovato");
            }

            return Ok(contatti);
        }
    }
}
