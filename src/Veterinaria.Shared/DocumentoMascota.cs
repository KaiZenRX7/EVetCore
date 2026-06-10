using System;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Shared;

public class DocumentoMascota
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // El ID de la mascota a la que pertenece este archivo
    public Guid MascotaId { get; set; }

    [Required(ErrorMessage = "El título del documento o foto es obligatorio.")]
    public string Titulo { get; set; } = string.Empty; // Ej: "Ecografía Abdominal", "Foto Tumor Pata Izquierda"

    [Required]
    public string ArchivoUrl { get; set; } = string.Empty; // Ruta física en el servidor o dispositivo

    public DateTime FechaSubida { get; set; } = DateTime.Now;

    [Required]
    public TipoDocumento Tipo { get; set; }

    public string Notas { get; set; } = string.Empty; // Ej: "Se observa reducción del 20% tras tratamiento"
}

public enum TipoDocumento
{
    ImagenLesion,  // Para fotos de tumores, heridas, evolución
    ExamenPDF,     // Para resultados de laboratorios, biopsias
    Radiografia,   // Para placas o imágenes médicas complejas
    Otro
}