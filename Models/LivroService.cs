using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Biblioteca.Models
{
    public class LivroService
    {
        public void Inserir(Livro l)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Livros.Add(l);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Livro l)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Livro livro = bc.Livros.Find(l.Id);
                livro.Autor = l.Autor;
                livro.Titulo = l.Titulo;
                livro.Ano = l.Ano;

                bc.SaveChanges();
            }
        }

        public ICollection<Livro> ListarTodos(FiltrosLivros filtro = null)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Livro> query;
                
                if(filtro != null)
                {
                    switch(filtro.TipoFiltro)
                    {
                        case "Autor":
                            query = bc.Livros.Where(l => l.Autor.Contains(filtro.Filtro));
                        break;

                        case "Titulo":
                            query = bc.Livros.Where(l => l.Titulo.Contains(filtro.Filtro));
                        break;

                        default:
                            query = bc.Livros;
                        break;
                    }
                }
                else
                {
                    query = bc.Livros;
                }
                
                return query.OrderBy(l => l.Titulo).ToList();
            }
        }

        public ICollection<Livro> ListarDisponiveis()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return
                    bc.Livros
                    .Where(l =>  !(bc.Emprestimos.Where(e => e.Devolvido == false).Select(e => e.LivroId).Contains(l.Id)) )
                    .ToList();
            }
        }

        public Livro ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Livros.Find(id);
            }
        }

        public (IEnumerable<Livro>, int) Paginacao(ICollection<Livro> livrosFiltrados, int pgAtual) {
            int numPorPagina = 10;

            int numeroDePaginas = Convert.ToInt32(Math.Ceiling(livrosFiltrados.Count() / (double)numPorPagina));

            int inicio = (pgAtual - 1) * numPorPagina;
            IEnumerable<Livro> livrosPaginados = livrosFiltrados.OrderBy(l=>l.Id).Skip(inicio).Take(numPorPagina);

            return (livrosPaginados, numeroDePaginas);
        }
    }
}