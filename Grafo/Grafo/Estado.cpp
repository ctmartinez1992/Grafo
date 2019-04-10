#include "Estado.h"

Estado::Estado() {
	camera = Camera();

	eixo[0]=0;
	eixo[1]=0;
	eixo[2]=0;
	light=GL_FALSE;
	apresentaNormais=GL_FALSE;
	caminhoCurto=GL_FALSE;
	caminhoForte=GL_FALSE;
	lightViewer=1;
	eixoTranslaccao = -4;
	picking=-1;
	xMouse=0;
	yMouse=0;
}