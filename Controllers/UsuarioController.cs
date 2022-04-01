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
                if (usuarioSessao.Id != 0) {
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
    }
}
