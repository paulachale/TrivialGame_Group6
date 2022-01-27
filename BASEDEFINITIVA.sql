DROP DATABASE IF EXISTS T6_Trivial;
CREATE DATABASE T6_Trivial;
USE T6_Trivial;

CREATE TABLE Usuario (
  Id int PRIMARY KEY NOT NULL,
  Nombre VARCHAR(20) NOT NULL,
  Contrasena VARCHAR(30) NOT NULL,
  Mail TEXT NOT NULL,
  Ganadas int NOT NULL,
  Cat1 int NOT NULL,
  Cat2 int NOT NULL,
  Cat3 int NOT NULL,
  Cat4 int NOT NULL,
  Cat5 int NOT NULL,
  Cat6 int NOT NULL
)ENGINE = InnoDB;

CREATE TABLE Partidas (
  Id int PRIMARY KEY NOT NULL,
  Ganador VARCHAR(30) NOT NULL
)ENGINE = InnoDB;

CREATE TABLE Participaciones (
  id_P integer NOT NULL,
  foreign key (id_P) references Partidas(Id),
  id_U integer NOT NULL,
  foreign key (id_U) references Usuario(Id)
)ENGINE = InnoDB;

CREATE TABLE Preguntas (
  categoria INTEGER NOT NULL,
  pregunta VARCHAR(100) NOT NULL,
  respuesta1 VARCHAR(100) NOT NULL,
  respuesta2 VARCHAR(100) NOT NULL,
  respuesta3 VARCHAR(100) NOT NULL,
  respuesta4 VARCHAR(100) NOT NULL,
  correcta INTEGER NOT NULL
)ENGINE = InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;





INSERT INTO Usuario VALUES(1,'Maria','123456', 'mariaubiergo@gmail.com',0,0,0,0,0,0,0);
INSERT INTO Usuario VALUES(2,'Jaume','654321', 'jaumegarcia@gmail.com',0,0,0,0,0,0,0);
INSERT INTO Usuario VALUES(3,'Minerva','hijabella', 'minervaubiergo@gmail.com',0,0,0,0,0,0,0);
INSERT INTO Usuario VALUES(4,'Cayetano','hijobello', 'cayetanoubiergo@gmail.com',0,0,0,0,0,0,0);
INSERT INTO Usuario VALUES(5,'Paula','hello', 'paulasopenacoello@gmail.com',0,0,0,0,0,0,0);

INSERT INTO Partidas VALUES(1,'Maria');
INSERT INTO Partidas VALUES(2,'Jaume');
INSERT INTO Partidas VALUES(3,'Minerva');
INSERT INTO Partidas VALUES(4,'Cayetano');
INSERT INTO Partidas VALUES(5,'Paula');




INSERT INTO Preguntas VALUES(1,'¿Cuál fue el primer país en aprobar el sufragio femenino?', '1.Nueva Zelanda', '2.España', '3.Italia','4.Suiza',1);
INSERT INTO Preguntas VALUES(1,'¿En qué año llegó el hombre a la Luna?', '1.1966', '2.1969', '3.1920','4.1970',2);
INSERT INTO Preguntas VALUES(1,'¿Qué importante batalla tuvo lugar en 1815?', '1.Batalla del Ebro', '2.Batalla de Trafalgar', '3.Batalla de Waterloo','4.Batalla de Bailén',3);
INSERT INTO Preguntas VALUES(1,'¿Cuál era la ciudad hogar de Marco Polo?', '1.Venecia', '2.Roma', '3.Florencia','4.Pisa',1);
INSERT INTO Preguntas VALUES(1,'¿En qué año tuvo lugar el genocidio de Ruanda?', '1.1994', '2.1995', '3.1990','4.1991',1);
INSERT INTO Preguntas VALUES(1,'¿En qué ciudad se entrevistaron Hitler y Franco?', '1.Madrid', '2.Berlín', '3.Hendaya','4.Barcelona',3);
INSERT INTO Preguntas VALUES(1,'¿Cuál era la ciudad hogar de Marco Polo?', '1.Venecia', '2.Roma', '3.Florencia','4.Pisa',1);

INSERT INTO Preguntas VALUES(2,'¿Cuál es la capital de Filipinas?', '1.Guadalajara', '2.Manila', '3.Ciudad de Filipinas','4.Buenos Aires',2);
INSERT INTO Preguntas VALUES(2,'¿Cuál es el rio más caudaloso del mundo?', '1.Ebro', '2.Nilo', '3.Amazonas','4.Mississipi',3);
INSERT INTO Preguntas VALUES(2,'¿En qué país se encuentra el rio Po?', '1.Francia', '2.Alemania', '3.Austria','4.Italia',4);
INSERT INTO Preguntas VALUES(2,'¿Cuanto mide la montaña "El monte Eibrús"(en metros)?', '1.5462', '2.4651', '3.6789','4.3412',1);
INSERT INTO Preguntas VALUES(2,'¿A qué país pertenece la isla de Creta?', '1.Eslovaquia', '2.Croacia', '3.Noruega','4.Grecia',4);
INSERT INTO Preguntas VALUES(2,'¿Cuál es el país más visitado del mundo?', '1.Francia', '2.España', '3.EEUU','4.China',1);
INSERT INTO Preguntas VALUES(2,'¿Por cuántos estados está formado EEUU?', '1.56', '2.53', '3.52','4.50',4);

INSERT INTO Preguntas VALUES(3,'¿Quién pintó el Guernica?', '1.Pablo Picasso', '2.Salvador Dalí', '3.Claude Monet','4.Leonardo da Vinci',1);
INSERT INTO Preguntas VALUES(3,'¿Qué tipo de instrumento es una cítara?', '1.Viento', '2.Cuerda', '3.Percusión','4.No es un instrumento',2);
INSERT INTO Preguntas VALUES(3,'¿Qué filósofo creó "El mito de la caverna"?', '1.Descartes', '2.Nietzsche', '3.Platón','4.Sócrates',3);
INSERT INTO Preguntas VALUES(3,'¿Cómo se llama el caballo de Don Quijote de la Mancha?', '1.Rocinante', '2.Rocío', '3.Ron','4.Rostrante',1);
INSERT INTO Preguntas VALUES(3,'¿Quién escribió "Sueño de una noche de verano?', '1.Virginia Woolf', '2.E.M. Hemingway', '3.Charles Dickens','4.William Shakespeare',4);
INSERT INTO Preguntas VALUES(3,'¿Qué tiene en Segovia 128 arcos?', '1.La catedral de Segovia', '2.El acueducto de Segovia', '3.Iglesia de Segovia','4.El puente principal de Segovia',2);
INSERT INTO Preguntas VALUES(3,'¿Qué poeta escribió el poema “Oda a Afrodita”?', '1.Safo de Mitilene', '2.Pablo Neruda', '3.Federico García Lorca','4.Mario Benedetti',1);

INSERT INTO Preguntas VALUES(4,'¿En qué mes el Sol está más cerca de la Tierra?', '1.Enero', '2.Diciembre', '3.Mayo','4.Septiembre',2);
INSERT INTO Preguntas VALUES(4,'¿En qué lado del cuerpo está el hígado?', '1.Tenemos dos hígados', '2.Izquierdo', '3.Derecho','4.En el medio',3);
INSERT INTO Preguntas VALUES(4,'¿Cuántos elementos tiene la tabla periódica?', '1.117', '2.120', '3.119','4.118',4);
INSERT INTO Preguntas VALUES(4,'¿Qué gas de la atmósfera nos protege de la radiación ultravioleta?', '1.Nitrógeno', '2.Oxígeno', '3.Ozono','4.Gas agua',3);
INSERT INTO Preguntas VALUES(4,'¿Cuál es el sentido que se desarrolla primero?', '1.Olfato', '2.Tacto','3.Gusto','4.Oído',1);
INSERT INTO Preguntas VALUES(4,'¿Cuál es el único mes que puede tener menos de 4 fases lunares?', '1.Enero', '2.Febrero', '3.Marzo','4.Abril',2);
INSERT INTO Preguntas VALUES(4,'¿Qué edad tiene la Tierra (en millones de años)?', '1.4543', '2.5543', '3.3543','4.6543',1);

INSERT INTO Preguntas VALUES(5,'¿Cuál fue la primera película de Disney', '1.Blancanieves', '2.Cenicienta', '3.Mickey Mouse: La Película','4.La bella durmiente',1);
INSERT INTO Preguntas VALUES(5,'Cómo se llama el pero de Tintín', '1.Malo', '2.Malten', '3.Milú','4.Balú',3);
INSERT INTO Preguntas VALUES(5,'¿Qué actriz protagonizó "Desayuno con diamantes?', '1.Patricia Neal', '2.Audrey Hepburn', '3.Beverly Powers','4.Janet Banzet',2);
INSERT INTO Preguntas VALUES(5,'¿Qué año se emitió el primer programa de OT?', '1.2001', '2.2000', '3.2002','4.1999',1);
INSERT INTO Preguntas VALUES(5,'¿Cómo se llamaba el cantante de Queen?', '1.Roger Taylor', '2.John Deacon', '3.Brian May','4.Freddie Mercury',4);
INSERT INTO Preguntas VALUES(5,'¿Qué película hizo famoso al director James Cameron?', '1.Desayuno con diamantes', '2.Titanic', '3.Mary Poppins','4.Avengers. Endgame',2);
INSERT INTO Preguntas VALUES(5,'¿Cuántos álbumes de estudio publicó Michael Jackson en vida?', '1.16', '2.18', '3.11','4.7',3);

INSERT INTO Preguntas VALUES(6,'¿Qué color no pertenece a los aros olímpicos?', '1.Rojo', '2.Amarillo', '3.Blanco','4.Negro',3);
INSERT INTO Preguntas VALUES(6,'¿En qué deporte destacó Carl Lewis?', '1.Atletismo', '2.Natación', '3.Fútbol','4.Tenis',1);
INSERT INTO Preguntas VALUES(6,'¿Dónde se inventó el ping-pong?', '1.China', '2.Japón', '3.España','4.Inglaterra',4);
INSERT INTO Preguntas VALUES(6,'¿Qué color no pertenece a los aros olímpicos?', '1.Rojo', '2.Amarillo', '3.Blanco','4.Negro',3);
INSERT INTO Preguntas VALUES(6,'¿Cuántos jugadores hay en un equipo de voleibol?', '1.8', '2.6', '3.7','4.4',2);
INSERT INTO Preguntas VALUES(6,'¿En qué ciudad española está el estadio de fútbol de Mestalla?', '1.Sevilla', '2.Madrid', '3.Barcelona','4.Valencia',4);
INSERT INTO Preguntas VALUES(6,'¿Cuántos rounds hay en un combate de boxeo olímpico?', '1.Tres', '2.Cinco', '3.Uno','4.Cuatro',1);













