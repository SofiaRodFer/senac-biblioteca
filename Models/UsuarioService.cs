using MySqlConnector;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public Usuario ValidarLogin(Usuario usuario)
        {
            using(BibliotecaContext bc = new BibliotecaContext()) {
                IQueryable<Usuario> query;
                
                query = bc.Usuarios.Where(u => u.Username.Equals(usuario.Username)).Where(u => u.Senha.Equals(usuario.Senha));
                if(query.Any()) {
                    return query.First();
                } else {
                    return null;
                }
            }
        }

        public void Inserir(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                string senhaEncriptada = Encriptar(u.Senha);
                u.Senha = senhaEncriptada;
                bc.Usuarios.Add(u);
                bc.SaveChanges();
            }
        }

        public void Deletar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(u);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuarios.Find(u.Id);
                usuario.Username = u.Username;
                usuario.Senha = Encriptar(u.Senha);

                bc.SaveChanges();
            }
        }

        public Usuario ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public Usuario ObterPorUsername(string username)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.SingleOrDefault(u => u.Username == username);
            }
        }

        public ICollection<Usuario> ListarTodos()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;
                query = bc.Usuarios;

                return query.ToList().OrderBy(u => u.Username).ToList();
            }
        }

        public string Encriptar(string decriptada) {
            string hash = "hashsupersegura";
            byte[] data = UTF8Encoding.UTF8.GetBytes(decriptada);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transformar = tripleDES.CreateEncryptor();
            byte[] resultado = transformar.TransformFinalBlock(data, 0, data.Length);
            
            return Convert.ToBase64String(resultado);
        }

        public string Decriptar(string encriptada) {
            string hash = "hashsupersegura";
            byte[] data = Convert.FromBase64String(encriptada);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transformar = tripleDES.CreateDecryptor();
            byte[] resultado = transformar.TransformFinalBlock(data, 0, data.Length);
            
            return UTF8Encoding.UTF8.GetString(resultado);
        }
    }
}