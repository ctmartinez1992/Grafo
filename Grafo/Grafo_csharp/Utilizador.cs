using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
public class Utilizador {

		public int id, estado;

		public string nome, data_nasc, telefone, email;


        public Utilizador(int id, string nome, string data_nasc, string telefone, string email, int estado)
        {
            this.id = id;
            this.nome = nome;
            this.data_nasc = data_nasc;
            this.telefone = telefone;
            this.email = email;
            this.estado = estado;
        }
}



}
