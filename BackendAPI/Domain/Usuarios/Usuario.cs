using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciarUsuarios.Usuarios
{
    public class Usuario
    {
        private static int nextId = 1;

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(Usuario), "ValidateDataNascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [EnumDataType(typeof(EscolaridadeEnum), ErrorMessage = "Escolaridade inválida.")]
        public int Escolaridade { get; set; }

        public Usuario(string nome, string sobrenome, string email, DateTime dataNascimento, int escolaridade)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        public void AtualizarEmail(string email)
        {
            Email = email;
        }

        public static ValidationResult ValidateDataNascimento(DateTime dataNascimento, ValidationContext context)
        {
            if (dataNascimento > DateTime.Now)
            {
                return new ValidationResult("A data de nascimento não pode ser maior que hoje.");
            }

            return ValidationResult.Success;
        }
    }

    public enum EscolaridadeEnum
    {
        Infantil,
        Fundamental,
        Medio,
        Superior
    }
}