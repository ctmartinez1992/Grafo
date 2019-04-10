#include "utilizador.h"

Utilizador::Utilizador() {
}

Utilizador::Utilizador(int id, char* nome, char* data_nasc, char* telefone,char* email, int estado) {
	this->id=id;
	this->nome=nome;
	this->data_nasc=data_nasc;
	this->telefone=telefone;
	this->email=email;
	this->estado=estado;
}