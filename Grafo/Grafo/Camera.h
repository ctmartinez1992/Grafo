#ifndef CAMERA_H
#define CAMERA_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>

typedef	GLdouble Vertice[3];
typedef	GLdouble Vector[4];

class Camera {

	public:	
		GLfloat fov;
		GLdouble dir_lat;
		GLdouble dir_long;
		GLfloat dist;
		Vertice center;

	public:
		Camera();

		GLfloat getFOV();
		GLdouble getDirLat();
		GLdouble getDirLong();
		GLfloat getDist();
};

#endif