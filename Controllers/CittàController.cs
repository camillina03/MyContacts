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

        [HttpGet]
        public ActionResult<IEnumerable<Citt‡>> GetAllCitt‡()
        {
            var citt‡ = _dbManager.GetAllCitt‡();

            if (!citt‡.Any())
            {
                return NotFound("Nessuna citt‡ trovata.");
            }

            return Ok(citt‡);
        }

        //[HttpGet("{Nome}")]
        //public ActionResult<Citt‡> GetCitt‡(string nome )
        //{
        //    var citt‡ = _dbManager.GetCitt‡(nome);

        //    if (citt‡== null)
        //    {
        //        return NotFound("Citt‡ non trovata.");
        //    }

        //    return Ok(citt‡);
        //}









    }
}
