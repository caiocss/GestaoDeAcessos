using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeAcessos
{
    class Perfil
    {
        public string Nome { get; private set; }
        public Sistema Sistema { get; private set; }

        public Perfil(string nome, Sistema sistema)
        {
            this.Nome = nome;
            this.Sistema = sistema;
        }
    }
}
