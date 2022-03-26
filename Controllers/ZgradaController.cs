using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZgradaController : ControllerBase
    {
        public HotelContext Context {get; set; }

        public ZgradaController(HotelContext context)
        {
            Context = context;
        }

        [Route("PrikaziZgrade")]
        [HttpGet]
        public async Task<ActionResult> PrikaziZgrade()
        {
           var zgrade = await Context.Zgrade.ToListAsync();
           return Ok(zgrade.Select(p=> new
            {
               ImeZgrade=p.ImeZgrade,
               Grad=p.Grad,
            }).ToList()
           );
        }

        [Route("DodajZgradu/{imezgrade}/{grad}")]
        [HttpPost]

        public async Task<ActionResult> DodajZgradu(string imezgrade, string grad)
        {
            if(string.IsNullOrEmpty(imezgrade))
                return BadRequest("Nevalidno ime radnika!");
            if(string.IsNullOrEmpty(grad))
                return BadRequest("Nevalidno ime radnika!");
       
            var lok = new Zgrada
            {
                ImeZgrade=imezgrade,
                Grad=grad
            };
            try
            {
                Context.Zgrade.Add(lok);
                await Context.SaveChangesAsync();

                return Ok($"Uspesno je dodat hotel");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
