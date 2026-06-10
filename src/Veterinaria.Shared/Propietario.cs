using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Veterinaria.Shared.Validaciones;

namespace Veterinaria.Shared;

public class Propietario
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "El RUT es obligatorio.")]
    [StringLength(12, ErrorMessage = "El RUT no puede exceder los 12 caracteres (ej: 12345678-9).")]
    [RutValido] // <--- Validación personalizada para RUT chileno
    public string Rut { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono de contacto es obligatorio.")]
    [Phone(ErrorMessage = "Formato de teléfono inválido.")]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es vital para las visitas a domicilio.")]
    [StringLength(200)]
    public string Direccion { get; set; } = string.Empty;

    // Relación 1 a N: Un propietario puede tener múltiples mascotas
    public List<Mascota> Mascotas { get; set; } = new();

    // Trazabilidad y Auditoría (Alineado con el MVP)
    public Guid CreadoPorUsuarioId { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public Guid? ModificadoPorUsuarioId { get; set; }
    public DateTime? FechaUltimaModificacion { get; set; }
}