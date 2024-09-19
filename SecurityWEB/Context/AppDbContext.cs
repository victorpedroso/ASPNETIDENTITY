using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityWEB.Entities;

namespace SecurityWEB.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Aluno 1",
                    SophiaId = 1111,
                    SophiaRm = 1112,
                    DataCadastro = DateTime.Now,
                    PathFoto = @"\\10.100.15.232\teste.jpg",
                    UrlFoto = "http://10.100.15.232:3000"
                });
        }
    }
}
