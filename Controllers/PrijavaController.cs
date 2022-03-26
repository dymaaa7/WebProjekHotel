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
    public class PrijavaController : ControllerBase
    {
        HotelContext Context { get; set; }

        public PrijavaController(HotelContext context)
        {
            Context = context;
        }
        
        [Route("DodajPrijavu/{imezgrade}/{licenca}/{pasos}/{brsobe}")]
        [HttpPost]
        public async Task<ActionResult> DodajPrijavu(string imezgrade, int licenca, int pasos, int brsobe)
        {
            try
            {
                var zgrada = Context.Zgrade.Where(p=> p.ImeZgrade==imezgrade).FirstOrDefault();
                var korisnik=Context.Korisnici.Where(p=>p.BrojPasosa==pasos).FirstOrDefault();
                var zaposleni=Context.Zaposlenii.Where(p=>p.BrojLicence==licenca).FirstOrDefault();

                if(imezgrade==null)
                    return BadRequest("Nevalidan unos.");

                var prijava = new Prijava
                {
                    Zgrada=zgrada,
                    Zaposleni=zaposleni,
                    Korisnik=korisnik,
                    BrojSobe=brsobe
                };

                Context.Prijave.Add(prijava);
                await Context.SaveChangesAsync();

                return Ok(prijava);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("DodajPrijavu/{imeZgrade}/{brojLicence}/{imeGosta}/{prezimeGosta}/{brojSobe}")]
        [HttpPost]
        public async Task<ActionResult> DodajPrijavu(string imeZgrade, int brojLicence,string imeGosta,string prezimeGosta, int brojSobe)
        {
            try
            {
                var zgrada=Context.Zgrade.Where(p=>p.ImeZgrade==imeZgrade).FirstOrDefault();
                var gost=Context.Korisnici.Where(p=>p.Ime==imeGosta&&p.Prezime==prezimeGosta).FirstOrDefault();
                var radnik=Context.Zaposlenii.Where(p=>p.BrojLicence==brojLicence).FirstOrDefault();
/*
                if(zgrada==null||gost==null||radnik==null)
                    return BadRequest("Nevalidan unos!");
*/
                var prijava = new Prijava
                {
                    Zgrada=zgrada,
                    Zaposleni=radnik,
                    Korisnik=gost,
                    BrojSobe=brojSobe
                };

                Context.Prijave.Add(prijava);
                await Context.SaveChangesAsync();

                return Ok(prijava);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            

        }

        [Route("IzbrisiPrijavu/{imesobe}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPrijavu(int imesobe)
        {
            try
            {
                var prijava = Context.Prijave.Where(p=> p.BrojSobe==imesobe).FirstOrDefault();
                Context.Prijave.Remove(prijava);

                await Context.SaveChangesAsync();
                return Ok("Uspesno je uklonjena prijava!");
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [Route("PrikaziPrijavu/{brsobe}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziPrijavu(int brsobe)
        {

            var korisnik=await Context.Korisnici.ToListAsync();
            var zaposleni=await Context.Zaposlenii.ToListAsync();
            var zgrada=await Context.Zgrade.ToListAsync();

            try
            {
                var prijavaa = await Context.Prijave
                .Include(p=>p.Korisnik)
                .Include(p=>p.Zaposleni)
                .Include(p=>p.Zgrada)
                .Where(p=>p.BrojSobe==brsobe)
                .Select(
                   p=>new
                    {
                        ImeZgrade=p.Zgrada.ImeZgrade,
                        ImeGosta=p.Korisnik.Ime,
                        PrezimeGosta=p.Korisnik.Prezime,
                        BrojPasosa=p.Korisnik.BrojPasosa,
                        BrojSobe=p.BrojSobe,
                        ZaposleniLicenca=p.Zaposleni.BrojLicence
                    }
                ).ToListAsync();
                

                return Ok(prijavaa);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
        }

        [Route("PrikaziPrijavuSve/{imeZgrade}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziPrijavuSve(string imeZgrade)
        {
            if(String.IsNullOrWhiteSpace(imeZgrade))
                return BadRequest("Los unos!");
/*
            var korisnik=await Context.Korisnici.ToListAsync();
            var zaposleni=await Context.Zaposlenii.ToListAsync();
            var zgrade=await Context.Zgrade.ToListAsync();
*/
            try
            {
                var prijavaa = await Context.Prijave
                .Include(p=>p.Korisnik)
                .Include(p=>p.Zaposleni)
                .Include(p=>p.Zgrada)
                .Where(p=>p.Zgrada.ImeZgrade==imeZgrade)
                .Select(
                    p=>new
                    {
                        ImeZgrade=p.Zgrada.ImeZgrade,
                        ImeGosta=p.Korisnik.Ime,
                        PrezimeGosta=p.Korisnik.Prezime,
                        BrojPasosa=p.Korisnik.BrojPasosa,
                        BrojSobe=p.BrojSobe,
                        ZaposleniLicenca=p.Zaposleni.BrojLicence
                    }
                ).ToListAsync();
                

                return Ok(prijavaa);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }    
        }            
    }   
}