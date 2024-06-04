CREATE DATABASE [Crud]
GO

USE [Crud]
GO

CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[FechaDeNacimiento] [datetime] NOT NULL,
	[Cuit] [varchar](11) NOT NULL,
	[Domicilio] [varchar](100) NOT NULL,
	[Celular] [varchar](15) NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Clientes] ([Id], [Nombre], [Apellido], [FechaDeNacimiento], [Cuit], [Domicilio], [Celular], [Email]) VALUES (1, N'Zair', N'Mar', CAST(N'2000-05-24T00:00:00.000' AS DateTime), N'51217738', N'Estrada 2651', N'1168697111', N'zair-m@hotmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombre], [Apellido], [FechaDeNacimiento], [Cuit], [Domicilio], [Celular], [Email]) VALUES (2, N'Romina', N'Quinteros', CAST(N'1997-03-24T00:00:00.000' AS DateTime), N'40255583', N'Estrada 2651', N'1152428841', N'romina5@hotmail.com')
GO
INSERT [dbo].[Clientes] ([Id], [Nombre], [Apellido], [FechaDeNacimiento], [Cuit], [Domicilio], [Celular], [Email]) VALUES (3, N'Titi', N'Mar', CAST(N'2010-03-24T00:00:00.000' AS DateTime), N'12345678910', N'Estrada 2651', N'1152428841', N'romina5@hotmail.com')
GO

