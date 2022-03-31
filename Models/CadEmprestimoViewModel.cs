using System;
using System.Collections.Generic;
using System.Linq;  

namespace Biblioteca.Models
{
    public class CadEmprestimoViewModel
    {
        public ICollection<Livro> Livros { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}