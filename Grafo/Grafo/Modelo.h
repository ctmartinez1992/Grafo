#ifndef MODELO_H
#define MODELO_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>
#include "Estru.h"

#include "mathlib.h"
#include "studio.h"
#include "mdlviewer.h"
class Modelo {

	public:
		tipo_material cor_cubo;
//		list<int> iluminacaoCam;
		GLfloat g_pos_luz1[4];
		GLfloat g_pos_luz2[4];
		StudioModel homer;
		GLfloat escala;
		GLUquadric *quad;

	public:
		Modelo();
};

#endif