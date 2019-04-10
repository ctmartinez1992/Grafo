#ifndef _GRAFO_INCLUDE
#define _GRAFO_INCLUDE

#include "Arco.h"
#include "No.h"

#define _MAX_NOS_GRAFO 100
#define _MAX_ARCOS_GRAFO 200

#define NORTE_SUL	0
#define ESTE_OESTE	1
#define PLANO		2

extern No nos[];
extern Arco arcos[];
extern int numNos, numArcos;

void addNo(No);
void deleteNo(int);
void imprimeNo(No);
void listNos();

void addArco(Arco);
void deleteArco(int);
void imprimeArco(Arco);
void listArcos();

void gravaGrafo();
void leGrafo();

#endif