USE [DentplexDB]
GO
/****** Object:  Table [dbo].[ProductGroups]    Script Date: 12/2/2017 10:23:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductGroups](
	[ProductGroupID] [int] IDENTITY(1,1) NOT NULL,
	[ProductParentGroupID] [int] NULL,
	[ProductGroupTitle] [nvarchar](150) NOT NULL,
	[ProductGroupName] [varchar](150) NULL,
	[ProductGroupImage] [varchar](250) NULL,
	[ProductGroupBanner] [varchar](250) NULL,
 CONSTRAINT [PK_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[ProductGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/2/2017 10:23:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductGroupID] [int] NOT NULL,
	[ProductSubGroupID] [int] NOT NULL,
	[ProductTitle] [nvarchar](250) NOT NULL,
	[ProductName] [varchar](250) NULL,
	[ProductShortText] [nvarchar](350) NOT NULL,
	[ProductText] [nvarchar](max) NULL,
	[ProductImage] [varchar](100) NULL,
	[ProductIsFavourite] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SliderItems]    Script Date: 12/2/2017 10:23:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SliderItems](
	[SlideItemID] [int] IDENTITY(1,1) NOT NULL,
	[SlideID] [int] NOT NULL,
	[SlideItemTitle] [nvarchar](150) NOT NULL,
	[SlideItemImage] [varchar](250) NULL,
	[SlideItemLink] [varchar](150) NULL,
	[SlideItemOrder] [int] NOT NULL,
 CONSTRAINT [PK_SliderItems] PRIMARY KEY CLUSTERED 
(
	[SlideItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sliders]    Script Date: 12/2/2017 10:23:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sliders](
	[SlideID] [int] IDENTITY(1,1) NOT NULL,
	[SlideTitle] [nvarchar](250) NOT NULL,
	[SlideImage] [varchar](150) NULL,
	[SlideIsActive] [bit] NOT NULL,
	[SlideDesc] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sliders] PRIMARY KEY CLUSTERED 
(
	[SlideID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/2/2017 10:23:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[UserPassword] [nvarchar](350) NOT NULL,
	[UserEmail] [nvarchar](250) NOT NULL,
	[UserIsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ProductGroups] ON 

INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (1, NULL, N'چشم ها', NULL, N'48ed5976-8ad1-4b13-ae65-aa1d2d4f07d5.jpg', N'76f5954a-ecfe-4daa-9ddf-0d829b34d606.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (2, NULL, N'لب ها', NULL, N'7fddd9a5-b02c-46a4-bc58-49b2e7236879.jpg', N'c373f2b2-43fb-423f-a39b-2b06ad613fc4.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (3, NULL, N'صورت', NULL, N'b293ee38-ce1e-4d88-b0d5-59b17671987e.jpg', N'939810b6-e33f-49e0-abf0-be6bdc4dbff1.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (4, NULL, N'مراقبت ناخن', NULL, N'9b445808-ebcd-4317-8e21-434fb871ca74.jpg', N'2b75e024-84dd-4637-8fb2-c72476c854b3.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (5, NULL, N'زیبایی', NULL, N'85bab3f5-9793-462b-b1f9-81ebc8325dab.jpg', N'2adb8c77-2c78-43a1-bbb7-74d91d990667.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (7, 1, N'خط چشم', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (8, 2, N'رژ لب', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (9, 3, N'پن کیک', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (10, 4, N'ناخن زشت', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (11, 5, N'زیبایی صورت', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (13, 2, N'سایه لب', NULL, NULL, NULL)
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (14, NULL, N'ناخن', NULL, N'ce5805cc-a8f4-4152-9b0b-e328a2c9cab9.jpg', N'd143c69b-889c-4dc1-abf8-18dcb8caa262.jpg')
INSERT [dbo].[ProductGroups] ([ProductGroupID], [ProductParentGroupID], [ProductGroupTitle], [ProductGroupName], [ProductGroupImage], [ProductGroupBanner]) VALUES (15, 14, N'ناخن خشکل', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProductGroups] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductGroupID], [ProductSubGroupID], [ProductTitle], [ProductName], [ProductShortText], [ProductText], [ProductImage], [ProductIsFavourite]) VALUES (1, 3, 9, N'پن کیک اصل ایتالیایی', NULL, N'این محصول به درد نمی خوره نخرید...', N'این محصول به درد نمی خوره نخرید...', N'e8e24ff2-5b40-43d9-b31e-3b18c874c547.jpg', 1)
INSERT [dbo].[Products] ([ProductID], [ProductGroupID], [ProductSubGroupID], [ProductTitle], [ProductName], [ProductShortText], [ProductText], [ProductImage], [ProductIsFavourite]) VALUES (2, 4, 10, N'خط چشم ایتالیایی', NULL, N'سیب', NULL, N'f6f5c8d2-c449-40f3-9261-b7b10a6da019.jpg', 1)
INSERT [dbo].[Products] ([ProductID], [ProductGroupID], [ProductSubGroupID], [ProductTitle], [ProductName], [ProductShortText], [ProductText], [ProductImage], [ProductIsFavourite]) VALUES (3, 2, 8, N'رژ لب بنفش افریقایی', NULL, N'بخرید خیلی خوبه ...', NULL, N'e7724a28-d08d-4f3f-a0a8-660fb7e35cd5.jpg', 1)
INSERT [dbo].[Products] ([ProductID], [ProductGroupID], [ProductSubGroupID], [ProductTitle], [ProductName], [ProductShortText], [ProductText], [ProductImage], [ProductIsFavourite]) VALUES (4, 1, 7, N'خط چشم ایتالیایی', NULL, N'سیبسیب', NULL, N'e9fa9feb-cb38-405e-a4c5-036a10a5629d.jpg', 1)
INSERT [dbo].[Products] ([ProductID], [ProductGroupID], [ProductSubGroupID], [ProductTitle], [ProductName], [ProductShortText], [ProductText], [ProductImage], [ProductIsFavourite]) VALUES (6, 14, 15, N'ناخن یک', NULL, N'بسیب', N'سیب', N'eade68c1-1f79-4b22-a7e4-b919a2aa866f.jpg', 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[SliderItems] ON 

INSERT [dbo].[SliderItems] ([SlideItemID], [SlideID], [SlideItemTitle], [SlideItemImage], [SlideItemLink], [SlideItemOrder]) VALUES (13, 1, N'اسلاید1', N'd1871a4ae0cc48b2a2b529181bb3b5b5.jpg', N'http://google.com', 1)
INSERT [dbo].[SliderItems] ([SlideItemID], [SlideID], [SlideItemTitle], [SlideItemImage], [SlideItemLink], [SlideItemOrder]) VALUES (14, 1, N'اسلاید2', N'3e31570e35c4485d84ee4766928d5c25.jpg', N'http://google.com', 2)
INSERT [dbo].[SliderItems] ([SlideItemID], [SlideID], [SlideItemTitle], [SlideItemImage], [SlideItemLink], [SlideItemOrder]) VALUES (15, 1, N'اسلاید3', N'6ae87363d5b14bc283cbe0fd8a4bb96c.jpg', N'http://google.com', 3)
SET IDENTITY_INSERT [dbo].[SliderItems] OFF
SET IDENTITY_INSERT [dbo].[Sliders] ON 

INSERT [dbo].[Sliders] ([SlideID], [SlideTitle], [SlideImage], [SlideIsActive], [SlideDesc]) VALUES (1, N'اسلاید تاپ صفحه اصلی', N'f3f21de59ef64be3a761323d10f83433.jpg', 1, NULL)
SET IDENTITY_INSERT [dbo].[Sliders] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserName], [UserPassword], [UserEmail], [UserIsActive]) VALUES (1, N'Admin', N'123', N'a@a.com', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[ProductGroups]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroups_ProductGroups] FOREIGN KEY([ProductParentGroupID])
REFERENCES [dbo].[ProductGroups] ([ProductGroupID])
GO
ALTER TABLE [dbo].[ProductGroups] CHECK CONSTRAINT [FK_ProductGroups_ProductGroups]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductGroups] FOREIGN KEY([ProductGroupID])
REFERENCES [dbo].[ProductGroups] ([ProductGroupID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductGroups]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductGroups1] FOREIGN KEY([ProductSubGroupID])
REFERENCES [dbo].[ProductGroups] ([ProductGroupID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductGroups1]
GO
ALTER TABLE [dbo].[SliderItems]  WITH CHECK ADD  CONSTRAINT [FK_SliderItems_Sliders] FOREIGN KEY([SlideID])
REFERENCES [dbo].[Sliders] ([SlideID])
GO
ALTER TABLE [dbo].[SliderItems] CHECK CONSTRAINT [FK_SliderItems_Sliders]
GO
