using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
    class Estado
    {
        public Camera		camera;
        public int xMouse, yMouse;
        public bool light;
        public bool apresentaNormais;
        public bool caminhoForte;
        public bool caminhoCurto;
        public int lightViewer;
        public int eixoTranslaccao;
        public int picking;
        public double[] eixo;//x,y,z
        public int mainWindow, topSubwindow, navigateSubwindow;

        public Estado()
        {
	        camera = new Camera();

	        eixo[0]=0;
	        eixo[1]=0;
	        eixo[2]=0;
	        light=false;
	        apresentaNormais=false;
	        caminhoCurto=false;
	        caminhoForte=false;
	        lightViewer=1;
	        eixoTranslaccao = -4;
	        picking=-1;
	        xMouse=0;
	        yMouse=0;
        }

    }
}
