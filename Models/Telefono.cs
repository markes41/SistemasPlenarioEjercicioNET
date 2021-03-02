using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjercicioNET.Models
{
    public class Telefono
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TelefonoID { get; set; }
        [Required]
        public string Numero_Telefono { get; set; }
        public Persona Persona { get; set; }
    }
}