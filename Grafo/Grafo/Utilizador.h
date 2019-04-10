#ifndef UTILIZADOR_H
#define UTILIZADOR_H

#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>    
#include <iostream>

class Utilizador {

	public:
		int id, estado;
		char *nome, *data_nasc, *telefone, *email;
	public:
		
		Utilizador();
		Utilizador(int id, char* nome, /*lista tags,*/char* data_nasc,char* telefone, char* email, int estado);
};

#endif	