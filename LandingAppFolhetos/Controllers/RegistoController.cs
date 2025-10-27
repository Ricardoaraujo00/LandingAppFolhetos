using LandingAppFolhetos.Data;
using LandingAppFolhetos.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandingAppFolhetos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistoController : ControllerBase
    {
        private readonly DatabaseDbContext _db;
        public RegistoController(DatabaseDbContext db) => _db = db;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RegistoLeituraFolheto registo)
        {
            _db.Add(registo);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
