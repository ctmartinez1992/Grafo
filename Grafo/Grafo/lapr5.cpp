#define _USE_MATH_DEFINES
#include <math.h>
#include <stdlib.h>     
#include <GL\glut.h>
#include <iostream>
#include "grafos.h"
#include "Estado.h"
#include <list>
#include "mathlib.h"
#include "studio.h"
#include "Modelo.h"
#include <cstdio>
using namespace std;



#pragma comment (lib, "glaux.lib")

extern "C" int read_JPEG_file(const char *, char **, int *, int *, int *);

#define graus(X) (double)((X)*180/M_PI)
#define rad(X)   (double)((X)*M_PI/180)

#ifdef __cplusplus
	inline tipo_material operator++(tipo_material &rs, int ) {
		return rs = (tipo_material)(rs + 1);
	}
#endif

typedef	GLdouble Vertice[3];
typedef	GLdouble Vector[4];

Estado estado;
Modelo modelo;

void myInit()
{
	GLfloat LuzAmbiente[]={0.5,0.5,0.5, 0.0};

	glClearColor (0.0, 0.0, 0.0, 0.0);

	glEnable(GL_SMOOTH); /*enable smooth shading */
	glEnable(GL_LIGHTING); /* enable lighting */
	glEnable(GL_DEPTH_TEST); /* enable z buffer */
	glEnable(GL_NORMALIZE);

	glDepthFunc(GL_LESS);

	glLightModelfv(GL_LIGHT_MODEL_AMBIENT, LuzAmbiente); 
	glLightModeli(GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer); 
	glLightModeli(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE); 

	modelo = Modelo();
	estado = Estado();
	modelo.quad=gluNewQuadric();
	gluQuadricDrawStyle(modelo.quad, GLU_FILL);
	gluQuadricNormals(modelo.quad, GLU_OUTSIDE);

	leGrafo();
}

void imprime_ajuda(void)
{
  printf("\n\nDesenho de um labirinto a partir de um grafo\n");
  printf("h,H - Ajuda \n");
  printf("i,I - Reset dos Valores \n");
  printf("******* Diversos ******* \n");
  printf("l,L - Alterna o calculo luz entre Z e eye (GL_LIGHT_MODEL_LOCAL_VIEWER)\n");
  printf("k,K - Alerna luz de camera com luz global \n");
  printf("s,S - PolygonMode Fill \n");
  printf("w,W - PolygonMode Wireframe \n");
  printf("p,P - PolygonMode Point \n");
  printf("c,C - Liga/Desliga Cull Face \n");
  printf("n,N - Liga/Desliga apresentação das normais \n");
  printf("******* grafos ******* \n");
  printf("F1  - Grava grafo do ficheiro \n");
  printf("F2  - Lê grafo para ficheiro \n");
  printf("F6  - Cria novo grafo\n");
  printf("******* Camera ******* \n");
  printf("Botão esquerdo - Arrastar os eixos (centro da camera)\n");
  printf("Botão direito  - Rodar camera\n");
  printf("Botão direito com CTRL - Zoom-in/out\n");
  printf("PAGE_UP, PAGE_DOWN - Altera distância da camara \n");
  printf("ESC - Sair\n");
}


void material(enum tipo_material mat)
{
	glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, mat_ambient[mat]);
	glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, mat_diffuse[mat]);
	glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, mat_specular[mat]);
	glMaterialf(GL_FRONT_AND_BACK, GL_SHININESS, mat_shininess[mat]);
}

const GLfloat red_light[] = {1.0, 0.0, 0.0, 1.0};
const GLfloat green_light[] = {0.0, 1.0, 0.0, 1.0};
const GLfloat blue_light[] = {0.0, 0.0, 1.0, 1.0};
const GLfloat white_light[] = {1.0, 1.0, 1.0, 1.0};


void putLights(GLfloat* diffuse)
{
	const GLfloat white_amb[] = {0.2, 0.2, 0.2, 1.0};

	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT0, GL_SPECULAR, white_light);
	glLightfv(GL_LIGHT0, GL_AMBIENT, white_amb);
	glLightfv(GL_LIGHT0, GL_POSITION, modelo.g_pos_luz1);

	glLightfv(GL_LIGHT1, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT1, GL_SPECULAR, white_light);
	glLightfv(GL_LIGHT1, GL_AMBIENT, white_amb);
	glLightfv(GL_LIGHT1, GL_POSITION, modelo.g_pos_luz2);

	/* desenhar luz */
	//material(red_plastic);
	//glPushMatrix();
	//	glTranslatef(modelo.g_pos_luz1[0], modelo.g_pos_luz1[1], modelo.g_pos_luz1[2]);
	//	glDisable(GL_LIGHTING);
	//	glColor3f(1.0, 1.0, 1.0);
	//	glutSolidCube(0.1);
	//	glEnable(GL_LIGHTING);
	//glPopMatrix();
	//glPushMatrix();
	//	glTranslatef(modelo.g_pos_luz2[0], modelo.g_pos_luz2[1], modelo.g_pos_luz2[2]);
	//	glDisable(GL_LIGHTING);
	//	glColor3f(1.0, 1.0, 1.0);
	//	glutSolidCube(0.1);
	//	glEnable(GL_LIGHTING);
	//glPopMatrix();

	glEnable(GL_LIGHT0);
	glEnable(GL_LIGHT1);
}

void desenhaSolo(){
#define STEP 10
	glBegin(GL_QUADS);
		glNormal3f(0,0,1);
		for(int i=-300;i<300;i+=STEP)
			for(int j=-300;j<300;j+=STEP){
				glVertex2f(i,j);
				glVertex2f(i+STEP,j);
				glVertex2f(i+STEP,j+STEP);
				glVertex2f(i,j+STEP);
			}
	glEnd();
}

void CrossProduct (GLdouble v1[], GLdouble v2[], GLdouble cross[])
{
	cross[0] = v1[1]*v2[2] - v1[2]*v2[1];
	cross[1] = v1[2]*v2[0] - v1[0]*v2[2];
	cross[2] = v1[0]*v2[1] - v1[1]*v2[0];
}

GLdouble VectorNormalize (GLdouble v[])
{
	int		i;
	GLdouble	length;

	if ( fabs(v[1] - 0.000215956) < 0.0001)
		i=1;

	length = 0;
	for (i=0 ; i< 3 ; i++)
		length += v[i]*v[i];
	length = sqrt (length);
	if (length == 0)
		return 0;
		
	for (i=0 ; i< 3 ; i++)
		v[i] /= length;	

	return length;
}

void desenhaNormal(GLdouble x, GLdouble y, GLdouble z, GLdouble normal[], tipo_material mat){

	switch (mat){
		case red_plastic:
				glColor3f(1,0,0);
			break;
		case azul:
				glColor3f(0,0,1);
			break;
		case emerald:
				glColor3f(0,1,0);
			break;
		default:
				glColor3f(1,1,0);
	}
	glDisable(GL_LIGHTING);
	glPushMatrix();
		glTranslated(x,y,z);
		glScaled(0.4,0.4,0.4);
		glBegin(GL_LINES);
			glVertex3d(0,0,0);
			glVertex3dv(normal);
		glEnd();
		glPopMatrix();
	glEnable(GL_LIGHTING);
}

void desenhaParede(GLfloat xi, GLfloat yi, GLfloat zi, GLfloat xf, GLfloat yf, GLfloat zf){
	GLdouble v1[3],v2[3],cross[3];
	GLdouble length;
	v1[0]=xf-xi;
	v1[1]=yf-yi;
	v1[2]=0;
	v2[0]=0;
	v2[1]=0;
	v2[2]=1;
	CrossProduct(v1,v2,cross);
	//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
	length=VectorNormalize(cross);
	//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

	material(emerald);
	glBegin(GL_QUADS);
		glNormal3dv(cross);
		glVertex3f(xi,yi,zi);
		glVertex3f(xf,yf,zf+0);
		glVertex3f(xf,yf,zf+1);
		glVertex3f(xi,yi,zi+1);
	glEnd();

	if(estado.apresentaNormais) {
		desenhaNormal(xi,yi,zi,cross,emerald);
		desenhaNormal(xf,yf,zf,cross,emerald);
		desenhaNormal(xf,yf,zf+1,cross,emerald);
		desenhaNormal(xi,yi,zi+1,cross,emerald);
	}
}

void desenhaChao(GLfloat xi, GLfloat yi, GLfloat zi, GLfloat xf, GLfloat yf, GLfloat zf, int orient){
	GLdouble v1[3],v2[3],cross[3];
	GLdouble length;
	v1[0]=xf-xi;
	v1[1]=0;
	v2[0]=0;
	v2[1]=yf-yi;

	switch(orient) {
		case NORTE_SUL :
				v1[2]=0;
				v2[2]=zf-zi;
				CrossProduct(v1,v2,cross);
				//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
				length=VectorNormalize(cross);
				//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

				material(red_plastic);
				glBegin(GL_QUADS);
					glNormal3dv(cross);
					glVertex3f(xi,yi,zi);
					glVertex3f(xf,yi,zi);
					glVertex3f(xf,yf,zf);
					glVertex3f(xi,yf,zf);
				glEnd();
				if(estado.apresentaNormais) {
					desenhaNormal(xi,yi,zi,cross,red_plastic);
					desenhaNormal(xf,yi,zi,cross,red_plastic);
					desenhaNormal(xf,yf,zf,cross,red_plastic);
					desenhaNormal(xi,yi,zf,cross,red_plastic);
				}
			break;
		case ESTE_OESTE :
				v1[2]=zf-zi;
				v2[2]=0;
				CrossProduct(v1,v2,cross);
				//printf("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
				length=VectorNormalize(cross);
				//printf("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);
				material(red_plastic);
				glBegin(GL_QUADS);
					glNormal3dv(cross);
					glVertex3f(xi,yi,zi);
					glVertex3f(xf,yi,zf);
					glVertex3f(xf,yf,zf);
					glVertex3f(xi,yf,zi);
				glEnd();
				if(estado.apresentaNormais) {
					desenhaNormal(xi,yi,zi,cross,red_plastic);
					desenhaNormal(xf,yi,zf,cross,red_plastic);
					desenhaNormal(xf,yf,zf,cross,red_plastic);
					desenhaNormal(xi,yi,zi,cross,red_plastic);
				}
			break;
		default:
				cross[0]=0;
				cross[1]=0;
				cross[2]=1;
				material(azul);
				glBegin(GL_QUADS);
					glNormal3f(0,0,1);
					glVertex3f(xi,yi,zi);
					glVertex3f(xf,yi,zf);
					glVertex3f(xf,yf,zf);
					glVertex3f(xi,yf,zi);
				glEnd();
				if(estado.apresentaNormais) {
					desenhaNormal(xi,yi,zi,cross,azul);
					desenhaNormal(xf,yi,zf,cross,azul);
					desenhaNormal(xf,yf,zf,cross,azul);
					desenhaNormal(xi,yi,zi,cross,azul);
				}
			break;
	}
}

void desenhaNo(int no){

	GLboolean norte,sul,este,oeste;
	GLfloat larguraNorte,larguraSul,larguraEste,larguraOeste;
	Arco arco=arcos[0];
	No *noi=&nos[no],*nof;
	norte=sul=este=oeste=GL_TRUE;


	glPushName(no);
		glPushMatrix();
			glTranslatef(nos[no].x,nos[no].y,nos[no].z);
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
			glColor3f(0.0,0.0,1.0);
			glutSolidSphere((nos[no].largura)/2.0, 50, 40);
		glPopMatrix();
	glPopName();


	for(int i=0;i<numArcos; arco=arcos[++i]){
		if(arco.noi==no)
			nof=&nos[arco.nof];
		else 
			if(arco.nof==no)
				nof=&nos[arco.noi];
			else
				continue;
		if(noi->x==nof->x)
			if(noi->y<nof->y){
				norte=GL_FALSE;
				larguraNorte=arco.largura;
			}
			else{
				sul=GL_FALSE;
				larguraSul=arco.largura;
			}
		else 
				if(noi->x<nof->x){
					oeste=GL_FALSE;
					larguraOeste=arco.largura;
				}
				else{
					este=GL_FALSE;
					larguraEste=arco.largura;
				}
		if (norte && sul && este && oeste)
			return;
	}		
	
}

void desenhaEixo(){
	gluCylinder(modelo.quad,0.5,0.5,20,16,15);
	glPushMatrix();
		glTranslatef(0,0,20);
		glPushMatrix();
			glRotatef(180,0,1,0);
			gluDisk(modelo.quad,0.5,2,16,6);
		glPopMatrix();
		gluCylinder(modelo.quad,2,0,5,16,15);
	glPopMatrix();
}

double distanciaEntrePontos(No p0, No p1) {
	double distancia = 0;
	double dx=0, dy=0;

	dx=p1.x-p0.x;
	dy=p1.y-p0.y;

	distancia = sqrt( (dx*dx) + (dy*dy) );

	return distancia;
}
bool iluminaLigacao(int id_ini,int id_f){
	
	list<int> ids;
	ids.empty();
	if(estado.caminhoCurto){
		//chama metodo caminho curto
		ids.push_back(4);
		ids.push_back(5);
		ids.push_back(6);
		//modelo.iluminacaoCam=ids;
	}
	else if(estado.caminhoForte){
		//chama metodo caminho forte
		ids.push_back(2);
		ids.push_back(3);
		ids.push_back(6);
		ids.push_back(5);
		//modelo.iluminacaoCam=ids;
	}
	int i=0;
		for(list<int>::iterator list_iter = ids.begin(); list_iter != ids.end(); list_iter++)
		{
			i++;
			if(*list_iter==id_ini || *list_iter==id_f){
				if(i!=ids.size()){
					list<int>::iterator list_iterf = list_iter;
					list_iterf++;
					if(*list_iterf==id_f || *list_iterf==id_ini){
						return true;
					}
				}
			}
		}
			return false;
}

void desenhaArco(Arco arco){
	No *noi,*nof;

		// arco vertical
		if(nos[arco.noi].y<nos[arco.nof].y){
			noi=&nos[arco.noi];
			nof=&nos[arco.nof];
		}else{
			nof=&nos[arco.noi];
			noi=&nos[arco.nof];
		}

		double compProj = sqrt(pow(nof->x - noi->x,2)+pow(nof->y - noi->y,2));
		double des = nof->z - noi->z;
		double comp = sqrt(pow(compProj,2)+pow(des,2));
		double raio = (noi->largura)/2.0;
		double inclinicao = atan2(des,compProj);
		double orientacao = atan2((nof->y - noi->y),(nof->x - noi->x));
		
		glPushMatrix();
			glTranslatef(noi->x,noi->y,noi->z);
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
			glRotatef(graus(orientacao),0,0,1);
			glRotatef(inclinicao,0,0,1);
			glRotatef(graus(M_PI/2.0-inclinicao),0,1,0);
			material(red_plastic);
			if(iluminaLigacao(noi->id,nof->id))
				material(azul);
			gluCylinder(modelo.quad,raio/2.0,raio/2.0,comp,38,8);
		glPopMatrix();

	
	}
void desenhaLabirinto(){
	glPushMatrix();
		glTranslatef(0,0,5);
		glScalef(5,5,5);
		material(azul);
		for(int i=0; i<numNos; i++){		
			desenhaNo(i);
		}
		material(emerald);
		for(int i=0; i<numArcos; i++)
			desenhaArco(arcos[i]);
		
	glPopMatrix();
}



#define EIXO_X		-1
#define EIXO_Y		-2
#define EIXO_Z		-3

void desenhaPlanoDrag(int eixo){
	glPushMatrix();
		glTranslated(estado.eixo[0],estado.eixo[1],estado.eixo[2]);
		switch (eixo) {
			case EIXO_Y :
					if(abs(estado.camera.dir_lat)<M_PI/4)
						glRotatef(-90,0,0,1);
					else
						glRotatef(90,1,0,0);
					material(red_plastic);
				break;
			case EIXO_X :
					if(abs(estado.camera.dir_lat)>M_PI/6)
						glRotatef(90,1,0,0);
					material(azul);
				break;
			case EIXO_Z :
					if(abs(cos(estado.camera.dir_long))>0.5)
						glRotatef(90,0,0,1);

					material(emerald);
				break;
		}
		glBegin(GL_QUADS);
			glNormal3f(0,1,0);
			glVertex3f(-100,0,-100);
			glVertex3f(100,0,-100);
			glVertex3f(100,0,100);
			glVertex3f(-100,0,100);
		glEnd();
	glPopMatrix();
}

void desenhaEixos(){

	glPushMatrix();
		glTranslated(estado.eixo[0],estado.eixo[1],estado.eixo[2]);
		material(emerald);
		glPushName(EIXO_Z);
			desenhaEixo();
		glPopName();
		glPushName(EIXO_Y);
			glPushMatrix();
				glRotatef(-90,1,0,0);
				material(red_plastic);
				desenhaEixo();
			glPopMatrix();
		glPopName();
		glPushName(EIXO_X);
			glPushMatrix();
				glRotatef(90,0,1,0);
				material(azul);
				desenhaEixo();
			glPopMatrix();
		glPopName();
	glPopMatrix();
}

void setCamera(){
	Vertice eye;
	eye[0]=estado.camera.center[0]+estado.camera.dist*cos(estado.camera.dir_long)*cos(estado.camera.dir_lat);
	eye[1]=estado.camera.center[1]+estado.camera.dist*sin(estado.camera.dir_long)*cos(estado.camera.dir_lat);
	eye[2]=estado.camera.center[2]+estado.camera.dist*sin(estado.camera.dir_lat);
	if(eye[2]<=0) eye[2]=1;

	if(estado.light){
		gluLookAt(eye[0],eye[1],eye[2],estado.camera.center[0],estado.camera.center[1],estado.camera.center[2],0,0,1);
		putLights((GLfloat*)white_light);
	}else{
		putLights((GLfloat*)white_light);
		gluLookAt(eye[0],eye[1],eye[2],estado.camera.center[0],estado.camera.center[1],estado.camera.center[2],0,0,1);
	}
}

void strokeString(char *str,double x, double y, double z, double s)
{
	int i,n;
	
	n = strlen(str);
	glPushMatrix();
	  glTranslated(x,y-119.05*0.5*s,z);
	  glScaled(s,s,s);
	  for(i=0;i<n;i++)
		  glutStrokeCharacter(GLUT_STROKE_ROMAN,(int)str[i]);
	glPopMatrix();

}




void keyboard(unsigned char key, int x, int y)
{

    switch (key)
    {
		case 27 :
				exit(0);
			break;
		case 'h':
		case 'H':
				imprime_ajuda();
			break;
		case 'l':
		case 'L':
				if(estado.lightViewer)
					estado.lightViewer=0;
				else
					estado.lightViewer=1;
				glLightModeli(GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer);
				glutPostRedisplay();
			break;
		case 'k':
		case 'K':
				estado.light=!estado.light;
				glutPostRedisplay();
			break;
		case 'w':
		case 'W':
				glDisable(GL_LIGHTING);
				glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
				glutPostRedisplay();
			break;
		case 'p':
		case 'P':
				glDisable(GL_LIGHTING);
				glPolygonMode(GL_FRONT_AND_BACK, GL_POINT);
				glutPostRedisplay();
			break;
		case 's':
		case 'S':
				glEnable(GL_LIGHTING);
				glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
				glutPostRedisplay();
			break;
		case 'c':
		case 'C':
				if(glIsEnabled(GL_CULL_FACE))
					glDisable(GL_CULL_FACE);
				else
					glEnable(GL_CULL_FACE);
				glutPostRedisplay();
			break;    
		case 'n':
		case 'N':
				estado.apresentaNormais=!estado.apresentaNormais;
				glutPostRedisplay();
			break;    		
		case 'i':
		case 'I':
			estado = Estado();
			modelo = Modelo();
				glutPostRedisplay();
			break;    
	}
}

void Special(int key, int x, int y){

	switch(key){
		case GLUT_KEY_F1 :
				gravaGrafo();
			break;
		case GLUT_KEY_F2 :
				leGrafo();
				glutPostRedisplay();
			break;	

		case GLUT_KEY_F6 :
				numNos=numArcos=0;
				addNo(No( 0, 10,1));  // 0
				addNo(No( 0,  5,1));  // 1
				addNo(No(-5,  5,1));  // 2
				addNo(No( 5,  5,1));  // 3
				addNo(No(-5,  0,1));  // 4
				addNo(No( 5,  0,1));  // 5
				addNo(No(-5, -5,1));  // 6
				addArco(Arco(0,1,2,1));  // 0 - 1
				addArco(Arco(1,2,2,1));  // 1 - 2
				addArco(Arco(1,3,1,1));  // 1 - 3
				addArco(Arco(2,4,1,1));  // 2 - 4
				addArco(Arco(3,5,1,1));  // 3 - 5
				addArco(Arco(4,5,1,1));  // 4 - 5
				addArco(Arco(4,6,1,1));  // 4 - 6
				glutPostRedisplay();
			break;	
		case GLUT_KEY_UP:
				estado.camera.dist-=1;
				glutPostRedisplay();
			break;
		case GLUT_KEY_DOWN:
				estado.camera.dist+=1;
				glutPostRedisplay();
			break;	}
}


void setProjection(int x, int y, GLboolean picking){
    glLoadIdentity();
	if (picking) { // se está no modo picking, lê viewport e define zona de picking
		GLint vport[4];
		glGetIntegerv(GL_VIEWPORT, vport);
		gluPickMatrix(x, glutGet(GLUT_WINDOW_HEIGHT)  - y, 4, 4, vport); // Inverte o y do rato para corresponder à jana
	}
	    
	gluPerspective(estado.camera.fov,(GLfloat)glutGet(GLUT_WINDOW_WIDTH) /glutGet(GLUT_WINDOW_HEIGHT) ,1,500);

}

void myReshape(int w, int h){	

	glViewport(0, 0, w, h);
    glMatrixMode(GL_PROJECTION);

	setProjection(0,0,GL_FALSE);
	glMatrixMode(GL_MODELVIEW);

}

void desenhaInformacao(int width, int height)  // largura e altura da janela
{
	int heightView=200;
	int widthView=heightView;
	int xview=estado.xMouse;
	int yview=height-estado.yMouse;
	if(estado.xMouse+widthView>width)
		xview=estado.xMouse-widthView;
	if(estado.yMouse<heightView)
		yview=height-estado.yMouse-heightView;
glViewport(xview, yview, widthView, heightView);
  glMatrixMode(GL_PROJECTION);
  glLoadIdentity();
  gluOrtho2D(-100, 100, -100, 100);

  glMatrixMode(GL_MODELVIEW);
  glLoadIdentity();

  //Blending (transparencias)

  glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA,GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_LIGHTING);
	glDisable(GL_DEPTH_TEST);

	glColor4f(0, 0, 0, 0.5f);
	glRectf(-100, -100, 100, 100);
	glPushMatrix();
	glColor3f(1,1,1);

		double tamanhoLetra=0.13;
		int linha=-119.05*0.5*tamanhoLetra-10;
		char str[10]="id:";
		int id=(nos[estado.picking].id);
		char buffer[100];  
		int n = sprintf(buffer,"%s%d",str,id);
		

		strokeString(buffer, -90, 90, 0 , tamanhoLetra); 
		strokeString("nome:", -90, 90+linha, 0 , tamanhoLetra); // string, x ,y ,z ,scale
		strokeString("idade:", -90, 90+linha*2, 0 , tamanhoLetra); // string, x ,y ,z ,scale

	glPopMatrix();
	glDisable(GL_BLEND);
	glEnable(GL_LIGHTING);
	glEnable(GL_DEPTH_TEST);
	myReshape(width, height);
	
	glutPostRedisplay();
}

void mymenu(int value) { 
	switch (value)
	{
	case 1:
		estado.caminhoCurto=GL_TRUE;
		estado.caminhoForte=GL_FALSE;
		cout << "Caminho mais curto para"<< nos[estado.picking].id;
		break;
	case 2:
		estado.caminhoCurto=GL_FALSE;
		estado.caminhoForte=GL_TRUE;
		cout << "Caminho mais forte para"<< nos[estado.picking].id;
		break;
	default:
		estado.caminhoForte=GL_FALSE;
		estado.caminhoCurto=GL_FALSE;
		break;
	}


}
void menuNos(){
	glutCreateMenu(mymenu);  // single menu, no need for id 
	glutAddMenuEntry("Caminho mais curto", 1); 
	glutAddMenuEntry("Caminho mais forte", 2);
	glutAddMenuEntry("Back", 3); 
	glutAttachMenu(GLUT_RIGHT_BUTTON);
}

void display(void)
{


	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	setCamera();


	material(slate);
	desenhaSolo();
	//desenhaEixos();
	
	desenhaLabirinto();
	/*glPushMatrix();
			glScalef(0.005,0.005,0.005);
			modelo.homer.SetSequence(0);
            mdlviewer_display(modelo.homer);
		glPopMatrix();*/
	if(estado.picking>=0){
			desenhaInformacao(glutGet(GLUT_WINDOW_WIDTH),glutGet(GLUT_WINDOW_HEIGHT));
			menuNos();
	}
	
	if(estado.eixoTranslaccao<0 && estado.eixoTranslaccao!=-4) {
		// desenha plano de translacção
		cout << "Translate... " << estado.eixoTranslaccao << endl; 
		desenhaPlanoDrag(estado.eixoTranslaccao);
	}
	
	glutPostRedisplay();
	glFlush();
	glutSwapBuffers();

}
void motionRotate(int x, int y){
#define DRAG_SCALE	0.01
	double lim=M_PI/2-0.1;
	estado.camera.dir_long+=(estado.xMouse-x)*DRAG_SCALE;
	estado.camera.dir_lat-=(estado.yMouse-y)*DRAG_SCALE*0.5;
	if(estado.camera.dir_lat>lim)
		estado.camera.dir_lat=lim;
	else 
		if(estado.camera.dir_lat<-lim)
			estado.camera.dir_lat=-lim;
	estado.xMouse=x;
	estado.yMouse=y;
	glutPostRedisplay();
}

void motionZoom(int x, int y){
#define ZOOM_SCALE	0.5
	estado.camera.dist-=(estado.yMouse-y)*ZOOM_SCALE;
	if(estado.camera.dist<5)
		estado.camera.dist=5;
	else 
		if(estado.camera.dist>200)
			estado.camera.dist=200;
	estado.yMouse=y;
	glutPostRedisplay();
}

void motionDrag(int x, int y){
	GLuint buffer[100];
	GLint vp[4];
	GLdouble proj[16], mv[16];
	int n;
	GLdouble newx, newy, newz;

	glSelectBuffer(100, buffer);
	glRenderMode(GL_SELECT);
	glInitNames();

	glMatrixMode(GL_PROJECTION);
	glPushMatrix(); // guarda a projecção
		glLoadIdentity();
		setProjection(x,y,GL_TRUE);
	
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	setCamera();
	desenhaPlanoDrag(estado.eixoTranslaccao);
	
	n = glRenderMode(GL_RENDER);
	if (n > 0) {
		glGetIntegerv(GL_VIEWPORT, vp);
		glGetDoublev(GL_PROJECTION_MATRIX, proj);
		glGetDoublev(GL_MODELVIEW_MATRIX, mv);
		gluUnProject(x, glutGet(GLUT_WINDOW_HEIGHT) - y, (double) buffer[2] / UINT_MAX, mv, proj, vp, &newx, &newy, &newz);
		//printf("Novo x:%lf, y:%lf, z:%lf\n\n", newx, newy, newz);
		switch (estado.eixoTranslaccao) {
			case EIXO_X :
					estado.eixo[0]=newx;
					printf("x");
					//estado.eixo[1]=newy;
				break;
			case EIXO_Y :
					estado.eixo[1]=newy;
					//estado.eixo[2]=newz;
				break;
			case EIXO_Z :
					//estado.eixo[0]=newx;
					estado.eixo[2]=newz;
				break;	
			case -4:
				break;
			default:
				printf("ja te comi");
				break;
		}
		glutPostRedisplay();
	}


	glMatrixMode(GL_PROJECTION); //repõe matriz projecção
	glPopMatrix();
	glMatrixMode(GL_MODELVIEW);
	glutPostRedisplay();
}

int picking(int x, int y){
	int i, n, objid=-4;
	double zmin = 10.0;
	GLuint buffer[100], *ptr;

	glSelectBuffer(100, buffer);
	glRenderMode(GL_SELECT);
	glInitNames();

	glMatrixMode(GL_PROJECTION);
	glPushMatrix(); // guarda a projecção
		glLoadIdentity();
		setProjection(x,y,GL_TRUE);
	
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	setCamera();
	//desenhaEixos();
	desenhaLabirinto();

	n = glRenderMode(GL_RENDER);
	if (n > 0)
	{
		ptr = buffer;
		for (i = 0; i < n; i++)
		{
			if (zmin > (double) ptr[1] / UINT_MAX) {
				zmin = (double) ptr[1] / UINT_MAX;
				objid = ptr[3];
			}
			ptr += 3 + ptr[0]; // ptr[0] contem o número de nomes (normalmente 1); 3 corresponde a numnomes, zmin e zmax
		}
	}


	glMatrixMode(GL_PROJECTION); //repõe matriz projecção
	glPopMatrix();
	glMatrixMode(GL_MODELVIEW);

	return objid;
}
void mouse(int btn, int state, int x, int y){
	
	switch(btn) {
		case GLUT_RIGHT_BUTTON :
					if(state == GLUT_DOWN){
						estado.xMouse=x;
						estado.yMouse=y;
						if(glutGetModifiers() & GLUT_ACTIVE_CTRL)
							glutMotionFunc(motionZoom);
						else
							glutMotionFunc(motionRotate);
						cout << "Left down\n";
					}
					else{
						glutMotionFunc(NULL);
						cout << "Left up\n";
					}
				break;
		case GLUT_LEFT_BUTTON :
					if(state == GLUT_DOWN){
						estado.eixoTranslaccao=picking(x,y);
						if(estado.eixoTranslaccao!=-4 && estado.eixoTranslaccao<0){
							glutMotionFunc(motionDrag);
						}
						else{
							cout << "Right down - objecto:" << estado.eixoTranslaccao << endl;
						}
					}
					else{
						if(estado.eixoTranslaccao!=-4 && estado.eixoTranslaccao<0) {
							estado.camera.center[0]=estado.eixo[0];
							estado.camera.center[1]=estado.eixo[1];
							estado.camera.center[2]=estado.eixo[2];
							glutMotionFunc(NULL);
							estado.eixoTranslaccao=-4;
							glutPostRedisplay();
						}
						cout << "Right up\n";
						if(estado.eixoTranslaccao>=0){
							printf("carregou num no");
						}
					}
				break;
	}
}

void mouse(int x, int y){
	estado.xMouse=x;
	estado.yMouse=y;
	estado.picking=picking(x,y);
}

void main(int argc, char **argv)
{
    glutInit(&argc, argv);

/* need both double buffering and z buffer */

    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
    glutInitWindowSize(640, 480);
    glutCreateWindow("OpenGL");
	myInit();
	mdlviewer_init( "homer.mdl", modelo.homer);
    glutReshapeFunc(myReshape);
    glutDisplayFunc(display);
	glutKeyboardFunc(keyboard);
	glutSpecialFunc(Special);
	glutMouseFunc(mouse);
	glutPassiveMotionFunc(mouse);
	
	
	
	
	imprime_ajuda();

    glutMainLoop();
}
