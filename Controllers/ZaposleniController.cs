using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace HotelProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZaposleniController : ControllerBase
    {
        HotelContext Context { get; set; }

        public ZaposleniController(HotelContext context)
        {
            Context = context;
        }
        
        [Route("DodatiZaposlenog/{ime}/{prezime}/{brojLicence}/{brtelefona}/{plata}")]
        [HttpPost]
        public async Task<ActionResult> DodatiZaposlenog(string ime, string prezime, int brojLicence, int brtelefona,int plata)
        {
            if(string.IsNullOrEmpty(ime)||ime.Length>50)
                return BadRequest("Nevalidno ime radnika!");
            if(string.IsNullOrEmpty(prezime)||prezime.Length>50)
                return BadRequest("Nevalidno prezime radnika!");
            if(brojLicence<100||brojLicence>149)
                return BadRequest("Lose unet broj licence radnika!");
            if(brtelefona<10000000||brtelefona>999999999)
                return BadRequest("Lose unet broj telefona!");
            if(plata<40000||plata>100000)
                return BadRequest("Neispravna plata.");
            var kor = new Zaposleni
            {
                ImeZaposleni=ime,
                PrezimeZaposleni=prezime,
                BrojLicence=brojLicence,
                BrTelefona=brtelefona,
                Plata=plata,
            };
            try
            {
                Context.Zaposlenii.Add(kor);
                await Context.SaveChangesAsync();

                return Ok($"Uspesno je dodat novi zaposleni. Broj licence: {brojLicence}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniZaposlenog/{brojLicence}/{plata}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniZaposlenog( int brojLicence, int plata)
        {
            if(brojLicence<100||brojLicence>149)
                return BadRequest("Lose unet broj radnika!");
            if(plata<40000||plata>100000)
                return BadRequest("Neispravna plata.");
            try
            {
                var kor=Context.Zaposlenii.Where(p=>p.BrojLicence==brojLicence).FirstOrDefault();
                if(kor!=null)
                {
                    kor.Plata=plata;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno izmenjen zaposleni sa brojem licne karte {brojLicence}");
                }
                else
                {
                    return BadRequest("Zaposleni ne postoji.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
     
        [Route("ObrisiZaposlenog/{brojLicence}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiZaposlenog(int brojLicence)
        {
            if(brojLicence<100||brojLicence>149)
                return BadRequest("Lose unet broj licence!");      
            try
            {
                var kor = await Context.Zaposlenii.Where(p=> p.BrojLicence==brojLicence).FirstOrDefaultAsync();
                Context.Zaposlenii.Remove(kor);
                await Context.SaveChangesAsync();
                
                return Ok($"Zaposleni sa Licencom {brojLicence} je uklonjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        } 
      
        //Izvlacimo sve korisnike
        [Route("PreuzmiSveZaposlene")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSveZaposlene()
        {
            var zaplosleni = await Context.Zaposlenii.ToListAsync();
            try
            {
                return Ok(zaplosleni.Select(p=> new
                {
                    ImeZaposleni=p.ImeZaposleni,
                    PrezimeZaposleni=p.PrezimeZaposleni,
                    BrojLicence=p.BrojLicence,
                    BrTelefona=p.BrTelefona,
                    Plata=p.Plata
                }).ToList()
                );
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    
    }

}
