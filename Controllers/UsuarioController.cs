using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            UsuarioService servico = new UsuarioService();
            Usuario usuario = new Usuario(login, senha);

            Usuario usuarioSessao = servico.ValidarLogin(usuario);
            if(!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha)) {
                if (usuarioSessao != null && usuarioSessao.Id != 0) {
                    ViewData["Mensagem"] = "Você está logado!";
                    HttpContext.Session.SetInt32("Id", usuarioSessao.Id);
                    HttpContext.Session.SetString("Username", usuarioSessao.Username);

                    return RedirectToAction("Index", "Home");
                } else {
                    ViewData["Mensagem"] = "Falha no login, verifique seus dados.";
                    return View();
                }
            } else {
                ViewData["Mensagem"] = "Falha no login! Verifique se você preencheu todas as informações corretamente.";
                return View();
            }
        }


        public IActionResult Cadastro() {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            UsuarioService usuarioService = new UsuarioService();

            Usuario existeUsuario = usuarioService.ObterPorUsername(usuario.Username);
            if(existeUsuario == null) {
                usuarioService.Inserir(usuario);
                return RedirectToAction("Listagem");
            } else {
                ViewData["Mensagem"] = "Esse username já está em uso!";
                return View();
            }
        }

        public IActionResult Edicao(int id)
        {
            UsuarioService usuarioService = new UsuarioService();
            Usuario u = usuarioService.ObterPorId(id);
            string senhaDecriptada = usuarioService.Decriptar(u.Senha);
            u.Senha = senhaDecriptada; 
            
            return View(u);
        }

        [HttpPost]
        public IActionResult Edicao(Usuario usuario)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(usuario.Username == null || usuario.Senha == null) {
                ViewData["Mensagem"] = "Preencha todos os campos!";
                return View("Edicao", usuario);
            } else {
                usuarioService.Atualizar(usuario);
                return RedirectToAction("Listagem");
            }
        }

        public IActionResult Listagem(int pagina = 1)
        {
            Autenticacao.CheckLogin(this);
            if(Autenticacao.CheckAdmin(this)) {
                UsuarioService usuarioService = new UsuarioService();
                var usuarios = usuarioService.ListarTodos();

                if(Request.QueryString.HasValue) {
                    int pageNum = int.Parse(Request.QueryString.Value.Split('=').Last());
                    ViewData["paginaAtual"] = pageNum;
                    ViewData["emprestimos"] = usuarios;
                    return View(usuarios);
                } else {
                    ViewData["paginaAtual"] = pagina;
                    ViewData["emprestimos"] = usuarios;
                    return View(usuarios);
                }
            } else {
                return View("Proibido");
            }
        }

        public IActionResult Deletar(int id)
        {
            UsuarioService usuarioService = new UsuarioService();
            Usuario u = usuarioService.ObterPorId(id);
            
            return View(u);
        }

        [HttpPost]
        public IActionResult Deletar(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            Usuario usuario = usuarioService.ObterPorId(u.Id);
            usuarioService.Deletar(usuario);

            return RedirectToAction("Listagem");
        }
    }
}
