USE [master]
GO
/****** Object:  Database [ShopBridge]    Script Date: 23-02-2021 22:36:11 ******/
CREATE DATABASE [ShopBridge]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopBridge', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ShopBridge.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopBridge_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ShopBridge_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ShopBridge] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopBridge].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopBridge] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopBridge] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopBridge] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopBridge] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopBridge] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopBridge] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopBridge] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopBridge] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopBridge] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopBridge] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopBridge] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopBridge] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopBridge] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopBridge] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopBridge] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopBridge] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopBridge] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopBridge] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopBridge] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopBridge] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopBridge] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopBridge] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopBridge] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopBridge] SET  MULTI_USER 
GO
ALTER DATABASE [ShopBridge] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopBridge] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopBridge] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopBridge] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShopBridge] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopBridge', N'ON'
GO
ALTER DATABASE [ShopBridge] SET QUERY_STORE = OFF
GO
USE [ShopBridge]
GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 23-02-2021 22:36:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](250) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ItemMaster] ON 

INSERT [dbo].[ItemMaster] ([ItemId], [ItemName], [Price], [Description], [ImagePath], [IsActive], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (1, N'Hard Drive', CAST(2600.00 AS Decimal(18, 2)), N'4 GB Hard Drive', N'/Uploads/Items/crop_I1_230220212133012.jpg', 1, CAST(N'2021-02-23T16:03:19.377' AS DateTime), N'Nikhil Jojare', NULL, NULL)
INSERT [dbo].[ItemMaster] ([ItemId], [ItemName], [Price], [Description], [ImagePath], [IsActive], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (2, N'RAM', CAST(3500.00 AS Decimal(18, 2)), N'4GB RAM', NULL, 0, CAST(N'2021-02-23T16:04:15.863' AS DateTime), N'Nikhil Jojare', CAST(N'2021-02-23T16:54:32.770' AS DateTime), N'Nikhil Jojare')
INSERT [dbo].[ItemMaster] ([ItemId], [ItemName], [Price], [Description], [ImagePath], [IsActive], [Created], [CreatedBy], [Modified], [ModifiedBy]) VALUES (3, N'Lenovo Battery', CAST(2500.00 AS Decimal(18, 2)), N'Green Cell Battery - Lenovo ThinkPad X220t, X230t, X230i Tablet - 4400mAh', N'/Uploads/Items/crop_OIP_230220212140911.jpg', 1, CAST(N'2021-02-23T16:10:32.987' AS DateTime), N'Nikhil Jojare', NULL, NULL)
SET IDENTITY_INSERT [dbo].[ItemMaster] OFF
USE [master]
GO
ALTER DATABASE [ShopBridge] SET  READ_WRITE 
GO
