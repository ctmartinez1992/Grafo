using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Grafo_csharp
{
    public class Modelo {

		    public Estrutura.tipo_material cor_cubo;
    //		list<int> iluminacaoCam;
		    public float[] g_pos_luz1 = new float[4];
		    public float[] g_pos_luz2 = new float[4];
		    //StudioModel homer;
		    public float escala;
		    public Glu.GLUquadric quad;

            public Modelo()
            {
                escala = (float)0.2;
                cor_cubo = Estrutura.tipo_material.brass;
                g_pos_luz1[0] = (float)-5.0;
                g_pos_luz1[1] = (float)5.0;
                g_pos_luz1[2] = 5.0f;
                g_pos_luz1[3] = 0.0f;
                g_pos_luz2[0] = 5.0f;
                g_pos_luz2[1] = -15.0f;
                g_pos_luz2[2] = 5.0f;
                g_pos_luz2[3] = 0.0f;
            }
    }

   
}
