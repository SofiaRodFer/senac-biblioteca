using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                livroService.Inserir(l);
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
            var livros = livroService.ListarTodos(objFiltro);

            if(Request.QueryString.HasValue) {
                string page = Request.QueryString.Value.Split('=').Last();
                int pageNum = int.Parse(page);
                ViewData["paginaAtual"] = pageNum;
                return View(livros);
            } else {
                ViewData["paginaAtual"] = pagina;
                return View(livros);
            }
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
    }
}