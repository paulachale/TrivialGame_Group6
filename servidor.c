
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <my_global.h>

typedef struct{
	char nombre[20];
	int socket;
}Conectado;
typedef struct{
	Conectado conectados[100];
	int num;
}ListaConectados;
MYSQL *conn;
ListaConectados miLista;
typedef struct{
	int libre;
	char usuarios[50];
	int numero; //número de personas invitadas
	int aceptada; // cambia a 0 si alguien rechazado
	int contador; //personas que han contestado
}Partida;
typedef Partida TablaPartidas[100];
TablaPartidas partidas;//tabla con todas las partidas actuales
int i;
int sockets[100];
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int Pon(ListaConectados *lista,char nombre[20],int socket){
	//Añade a la lista un usuario nuevo. En caso de que esté llena
	//devuelve -1, si se añade correctamente 0
	if (lista->num==100){
		return -1;
	}
	else{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket=socket;
		lista->num++;
		return 0;
	}
}
int DamePosicion(ListaConectados *lista, char nombre[20]){
	//Dado un nombre retorna la posicin si lo encuentra en la lista
	//en caso contrario, devuelve -1
	int i=0;
	int encontrado=0;
	while((i<lista->num) && !encontrado){
		if (strcmp(lista->conectados[i].nombre, nombre)==0){
			encontrado=1;
		}
		else{
			i=i+1;
		}
	}
	if (encontrado){
		return i;
	}
	else{
		return -1;
	}
}
int Eliminar(ListaConectados *lista, char nombre[20]){
	//Retorna 0 si elimina correctamente el usuario de la lista 
	//y -1 en caso contrario
	int pos=DamePosicion(lista, nombre);
	if (pos==-1)
		return -1;
	else{
		int i=0;
		for(i=pos; i<((lista->num)-1);i++){
			strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);
			lista->conectados[i].socket=lista->conectados[i+1].socket;
		}
		lista->num--;
		return 0;
	}
}
void DameConectados(ListaConectados *lista, char conectados[300]){
	//retorna en un vector de char introducido los nobres de todos los conectados, separados
	//por barras de la lista intoducida
	sprintf(conectados, "%s%d/",conectados,lista->num);
	int i;
	for(i=0;i<lista->num;i++){
		sprintf(conectados,"%s%s/",conectados,lista->conectados[i].nombre);
	}
}
void abrirbd(){
	//abre la base de datos con los usuarios, partidas y preguntas
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexiï¿³n: %u %s\n", 
			mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
//inicializar la conexin
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T6_Trivial",0, NULL, 0);
	//conn = mysql_real_connect (conn, "localhost","root", "mysql", "T6_Trivial",0, NULL, 0);
	

	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
			mysql_errno(conn), mysql_error(conn));
		exit (1);
	
	}
	
}
int DameSocket(ListaConectados *lista, char nombre[20]){
	//Dado un nombre retorna la posicin si lo encuentra en la lista
	//en caso contrario, devuelve -1
	int i=0;
	int encontrado=0;
	while((i<lista->num) && !encontrado){
		if (strcmp(lista->conectados[i].nombre, nombre)==0){
			encontrado=1;
		}
		else{
			i=i+1;
		}
	}
	if (encontrado){
		return lista->conectados[i].socket;
	}
	else{
		return -1;
	}
}
int PonPartida(TablaPartidas partidas,ListaConectados *miLista,char invitados[50], char sockets[20] ){
	//crea una nueva partida en la tabla con todos los invitados introducidos en un vector separados por barras
	//y tambien los sockets introducidos d ela misma forma. Devuelve la posición de la tabla donde se ha introducido
	int b=0;
	printf("libre1=%d\n",partidas[1].libre);
	printf("peticion:%s\n",invitados);
	int posicion=-1;
	int encontrado=0;
	while ((b<100)&&(encontrado!=1)){
		
		if(partidas[b].libre==0){
			encontrado=1;
			printf("Encontrado un hueco libre en la tabla\n");
			int numero=1;//el que crea la partida ya lo añadimos al número de participantes
			posicion=b;
			char usuarios[50];
			partidas[posicion].libre=1;
			char *p;
			p=strtok(invitados, "/"); //eliminamos el código 7
			p=strtok(NULL,"/");
			partidas[posicion].aceptada=1;
			partidas[posicion].contador=0;
			strcpy(usuarios, p);
			printf("usuarios:%s\n",usuarios);
			
			
			p=strtok(NULL, "/");
			strcpy(sockets,"");
			while(p!=NULL){
				char s[20];
				strcpy(s,p);
				sprintf(usuarios, "%s/%s",usuarios,s);
				char usuario[20];
				numero=numero+1;
				
				
				int socket=DameSocket(miLista, s);
				sprintf(sockets,"%s%d/",sockets,socket);
				p=strtok(NULL, "/");
				
			}
			printf("Usuarios:%s\n",usuarios);
			strcpy(partidas[posicion].usuarios,usuarios);
			partidas[posicion].numero=numero;

		}
		else
		   printf("No entra\n");
		b++;
	}
	printf("La posición en la tabla es %d\n",posicion);
	return posicion;
}






int log_in(char usuario[20], char contrasena[20] ){
	//con dos char de usuario y contraseña como inputs comprueba si existe el usuario
	//y retorna 0 si no se identifica y 1 en caso contrario
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT Usuario.Nombre FROM (Usuario) WHERE Usuario.Nombre='");
	strcat(consulta, usuario);
	strcat(consulta, "'AND Usuario.Contrasena='");
	strcat(consulta, contrasena);
	strcat(consulta,"'");
	
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int posi=DamePosicion(&miLista,usuario);
	if (row == NULL || posi!=-1){
		printf ("No se ha identificado correctamente.\n");
		return 0;
	}
	else{
		printf("Se ha identificado correctamente.\n");
		return 1;
	}
	//				sprintf(respuesta, "%s%s", respuesta, "\n");
	
}
int registro(char usuario[20], char contrasena[20], char mail[50]){
	//con los tres char de input usuario, contraseña y mail, comprueba que no exista el usuario 
	//y en ese caso lo añade a la base de datos retornando un 1 ,o un 0 en caso contrario
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT Usuario.Nombre FROM (Usuario) WHERE Usuario.Nombre='");
	strcat(consulta, usuario);
	strcat(consulta,"'");
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){ //No existe el usuario
		
		printf ("No existe ningún usuario con ese nombre.\n");
		//Hacemos una consulta para saber cuantos usuarios hay ya añadidos en la tabla
		char consultaID[200];
		strcpy(consultaID, "SELECT Usuario.Nombre FROM (Usuario)");
		
		err=mysql_query (conn, consultaID);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		int n=1;
		while(row != NULL){
			n=n+1;
			row = mysql_fetch_row (resultado);
		}
		//ID del nuevo usuario
		char idtext[10];
		
		char consulta2[300];
		sprintf(idtext, "%d", n);
		strcpy(consulta2, "INSERT INTO Usuario VALUES ("); //Creamos la consulta de insertar nuevo usuario
		strcat(consulta2, idtext);
		strcat(consulta2, ",'");
		strcat(consulta2, usuario);
		strcat(consulta2,"','");
		strcat(consulta2,contrasena);
		strcat(consulta2,"','");
		strcat(consulta2,mail);
		strcat(consulta2,"',0,0,0,0,0,0,0)");
		printf("%s\n", consulta2);
		
		
		err=mysql_query (conn, consulta2);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		return 1;
		
	}
	else{
		
		
		printf("Ya existe un usuario con ese nombre.\n");
		return 0;
	}
}
int eliminar(char usuario[20], char contrasena[20] ){
	//comprueba si el usuario y contraseña son correctos y sustituye su nombre por eliminado
	//retorna 0 en caso de no poder realizarse y 1 si se elimina correctamente
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT Usuario.Nombre FROM (Usuario) WHERE Usuario.Nombre='");
	strcat(consulta, usuario);
	strcat(consulta, "'AND Usuario.Contrasena='");
	strcat(consulta, contrasena);
	strcat(consulta,"'");
	
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se ha identificado correctamente.\n");
		return 0;
	}
	else{
		printf("Se ha identificado correctamente.\n");
		char update[500];
		sprintf(update,"UPDATE Usuario SET Nombre='eliminado' WHERE Nombre='%s'",usuario);
		int err=mysql_query (conn, update);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		return 1;
	}
	//				sprintf(respuesta, "%s%s", respuesta, "\n");
	
}
void consultaganador (char respuesta[100]){
	//consulta los usuarios con más partidas ganadas y los retorna en un vector introducido como input
	//separados por barras
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta,"SELECT Usuario.Nombre  FROM (Usuario) WHERE Usuario.Ganadas=(Select MAX(Usuario.Ganadas) FROM (Usuario)) AND Usuario.Nombre !='eliminado'");


	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);	
	row = mysql_fetch_row (resultado);	
	if (row == NULL)
		sprintf (respuesta, "3/No se han encontrado ganadores.");
	else{
		
		strcpy(respuesta, "3/");
		sprintf(respuesta, "%s%s",respuesta, row[0]);
		row = mysql_fetch_row (resultado);	
		while (row !=NULL){
			sprintf(respuesta, "%s, %s", respuesta, row[0]);
			row = mysql_fetch_row (resultado);
		}
	}
	printf("Respuesta codigo 3 %s\n", respuesta);
}
void consultamail(char nombre[20],char respuesta[100]){
	//consulta el mail del usuario introducido como input en el char nombre y devuelve en el char respuesta
	//el mail o un mensaje pertinente si no encuentra el usuario
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT  Usuario.Mail FROM (Usuario) WHERE Usuario.Nombre='");
	strcat(consulta,nombre);
	strcat(consulta,"'");
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row==NULL)
		sprintf (respuesta, "4/No hay registro de este usuario.");
	else{
		strcpy(respuesta, "4/");
		sprintf(respuesta, "%s%s",respuesta, row[0]);
	}
	
	
	//sprintf(respuesta, "%s %s", respuesta, "\n");
	printf ("Respuesta: %s\n", respuesta);
	
}
void consultapartidas(char nombre[20], char respuesta[100]){
	//consulta cuantas partidas ha ganado el usuario introducido como input
	//y lo devuelve en el char respuesta o el mensaje pertinente si no encuentra el usuario
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta,"SELECT Usuario.Ganadas FROM(Usuario) WHERE Usuario.Nombre='");
	strcat(consulta,nombre);
	strcat(consulta,"'");
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row==NULL)
		sprintf (respuesta, "5/No hay registro de tus partidas.");
	else{
		strcpy(respuesta, "5/");
		sprintf(respuesta, "%s%s",respuesta, row[0]);
	}
	
	printf ("Respuesta: %s\n", respuesta);
	
}
int anadirpregunta(char consulta[200]){
	//añade una nueva pregunta a la base de datos, como input entra la consulta ya preparada
	//y retorna un 1 si se hace correctamente y un 0 en caso contrario
	int resp=1;
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		resp=0;
		exit (1);
	}
	return resp;
}
void buscarpregunta(char respuesta[500],char consulta[200],char consulta1[200]){
	//primero con consulta1 averigua el número de preguntas de la categoria correspondiente,
	//obtiene un número aleatorio de como máximo el número obtenido y hace la consulta otra
	//para obtener las preguntas y selecciona la del número aleatorio. devuelve categoria/pregunta
	//,resp1,resp2,resp3,resp4/correcta
	int err=mysql_query (conn, consulta1);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		
		exit (1);
	}
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	int numero=atoi(row[0]);
	printf("num:%d\n",numero);
	int aleatorio=rand() % numero;
	printf("aleatorio:%d\n",aleatorio);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
	
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int i =0;
	while (i<aleatorio){
		row = mysql_fetch_row (resultado);
		i=i+1;
	}
	sprintf(respuesta,"%s%s/%s,%s,%s,%s,%s/%s/",respuesta,row[0],row[1],row[2],row[3],row[4],row[5],row[6]);
	
}
int FinalizarPartida(char ganador[20], char usuarios[200] ){
	//como input recibe el ganador de la partida y los usuarios que han jugado. consulta cuantas partidas hay 
	//en la base y añade con el id=número de partidas ya en la base, la nueva partida ya finalizada
	//Al ganador le suma 1 en sus partidas ganadas. Devuelve 2 si todo correcto y 1 si hay errores
	int resp=2;
	char consulta5[500];
	strcpy(consulta5,"SELECT COUNT(*) FROM (Partidas)" );
	int err=mysql_query (conn, consulta5);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		
		exit (1);
	}
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int partida=atoi( row[0]);
	
	char consulta6[300];
	
	strcpy(consulta6, "INSERT INTO Partidas VALUES ("); //Creamos la consulta de insertar nueva partida
	strcat(consulta6,row[0]);
	strcat(consulta6, ",'");
	strcat(consulta6, ganador);
	strcat(consulta6,"')");
	err=mysql_query (conn, consulta6);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		
		exit (1);
		resp=1;;
	}
	char consulta30[200];
	sprintf(consulta30,"UPDATE Usuario SET Ganadas=Ganadas+1 WHERE Nombre='%s'",ganador);
	err=mysql_query (conn, consulta30);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		
		exit (1);
		resp=1;;
	}
	return resp;
	
	
}

		
void *AtenderCliente(void *socket){
	int sock_conn;
	int *s;
	s=(int*) socket;
	sock_conn=*s;
	char peticion[512];
	char peticion1[512];
	
	char respuesta[512];
	int ret;
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		//printf("libre5=%d\n",partidas[1].libre);
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		char miUsuario[20];
		peticion[ret]='\0';
		printf ("Peticion: %s\n",peticion);
		strcpy(peticion1,peticion);
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		printf("codigo:%d\n",codigo);
		char usuario[20];
		
		// Ya tenemos el codigo de la petici?n
		if (codigo ==0) {//peticion de desconexi?n
			terminar=1;
			pthread_mutex_lock( &mutex );
			int res=Eliminar(&miLista, usuario);
			pthread_mutex_unlock( &mutex );
			//Notificar los clientes conectados
			char notificacion1[100];
			strcpy(notificacion1, "6/");
			DameConectados(&miLista,notificacion1);
			printf ("Notificación: %s\n", notificacion1);
			//Enviamos respuesta
			int j;
			for (j=0; j<i; j++)
				write (sockets[j],notificacion1, strlen(notificacion1));
			
			if (res==-1){
				printf("No eliminado de la lista correctamente");
			}
			else if(res==0){
				printf("Eliminado correctamente");
			}
			
		}
		//Codigo 1: peticion de log in.
		if (codigo ==1)
		{
			
			p = strtok( NULL, "/");
			
			strcpy (usuario, p);
			
			p = strtok( NULL, "/");
			char contrasena[20];
			strcpy (contrasena, p);
			int res=log_in(usuario, contrasena);
			char respuesta1[100];
			if (res==0){
				strcpy(respuesta1, "1/NO");
				
			}
			else if (res==1){
				strcpy(respuesta1, "1/SI");
				strcpy(miUsuario,usuario);
			}
			printf ("Respuesta: %s\n", respuesta1);
			
			if (res==1){
				pthread_mutex_lock( &mutex );
				
				int res1=Pon(&miLista, usuario,sock_conn);
				pthread_mutex_unlock( &mutex );
				//Notificar los clientes conectados
				char notificacion[100];
				strcpy(notificacion, "6/");
				DameConectados(&miLista,notificacion);
				printf ("Notificación: %s\n", notificacion);
				//Enviamos respuesta
				int j;
				for (j=0; j<miLista.num; j++)
					write (miLista.conectados[j].socket,notificacion, strlen(notificacion));
				if(res1==0)
					printf("Añadido correctamente");
				else if(res1==-1)
					printf("No añadido correctamente");
				
			}
			// Enviamos respuesta
			write (sock_conn,respuesta1, strlen(respuesta1));
		}
		if (codigo==2){
			//registro de nuevo usuario
			
			p=strtok( NULL, "/");
			char usuario1[20];
			strcpy (usuario1, p);
			p=strtok( NULL, "/");
			char contrasena1[20];
			strcpy (contrasena1, p);
			p=strtok( NULL, "/");
			char mail[100];
			char resultado[200];
			char respuesta2[100];
			strcpy (mail, p);
			int res=registro(usuario1, contrasena1,mail);
			printf("%d\n", res);
			if (res==0){
				strcpy(respuesta2, "2/NO");
			}
			else if (res==1){
				strcpy(respuesta2,"2/SI");
			}
			//sprintf(respuesta, "%s %s", respuesta, "\n");
			printf ("Respuesta: %s\n", respuesta2);
			
			
			// Enviamos respuesta
			write (sock_conn,respuesta2, strlen(respuesta2));
		}
			
		if (codigo==3){
			//consulta quien ha ganado más partidas
			
			char respuesta3[100];
			consultaganador(respuesta3);
			//sprintf(respuesta3, "%s %s", respuesta3, "\n");
			printf ("Respuesta: %s\n", respuesta3);
			
			// Enviamos respuesta
			write (sock_conn,respuesta3, strlen(respuesta3));
		
		}
		if (codigo ==4){
			//consulta mail de un usuario recibido
			
			p=strtok(NULL, "/");
			char nombre[20];
			strcpy(nombre, p);
			
			char respuesta4[100];
			consultamail(nombre,respuesta4);
			printf ("Respuesta: %s\n", respuesta4);
			// Enviamos respuesta
			write (sock_conn,respuesta4, strlen(respuesta4));
		}
		if (codigo==5){
			//consulta partidas ganadas por un usuario
			p=strtok(NULL, "/");
			char nombre[20];
			strcpy(nombre,p);
			
			char respuesta5[100];
			consultapartidas(nombre, respuesta5);
			printf ("Respuesta: %s\n", respuesta5);
			// Enviamos respuesta
			write (sock_conn,respuesta5, strlen(respuesta5));
		}
		if (codigo==7){
			//alguien ha empezado una partida, hay que crearla y informar a los invitados
			printf("Ha entrado en codigo 7\n");
			
			char sockets[20];
			printf("%s\n",peticion);
			printf("%s\n",peticion1);
			pthread_mutex_lock( &mutex );
			int posicion=PonPartida(partidas,&miLista,peticion1,sockets);//crear en la tabla
			pthread_mutex_unlock( &mutex );
			printf("%s\n",peticion);
			printf("%s\n",peticion1);
			printf("posicion:%d\n",posicion);
			if (posicion==-1){
				printf("Tabla llena\n");
			}
			else{
				char invitacion [50];
				strcpy(invitacion, "8/");
				
				sprintf(invitacion,"%s%s/%d",invitacion,miUsuario,posicion);
				printf("Invitación:%s\n",invitacion);
				int c=0;
				char *p;
				p=strtok(sockets,"/");
				printf("socket:%s\n",p);
				//se envia la invitacion a todos los invitados
				while(p !=NULL){
					printf("socket:%s\n",p);
					write(atoi(p),invitacion,strlen(invitacion));
					p=strtok(NULL,"/");
				}

			}
		}
		if (codigo==9){
			//se recibe la respuesta de un invitado
			char resp[5];
			
			p=strtok(NULL,"/");
			strcpy(resp,p);
			int respuesta;//almacenamos la respuesta 0-no, 1-si
			if (strcmp(resp,"NO")==0){
				respuesta=0;
			}
			else
				respuesta=1;
			p=strtok(NULL,"/");
			int partida=atoi(p);
			if (respuesta==0){//se pone en la tabla que ha contestado uno más pero se pone a 0 aceptada
				//porque alguien ha rechazado
				pthread_mutex_lock( &mutex );
				partidas[partida].aceptada=0;
				partidas[partida].contador=partidas[partida].contador+1;
				printf("contador no:%d\n",partidas[partida].contador);
				pthread_mutex_unlock( &mutex );
			}
			else{
			//no ha rechazado, actualizamos el número de personas que ha contestado
				pthread_mutex_lock( &mutex );
				partidas[partida].contador=partidas[partida].contador+1;
				printf("contador si:%d\n",partidas[partida].contador);
				pthread_mutex_unlock( &mutex );
				
			}
			printf("numero:%d\n",partidas[partida].numero);
			if ((partidas[partida].numero==partidas[partida].contador+1)&&(partidas[partida].aceptada==1)){
				char mensaje[50];//en este mensaje informaremos a todos los jugadores de que empieza la partida porque han aceptado
				strcpy(mensaje,"10/SI/");
				char m[20];
				char users1[100];
				strcpy(users1,partidas[partida].usuarios);
				char users2[100];
				strcpy(users2,partidas[partida].usuarios);
				
				sprintf(mensaje,"%s%d/%s",mensaje,partida,users1);
				p=strtok(users1,"/");
				printf("p:%s\n",p);
				char primerturno[20];
				strcpy(primerturno,"11/");				
				char primerjugador[20];				
				strcpy(primerjugador,p);
				sprintf(primerturno,"%s%s",primerturno,primerjugador);
				
				
				while(p!=NULL){
					strcpy(m,p);
					int socket=DameSocket(&miLista,m);
					
					printf("socket;%d\n",socket);
					write(socket,mensaje,strlen(mensaje));
					printf("m10 enviado\n");
					p=strtok(NULL,"/");
					printf("p:%s\n",p);

				}
				printf("%s\n",partidas[partida].usuarios);
				
/*				char *t=strtok(users2,"/");*/
/*				printf("t:%s\n",t);*/
/*				while(t!=NULL){*/
/*					strcpy(m,t);*/
/*					int socket=DameSocket(&miLista,m);*/
/*					printf("socket;%d\n",socket);*/
/*					write(socket,primerturno,strlen(primerturno));*/
/*					printf("m11 enviado\n");*/
/*					t=strtok(NULL,"/");*/
/*					printf("t:%s\n",t);*/
					
/*				}*/
				

			}
			if((partidas[partida].numero==partidas[partida].contador+1)&&(partidas[partida].aceptada==0)){
				char mensaje[50];//en este mensaje informaremos a todos los jugadores de que no empieza la partida porque no han aceptado
				strcpy(mensaje,"10/NO/");
				pthread_mutex_lock( &mutex );
				partidas[partida].libre=0;
				pthread_mutex_unlock( &mutex );
				char m[20];
				sprintf(mensaje,"%s%d",mensaje,partida);
				p=strtok(partidas[partida].usuarios,"/");
				while(p!=NULL){
					strcpy(m,p);
					int socket=DameSocket(&miLista,m);
					write(socket,mensaje,strlen(mensaje));
					p=strtok(NULL,"/");

				}
			}
		}
		if (codigo==12){
			//se recibe movimiento de dado, se envia a todos los participantes de la partida
			//que un usuario ha de moverse X posiciones
			p=strtok(NULL,"/");
			int partida=atoi(p);
			p=strtok(NULL,"/");
			char miUsuario[20];
			strcpy(miUsuario, p);
			char dado[10];
			p=strtok(NULL,"/");
			strcpy(dado,p);
			char mensaje[50];
			sprintf(mensaje,"14/%s/%s",miUsuario, dado);
			char m[100];
			char users1[100];
			strcpy(users1,partidas[partida].usuarios);
			p=strtok(users1,"/");
			while(p!=NULL){
				strcpy(m,p);
				int socket=DameSocket(&miLista,m);
				write(socket,mensaje,strlen(mensaje));
				p=strtok(NULL,"/");
			}
			
			
			


		}
		
		if (codigo==13){//Recibimos el mensaje de que un usuario ha fallado, enviamos mensaje con el nuevo turno
			
			p=strtok(NULL, "/");
			int partida=atoi(p);
			char turno_anterior[20];
			p=strtok(NULL, "/");
			strcpy(turno_anterior,p);
			char users1[100];
			strcpy(users1,partidas[partida].usuarios);
			char *u=strtok(users1,"/");
			int encontrado=0;
			int contador=0;
			while(!encontrado){
				char user[20];
				strcpy(user,u);
				if(strcmp(turno_anterior,user)==0){
					encontrado=1;
					
				}
				else{
					u=strtok(NULL,"/");
				}
				contador++;
			}
			char turno[20];
			if (contador==partidas[partida].numero){
				char users2[100];
				strcpy(users2,partidas[partida].usuarios);
				char *v=strtok(users2,"/");
				
				strcpy(turno,v);
			}
			else{
				u=strtok(NULL,"/");
				strcpy(turno,u);
			}
			char mensaje[50];
			sprintf(mensaje,"11/%s",turno);
			char m[20];
			printf("%s\n",mensaje);
			strcpy(users1,partidas[partida].usuarios);
			p=strtok(users1,"/");
			while(p!=NULL){
				strcpy(m,p);
				int socket=DameSocket(&miLista,m);
				write(socket,mensaje,strlen(mensaje));
				p=strtok(NULL,"/");
				
				
			}
		}
		if(codigo==15){
			//mensaje que recibe el servidor cuando un usuario ha ganado una nueva fruta
			//se lo reenvia a todos los usuarios de la partida
			p=strtok(NULL,"/");
			int partida=atoi(p);
			char m[100];
			char users1[100];
			strcpy(users1,partidas[partida].usuarios);
			p=strtok(users1,"/");
			while(p!=NULL){
				strcpy(m,p);
				int socket=DameSocket(&miLista,m);
				write(socket,peticion1,strlen(peticion1));
				p=strtok(NULL,"/");
			}
		}
		if (codigo == 16){
			//mensaje que informa que ha finalizado la partida y contiene tambien el ganador
			//dejamos libre la partida en la tabla y la añadimos a la base de datos
			p=strtok(NULL,"/");
			int partida=atoi(p);
			p=strtok(NULL,"/");
			char ganador[20];
			strcpy(ganador,p);
			char users2[100];
			pthread_mutex_lock( &mutex );
			partidas[partida].libre=0;
			pthread_mutex_unlock( &mutex );
			strcpy(users2,partidas[partida].usuarios);
			pthread_mutex_lock( &mutex );
			int res=FinalizarPartida(ganador,users2);
			pthread_mutex_unlock( &mutex );
			if (res==2){
				printf("Partida añadida\n");
			}
			
		}
		if(codigo==17){
			//mensaje de chat, se reenvia a todos los usuarios de la partida para que lo pongan en el chat
			p=strtok(NULL,"/");
			int partida=atoi(p);
			char m[100];
			char users1[100];
			strcpy(users1,partidas[partida].usuarios);
			p=strtok(users1,"/");
			while(p!=NULL){
				strcpy(m,p);
				int socket=DameSocket(&miLista,m);
				write(socket,peticion1,strlen(peticion1));
				p=strtok(NULL,"/");
			}

		}
		if(codigo==18){
			//un usuario desea introducir una nueva pregunta, la peticion lleva toda la información de
			//esa pregunta
			
			char consulta[200];
			p=strtok(NULL,"/");
			char con[200];
			strcpy(con,p);
			strcpy(consulta, "INSERT INTO Preguntas VALUES ("); //Creamos la consulta de insertar nueva pregunta
			strcat(consulta,con );
			strcat(consulta,")");
			int resp=anadirpregunta(consulta);
			if (resp==0){
				printf("Error al añadir pregunta\n");
			}
			else{
				printf("Pregunta añadida correctamente.\n");
			}
		}
		if (codigo==19){
			//un usuario envia esta petición cuando necesita una pregunta, 
			//se utilizará la función buscarpregunta con dos consultas para enviarle a ese usuario la pregunta
			char usuario[20];
			char cat[2];
			p=strtok(NULL,"/");
			strcpy(usuario,p);
			p=strtok(NULL,"/");
			strcpy(cat,p);
			char consulta1[200];
			sprintf(consulta1,"SELECT * FROM (Preguntas) WHERE Preguntas.categoria=%s",cat);
			char consulta2[200];
			sprintf(consulta2,"SELECT COUNT(*) FROM (Preguntas) WHERE Preguntas.categoria=%s",cat); 
			char respuesta19[500];
			sprintf(respuesta19,"20/%s/",usuario);
			buscarpregunta(respuesta19,consulta1,consulta2);
			printf("%s\n",respuesta19);
			int socket=DameSocket(&miLista,usuario);
			write(socket,respuesta19,strlen(respuesta19));
			
		}
		if (codigo==20){
			//un usuario desea eliminarse, se le devolverà un mensaje informando de si se ha realizado correctamente
			p = strtok( NULL, "/");
			char nombre20[20];
			strcpy (nombre20, p);
			
			p = strtok( NULL, "/");
			char contrasena20[20];
			strcpy (contrasena20, p);
			int res=eliminar(nombre20, contrasena20);
			char respuesta20[200];
			if (res==0){
				strcpy(respuesta20,"21/Error al eliminar usuario, los datos introducidos no coinciden");
			}
			if (res==1){
				strcpy(respuesta20,"21/Usuario eliminado correctamente");
			}
			write(sock_conn,respuesta20,strlen(respuesta20));
		}
	 
	}
	close(sock_conn);
}

	

int main(int argc, char **argv)
	{
		
		//Creamos una conexion al servidor MYSQL 
		
		abrirbd();//abrimos la base de datos
		
		int sock_conn, sock_listen;
		int puerto=50069;
		//int puerto=9070;
		miLista.num=0;
		int a =0;
		
		while (a<100) {
			partidas[a].libre=0;
			partidas[a].aceptada=1;
			partidas[a].contador=0;
			
			a++;
		}
		
		
		
		
		struct sockaddr_in serv_adr;
		
		// INICIALITZACIONS
		// Obrim el socket
		if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
			printf("Error creant socket");
		
		memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
		serv_adr.sin_family = AF_INET;
		
		// asocia el socket a cualquiera de las IP de la m?quina. 
		//htonl formatea el numero que recibe al formato necesario
		serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
		// establecemos el puerto de escucha
		serv_adr.sin_port = htons(puerto);
		if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
			printf ("Error al bind");
		if (listen(sock_listen, 3) < 0)
			printf("Error en el Listen");
		
		
		pthread_t thread[100];
		// Bucle infinito
		for (i=0;i<100;i++){
			printf ("Escuchando\n");
			
			sock_conn = accept(sock_listen, NULL, NULL);
			printf ("He recibido conexion\n");
			sockets[i]=sock_conn;
			//sock_conn es el socket que usaremos para este cliente
			
			// Crear thead y decirle lo que tiene que hacer
			
			pthread_create (&thread[i], NULL, AtenderCliente,&sockets[i]);
			
			
		}
}
		
			
			//sock_conn es el socke
