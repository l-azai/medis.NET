USE [medis]
GO
/****** Object:  Table [dbo].[VideoCategories]    Script Date: 04/11/2015 19:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[UrlName] [nvarchar](max) NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedByUserId] [int] NOT NULL,
	[DateUpdated] [datetime] NULL,
	[UpdatedByUserId] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[DeletedByUserId] [int] NULL,
 CONSTRAINT [PK_dbo.VideoCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VideoImages]    Script Date: 04/11/2015 19:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VideoImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VideoId] [int] NOT NULL,
	[Content] [varbinary](max) NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedByUserId] [int] NOT NULL,
	[DateUpdated] [datetime] NULL,
	[UpdatedByUserId] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[DeletedByUserId] [int] NULL,
 CONSTRAINT [PK_dbo.VideoImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 04/11/2015 19:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[YearReleased] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ImageId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedByUserId] [int] NOT NULL,
	[DateUpdated] [datetime] NULL,
	[UpdatedByUserId] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[DeletedByUserId] [int] NULL,
 CONSTRAINT [PK_dbo.Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[VideoCategories] ON 

INSERT [dbo].[VideoCategories] ([Id], [Name], [UrlName], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (1, N'Animations', N'animations', CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[VideoCategories] ([Id], [Name], [UrlName], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (2, N'Asian Movies', N'asian-movies', CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[VideoCategories] ([Id], [Name], [UrlName], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (3, N'Asian TV Series', N'asian-tv-series', CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[VideoCategories] ([Id], [Name], [UrlName], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (4, N'TV Series', N'tv-series', CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[VideoCategories] ([Id], [Name], [UrlName], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (5, N'Movies', N'movies', CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[VideoCategories] OFF
SET IDENTITY_INSERT [dbo].[VideoImages] ON 

INSERT [dbo].[VideoImages] ([Id], [VideoId], [Content], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (1, 1, NULL, CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[VideoImages] OFF
SET IDENTITY_INSERT [dbo].[Videos] ON 

INSERT [dbo].[Videos] ([Id], [Name], [YearReleased], [CategoryId], [ImageId], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (1, N'Despicable Me', 2010, 1, 1, CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Videos] ([Id], [Name], [YearReleased], [CategoryId], [ImageId], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (2, N'Brave', 2012, 1, 1, CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Videos] ([Id], [Name], [YearReleased], [CategoryId], [ImageId], [DateCreated], [CreatedByUserId], [DateUpdated], [UpdatedByUserId], [DateDeleted], [DeletedByUserId]) VALUES (3, N'Finding Nemo', 2003, 1, 1, CAST(0x0000A54300000000 AS DateTime), 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Videos] OFF
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Videos_dbo.VideoCategories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[VideoCategories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_dbo.Videos_dbo.VideoCategories_CategoryId]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Videos_dbo.VideoImages_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[VideoImages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_dbo.Videos_dbo.VideoImages_ImageId]
GO
