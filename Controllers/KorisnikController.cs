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
    public class KorisnikController : ControllerBase
    {
        HotelContext Context { get; set; }

        public KorisnikController(HotelContext context)
        {
            Context = context;
        }
        
        [Route("DodatiKorisnika/{ime}/{prezime}/{brojPasosa}/{brTelefona}")]
        [HttpPost]
        public async Task<ActionResult> DodatiKorisnika(string ime, string prezime, int brojPasosa, int brTelefona)
        {
            if(string.IsNullOrEmpty(ime)||ime.Length>50)
                return BadRequest("Nevalidno ime gosta!");
            if(string.IsNullOrEmpty(prezime)||prezime.Length>50)
                return BadRequest("Nevalidno prezime gosta!");
            if(brojPasosa<10000000||brojPasosa>99999999)
                return BadRequest("Lose unet broj pasosa!");
            if(brTelefona<10000000||brTelefona>99999999)
                return BadRequest("Lose unet broj telefona!");
            var kor = new Korisnik
            {
                Ime=ime,
                Prezime=prezime,
                BrojPasosa=brojPasosa,
                BrTelefona=brTelefona,
            };
            try
            {
                Context.Korisnici.Add(kor);
                await Context.SaveChangesAsync();

                return Ok($"Uspesno je dodat gost! Broj pasosa je {kor.BrojPasosa}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniKorisnika/{ime}/{prezime}/{brojPasosa}/{brTelefona}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniKorisnika(string ime, string prezime, int brojPasosa, int brTelefona)
        {
            if(brojPasosa<10000000||brojPasosa>99999999)
                return BadRequest("Lose unet broj  novog pasosa!");
            if(string.IsNullOrEmpty(ime)||ime.Length>50)
                return BadRequest("Nevalidno ime gosta!");
            if(string.IsNullOrEmpty(prezime)||prezime.Length>50)
                return BadRequest("Nevalidno prezime gosta!");
            if(brTelefona<10000000||brTelefona>99999999)
                return BadRequest("Lose unet broj telefona!");
            try
            {
                var kor=Context.Korisnici.Where(p=>p.BrojPasosa==brojPasosa).FirstOrDefault();
                if(kor!=null)
                {
                    kor.Ime=ime;
                    kor.Prezime=prezime;
                    kor.BrTelefona=brTelefona;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno izmenjen gost sa brojem pasosa {brojPasosa}");
                }
                else
                {
                    return BadRequest("Gost nije prijavljen.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("ObrisiKorisnika/{brojPasosa}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiKorisnika(int brojPasosa)
        {
            if(brojPasosa<10000000||brojPasosa>99999999)
                return BadRequest("Lose unet broj pasosa!");      
            try
            {

                var kor =  Context.Korisnici.Where(p=> p.BrojPasosa==brojPasosa).FirstOrDefault();
                Context.Korisnici.Remove(kor);
                await Context.SaveChangesAsync();
                
                return Ok($"Gost sa brojem pasosa {brojPasosa} je uklonjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //Izvlacimo sve korisnike
        [Route("PreuzmiSveGoste")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSveGoste()
        {
            try
            {
              
                var gost = await Context.Korisnici.ToListAsync();/*
                    .Include(p => p.KorisnikPrijava)
                    .ThenInclude(p => p.Soba)
                    .Include(p => p.KorisnikPrijava)
                    .ThenInclude(p => p.Zaposleni);*/


                return Ok
                (
                    gost.Select(p =>
                    new
                    {
                        BrojPasosa=p.BrojPasosa,
                        Ime=p.Ime,
                        Prezime=p.Prezime,
                        BrTelefona=p.BrTelefona
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
          
    }
}
