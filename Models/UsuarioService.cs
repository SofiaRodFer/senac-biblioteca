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

// .Where(u => u.Username.Equals(usuario.Username) && u.Senha.Equals(usuario.Senha));


        public string Encrypt(string decrypted) {
            string hash = "hashsupersegura";
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transformar = tripleDES.CreateEncryptor();
            byte[] resultado = transformar.TransformFinalBlock(data, 0, data.Length);
            
            return Convert.ToBase64String(resultado);
        }

        public string Decrypt(string encrypted) {
            string hash = "hashsupersegura";
            byte[] data = Convert.FromBase64String(encrypted);
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