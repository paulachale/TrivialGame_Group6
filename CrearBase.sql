DROP DATABASE IF EXISTS T6_Trivial;
CREATE DATABASE T6_Trivial;
USE T6_Trivial;

CREATE TABLE Usuario (
  Id int PRIMARY KEY NOT NULL,
  Nombre VARCHAR(20) NOT NULL,
  Contrasena VARCHAR(30) NOT NULL,
  Mail TEXT NOT NULL
)ENGINE = InnoDB;

CREATE TABLE Partidas (
  Id int PRIMARY KEY NOT NULL,
  FechaFin VARCHAR(20) NOT NULL,
  HoraFin VARCHAR(20) NOT NULL,
  Duracion int NOT NULL,
  Ganador VARCHAR(30) NOT NULL
)ENGINE = InnoDB;

CREATE TABLE Participaciones (
  id_P integer NOT NULL,
  foreign key (id_P) references Partidas(Id),
  id_U integer NOT NULL,
  foreign key (id_U) references Usuario(Id),
  puntos integer NOT NULL

)ENGINE = InnoDB;



INSERT INTO Usuario VALUES(1,'Maria','123456', 'mariaubiergo@gmail.com');
INSERT INTO Usuario VALUES(2,'Jaume','654321', 'jaumegarcia@gmail.com');
INSERT INTO Usuario VALUES(3,'Minerva','hijabella', 'minervaubiergo@gmail.com');
INSERT INTO Usuario VALUES(4,'Cayetano','hijobello', 'cayetanoubiergo@gmail.com');
INSERT INTO Usuario VALUES(5,'Paula','hello', 'paulasopenacoello@gmail.com');

INSERT INTO Partidas VALUES(1, '041021', '1000', 8, 'Maria');
INSERT INTO Partidas VALUES(2,'221021', '1600', 9, 'Jaume');
INSERT INTO Partidas VALUES(3,'211021', '1500', 11, 'Minerva');
INSERT INTO Partidas VALUES(4,'191021', '1700', 12, 'Cayetano');
INSERT INTO Partidas VALUES(5,'181021', '1900', 15, 'Paula');


INSERT INTO Participaciones VALUES(1,1,12);
INSERT INTO Participaciones VALUES(2,2,11);
INSERT INTO Participaciones VALUES(3,3,9);
INSERT INTO Participaciones VALUES(4,4,8);
INSERT INTO Participaciones VALUES(5,5,5);
INSERT INTO Participaciones VALUES(1,2,10);
INSERT INTO Participaciones VALUES(2,3,9);
INSERT INTO Participaciones VALUES(3,4,7);
INSERT INTO Participaciones VALUES(4,5,7);
INSERT INTO Participaciones VALUES(5,1,4);












