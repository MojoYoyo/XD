using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using kol1.DataBase;
using kol1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kol1.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDbAccess _dbAccess;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDbAccess dbAccess)
        {
            _logger = logger;
            _dbAccess = dbAccess;
        }

        [HttpGet, Route("championship/teams/{id}")]
        public IActionResult GetChampionship(int id)
        {
            var championship = _dbAccess.GetChampionship(id);
            if(championship == null)
            {
                return BadRequest(HttpStatusCode.NotFound);
            }
            return Ok(championship.Teams);
        }

        [HttpGet, Route("team/{name}/{lastname}/{birth}/{teamId}")]
        public IActionResult AsignPlayer(string name, string lastname, string birth, int teamId)
        {
            var player = new Player
            {
                BirthDate = DateTime.ParseExact(birth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                FirstName = name,
                LastName = lastname,
                TeamId = teamId
            };

            try
            {
                _dbAccess.AsignPlayer(player);
                return Ok(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
