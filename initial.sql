-- CREACION DE LA DB
if not exists(select * from sys.databases where name = 'PW3TP_20181C_Tareas')
BEGIN
	CREATE DATABASE [PW3TP_20181C_Tareas]
END
GO

USE [PW3TP_20181C_Tareas]
GO
ALTER TABLE [dbo].[Tarea] DROP CONSTRAINT [FK_Tareas_Usuarios]
GO
ALTER TABLE [dbo].[ComentarioTarea] DROP CONSTRAINT [FK_Comentario_Tarea]
GO
ALTER TABLE [dbo].[Carpeta] DROP CONSTRAINT [FK_Carpeta_Usuario]
GO
ALTER TABLE [dbo].[ArchivoTarea] DROP CONSTRAINT [FK_ArchivoTarea_Tarea]
GO
ALTER TABLE [dbo].[Tarea] DROP CONSTRAINT [DF_Tarea_FechaCreacion]
GO
ALTER TABLE [dbo].[ComentarioTarea] DROP CONSTRAINT [DF_ComentarioTarea_FechaCreacion]
GO
ALTER TABLE [dbo].[Carpeta] DROP CONSTRAINT [DF_Carpeta_FechaCreacion]
GO
ALTER TABLE [dbo].[ArchivoTarea] DROP CONSTRAINT [DF_ArchivoTarea_FechaCreacion]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 4/11/2018 2:08:37 PM ******/
DROP TABLE [dbo].[Usuario]
GO
/****** Object:  Table [dbo].[Tarea]    Script Date: 4/11/2018 2:08:37 PM ******/
DROP TABLE [dbo].[Tarea]
GO
/****** Object:  Table [dbo].[ComentarioTarea]    Script Date: 4/11/2018 2:08:37 PM ******/
DROP TABLE [dbo].[ComentarioTarea]
GO
/****** Object:  Table [dbo].[Carpeta]    Script Date: 4/11/2018 2:08:37 PM ******/
DROP TABLE [dbo].[Carpeta]
GO
/****** Object:  Table [dbo].[ArchivoTarea]    Script Date: 4/11/2018 2:08:37 PM ******/
DROP TABLE [dbo].[ArchivoTarea]
GO
/****** Object:  Table [dbo].[ArchivoTarea]    Script Date: 4/11/2018 2:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivoTarea](
	[IdArchivoTarea] [int] IDENTITY(1,1) NOT NULL,
	[RutaArchivo] [nvarchar](max) NOT NULL,
	[IdTarea] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_ArchivoTarea] PRIMARY KEY CLUSTERED 
(
	[IdArchivoTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Carpeta]    Script Date: 4/11/2018 2:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carpeta](
	[IdCarpeta] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Carpetas] PRIMARY KEY CLUSTERED 
(
	[IdCarpeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComentarioTarea]    Script Date: 4/11/2018 2:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComentarioTarea](
	[IdComentarioTarea] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[IdTarea] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Comentario] PRIMARY KEY CLUSTERED 
(
	[IdComentarioTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tarea]    Script Date: 4/11/2018 2:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarea](
	[IdTarea] [int] IDENTITY(1,1) NOT NULL,
	[IdCarpeta] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NULL,
	[EstimadoHoras] [decimal](18,2) NULL,
	[FechaFin] [datetime] NULL,
	[Prioridad] [smallint] NOT NULL,
	[Completada] [smallint] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[IdTarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 4/11/2018 2:08:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Contrasenia] [nvarchar](50) NOT NULL,
	[Activo] [smallint] NOT NULL,
	[FechaRegistracion] [datetime] NOT NULL,
	[FechaActivacion] [datetime] NULL,
	[CodigoActivacion] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ArchivoTarea] ADD  CONSTRAINT [DF_ArchivoTarea_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Carpeta] ADD  CONSTRAINT [DF_Carpeta_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[ComentarioTarea] ADD  CONSTRAINT [DF_ComentarioTarea_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Tarea] ADD  CONSTRAINT [DF_Tarea_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[ArchivoTarea]  WITH CHECK ADD  CONSTRAINT [FK_ArchivoTarea_Tarea] FOREIGN KEY([IdTarea])
REFERENCES [dbo].[Tarea] ([IdTarea])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivoTarea] CHECK CONSTRAINT [FK_ArchivoTarea_Tarea]
GO
ALTER TABLE [dbo].[Carpeta]  WITH CHECK ADD  CONSTRAINT [FK_Carpeta_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carpeta] CHECK CONSTRAINT [FK_Carpeta_Usuario]
GO
ALTER TABLE [dbo].[ComentarioTarea]  WITH CHECK ADD  CONSTRAINT [FK_Comentario_Tarea] FOREIGN KEY([IdTarea])
REFERENCES [dbo].[Tarea] ([IdTarea])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ComentarioTarea] CHECK CONSTRAINT [FK_Comentario_Tarea]
GO
ALTER TABLE [dbo].[Tarea]  WITH CHECK ADD  CONSTRAINT [FK_Tareas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tarea] CHECK CONSTRAINT [FK_Tareas_Usuarios]
GO


select * from Carpeta;
-- USUARIOS
INSERT INTO Usuario (Nombre,Apellido,Email,Contrasenia,Activo,FechaRegistracion,FechaActivacion,CodigoActivacion) VALUES ('Carlos','Romero','cperez@test.com','asd123',1,'2018-01-01 00:00:00.000','2018-01-01 00:00:00.000','4AE52B1C-C3E2-4AB1-8EFD-859FCB87F5B6');
INSERT INTO Usuario (Nombre,Apellido,Email,Contrasenia,Activo,FechaRegistracion,FechaActivacion,CodigoActivacion) VALUES ('Pedro','Gaudio','pguadio@test.com','asd123',1,'2018-01-01 00:00:00.000','2018-01-01 00:00:00.000','4AE52B1C-C3E2-4AB1-8EFD-859FCB87F5B6');
INSERT INTO Usuario (Nombre,Apellido,Email,Contrasenia,Activo,FechaRegistracion,FechaActivacion,CodigoActivacion) VALUES ('Pedro','Guemes','pguemes@test.com','asd123',0,'2018-01-01 00:00:00.000','2018-01-01 00:00:00.000','4AE52B1C-C3E2-4AB1-8EFD-859FCB87F5B6');
-- CARPETAS
INSERT INTO Carpeta (IdUsuario,Nombre,Descripcion,FechaCreacion) VALUES (1,'Carpeta uno', 'Descripcion Carpeta uno', '2018-01-01 00:00:00.000' );
INSERT INTO Carpeta (IdUsuario,Nombre,Descripcion,FechaCreacion) VALUES (2,'Carpeta dos', 'Descripcion Carpeta dos', '2018-01-01 00:00:00.000' );
INSERT INTO Carpeta (IdUsuario,Nombre,Descripcion,FechaCreacion) VALUES (3,'Carpeta tres', 'Descripcion Carpeta tres', '2018-01-01 00:00:00.000' );
-- TAREAS
INSERT INTO Tarea (IdCarpeta, IdUsuario, Nombre, Descripcion,EstimadoHoras, FechaFin, Prioridad, Completada, FechaCreacion) VALUES ( 1, 1, 'Tarea 1', 'Descripción de Tarea 1', 10.50, '2018-01-01 00:00:00.000', '1', '0', '2018-01-01 00:00:00.000');
INSERT INTO Tarea (IdCarpeta, IdUsuario, Nombre, Descripcion,EstimadoHoras, FechaFin, Prioridad, Completada, FechaCreacion) VALUES ( 2, 1, 'Tarea 2', 'Descripción de Tarea 2', 11, '2019-01-01 00:00:00.000', '1', '0', '2018-01-01 00:00:00.000');
INSERT INTO Tarea (IdCarpeta, IdUsuario, Nombre, Descripcion,EstimadoHoras, FechaFin, Prioridad, Completada, FechaCreacion) VALUES ( 2, 2, 'Tarea 3', 'Descripción de Tarea 3', 3.50, '2018-05-10 00:00:00.000', '1', '0', '2018-01-01 00:00:00.000');
INSERT INTO Tarea (IdCarpeta, IdUsuario, Nombre, Descripcion,EstimadoHoras, FechaFin, Prioridad, Completada, FechaCreacion) VALUES ( 3, 3, 'Tarea 4', 'Descripción de Tarea 4', 11.50, '2020-01-01 00:00:00.000', '1', '0', '2018-01-01 00:00:00.000');
INSERT INTO Tarea (IdCarpeta, IdUsuario, Nombre, Descripcion,EstimadoHoras, FechaFin, Prioridad, Completada, FechaCreacion) VALUES ( 3, 3, 'Tarea 5', 'Descripción de Tarea 5', 5.52, '2018-01-01 00:00:00.000', '1', '0', '2018-01-01 00:00:00.000');
-- COMENTARIO TAREAS
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 1', 1, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 2', 1, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 3', 1, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 4', 2, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 5', 2, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 6', 2, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 7', 3, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 8', 3, '2018-01-01 00:00:00.000');
INSERT INTO ComentarioTarea (Texto, IdTarea, FechaCreacion) VALUES ('ComentarioTarea 9', 3, '2018-01-01 00:00:00.000');