#ifndef NO_H
#define NO_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>
class No {

	public:

		float x, y, z, largura;
		int id;
	public:
		
		No();
		No(float x, float y, float z);
};

#endif	