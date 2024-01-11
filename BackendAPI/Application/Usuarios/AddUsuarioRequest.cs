namespace GerenciarUsuarios.Usuarios;

public record AddUsuarioRequest(string Nome, string Sobrenome, string Email, DateTime DataNascimento, int Escolaridade);