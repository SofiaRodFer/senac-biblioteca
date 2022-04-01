using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Usuario
    {
        private UsuarioService servico = new UsuarioService();

        public Usuario(string username, string senha) {
            this.Username = username;
            this.Senha = servico.Encrypt(senha);
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
    }
}