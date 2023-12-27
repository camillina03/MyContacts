using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Connections;
using MyContacts.Entities;

namespace MyContacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Citt‡Controller : ControllerBase
    {
        private readonly DatabaseManager _dbManager;

        public Citt‡Controller(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        [HttpGet("GetAllCitt‡")]
        public ActionResult<IEnumerable<Citt‡>> GetAllCitt‡()
        {
            var citt‡ = _dbManager.GetAllCitt‡();

            if (!citt‡.Any())
            {
                return NotFound("Nessuna citt‡ trovata.");
            }

            return Ok(citt‡);
        }

    }
}
