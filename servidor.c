
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
TablaPartidas partidas;
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
	sprintf(conectados, "%s%d/",conectados,lista->num);
	int i;
	for(i=0;i<lista->num;i++){
		sprintf(conectados,"%s%s/",conectados,lista->conectados[i].nombre);
	}
}
void abrirbd(){
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexiï¿³n: %u %s\n", 
			mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
//inicializar la conexin
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T6_Trivial",0, NULL, 0);
	//conn = mysql_real_connect (conn, "localhost","root", "mysql", "Trivial",0, NULL, 0);

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
int PonPartida(TablaPartidas *partidas,ListaConectados *miLista,char invitados[50], char sockets[20] ){
	int b=0;
	printf("peticion:%s\n",invitados);
	int posicion=-1;
	int encontrado=0;
	while ((b<100)&&(!encontrado)){
		if(partidas[b]->libre==0){
			encontrado=1;
			printf("Encontrado un hueco libre en la tabla\n");
			int numero=1;//el que crea la partida ya lo añadimos al número de participantes
			posicion=b;
			char usuarios[50];
			partidas[posicion]->libre=1;
			char *p;
			p=strtok(invitados, "/"); //eliminamos el código 7
			p=strtok(NULL,"/");
			
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
			strcpy(partidas[posicion]->usuarios,usuarios);
			partidas[posicion]->numero=numero;

		}
		b++;
	}
	printf("La posición en la tabla es %d\n",posicion);
	return posicion;
}






int log_in(char usuario[20], char contrasena[20] ){
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
		return 1;
	}
	//				sprintf(respuesta, "%s%s", respuesta, "\n");
	
}
int registro(char usuario[20], char contrasena[20], char mail[50]){
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
		strcpy(consultaID, "SELECT Usuario.Nombre  FROM (Usuario)");
		
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
		char idtext[5];
		
		char consulta2[200];
		sprintf(idtext, "%d", n);
		strcpy(consulta2, "INSERT INTO Usuario VALUES ("); //Creamos la consulta de insertar nuevo usuario
		strcat(consulta2, idtext);
		strcat(consulta2, ",'");
		strcat(consulta2, usuario);
		strcat(consulta2,"','");
		strcat(consulta2,contrasena);
		strcat(consulta2,"','");
		strcat(consulta2,mail);
		strcat(consulta2,"')");
		
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
void consultafecha (char fecha[6], char respuesta[100]){
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT distinct Usuario.Nombre FROM (Usuario, Partidas, Participaciones) WHERE Partidas.FechaFin='");
	strcat(consulta, fecha);
	strcat(consulta, "' AND Partidas.Id=Participaciones.id_P AND Participaciones.id_U=Usuario.Id");
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);	
	row = mysql_fetch_row (resultado);	
	if (row == NULL)
		sprintf (respuesta, "3/No se han encontrado usuarios que jugaran en esta fecha.");
	else{
		
		strcpy(respuesta, "3/");
		sprintf(respuesta, "%s%s",respuesta, row[0]);
		row = mysql_fetch_row (resultado);	
		while (row !=NULL){
			sprintf(respuesta, "%s, %s", respuesta, row[0]);
			row = mysql_fetch_row (resultado);
		}
	}
}
void consultamail(char respuesta[100]){
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta, "SELECT  Usuario.Mail FROM (Usuario, Partidas, Participaciones) WHERE Participaciones.Puntos=(SELECT MAX(Participaciones.Puntos) FROM (Participaciones)) AND Participaciones.id_U=Usuario.Id");
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	strcpy(respuesta, "4/");
	sprintf(respuesta, "%s%s",respuesta, row[0]);
	
	//sprintf(respuesta, "%s %s", respuesta, "\n");
	printf ("Respuesta: %s\n", respuesta);
	
}
void consultapuntos(char puntos[4], char respuesta[100]){
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	strcpy(consulta,"SELECT distinct Usuario.Nombre FROM(Usuario, Partidas, Participaciones) WHERE Participaciones.Puntos>");
	strcat(consulta,puntos);
	strcat(consulta," AND Participaciones.id_U=Usuario.Id AND Participaciones.id_P=Partidas.Id AND Usuario.Nombre=Partidas.Ganador");
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row==NULL)
		sprintf (respuesta, "5/Nadie ha ganado con más de %s puntos",puntos);
	else{
		strcpy(respuesta, "5/");
		sprintf(respuesta, "%s%s",respuesta, row[0]);
		row = mysql_fetch_row (resultado);
		while (row != NULL){
			sprintf(respuesta, "%s, %s", respuesta, row[0]);
			row = mysql_fetch_row (resultado);
		}
	}
	
	printf ("Respuesta: %s\n", respuesta);
	
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
		
		// Ya tenemos el c?digo de la petici?n
		if (codigo ==0) {//petici?n de desconexi?n
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
		//C\ufff3digo 1: petici\ufff3n de log in.
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
			
			
			p=strtok(NULL, "/");
			char fecha[6];
			strcpy(fecha, p);
			char respuesta3[100];
			consultafecha(fecha, respuesta3);
			//sprintf(respuesta3, "%s %s", respuesta3, "\n");
			printf ("Respuesta: %s\n", respuesta3);
			
			// Enviamos respuesta
			write (sock_conn,respuesta3, strlen(respuesta3));
		
		}
		if (codigo ==4){
			
			
			char respuesta4[100];
			consultamail(respuesta4);
			printf ("Respuesta: %s\n", respuesta4);
			// Enviamos respuesta
			write (sock_conn,respuesta4, strlen(respuesta4));
		}
		if (codigo==5){
			p=strtok(NULL, "/");
			char puntos[4];
			strcpy(puntos,p);
			
			char respuesta5[100];
			consultapuntos(puntos, respuesta5);
			printf ("Respuesta: %s\n", respuesta5);
			// Enviamos respuesta
			write (sock_conn,respuesta5, strlen(respuesta5));
		}
		if (codigo==7){
			printf("Ha entrado en codigo 7\n");
			
			char sockets[20];
			pthread_mutex_lock( &mutex );
			int posicion=PonPartida(&partidas,&miLista,peticion1,sockets);
			pthread_mutex_unlock( &mutex );
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
				while(p !=NULL){
					printf("socket:%s\n",p);
					write(atoi(p),invitacion,strlen(invitacion));
					p=strtok(NULL,"/");
				}

			}
		}
		if (codigo==9){
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
			if (respuesta==0){
				pthread_mutex_lock( &mutex );
				partidas[partida].aceptada=0;
				partidas[partida].contador=partidas[partida].contador+1;
				printf("contador no:%d\n",partidas[partida].contador);
				pthread_mutex_unlock( &mutex );
			}
			else{
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
				sprintf(mensaje,"%s%d",mensaje,partida);
				p=strtok(partidas[partida].usuarios,"/");
				while(p!=NULL){
					strcpy(m,p);
					int socket=DameSocket(&miLista,m);
					write(socket,mensaje,strlen(mensaje));
					p=strtok(NULL,"/");

				}
				

			}
			if((partidas[partida].numero==partidas[partida].contador+1)&&(partidas[partida].aceptada==0)){
				char mensaje[50];//en este mensaje informaremos a todos los jugadores de que no empieza la partida porque no han aceptado
				strcpy(mensaje,"10/NO/");
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
			
	 
	}
	close(sock_conn);
}

	

int main(int argc, char **argv)
	{
		
		//Creamos una conexion al servidor MYSQL 
		
		abrirbd();
		
		int sock_conn, sock_listen;
		int puerto=50069;
		//int puerto=9050;
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
