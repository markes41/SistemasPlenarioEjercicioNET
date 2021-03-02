using Microsoft.EntityFrameworkCore;

namespace EjercicioNET.Models
{
    public class PersonasContext : DbContext
    {
        public PersonasContext(DbContextOptions<PersonasContext> options)
            : base (options)
        { }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
    }
}