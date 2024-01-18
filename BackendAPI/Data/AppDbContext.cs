using GerenciarUsuarios.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace GerenciarUsuarios.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
           // string connectionString = "Server=NOME_DO_SERVIDOR;Database=NOME_DO_BANCO_DE_DADOS;User Id=NOME_DE_USUARIO;Password=SENHA_DO_USUARIO;TrustServerCertificate=True;Encrypt=False;";
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GUsuario;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }


    }
}