#ifndef ARCO_H
#define ARCO_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>

class Arco {

	public:

		int noi, nof;
		float peso, largura;

	public:
		
		Arco();
		Arco(int noi, int nof, float peso, float largura);
};

#endif	