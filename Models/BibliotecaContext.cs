using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                   
            optionsBuilder.UseMySql("Server=localhost;DataBase=Biblioteca;Uid=root;Pwd=1234");
        }

        public DbSet<Livro> Livros {get; set;}
        public DbSet<Emprestimo> Emprestimos {get; set;}
    }
}
