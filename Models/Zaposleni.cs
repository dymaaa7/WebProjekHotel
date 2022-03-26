using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{   
    [Table("Zaposleni")]
    public class Zaposleni
    {
        [Key]
        public int IDRadnika { get; set; }

        [Required]
        [MaxLength(20)]
        public string ImeZaposleni { get; set; }
    
        [Required]
        [MaxLength(20)]
        public string PrezimeZaposleni { get; set; }
        
        [Required]
        [Range(100,149)]
        public int BrojLicence { get; set; }

        [Required]
        [Range(10000000,999999999)]
        public int BrTelefona {get; set;}

        [Required]
        [Range(40000,100000)]
        public int Plata { get; set; }

        [Required]
        [JsonIgnore] /*???????????*/
        public virtual List<Prijava> ZaposleniPrijava{get;set;}
    }
}