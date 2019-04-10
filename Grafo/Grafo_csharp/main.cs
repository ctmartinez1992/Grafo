using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace Grafo_csharp
{
    class main
    {


        public static float graus(double x) { return (float)((x) * 180 / Math.PI); }
        public static float rad(double x) { return (float)((x) * Math.PI / 180); }

//#ifdef __cplusplus
//    inline tipo_material operator++(tipo_material &rs, int ) {
//        return rs = (tipo_material)(rs + 1);
//    }

public  Estado estado = new Estado();
public Modelo modelo = new Modelo();


static void myInit()
{
	float[] LuzAmbiente={(float)0.5,(float)0.5,(float)0.5,(float) 0.0};

	Gl.glClearColor (0, 0, 0, 0);
   
	Gl.glEnable(Gl.GL_SMOOTH); /*enable smooth shading */
	Gl.glEnable(Gl.GL_LIGHTING); /* enable lighting */
	Gl.glEnable(Gl.GL_DEPTH_TEST); /* enable z buffer */
	Gl.glEnable(Gl.GL_NORMALIZE);

	Gl.glDepthFunc(Gl.GL_LESS);

	Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, LuzAmbiente); 
	Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer); 
	Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE, 1/*true*/); 
    
	modelo = new Modelo();
	estado = new Estado();
	modelo.quad=Glu.gluNewQuadric();
	Glu.gluQuadricDrawStyle(modelo.quad, Glu.GLU_FILL);
	Glu.gluQuadricNormals(modelo.quad, Glu.GLU_OUTSIDE);

	//leGrafo();
}

static void imprime_ajuda()
{
  Console.Write("\n\nDesenho de um labirinto a partir de um grafo\n");
  Console.Write("h,H - Ajuda \n");
  Console.Write("i,I - Reset dos Valores \n");
  Console.Write("******* Diversos ******* \n");
  Console.Write("l,L - Alterna o calculo luz entre Z e eye (GL_LIGHT_MODEL_LOCAL_VIEWER)\n");
  Console.Write("k,K - Alerna luz de camera com luz global \n");
  Console.Write("s,S - PolygonMode Fill \n");
  Console.Write("w,W - PolygonMode Wireframe \n");
  Console.Write("p,P - PolygonMode Point \n");
  Console.Write("c,C - Liga/Desliga Cull Face \n");
  Console.Write("n,N - Liga/Desliga apresentação das normais \n");
  Console.Write("******* grafos ******* \n");
  Console.Write("F1  - Grava grafo do ficheiro \n");
  Console.Write("F2  - Lê grafo para ficheiro \n");
  Console.Write("F6  - Cria novo grafo\n");
  Console.Write("******* Camera ******* \n");
  Console.Write("Botão esquerdo - Arrastar os eixos (centro da camera)\n");
  Console.Write("Botão direito  - Rodar camera\n");
  Console.Write("Botão direito com CTRL - Zoom-in/out\n");
  Console.Write("PAGE_UP, PAGE_DOWN - Altera distância da camara \n");
  Console.Write("ESC - Sair\n");
}


public static float[] red_light = new float[] {(float)1.0, (float)0.0, (float)0.0, (float)1.0};
public static float[] green_light = new float[] { (float)0.0, (float)1.0, (float)0.0, (float)1.0 };
public static float[] blue_light = new float[] { (float)0.0, (float)0.0, (float)1.0, (float)1.0 };
public static float[] white_light = new float[] { (float)1.0, (float)1.0, (float)1.0, (float)1.0 };

//---------REVER-------------
static void material(Estrutura.tipo_material mat)
{
        
    Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT, Estrutura.mat_ambient[(int)mat]);
    Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, Estrutura.mat_diffuse[(int)mat]);
    Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, Estrutura.mat_specular[(int)mat]);
    Gl.glMaterialf(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, Estrutura.mat_shininess[(int)mat]);
}



static void putLights(float[] diffuse)
{
	float[] white_amb = new float[] {(float)0.2, (float)0.2, (float)0.2, (float)1.0};

	Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuse);
	Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, white_light);
	Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, white_amb);
	Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, modelo.g_pos_luz1);

	Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, diffuse);
	Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, white_light);
	Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, white_amb);
	Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, modelo.g_pos_luz2);

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

	Gl.glEnable(Gl.GL_LIGHT0);
	Gl.glEnable(Gl.GL_LIGHT1);
}

static void desenhaSolo(){
int STEP = 10;
	Gl.glBegin(Gl.GL_QUADS);
		Gl.glNormal3f(0,0,1);
		for(int i=-300;i<300;i+=STEP)
			for(int j=-300;j<300;j+=STEP){
				Gl.glVertex2f(i,j);
				Gl.glVertex2f(i+STEP,j);
				Gl.glVertex2f(i+STEP,j+STEP);
				Gl.glVertex2f(i,j+STEP);
			}
	Gl.glEnd();
}

void CrossProduct (double[] v1, double[] v2, double[] cross)
{
	cross[0] = v1[1]*v2[2] - v1[2]*v2[1];
	cross[1] = v1[2]*v2[0] - v1[0]*v2[2];
	cross[2] = v1[0]*v2[1] - v1[1]*v2[0];
}

double VectorNormalize (double[] v)
{
	int		i;
	double	length;
    
	if (Math.Abs(v[1] - 0.000215956) < 0.0001)
		i=1;

	length = 0;
	for (i=0 ; i< 3 ; i++)
		length += v[i]*v[i];
	length = Math.Sqrt (length);
	if (length == 0)
		return 0;
		
	for (i=0 ; i< 3 ; i++)
		v[i] /= length;	

	return length;
}

void desenhaNormal(double x, double y, double z, double[] normal, Estrutura.tipo_material mat){

	switch (mat){
		case Estrutura.tipo_material.red_plastic:
				Gl.glColor3f(1,0,0);
			break;
		case Estrutura.tipo_material.azul:
				Gl.glColor3f(0,0,1);
			break;
		case Estrutura.tipo_material.emerald:
				Gl.glColor3f(0,1,0);
			break;
		default:
				Gl.glColor3f(1,1,0);
            break;
	}
	Gl.glDisable(Gl.GL_LIGHTING);
	Gl.glPushMatrix();
		Gl.glTranslated(x,y,z);
		Gl.glScaled(0.4,0.4,0.4);
		Gl.glBegin(Gl.GL_LINES);
			Gl.glVertex3d(0,0,0);
			Gl.glVertex3dv(normal);
		Gl.glEnd();
		Gl.glPopMatrix();
	Gl.glEnable(Gl.GL_LIGHTING);
}

void desenhaParede(float xi, float yi, float zi, float xf, float yf, float zf){
	double []v1= new double[3];
    double[] v2 = new double[3];
    double[] cross = new double[3];
	double length;
	v1[0]=xf-xi;
	v1[1]=yf-yi;
	v1[2]=0;
	v2[0]=0;
	v2[1]=0;
	v2[2]=1;
	CrossProduct(v1,v2,cross);
	//Console.Write("cross x=%lf y=%lf z=%lf",cross[0],cross[1],cross[2]);
	length=VectorNormalize(cross);
	//Console.Write("Normal x=%lf y=%lf z=%lf length=%lf\n",cross[0],cross[1],cross[2]);

	material(Estrutura.tipo_material.emerald);
	Gl.glBegin(Gl.GL_QUADS);
		Gl.glNormal3dv(cross);
		Gl.glVertex3f(xi,yi,zi);
		Gl.glVertex3f(xf,yf,zf+0);
		Gl.glVertex3f(xf,yf,zf+1);
		Gl.glVertex3f(xi,yi,zi+1);
	Gl.glEnd();

	if(estado.apresentaNormais) {
		desenhaNormal(xi,yi,zi,cross,Estrutura.tipo_material.emerald);
		desenhaNormal(xf,yf,zf,cross,Estrutura.tipo_material.emerald);
		desenhaNormal(xf,yf,zf+1,cross,Estrutura.tipo_material.emerald);
		desenhaNormal(xi,yi,zi+1,cross,Estrutura.tipo_material.emerald);
	}
}


static void desenhaNo(int no){

	bool norte,sul,este,oeste;
	float larguraNorte,larguraSul,larguraEste,larguraOeste;
	Arco arco=Grafos.arcos[0];
	No noi=Grafos.nos[no],nof;
	norte=sul=este=oeste=true;


	Gl.glPushName(no);
		Gl.glPushMatrix();
        Gl.glTranslatef(Grafos.nos[no].x, Grafos.nos[no].y, Grafos.nos[no].z);
			Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
			Gl.glColor3f(0,0,1);
            Glut.glutSolidSphere((Grafos.nos[no].largura) / 2.0, 50, 40);//nao consegui
		Gl.glPopMatrix();
	Gl.glPopName();


    for (int i = 0; i < Grafos.numArcos; arco = Grafos.arcos[++i])
    {
		if(arco.noi==no)
            nof = Grafos.nos[arco.nof];
		else 
			if(arco.nof==no)
                nof = Grafos.nos[arco.noi];
			else
				continue;
		if(noi.x==nof.x)
			if(noi.y<nof.y){
				norte=false;
				larguraNorte=arco.largura;
			}
			else{
				sul=false;
				larguraSul=arco.largura;
			}
		else 
				if(noi.x<nof.x){
					oeste=false;
					larguraOeste=arco.largura;
				}
				else{
					este=false;
					larguraEste=arco.largura;
				}
		if (norte && sul && este && oeste)
			return;
	}		
	
}

void desenhaEixo(){
	Glu.gluCylinder(modelo.quad,0.5,0.5,20,16,15);
	Gl.glPushMatrix();
		Gl.glTranslatef(0,0,20);
		Gl.glPushMatrix();
			Gl.glRotatef(180,0,1,0);
			Glu.gluDisk(modelo.quad,0.5,2,16,6);
		Gl.glPopMatrix();
		Glu.gluCylinder(modelo.quad,2,0,5,16,15);
	Gl.glPopMatrix();
}

double distanciaEntrePontos(No p0, No p1) {
	double distancia = 0;
	double dx=0, dy=0;

	dx=p1.x-p0.x;
	dy=p1.y-p0.y;

	distancia = Math.Sqrt( (dx*dx) + (dy*dy) );

	return distancia;
}
static bool iluminaLigacao(int id_ini, int id_f)
{
	
	List<int> ids= new List<int>();
	ids.Clear();
	if(estado.caminhoCurto){
		//chama metodo caminho curto
		ids.Add(4);
		ids.Add(5);
		ids.Add(6);
		//modelo.iluminacaoCam=ids;
	}
	else if(estado.caminhoForte){
		//chama metodo caminho forte
		ids.Add(2);
		ids.Add(3);
		ids.Add(6);
		ids.Add(5);
		//modelo.iluminacaoCam=ids;
	}
	int i=0;
		foreach (int value in ids)
            {
			i++;
			if(value==id_ini || value==id_f){
				if(i!=ids.Count()){
                    
					if(ids[i++]==id_f || ids[i++]==id_ini){
						return true;
					}
				}
			}
		}
			return false;
}

static void desenhaArco(Arco arco){
	No noi,nof;

		// arco vertical
    if (Grafos.nos[arco.noi].y < Grafos.nos[arco.nof].y)
    {
			noi=Grafos.nos[arco.noi];
            nof = Grafos.nos[arco.nof];
		}else{
            nof = Grafos.nos[arco.noi];
            noi = Grafos.nos[arco.nof];
		}

		double compProj = Math.Sqrt(Math.Pow(nof.x - noi.x,2)+Math.Pow(nof.y - noi.y,2));
		double des = nof.z - noi.z;
        double comp = Math.Sqrt(Math.Pow(compProj, 2) + Math.Pow(des, 2));
		double raio = (noi.largura)/2.0;
		double inclinicao = Math.Atan2(des,compProj);
        double orientacao = Math.Atan2((nof.y - noi.y), (nof.x - noi.x));
		
		Gl.glPushMatrix();
			Gl.glTranslatef(noi.x,noi.y,noi.z);
			Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
			Gl.glRotatef(graus(orientacao),0,0,1);
			Gl.glRotatef((float)inclinicao,0,0,1);
			Gl.glRotatef(graus(Math.PI/2.0-inclinicao),0,1,0);
			material(Estrutura.tipo_material.red_plastic);
			if(iluminaLigacao(noi.id,nof.id))
				material(Estrutura.tipo_material.azul);
			Glu.gluCylinder(modelo.quad,raio/2.0,raio/2.0,comp,38,8);
		Gl.glPopMatrix();

	
	}
static void desenhaLabirinto(){
	Gl.glPushMatrix();
		Gl.glTranslatef(0,0,5);
		Gl.glScalef(5,5,5);
		material(Estrutura.tipo_material.azul);
		for(int i=0; i<Grafos.numNos; i++){		
			desenhaNo(i);
		}
		material(Estrutura.tipo_material.emerald);
        for (int i = 0; i < Grafos.numArcos; i++)
            desenhaArco(Grafos.arcos[i]);
		
	Gl.glPopMatrix();
}



const int EIXO_X = -1;
const int EIXO_Y = -2;
const int EIXO_Z = -3;

static void desenhaPlanoDrag(int eixo)
{
	Gl.glPushMatrix();
		Gl.glTranslated(estado.eixo[0],estado.eixo[1],estado.eixo[2]);
		switch (eixo) {
			case EIXO_Y :
					if(Math.Abs(estado.camera.dir_lat)<Math.PI/4)
						Gl.glRotatef(-90,0,0,1);
					else
						Gl.glRotatef(90,1,0,0);
					material(Estrutura.tipo_material.red_plastic);
				break;
			case EIXO_X :
					if(Math.Abs(estado.camera.dir_lat)>Math.PI/6)
						Gl.glRotatef(90,1,0,0);
					material(Estrutura.tipo_material.azul);
				break;
			case EIXO_Z :
					if(Math.Abs(Math.Cos(estado.camera.dir_long))>0.5)
						Gl.glRotatef(90,0,0,1);

					material(Estrutura.tipo_material.emerald);
				break;
		}
		Gl.glBegin(Gl.GL_QUADS);
			Gl.glNormal3f(0,1,0);
			Gl.glVertex3f(-100,0,-100);
			Gl.glVertex3f(100,0,-100);
			Gl.glVertex3f(100,0,100);
			Gl.glVertex3f(-100,0,100);
		Gl.glEnd();
	Gl.glPopMatrix();
}

void desenhaEixos(){

	Gl.glPushMatrix();
		Gl.glTranslated(estado.eixo[0],estado.eixo[1],estado.eixo[2]);
		material(Estrutura.tipo_material.emerald);
		Gl.glPushName(EIXO_Z);
			desenhaEixo();
		Gl.glPopName();
		Gl.glPushName(EIXO_Y);
			Gl.glPushMatrix();
				Gl.glRotatef(-90,1,0,0);
				material(Estrutura.tipo_material.red_plastic);
				desenhaEixo();
			Gl.glPopMatrix();
		Gl.glPopName();
		Gl.glPushName(EIXO_X);
			Gl.glPushMatrix();
				Gl.glRotatef(90,0,1,0);
				material(Estrutura.tipo_material.azul);
				desenhaEixo();
			Gl.glPopMatrix();
		Gl.glPopName();
	Gl.glPopMatrix();
}

static void setCamera(){
	double[] eye=new double[3];
	eye[0]=estado.camera.center[0]+estado.camera.dist*Math.Cos(estado.camera.dir_long)*Math.Cos(estado.camera.dir_lat);
	eye[1]=estado.camera.center[1]+estado.camera.dist*Math.Sin(estado.camera.dir_long)*Math.Cos(estado.camera.dir_lat);
	eye[2]=estado.camera.center[2]+estado.camera.dist*Math.Sin(estado.camera.dir_lat);
	if(eye[2]<=0) eye[2]=1;

	if(estado.light){
		Glu.gluLookAt(eye[0],eye[1],eye[2],estado.camera.center[0],estado.camera.center[1],estado.camera.center[2],0,0,1);
		putLights((float[])white_light);
	}else{
		putLights((float[])white_light);
		Glu.gluLookAt(eye[0],eye[1],eye[2],estado.camera.center[0],estado.camera.center[1],estado.camera.center[2],0,0,1);
	}
}

static void strokeString(string str, double x, double y, double z, double s)
{
	int i,n;
	
	n = str.Length;
	Gl.glPushMatrix();
	  Gl.glTranslated(x,y-119.05*0.5*s,z);
	  Gl.glScaled(s,s,s);
	  for(i=0;i<n;i++)
		  Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN,(int)str[i]);
	Gl.glPopMatrix();
    
}

public static void keyboard(byte key, int x, int y)
{
    switch (key)
    {
		case 27 :
				return;
			break;
		case 104://h
		case 72:
				//imprime_ajuda();
			break;
		case 108://l
		case 76:
				if(estado.lightViewer==1)
					estado.lightViewer=0;
				else
					estado.lightViewer=1;
				Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER, estado.lightViewer);
				Glut.glutPostRedisplay();
			break;
		case 107://k
		case 75:
				estado.light=!estado.light;
				Glut.glutPostRedisplay();
			break;
		case 119://w
		case 87:
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
				Glut.glutPostRedisplay();
			break;
		case 112://p
		case 80:
				Gl.glDisable(Gl.GL_LIGHTING);
				Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
				Glut.glutPostRedisplay();
			break;
		case 115: //s
		case 83:
				Gl.glEnable(Gl.GL_LIGHTING);
				Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
				Glut.glutPostRedisplay();
			break;
		case 99://c
		case 67:
				if(Gl.glIsEnabled(Gl.GL_CULL_FACE)==1)
					Gl.glDisable(Gl.GL_CULL_FACE);
				else
					Gl.glEnable(Gl.GL_CULL_FACE);
				Glut.glutPostRedisplay();
			break;    
		case 110://n
		case 78:
				estado.apresentaNormais=!estado.apresentaNormais;
				Glut.glutPostRedisplay();
			break;    		
		case 105://i
		case 73:
			estado = new Estado();
			modelo = new Modelo();
				Glut.glutPostRedisplay();
			break;    
	}
}
static void specialKey(int key, int x, int y)
        {

	switch(key){
		case Glut.GLUT_KEY_F1 :
				//gravaGrafo();
			break;
		case Glut.GLUT_KEY_F2 :
				//leGrafo();
				Glut.glutPostRedisplay();
			break;	

		case Glut.GLUT_KEY_F6 :
            Grafos.numNos = Grafos.numArcos = 0;
				Grafos.addNo(new No(0, 10,1));  // 0
				Grafos.addNo(new No(0,  5,1));  // 1
				Grafos.addNo(new No(-5,  5,1));  // 2
				Grafos.addNo(new No( 5,  5,1));  // 3
				Grafos.addNo(new No(-5,  0,1));  // 4
				Grafos.addNo(new No( 5,  0,1));  // 5
				Grafos.addNo(new No(-5, -5,1));  // 6
				Grafos.addArco(new Arco(0,1,2,1));  // 0 - 1
				Grafos.addArco(new Arco(1,2,2,1));  // 1 - 2
				Grafos.addArco(new Arco(1,3,1,1));  // 1 - 3
				Grafos.addArco(new Arco(2,4,1,1));  // 2 - 4
				Grafos.addArco(new Arco(3,5,1,1));  // 3 - 5
				Grafos.addArco(new Arco(4,5,1,1));  // 4 - 5
				Grafos.addArco(new Arco(4,6,1,1));  // 4 - 6
				Glut.glutPostRedisplay();
			break;	
		case Glut.GLUT_KEY_UP:
				estado.camera.dist-=1;
				Glut.glutPostRedisplay();
			break;
		case Glut.GLUT_KEY_DOWN:
				estado.camera.dist+=1;
				Glut.glutPostRedisplay();
			break;	}
}


static void setProjection(int x, int y, bool picking){
    Gl.glLoadIdentity();
	if (picking) { // se está no modo picking, lê viewport e define zona de picking
		int[] vport = new int[4];
		Gl.glGetIntegerv(Gl.GL_VIEWPORT, vport);
		Glu.gluPickMatrix(x, Glut.glutGet(Glut.GLUT_WINDOW_HEIGHT)  - y, 4, 4, vport); // Inverte o y do rato para corresponder à jana
	}
	    
	Glu.gluPerspective(estado.camera.fov,(float)Glut.glutGet(Glut.GLUT_WINDOW_WIDTH) /Glut.glutGet(Glut.GLUT_WINDOW_HEIGHT) ,1,500);

}

static void myReshape(int w, int h){	

	Gl.glViewport(0, 0, w, h);
    Gl.glMatrixMode(Gl.GL_PROJECTION);

	setProjection(0,0,false);
	Gl.glMatrixMode(Gl.GL_MODELVIEW);

}

static void desenhaInformacao(int width, int height)  // largura e altura da janela
{
	int heightView=200;
	int widthView=heightView;
	int xview=estado.xMouse;
	int yview=height-estado.yMouse;
	if(estado.xMouse+widthView>width)
		xview=estado.xMouse-widthView;
	if(estado.yMouse<heightView)
		yview=height-estado.yMouse-heightView;
    Gl.glViewport(xview, yview, widthView, heightView);
  Gl.glMatrixMode(Gl.GL_PROJECTION);
  Gl.glLoadIdentity();
  Glu.gluOrtho2D(-100, 100, -100, 100);

  Gl.glMatrixMode(Gl.GL_MODELVIEW);
  Gl.glLoadIdentity();

  //Blending (transparencias)

    Gl.glEnable(Gl.GL_BLEND);
	Gl.glBlendFunc(Gl.GL_SRC_ALPHA,Gl.GL_ONE_MINUS_SRC_ALPHA);
	Gl.glDisable(Gl.GL_LIGHTING);
	Gl.glDisable(Gl.GL_DEPTH_TEST);

	Gl.glColor4f(0, 0, 0, 0.5f);
	Gl.glRectf(-100, -100, 100, 100);
	Gl.glPushMatrix();
	Gl.glColor3f(1,1,1);

		double tamanhoLetra=0.13;
		double linha=-119.05*0.5*tamanhoLetra-10;
		int id=(Grafos.nos[estado.picking].id);
        string str="id:"+id;

		strokeString(str, -90, 90, 0 , tamanhoLetra); 
		strokeString("nome:", -90, 90+linha, 0 , tamanhoLetra); // string, x ,y ,z ,scale
		strokeString("idade:", -90, 90+linha*2, 0 , tamanhoLetra); // string, x ,y ,z ,scale

	Gl.glPopMatrix();
	Gl.glDisable(Gl.GL_BLEND);
	Gl.glEnable(Gl.GL_LIGHTING);
	Gl.glEnable(Gl.GL_DEPTH_TEST);
	myReshape(width, height);
	
	Glut.glutPostRedisplay();
}

static void mymenu(int value)
{ 
	switch (value)
	{
	case 1:
		estado.caminhoCurto=true;
		estado.caminhoForte=false;
		Console.Write("Caminho mais curto para" + Grafos.nos[estado.picking].id);
		break;
	case 2:
		estado.caminhoCurto=false;
		estado.caminhoForte=true;
		Console.Write("Caminho mais forte para" + Grafos.nos[estado.picking].id);
		break;
	default:
		estado.caminhoForte=false;
		estado.caminhoCurto=false;
		break;
	}


}
static void menuNos()
{
	Glut.glutCreateMenu(mymenu);  // single menu, no need for id 
	Glut.glutAddMenuEntry("Caminho mais curto", 1); 
	Glut.glutAddMenuEntry("Caminho mais forte", 2);
	Glut.glutAddMenuEntry("Back", 3); 
	Glut.glutAttachMenu(Glut.GLUT_RIGHT_BUTTON);
}

static void display()
{
	Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
	Gl.glLoadIdentity();
	setCamera();

	material(Estrutura.tipo_material.slate);
	desenhaSolo();
	//desenhaEixos();
	
	desenhaLabirinto();
	/*glPushMatrix();
			glScalef(0.005,0.005,0.005);
			modelo.homer.SetSequence(0);
            mdlviewer_display(modelo.homer);
		glPopMatrix();*/
	if(estado.picking>=0){
			desenhaInformacao(Glut.glutGet(Glut.GLUT_WINDOW_WIDTH),Glut.glutGet(Glut.GLUT_WINDOW_HEIGHT));
			menuNos();
	}
	
	if(estado.eixoTranslaccao<0 && estado.eixoTranslaccao!=-4) {
		// desenha plano de translacção
		Console.Write("Translate... " + estado.eixoTranslaccao+"\n"); 
		desenhaPlanoDrag(estado.eixoTranslaccao);
	}
	
	Glut.glutPostRedisplay();
	Gl.glFlush();
	Glut.glutSwapBuffers();

}
static void motionRotate(int x, int y)
{
    double DRAG_SCALE=0.01;
	double lim=Math.PI/2-0.1;
	estado.camera.dir_long+=(estado.xMouse-x)*DRAG_SCALE;
	estado.camera.dir_lat-=(estado.yMouse-y)*DRAG_SCALE*0.5;
	if(estado.camera.dir_lat>lim)
		estado.camera.dir_lat=lim;
	else 
		if(estado.camera.dir_lat<-lim)
			estado.camera.dir_lat=-lim;
	estado.xMouse=x;
	estado.yMouse=y;
	Glut.glutPostRedisplay();
}

static void motionZoom(int x, int y)
{
    float ZOOM_SCALE = 0.5f;
	estado.camera.dist-=(estado.yMouse-y)*ZOOM_SCALE;
	if(estado.camera.dist<5)
		estado.camera.dist=5;
	else 
		if(estado.camera.dist>200)
			estado.camera.dist=200;
	estado.yMouse=y;
	Glut.glutPostRedisplay();
}

static void motionDrag(int x, int y)
{
	int[] buffer= new int[100];
    int[] vp = new int[4];
    double[] proj = new double[16];
    double[] mv = new double[16];
	int n;
	double newx=0, newy=0, newz=0;

    Gl.glSelectBuffer(100, buffer);
    Gl.glRenderMode(Gl.GL_SELECT);
    Gl.glInitNames();

    Gl.glMatrixMode(Gl.GL_PROJECTION);
    Gl.glPushMatrix(); // guarda a projecção
    Gl.glLoadIdentity();
	setProjection(x,y,true);

    Gl.glMatrixMode(Gl.GL_MODELVIEW);
    Gl.glLoadIdentity();
	setCamera();
	desenhaPlanoDrag(estado.eixoTranslaccao);

    n = Gl.glRenderMode(Gl.GL_RENDER);
	if (n > 0) {
        Gl.glGetIntegerv(Gl.GL_VIEWPORT, vp);
        Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, proj);
        Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, mv);
        //Glu.gluUnProject(x, Glut.glutGet(Glut.GLUT_WINDOW_HEIGHT) - y, (int)buffer[2] / int.MaxValue, mv, proj, vp, newx, newy, newz);
		//Console.Write("Novo x:%lf, y:%lf, z:%lf\n\n", newx, newy, newz);
		switch (estado.eixoTranslaccao) {
			case EIXO_X :
					estado.eixo[0]=newx;
					Console.Write("x");
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
				Console.Write("ja te comi");
				break;
		}
        Glut.glutPostRedisplay();
	}


    Gl.glMatrixMode(Gl.GL_PROJECTION); //repõe matriz projecção
    Gl.glPopMatrix();
    Gl.glMatrixMode(Gl.GL_MODELVIEW);
    Glut.glutPostRedisplay();
}

static int picking(int x, int y)
{
	int i, n, objid=-4;
	double zmin = 10.0;
    int[] buffer=new int[100]; 
    int[] ptr;

	Gl.glSelectBuffer(100, buffer);
    Gl.glRenderMode(Gl.GL_SELECT);
    Gl.glInitNames();

    Gl.glMatrixMode(Gl.GL_PROJECTION);
    Gl.glPushMatrix(); // guarda a projecção
    Gl.glLoadIdentity();
		setProjection(x,y,true);

    Gl.glMatrixMode(Gl.GL_MODELVIEW);
    Gl.glLoadIdentity();
	setCamera();
	//desenhaEixos();
	desenhaLabirinto();

    n = Gl.glRenderMode(Gl.GL_RENDER);
    //if (n > 0)
    //{
    //    ptr = buffer;
    //    for (i = 0; i < n; i++)
    //    {
    //        if (zmin > (double) ptr[1] / int.MaxValue) {
    //            zmin = (double) ptr[1] / int.MaxValue;
    //            objid = ptr[3];
    //        }
    //        ptr += 3 + ptr[0]; // ptr[0] contem o número de nomes (normalmente 1); 3 corresponde a numnomes, zmin e zmax
    //    }
    //}


    Gl.glMatrixMode(Gl.GL_PROJECTION); //repõe matriz projecção
    Gl.glPopMatrix();
    Gl.glMatrixMode(Gl.GL_MODELVIEW);

	return objid;
}
static void mouse(int btn, int state, int x, int y)
{
	
	switch(btn) {
		case Glut.GLUT_RIGHT_BUTTON :
					if(state == Glut.GLUT_DOWN){
						estado.xMouse=x;
						estado.yMouse=y;
                        if (Glut.glutGetModifiers()==1 & Glut.GLUT_ACTIVE_CTRL==1)
                            Glut.glutMotionFunc(motionZoom);
						else
                            Glut.glutMotionFunc(motionRotate);
						Console.Write("Left down\n");
					}
					else{
						Glut.glutMotionFunc(null);
                        Console.Write("Left up\n");
					}
				break;
        case Glut.GLUT_LEFT_BUTTON:
                if (state == Glut.GLUT_DOWN)
                {
						estado.eixoTranslaccao=picking(x,y);
						if(estado.eixoTranslaccao!=-4 && estado.eixoTranslaccao<0){
                            Glut.glutMotionFunc(motionDrag);
						}
						else{
							Console.Write("Right down - objecto:" + estado.eixoTranslaccao+"\n");
						}
					}
					else{
						if(estado.eixoTranslaccao!=-4 && estado.eixoTranslaccao<0) {
							estado.camera.center[0]=estado.eixo[0];
							estado.camera.center[1]=estado.eixo[1];
							estado.camera.center[2]=estado.eixo[2];
                            Glut.glutMotionFunc(null);
							estado.eixoTranslaccao=-4;
                            Glut.glutPostRedisplay();
						}
						Console.Write("Right up\n");
						if(estado.eixoTranslaccao>=0){
							Console.Write("carregou num no");
						}
					}
				break;
	}
}

static void mouse(int x, int y)
{
	estado.xMouse=x;
	estado.yMouse=y;
	estado.picking=picking(x,y);
}

static void Main(string[] args)
{
    Glut.glutInit();  

/* need both double buffering and z buffer */

    Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB | Glut.GLUT_DEPTH);
    Glut.glutInitWindowSize(640, 480);
    Glut.glutCreateWindow("OpenGL");
	myInit();
	//mdlviewer_init( "homer.mdl", modelo.homer);
    Glut.glutReshapeFunc(myReshape);
    Glut.glutDisplayFunc(display);
    Glut.glutKeyboardFunc(keyboard);
    Glut.glutSpecialFunc(specialKey);
    Glut.glutMouseFunc(mouse);
    Glut.glutPassiveMotionFunc(mouse);
	
	
	
	
	imprime_ajuda();

    Glut.glutMainLoop();
}
    }
}