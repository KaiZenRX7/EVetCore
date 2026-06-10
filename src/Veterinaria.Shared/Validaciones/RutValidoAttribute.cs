using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Veterinaria.Shared.Validaciones;

public class RutValidoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return new ValidationResult("El RUT es obligatorio.");

        var rutOriginal = value.ToString()!.Replace(".", "").Replace("-", "").ToUpper();
        if (!Regex.IsMatch(rutOriginal, @"^[0-9]+[0-9K]$"))
            return new ValidationResult("Formato de RUT inválido.");

        string rut = rutOriginal.Substring(0, rutOriginal.Length - 1);
        char dvCargado = rutOriginal[rutOriginal.Length - 1];

        int suma = 0, multiplicador = 2;
        for (int i = rut.Length - 1; i >= 0; i--)
        {
            suma += int.Parse(rut[i].ToString()) * multiplicador;
            multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
        }

        int resto = suma % 11;
        int dvCalculado = 11 - resto;
        char dvEsperado = dvCalculado == 10 ? 'K' : dvCalculado == 11 ? '0' : dvCalculado.ToString()[0];

        return dvCargado == dvEsperado 
            ? ValidationResult.Success 
            : new ValidationResult("El RUT ingresado no es válido (Módulo 11).");
    }
}