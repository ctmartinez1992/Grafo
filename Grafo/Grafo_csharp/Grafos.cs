using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
    class Grafos
    {


        public static int _MAX_NOS_GRAFO = 100;
        public static int _MAX_ARCOS_GRAFO = 200;

        public static int NORTE_SUL = 0;
        public static int ESTE_OESTE = 1;
        public static int PLANO = 2;

        public static string __GRAFO__FILE__ = "exemplo.grafo";

        public static No[] nos = new No[_MAX_NOS_GRAFO];
        public static Arco[] arcos;
        public static int numNos = 0, numArcos = 0;


        public static void addNo(No no)
        {
            if (numNos < _MAX_NOS_GRAFO)
            {
                nos[numNos] = no;
                numNos++;
            }
            else
            {
                Console.Write("Número de nós chegou ao limite\n");
            }
        }

        public void deleteNo(int indNo)
        {
            if (indNo >= 0 && indNo < numNos)
            {
                for (int i = indNo; i < numNos; nos[i++] = nos[i + i]) ;
                numNos--;
            }
            else
            {
                Console.Write("Indíce de nó inválido\n");
            }
        }

        public void imprimeNo(No no)
        {
            Console.Write("X:" + no.x + "Y:" + no.y + "Z:" + no.z + "\n");
        }

        public void listNos()
        {
            for (int i = 0; i < numNos; imprimeNo(nos[i++])) ;
        }

        public static void addArco(Arco arco)
        {
            if (numArcos < _MAX_ARCOS_GRAFO)
            {
                arcos[numArcos] = arco;
                numArcos++;
            }
            else
            {
                Console.Write("Número de arcos chegou ao limite\n");
            }
        }

        public void deleteArco(int indArco)
        {
            if (indArco >= 0 && indArco < numArcos)
            {
                for (int i = indArco; i < numArcos; arcos[i++] = arcos[i + i]) ;
                numArcos--;
            }
            else
            {
                Console.Write("Indíce de arco inválido");
            }
        }

        public void imprimeArco(Arco arco)
        {
            Console.Write("No início:" + arco.noi + "Nó final:" + arco.nof + "Peso:" + arco.peso + "Largura:" + arco.largura);
        }

        public void listArcos()
        {
            for (int i = 0; i < numArcos; imprimeArco(arcos[i++])) ;
        }

        public void gravaGrafo()
        {
            //ofstream myfile;

            //myfile.open (__GRAFO__FILE__, ios::out);
            //if (!myfile.is_open()) {
            //    Console.Write("Erro ao abrir " + __GRAFO__FILE__ + "para escrever\n");
            //    return
            //}
            //myfile << numNos << endl;
            //for(int i=0; i<numNos;i++)
            //    myfile << nos[i].x << " " << nos[i].y << " " << nos[i].z <<endl;
            //myfile << numArcos << endl;
            //for(int i=0; i<numArcos;i++)
            //    myfile << arcos[i].noi << " " << arcos[i].nof << " " << arcos[i].peso << " " << arcos[i].largura << endl;
            //myfile.close();
        }
        public void leGrafo()
        {
            //ifstream myfile;

            //myfile.open (__GRAFO__FILE__, ios::in);
            //if (!myfile.is_open()) {
            //    cout << "Erro ao abrir " << __GRAFO__FILE__ << "para ler" <<endl;
            //    exit(1);
            //}
            //myfile >> numNos;
            //for(int i=0; i<numNos;i++)
            //    myfile >> nos[i].x >> nos[i].y >> nos[i].z >> nos[i].id;
            //myfile >> numArcos ;
            //for(int i=0; i<numArcos;i++)
            //    myfile >> arcos[i].noi >> arcos[i].nof >> arcos[i].peso >> arcos[i].largura ;
            //myfile.close();
            //// calcula a largura de cada no = maior largura dos arcos que divergem/convergem desse/nesse no	
            //for(int i=0; i<numNos;i++){
            //    nos[i].largura=0;
            //    for(int j=0; j<numArcos; j++)
            //        if ((arcos[j].noi == i || arcos[j].nof == i) && nos[i].largura < arcos[j].largura)
            //            nos[i].largura = arcos[j].largura;
            //}	

        }
    }
}
