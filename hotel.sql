USE [HotelManagement]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 3/7/2023 8:42:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [nvarchar](50) NULL,
	[Avartar] [nvarchar](4000) NULL,
	[Phone] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[isEmployee] [bit] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookRoom]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CouponId] [nvarchar](50) NULL,
	[AccountId] [int] NULL,
	[StarTime] [date] NULL,
	[EndTime] [date] NULL,
	[CMND] [decimal](18, 0) NULL,
	[Status] [nvarchar](50) NULL,
	[total] [decimal](18, 0) NULL,
 CONSTRAINT [PK_BookRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[Id] [nvarchar](50) NOT NULL,
	[Discount] [int] NULL,
	[Description] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] NOT NULL,
	[RoomTypeId] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[amount] [decimal](18, 0) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomInBooking]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomInBooking](
	[BookRoomId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_RoomInBooking] PRIMARY KEY CLUSTERED 
(
	[BookRoomId] ASC,
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](4000) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] NOT NULL,
	[ServiceTypeId] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[InStock] [int] NULL,
	[amount] [decimal](18, 0) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceInRoom]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInRoom](
	[BookRoomId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_ServiceInRoom] PRIMARY KEY CLUSTERED 
(
	[BookRoomId] ASC,
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 3/7/2023 8:42:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[Id] [int] NOT NULL,
	[Title] [nchar](10) NULL,
	[Description] [nchar](10) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (2, N'12@gmail.com', N'1', N'11515151', N'book2.webp', N'1', 0, 1, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (3, N'khoa@gmail.com', N'123456', N'1515151', N'book2.webp', N'0231231234', 1, 0, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (17, N'tiensidien1234@gmail.com', N'1', N'Hokirita', N'khoa.jpg', N'0918293187', 1, 1, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11113, N'viet1@gmail.com', N'1234567', N'Đặng Hoàng Việt', N'b0067ade5e832d2aefec8ee9bda50fdc.gif', N'0938231511', 1, 0, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11117, N'ad@gmail.com', N'123456', N'12315415151', N'khoa.jpg', N'1154165161', 1, 0, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11118, N'thuthao1251@gmail.com', N'123456', N'1111111111111', N'khoa.jpg', N'1231515111', 1, 0, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11119, N'tiensidien12351514@gmail.com', N'123456', N'123456', N'khoa.jpg', N'1123415161', 1, 1, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11121, N'ngu@gmail.com', N'123456', N'123125151', N'khoa.jpg', N'1231516511', 1, 0, 0)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11122, N'ngu1@gmail.com', N'123456', N'anh khoa', N'khoa.jpg', N'1111231414', 1, 1, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11124, N'nguqua1@gmail.com', N'1234567', N'khoa ko ngu nha', N'123.jpg', N'0919283198', 1, 1, 0)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11126, N'tiensidien1@gmail.com', N'123456', N'Duy Đan', N'avatar .webp', N'', 1, 0, 1)
INSERT [dbo].[Account] ([Id], [Email], [Password], [Fullname], [Avartar], [Phone], [Gender], [isEmployee], [Active]) VALUES (11127, N'test@gmail.com', N'123456', N'testtest', N'avatar .webp', N'', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[BookRoom] ON 

INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (1, NULL, 2, CAST(N'2022-06-29' AS Date), CAST(N'2022-06-30' AS Date), CAST(192837419 AS Decimal(18, 0)), N'FINISH', CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (2, NULL, 3, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-06' AS Date), CAST(192837419 AS Decimal(18, 0)), N'BOOKED', CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (15, NULL, 3, CAST(N'2022-07-06' AS Date), CAST(N'2022-07-07' AS Date), CAST(123415161 AS Decimal(18, 0)), N'BOOKED', CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (16, NULL, 3, CAST(N'2022-07-07' AS Date), CAST(N'2022-07-10' AS Date), CAST(123415161 AS Decimal(18, 0)), N'CHECKIN', CAST(500 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (19, NULL, 11113, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-06' AS Date), CAST(124151515151 AS Decimal(18, 0)), N'CANCEL', CAST(410 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (20, NULL, 11113, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-06' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CANCEL', CAST(830 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (21, NULL, 11113, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-06' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'BOOKED', CAST(710 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (22, N'AK15Z', 11113, CAST(N'0001-01-01' AS Date), CAST(N'0001-01-01' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'BOOKED', CAST(82 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (23, N'AS10W', 11113, CAST(N'2022-07-05' AS Date), CAST(N'2022-07-06' AS Date), CAST(124151515151 AS Decimal(18, 0)), N'BOOKED', CAST(287 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (24, NULL, 11113, CAST(N'2022-07-08' AS Date), CAST(N'2022-07-10' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CHECKIN', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (25, NULL, 11113, CAST(N'2022-07-09' AS Date), CAST(N'2022-07-15' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (26, NULL, 11113, CAST(N'2022-07-07' AS Date), CAST(N'2022-07-08' AS Date), CAST(123141414 AS Decimal(18, 0)), N'BOOKED', CAST(151 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (27, NULL, 11113, CAST(N'2022-07-08' AS Date), CAST(N'2022-07-09' AS Date), CAST(123141414 AS Decimal(18, 0)), N'BOOKED', NULL)
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (29, NULL, 11113, CAST(N'2022-07-11' AS Date), CAST(N'2022-07-12' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CHECKOUT', CAST(270 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (30, NULL, 11113, CAST(N'2022-07-13' AS Date), CAST(N'2022-07-16' AS Date), CAST(124151515151 AS Decimal(18, 0)), N'CANCEL', CAST(630 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (31, NULL, 11113, CAST(N'2022-07-11' AS Date), CAST(N'2022-07-12' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CHECKIN', CAST(260 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (32, NULL, 11113, CAST(N'2022-07-14' AS Date), CAST(N'2022-07-17' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CANCEL', CAST(630 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (33, NULL, 11113, CAST(N'2022-07-16' AS Date), CAST(N'2022-07-18' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CANCEL', CAST(420 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (34, NULL, 11113, CAST(N'2022-07-11' AS Date), CAST(N'2022-07-12' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(300 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (35, NULL, 11113, CAST(N'2022-07-11' AS Date), CAST(N'2022-07-12' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(300 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (36, NULL, 17, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(2432 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (37, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(280 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (38, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(622 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (39, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(410 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (40, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (41, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (42, NULL, 11113, CAST(N'2022-07-12' AS Date), CAST(N'2022-07-13' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(390 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (43, NULL, 11113, CAST(N'2022-07-13' AS Date), CAST(N'2022-07-14' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'BOOKED', CAST(410 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (44, NULL, 11113, CAST(N'2022-07-13' AS Date), CAST(N'2022-07-14' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CHECKOUT', CAST(702 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (45, N'AK15Z', 11113, CAST(N'2022-07-23' AS Date), CAST(N'2022-07-28' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CANCEL', CAST(1640 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (46, NULL, 11113, CAST(N'2022-07-18' AS Date), CAST(N'2022-07-19' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(1210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (47, NULL, 11113, CAST(N'2022-07-18' AS Date), CAST(N'2022-07-19' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'BOOKED', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (48, N'AK15Z', 11126, CAST(N'2022-07-21' AS Date), CAST(N'2022-07-22' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CANCEL', CAST(328 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (49, NULL, 11126, CAST(N'2022-07-20' AS Date), CAST(N'2022-07-21' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(410 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (50, NULL, 11126, CAST(N'2022-07-20' AS Date), CAST(N'2022-07-21' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'FINISH', CAST(47130 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (51, NULL, 11126, CAST(N'2022-07-20' AS Date), CAST(N'2022-07-21' AS Date), CAST(10151616161 AS Decimal(18, 0)), N'CHECKOUT', CAST(2600 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (52, NULL, 2, CAST(N'2022-10-24' AS Date), CAST(N'2022-10-25' AS Date), CAST(12312312323 AS Decimal(18, 0)), N'BOOKED', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (53, NULL, 3, CAST(N'2022-11-03' AS Date), CAST(N'2022-11-04' AS Date), CAST(9876525374 AS Decimal(18, 0)), N'FINISH', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (54, NULL, 3, CAST(N'2022-11-03' AS Date), CAST(N'2022-11-04' AS Date), CAST(9876525374 AS Decimal(18, 0)), N'CHECKIN', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (55, NULL, 3, CAST(N'2022-11-06' AS Date), CAST(N'2022-11-07' AS Date), CAST(324234324234 AS Decimal(18, 0)), N'BOOKED', CAST(210 AS Decimal(18, 0)))
INSERT [dbo].[BookRoom] ([Id], [CouponId], [AccountId], [StarTime], [EndTime], [CMND], [Status], [total]) VALUES (56, NULL, 2, CAST(N'2023-03-07' AS Date), CAST(N'2023-03-08' AS Date), CAST(23232323232 AS Decimal(18, 0)), N'FINISH', CAST(210 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[BookRoom] OFF
GO
INSERT [dbo].[Coupon] ([Id], [Discount], [Description]) VALUES (N'AK15Z', 20, N'ngay le tinh nhan')
INSERT [dbo].[Coupon] ([Id], [Discount], [Description]) VALUES (N'AS10W', 30, N'uu dai 30/4')
GO
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (1, 1, N'101', N'2d5aae751bcbe7f55af98a3944bc8c66.jpg', N'a', CAST(210 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (2, 2, N'102', N'download (1).jpg', N'a', CAST(200 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (3, 3, N'103', N'download (2).jpg', N'adad', CAST(1241 AS Decimal(18, 0)), 0)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (4, 4, N'201', N'download (3).jpg', N'1', CAST(300 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (5, 3, N'202', N'download (4).jpg', N'1', CAST(120 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (6, 3, N'203', N'img_phong_khach.jpg', N'2', CAST(280 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (7, 2, N'301', N'download.jpg', N'1', CAST(320 AS Decimal(18, 0)), 1)
INSERT [dbo].[Room] ([Id], [RoomTypeId], [Title], [Image], [Description], [amount], [Active]) VALUES (8, 2, N'302', N'img_van_phong.jpg', N'ada1a141', CAST(302 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (2, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (2, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (2, 3)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (15, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (16, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (16, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (19, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (20, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (21, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (21, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (21, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (22, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (23, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (24, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (24, 6)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (25, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (25, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (27, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (27, 5)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (29, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (30, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (31, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (32, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (33, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (34, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (35, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (36, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (36, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (36, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (37, 6)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (38, 7)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (38, 8)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (39, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (39, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (40, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (41, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (41, 5)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (42, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (43, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (43, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (44, 5)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (44, 6)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (44, 8)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (45, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (45, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (46, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (47, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (48, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (48, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (49, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (49, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (50, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (51, 2)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (51, 4)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (52, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (53, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (54, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (55, 1)
INSERT [dbo].[RoomInBooking] ([BookRoomId], [RoomId]) VALUES (56, 1)
GO
INSERT [dbo].[RoomType] ([Id], [Title], [Description], [Active]) VALUES (1, N'king', N'2 phòng 1 giường', 1)
INSERT [dbo].[RoomType] ([Id], [Title], [Description], [Active]) VALUES (2, N'queen', N'2 giường 1 toilet', 1)
INSERT [dbo].[RoomType] ([Id], [Title], [Description], [Active]) VALUES (3, N'normal', N'2 giường 1 toilet', 1)
INSERT [dbo].[RoomType] ([Id], [Title], [Description], [Active]) VALUES (4, N'standard', N'2 giường 1 toilet', 1)
GO
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (1, 1, N'sting', N'sting.jpg', 0, CAST(21 AS Decimal(18, 0)), 1)
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (2, 2, N'Cơm xào hải sản', N'maxresdefault.jpg', 0, CAST(30 AS Decimal(18, 0)), 0)
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (3, 3, N'Tiger', N'8934822101336.jpg', 0, CAST(15 AS Decimal(18, 0)), 0)
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (4, 3, N'heniken', N'heniken.jpg', 0, CAST(25 AS Decimal(18, 0)), 1)
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (5, 2, N'mì gói 2 vắt có chanh', N'vienydhdt_migoi1.jpg', 0, CAST(20 AS Decimal(18, 0)), 1)
INSERT [dbo].[Service] ([Id], [ServiceTypeId], [Title], [Image], [InStock], [amount], [Active]) VALUES (7, 2, N'cocktail', N'sparkling-water.png', 0, CAST(10 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (16, 1, 15)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (16, 2, 13)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (16, 3, 18)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (29, 1, 2)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (29, 2, 2)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (29, 3, 4)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (29, 4, 3)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (31, 1, 2)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (31, 2, 9)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (31, 3, 4)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (36, 1, 82)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (42, 2, 5)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (42, 3, 2)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (46, 7, 100)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (50, 1, 1000)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (50, 2, 77)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (50, 3, 84)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (50, 4, 94)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (50, 5, 1000)
INSERT [dbo].[ServiceInRoom] ([BookRoomId], [ServiceId], [Quantity]) VALUES (51, 1, 100)
GO
INSERT [dbo].[ServiceType] ([Id], [Title], [Description], [Active]) VALUES (1, N'nước      ', N'1         ', 1)
INSERT [dbo].[ServiceType] ([Id], [Title], [Description], [Active]) VALUES (2, N'đồ ăn     ', N'1         ', 1)
INSERT [dbo].[ServiceType] ([Id], [Title], [Description], [Active]) VALUES (3, N'bia       ', N'4         ', NULL)
GO
ALTER TABLE [dbo].[BookRoom]  WITH CHECK ADD  CONSTRAINT [FK_BookRoom_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[BookRoom] CHECK CONSTRAINT [FK_BookRoom_Account]
GO
ALTER TABLE [dbo].[BookRoom]  WITH CHECK ADD  CONSTRAINT [FK_BookRoom_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([Id])
GO
ALTER TABLE [dbo].[BookRoom] CHECK CONSTRAINT [FK_BookRoom_Coupon]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [dbo].[RoomType] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomType]
GO
ALTER TABLE [dbo].[RoomInBooking]  WITH CHECK ADD  CONSTRAINT [FK_RoomInBooking_BookRoom] FOREIGN KEY([BookRoomId])
REFERENCES [dbo].[BookRoom] ([Id])
GO
ALTER TABLE [dbo].[RoomInBooking] CHECK CONSTRAINT [FK_RoomInBooking_BookRoom]
GO
ALTER TABLE [dbo].[RoomInBooking]  WITH CHECK ADD  CONSTRAINT [FK_RoomInBooking_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[RoomInBooking] CHECK CONSTRAINT [FK_RoomInBooking_Room]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[ServiceType] ([Id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO
ALTER TABLE [dbo].[ServiceInRoom]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInRoom_BookRoom] FOREIGN KEY([BookRoomId])
REFERENCES [dbo].[BookRoom] ([Id])
GO
ALTER TABLE [dbo].[ServiceInRoom] CHECK CONSTRAINT [FK_ServiceInRoom_BookRoom]
GO
ALTER TABLE [dbo].[ServiceInRoom]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInRoom_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[ServiceInRoom] CHECK CONSTRAINT [FK_ServiceInRoom_Service]
GO
