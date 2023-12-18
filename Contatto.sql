USE [MyContacts]
GO

/****** Object:  Table [dbo].[Contatto]    Script Date: 18/12/2023 10:47:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contatto](
	[Nome] [varchar](150) NOT NULL,
	[Cognome] [varchar](150) NOT NULL,
	[Sesso] [varchar](1) NOT NULL,
	[DataDiNascita] [date] NULL,
	[Telefono] [varchar](50) NULL,
	[Città] [varchar](150) NULL,
	[Mail] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Contatto] PRIMARY KEY CLUSTERED 
(
	[Mail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


