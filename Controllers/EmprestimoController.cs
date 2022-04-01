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
            Autenticacao.CheckLogin(this);
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarTodos();

            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            Autenticacao.CheckLogin(this);
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
            Autenticacao.CheckLogin(this);
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
                int pageNum = int.Parse(Request.QueryString.Value.Split('=').Last());
                ViewData["paginaAtual"] = pageNum;
                ViewData["emprestimos"] = emprestimos;
                return View(emprestimos);
            } else {
                ViewData["paginaAtual"] = pagina;
                ViewData["emprestimos"] = emprestimos;
                return View(emprestimos);
            }
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
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