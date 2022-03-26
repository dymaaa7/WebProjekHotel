using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Prijava")]
    public class Prijava
    {
        [Key]
        public int IDPrijave { get; set; }

        public int BrojSobe {get;set;}
        public virtual Korisnik Korisnik{get;set;}
        public virtual Zaposleni Zaposleni{get;set;}
        public virtual Zgrada Zgrada {get;set;}
    }
}