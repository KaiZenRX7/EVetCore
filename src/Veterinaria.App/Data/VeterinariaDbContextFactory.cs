using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Veterinaria.App.Data;

public class VeterinariaDbContextFactory : IDesignTimeDbContextFactory<VeterinariaDbContext>
{
    public VeterinariaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VeterinariaDbContext>();
        
        // Esta cadena de conexión es EXCLUSIVA para engañar a la CLI en tiempo de diseño.
        // En producción (en la tablet/PC), la app seguirá usando la ruta dinámica de MauiProgram.cs.
        optionsBuilder.UseSqlite("Filename=veterinaria_design.db");

        return new VeterinariaDbContext(optionsBuilder.Options);
    }
}