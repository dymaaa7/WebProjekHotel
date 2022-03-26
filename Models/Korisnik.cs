using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        public int IDKorisnika { get; set; }

        
        [Range(10000000,99999999)]
        public int BrojPasosa {get; set;}

        [Required]
        [MaxLength(20)]
        public string Ime {get; set;}

        [Required]
        [MaxLength(20)]
        public string Prezime {get; set;}

        

        [Required]
        [Range(10000000,999999999)]
        public int BrTelefona {get; set;}

        [Required]
        [JsonIgnore]
        public virtual List<Prijava> KorisnikPrijava {get; set;}
    }
}