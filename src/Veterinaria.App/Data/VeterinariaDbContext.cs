using Microsoft.EntityFrameworkCore;
using Veterinaria.Shared;

namespace Veterinaria.App.Data;

public class VeterinariaDbContext : DbContext
{
    public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options) : base(options)
    {
    }

    public DbSet<Propietario> Propietarios => Set<Propietario>();
    public DbSet<Mascota> Mascotas => Set<Mascota>();
    public DbSet<ConsultaClinica> ConsultasClinicas => Set<ConsultaClinica>();
    public DbSet<DocumentoMascota> DocumentosMascotas => Set<DocumentoMascota>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Propietario
        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Rut).IsUnique(); // Regla de negocio core
        });

        // Configuración de Mascota
        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.CodigoEstandarizado).IsUnique(); // Búsquedas rápidas indexadas

            // Relación 1 a N: Propietario -> Mascotas
            entity.HasOne<Propietario>()
                  .WithMany(p => p.Mascotas)
                  .HasForeignKey(m => m.PropietarioId)
                  .OnDelete(DeleteBehavior.Restrict); // No borramos al dueño en cascada
        });

        // Configuración de ConsultaClinica
        modelBuilder.Entity<ConsultaClinica>(entity =>
        {
            entity.HasKey(c => c.Id);

            // Almacenar Enum como string por seguridad y escalabilidad
            entity.Property(c => c.Estado)
                  .HasConversion<string>();

            // Relación 1 a N: Mascota -> Consultas
            entity.HasOne<Mascota>()
                  .WithMany()
                  .HasForeignKey(c => c.MascotaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de DocumentoMascota
        modelBuilder.Entity<DocumentoMascota>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Tipo)
                  .HasConversion<string>();

            // Relación 1 a N: Mascota
            entity.HasOne<Mascota>() // Relación con Mascota
                  .WithMany(m => m.Documentos)
                  .HasForeignKey(d => d.MascotaId)
                  .OnDelete(DeleteBehavior.Cascade); 
        });

        // Configuración de Usuario (RBAC Local)
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.Rol)
                  .HasConversion<string>();
        });
    }
}