using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Shared;

public class Mascota
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();


    public string CodigoEstandarizado { get; set; } = string.Empty; //ej: PM-001

    [Required(ErrorMessage = "El nombre del paciente es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty; // Ej: Eevee

    [Required(ErrorMessage = "Debes especificar la especie (Perro, Gato, etc.).")]
    public string Especie { get; set; } = string.Empty;

    public string Raza { get; set; } = "Mestizo";

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateTime FechaNacimiento { get; set; }

    // --- Nuevos campos críticos del MVP ---
    
    public bool EstaEsterilizado { get; set; } = false;

    [Range(0, 500, ErrorMessage = "El peso debe ser un valor válido.")]
    public double UltimoPeso { get; set; } = 0.0;

    // --------------------------------------

    public string NotasMedicas { get; set; } = string.Empty;

    // Relación con el dueño
    public Guid PropietarioId { get; set; }

    /// <summary>
    /// Propiedad calculada dinámicamente que entrega la edad legible de la mascota.
    /// NotMapped asegura que Entity Framework ignore esto al crear las tablas en SQLite/PostgreSQL.
    /// </summary>
    [NotMapped]
    public string EdadTexto
    {
        get
        {
            DateTime hoy = DateTime.Today;
            
            if (FechaNacimiento > hoy) 
                return "Fecha de nacimiento inválida (futura)";

            int anos = hoy.Year - FechaNacimiento.Year;
            int meses = hoy.Month - FechaNacimiento.Month;

            if (hoy.Day < FechaNacimiento.Day)
            {
                meses--;
            }

            if (meses < 0)
            {
                anos--;
                meses += 12;
            }

            if (anos == 0)
            {
                return $"{meses} {(meses == 1 ? "mes" : "meses")}";
            }

            return $"{anos} {(anos == 1 ? "año" : "años")} y {meses} {(meses == 1 ? "mes" : "meses")}";
        }
    }
    
    public string FotoPerfilUrl { get; set; } = string.Empty;

    // Colección de todos sus exámenes, fotos de tumores y PDFs vinculados
    public List<DocumentoMascota> Documentos { get; set; } = new();

    // Trazabilidad y Auditoría
    public Guid CreadoPorUsuarioId { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public Guid? ModificadoPorUsuarioId { get; set; } 
    public DateTime? FechaUltimaModificacion { get; set; }
}