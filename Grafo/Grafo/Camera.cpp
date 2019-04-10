#include "Camera.h"

Camera::Camera() {
	dir_lat=M_PI/4;
	dir_long=-M_PI/4;
	fov=60;
	dist=200;

	center[0]=0;
	center[1]=0;
	center[2]=0;
}

GLfloat Camera::getFOV() {
	return this->fov;
}

GLdouble Camera::getDirLat() {
	return this->dir_lat;
}

GLdouble Camera::getDirLong() {
	return this->dir_long;
}

GLfloat Camera::getDist() {
	return this->dist;
}