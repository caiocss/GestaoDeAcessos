using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeAcessos
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public string Login { get; set; }
        public string Perfil { get; set; }
        public string Status { get; set; }
        public Sistema Sistema { get; set; }

        public Usuario()
        {
            this.Status = "Ativo";
        }


    }   
}
