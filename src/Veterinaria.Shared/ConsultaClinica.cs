using System;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Shared;

public class ConsultaClinica
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid MascotaId { get; set; }

    [Required]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Range(0, 500, ErrorMessage = "El peso registrado debe ser un valor lógico.")]
    public double PesoRegistro { get; set; }

    [Required(ErrorMessage = "La anamnesis preliminar es obligatoria.")]
    public string Anamnesis { get; set; } = string.Empty;

    public string Diagnostico { get; set; } = string.Empty;

    // Máquina de estados crítica para el MVP
    public EstadoConsulta Estado { get; set; } = EstadoConsulta.Borrador;

    // Trazabilidad y Auditoría (RBAC)
    public Guid CreadoPorUsuarioId { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public Guid? ModificadoPorUsuarioId { get; set; }
    public DateTime? FechaUltimaModificacion { get; set; }
}

public enum EstadoConsulta
{
    Borrador,    // Editable por Supervisor/Asistente y Veterinario
    Finalizada   // Firmada por el Veterinario. Entidad de solo lectura.
}