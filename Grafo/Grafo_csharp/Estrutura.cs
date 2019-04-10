using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
    public class Estrutura
    {
        
        public static int NORTE_SUL = 0;
        public static int ESTE_OESTE = 1;
        public static int PLANO = 2;
        public int[] n1 = new int[4] { 2, 4, 6, 8 };
        public static float[][] mat_ambient = new float[][] {
                                          new float[] {0.33f, 0.22f, 0.03f, 1.0f},	// brass
								          new float[] {0.0f, 0.0f, 0.0f},			// red plastic
								          new float[] {0.0215f, 0.1745f, 0.0215f},	// emerald
								          new float[] {0.02f, 0.02f, 0.02f},		// slate
								          new float[] {0.0f, 0.0f, 0.1745f},		// azul
								          new float[] {0.02f, 0.02f, 0.02f},		// preto
								          new float[] {0.1745f, 0.1745f, 0.1745f}};// cinza

        public static float[][] mat_diffuse = new float[][] {
                                          new float[] {0.78f, 0.57f, 0.11f, 1.0f},		// brass
								          new float[] {0.5f, 0.0f, 0.0f},				// red plastic
								          new float[] {0.07568f, 0.61424f, 0.07568f},	// emerald
								          new float[] {0.78f, 0.78f, 0.78f},			// slate
								          new float[] {0.0f, 0.0f, 0.61424f},			// azul
								          new float[] {0.08f, 0.08f, 0.08f},			// preto
								          new float[] {0.61424f, 0.61424f, 0.61424f}};	// cinza

        public static float[][] mat_specular = new float[][] {
                                           new float[] {0.99f, 0.91f, 0.81f, 1.0f},			// brass
								           new float[] {0.7f, 0.6f, 0.6f},					// red plastic
								           new float[] {0.633f, 0.727811f, 0.633f},		// emerald
								           new float[] {0.14f, 0.14f, 0.14f},				// slate
								           new float[] {0.0f, 0.0f, 0.727811f},			// azul
								           new float[] {0.03f, 0.03f, 0.03f},				// preto
								           new float[] {0.727811f, 0.727811f, 0.727811f}};	// cinza

        public static float[] mat_shininess = new float[] {
                                         27.8f,	// brass
								         32.0f,	// red plastic
								         76.8f,	// emerald
								         18.78f,	// slate
								         30.0f,	// azul
								         75.0f,	// preto
								         60.0f};	// cinza

        public enum tipo_material { brass, red_plastic, emerald, slate, azul, preto, cinza };

    }
}
