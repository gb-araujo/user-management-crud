using Azure.Core;
using GerenciarUsuarios.Data;
using GerenciarUsuarios.Usuarios;
using Microsoft.EntityFrameworkCore;

public static class UsuariosEndpoints
{
    public static void AddEndpointsUsuarios(this WebApplication app)
    {
        var rotaUsuarios = app.MapGroup("usuarios");

        // POST

        rotaUsuarios.MapPost("",
            async (AddUsuarioRequest request, AppDbContext context) =>
            {

                var usuarioExiste = await context.Usuarios
                    .AnyAsync(usuario => usuario.Email == request.Email);

                if (usuarioExiste)
                    return Results.Conflict("Email já existe.");

                var novoUsuario = new Usuario

                    (request.Nome, request.Sobrenome, request.Email, request.DataNascimento, request.Escolaridade);

                await context.Usuarios.AddAsync(novoUsuario);
                await context.SaveChangesAsync();

                var usuarioRetonro = new UsuarioDTO(novoUsuario.Id, novoUsuario.Nome, novoUsuario.Sobrenome,
                    novoUsuario.Email, novoUsuario.DataNascimento, novoUsuario.Escolaridade);

                return Results.Ok(novoUsuario);
            });

        // GET 

        rotaUsuarios.MapGet("", async (AppDbContext context) =>
        {
            var usuarios = await context.Usuarios
                .Select(usuario => new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Email,
                    usuario.DataNascimento, usuario.Escolaridade))
                .ToListAsync();

            return Results.Ok(usuarios);
        });

        // UPDATE
        rotaUsuarios.MapPut("{id}",
            async (int id, UsuarioDTO updatedUser, AppDbContext context) =>
            {
                var usuario = await context.Usuarios
                    .SingleOrDefaultAsync(usuario => usuario.Id == id);

                if (usuario == null)
                    return Results.NotFound();

                // Atualiza todos os campos do usuário com os dados fornecidos
                usuario.Nome = updatedUser.Nome;
                usuario.Sobrenome = updatedUser.Sobrenome;
                usuario.Email = updatedUser.Email;
                usuario.DataNascimento = updatedUser.DataNascimento;
                usuario.Escolaridade = updatedUser.Escolaridade;

                await context.SaveChangesAsync();

                return Results.Ok(new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.Email,
                    usuario.DataNascimento, usuario.Escolaridade));
            });

    // DELETE
        rotaUsuarios.MapDelete("{id}", async (int id, AppDbContext context) =>
        {
            var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return Results.NotFound();
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}