using System;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Shared;

public class Usuario
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(100)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio para el acceso.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    // En el cliente local (SQLite) guardaremos un PIN rápido o un hash ligero. 
    // La verdadera seguridad de contraseñas vive en PostgreSQL.
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public RolSistema Rol { get; set; }

    // Vital para cuando un empleado deja la clínica; no lo borramos, lo desactivamos.
    public bool EstaActivo { get; set; } = true; 
}

/// <summary>
/// Matriz estricta de Roles de Negocio (RBAC)
/// </summary>
public enum RolSistema
{
    Veterinario,    // Acceso total. Firma consultas.
    Supervisor,     // Asistente en terreno. Lectura y datos demográficos.
    Administrador   // Gestión de usuarios y credenciales.
}