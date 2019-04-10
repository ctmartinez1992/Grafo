using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
    public class Arco
    {
		public int noi, nof;
		public float peso, largura;

        public Arco() {}

        public Arco(int noi, int nof, float peso, float largura) {
	        this.noi=noi;
	        this.nof=nof;
	        this.peso=peso;
	        this.largura=largura;
        }
    }
}
