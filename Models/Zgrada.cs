using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{   
    [Table("Zgrada")]
    public class Zgrada
    {   
        [Key]
        public int IDZgrade { get; set; }

        [Required]
        public string ImeZgrade{get;set;}

        public string Grad{get;set;}

        [Required]
        [JsonIgnore] /*???????????*/
        public virtual List<Prijava> ZgradaPrijava {get;set;}
    }
}