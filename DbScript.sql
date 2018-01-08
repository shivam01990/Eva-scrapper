USE [EvaScrapper]
GO
/****** Object:  Table [dbo].[tblscrapper]    Script Date: 1/8/2018 7:54:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblscrapper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[SourceUrl] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[CompanyUrl] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Houses] [nvarchar](max) NULL,
	[Prices] [nvarchar](max) NULL,
	[PagingURL] [nvarchar](max) NULL,
	[DetailsPageUrl] [nvarchar](max) NULL,
	[IsDetailsPageScrapped] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_tblscrapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblscrapper] ADD  CONSTRAINT [DF_tblscrapper_IsDetailsPageScrapped]  DEFAULT ((0)) FOR [IsDetailsPageScrapped]
GO
ALTER TABLE [dbo].[tblscrapper] ADD  CONSTRAINT [DF_tblscrapper_Createdon]  DEFAULT (getdate()) FOR [CreatedOn]
GO
