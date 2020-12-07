USE [MIBASEDEDATOS]
GO

PRINT 'INCIANDO EL SCRIPT....'

PRINT 'CREANDO TABLAS...';


CREATE TABLE [dbo].[ApiConfiguraciones](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Uri] [nvarchar](200) NULL,
	[Nombre] [nvarchar](200) NULL,
	[Token] [nvarchar](200) NULL,
	[Usuario] [nvarchar](200) NULL,
	[Clave] [nvarchar](200) NULL,
	[DirectorioVirtual] [nvarchar](200) NULL,
	[Visual] [bit] NOT NULL,
	[RutaToken] [nvarchar](200) NULL,
	[TipoSeguridad] [int] NOT NULL,
	[EncabezadoToken] [nvarchar](200) NULL,
	[EncabezadoUsuario] [nvarchar](200) NULL,
	[EncabezadoClave] [nvarchar](200) NULL,
	[Prefijo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Api_Configuraciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApiConfiguraciones_Rutas]    Script Date: 28/02/2019 11:46:01 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiConfiguraciones_Rutas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdApi] [bigint] NOT NULL,
	[Ruta] [nvarchar](250) NULL,
	[Descripcion] [nvarchar](500) NULL,
	[Identificador] [nvarchar](250) NULL,
 CONSTRAINT [PK_ApiConfiguraciones_Rutas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT 'LA TABLAS SE CREARON CORRECTAMENTE.';

PRINT 'CREANDO PROCEDIMIENTO...';
GO
CREATE PROCEDURE [dbo].[pa_Api_Informacion]
	@pidentificador nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ac.*, ar.Ruta, ar.Identificador from ApiConfiguraciones ac join ApiConfiguraciones_Rutas ar on  ac.Id = ar.IdApi where ar.Identificador = @pidentificador
END
GO


PRINT 'EL PROCEDIMIENTO SE CREO CORRECTAMENTE.';

PRINT 'FINALIZANDO SCRIPT...'