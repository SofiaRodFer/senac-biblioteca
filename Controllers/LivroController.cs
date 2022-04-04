using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                if(l.Titulo == null || l.Autor == null || l.Ano == 0) {
                    ViewData["Mensagem"] = "Preencha todos os campos!";
                    return View("Cadastro", l);
                } else {
                    livroService.Inserir(l);
                    return RedirectToAction("Listagem");
                }
            }
            else
            {
                livroService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int pagina = 1)
        {
            Autenticacao.CheckLogin(this);
            FiltrosLivros objFiltro = null;
            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
            
            LivroService livroService = new LivroService();
            ICollection<Livro> livrosFiltrados = livroService.ListarTodos(objFiltro);

            int paginaAtual;
            if(Request.QueryString.HasValue) {
                paginaAtual = int.Parse(Request.QueryString.Value.Split('=').Last());
                ViewData["paginaAtual"] = paginaAtual;

            } else {
                paginaAtual = pagina;
                ViewData["paginaAtual"] = pagina;
            }

            IEnumerable<Livro> livros = livroService.Paginacao(livrosFiltrados, paginaAtual).Item1;
            ViewData["numeroPaginas"] = livroService.Paginacao(livrosFiltrados, paginaAtual).Item2;

            return View(livros);
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            Livro l = livroService.ObterPorId(id);
            return View(l);
        }

        [HttpPost]
        public IActionResult Edicao(Livro livro)
        {
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();

            if(livro.Titulo == null || livro.Autor == null || livro.Ano == 0) {
                ViewData["Mensagem"] = "Preencha todos os campos!";
                return View("Edicao", livro);
            } else {
                livroService.Atualizar(livro);
                return RedirectToAction("Listagem");
            }
        }
    }
}