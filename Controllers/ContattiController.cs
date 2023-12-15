using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Connections;
using MyContacts.Entities;

namespace MyContacts.Controllers
{
    [Authorize]
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
            var contatti = _dbManager.GetContattoData();

            if (contatti.Count == 0)
            {
                return NotFound("Nessun contatto trovato");
            }

            return Ok(contatti);
        }
    }
}
