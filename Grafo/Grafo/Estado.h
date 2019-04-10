#ifndef ESTADO_H
#define ESTADO_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>
#include "Estru.h"
#include "Camera.h"

class Estado {

	public:
		Camera		camera;

		int			xMouse,yMouse;
		GLboolean	light;
		GLboolean	apresentaNormais;
		GLboolean	caminhoForte;
		GLboolean	caminhoCurto;
		GLint		lightViewer;
		GLint		eixoTranslaccao;
		GLint		picking;
		GLdouble	eixo[3];
		GLint         mainWindow,topSubwindow,navigateSubwindow;
	public:
		Estado();
};

#endif	