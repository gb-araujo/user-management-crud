namespace GerenciarUsuarios.Usuarios;

public record UsuarioDTO(int Id, string Nome, string Sobrenome, string Email, DateTime DataNascimento, int Escolaridade);