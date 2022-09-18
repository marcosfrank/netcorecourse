using Microsoft.EntityFrameworkCore;
using NetCoreCourse.FirstExample.WebApp.Entities;

#nullable disable

namespace NetCoreCourse.FirstExample.WebApp.DataAccess
{
    // Ver que esta clase hereda de DbContext
    public class ThingsContext : DbContext
    {
        /// <summary>
        /// Este constructor es utilizado para la creacion del contexto.
        /// </summary>
        /// <param name="options">Este parametro options es el que esta configurado en la clase program.</param>
        public ThingsContext(DbContextOptions options)
            : base(options)
        {
        }

        //Mostrar durante la clase de teoria.
        //Este metodo se utiliza normalmente para configurar relaciones y restricciones con
        //   metodos Fluent.
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Address>()
        //        .Property(a => a.Street)
        //        .HasMaxLength(100);

        //    modelBuilder.Entity<Address>()
        //        .Property(a => a.Number)
        //        .HasMaxLength(20);

        //    modelBuilder.Entity<Address>()
        //        .Property(a => a.City)
        //        .HasMaxLength(100);

        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Person> People { get; set; }

        //Agregar durante las clases de teoria.
        //public DbSet<Loan> Loans { get; set; }
    }

    /* Comandos PMC
     * Necesitan el NuGet Microsoft.EntityFrameworkCore.Tools para poder ser ejecutados.
     * Los comandos de PMC utilizan 2 "proyectos" de entrada (o pueden ser pasados como parametros de los comandos lo cual no es muy comodo.)
     * - StartUp Project: Es un tipo de proyecto que debe referenciar o ser el proyecto que contiene el DbContext y debe ser ejecutable (Consola, WebApp o DesktopApp).
     * - Default Project en PMC: Debe ser el proyecto que contiene las migraciones. Este proyecto debe contener la referencia a Microsoft.EntityFrameworkCore
     *
    */
}