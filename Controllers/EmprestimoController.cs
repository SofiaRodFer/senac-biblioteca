using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Biblioteca.Controllers
{
    
    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();

            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            EmprestimoService emprestimoService = new EmprestimoService();
            
            bool emprestimoAntesDaDevolucao = viewModel.Emprestimo.DataDevolucao.Date < viewModel.Emprestimo.DataEmprestimo.Date;

            if(!emprestimoAntesDaDevolucao) 
            {
                if(viewModel.Emprestimo.Id == 0)
                {
                    emprestimoService.Inserir(viewModel.Emprestimo);
                }
                else
                {
                    emprestimoService.Atualizar(viewModel.Emprestimo);
                }

                return RedirectToAction("Listagem");
            } else {
                return RedirectToAction();
            }
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, int pagina = 1)
        {
            FiltrosEmprestimos objFiltro = null;
            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
            EmprestimoService emprestimoService = new EmprestimoService();
            var emprestimos = emprestimoService.ListarTodos(objFiltro);

            if(Request.QueryString.HasValue) {
                string page = Request.QueryString.Value.Split('=').Last();
                int pageNum = int.Parse(page);
                ViewData["paginaAtual"] = pageNum;
                return View(emprestimos);
            } else {
                ViewData["paginaAtual"] = pagina;
                return View(emprestimos);
            }
        }

        public IActionResult Edicao(int id)
        {
            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();
            cadModel.Emprestimo = e;
            
            return View(cadModel);
        }
    }
}