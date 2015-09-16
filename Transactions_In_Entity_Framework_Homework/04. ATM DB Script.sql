USE [master]
GO
/****** Object:  Database [ATM]    Script Date: 7/27/2015 5:57:34 PM ******/
CREATE DATABASE [ATM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ATM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ATM.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ATM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ATM_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ATM] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ATM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ATM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ATM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ATM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ATM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ATM] SET ARITHABORT OFF 
GO
ALTER DATABASE [ATM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ATM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ATM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ATM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ATM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ATM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ATM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ATM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ATM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ATM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ATM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ATM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ATM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ATM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ATM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ATM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ATM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ATM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ATM] SET  MULTI_USER 
GO
ALTER DATABASE [ATM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ATM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ATM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ATM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ATM] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ATM]
GO
/****** Object:  Table [dbo].[CardAccounts]    Script Date: 7/27/2015 5:57:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CardAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [char](10) NOT NULL,
	[CardPIN] [char](4) NOT NULL,
	[CardCash] [money] NOT NULL,
 CONSTRAINT [PK_CardAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionHistory]    Script Date: 7/27/2015 5:57:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [char](10) NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_TransactionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CardAccounts] ON 

INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (1, N'1234567890', N'1234', 111.3400)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (2, N'2974236451', N'5703', 2580.5600)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (3, N'8765674563', N'coax', 0.0000)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (4, N'7672465735', N'next', 5678.5500)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (5, N'7637465728', N'7647', 458.0000)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (6, N'6732674167', N'gdc ', 29019893.5600)
INSERT [dbo].[CardAccounts] ([Id], [CardNumber], [CardPIN], [CardCash]) VALUES (7, N'5635463250', N'5b5h', 74576.6600)
SET IDENTITY_INSERT [dbo].[CardAccounts] OFF
SET IDENTITY_INSERT [dbo].[TransactionHistory] ON 

INSERT [dbo].[TransactionHistory] ([Id], [CardNumber], [TransactionDate], [Amount]) VALUES (1, N'7672465735', CAST(N'2013-02-09' AS Date), 23.0000)
INSERT [dbo].[TransactionHistory] ([Id], [CardNumber], [TransactionDate], [Amount]) VALUES (2, N'1234567890', CAST(N'2015-09-09' AS Date), 20.0000)
INSERT [dbo].[TransactionHistory] ([Id], [CardNumber], [TransactionDate], [Amount]) VALUES (3, N'1234567890', CAST(N'2015-09-09' AS Date), 11.0000)
INSERT [dbo].[TransactionHistory] ([Id], [CardNumber], [TransactionDate], [Amount]) VALUES (4, N'1234567890', CAST(N'2015-07-27' AS Date), 11.0000)
SET IDENTITY_INSERT [dbo].[TransactionHistory] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CardAccounts]    Script Date: 7/27/2015 5:57:35 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CardAccounts] ON [dbo].[CardAccounts]
(
	[Id] ASC,
	[CardNumber] ASC,
	[CardPIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ATM] SET  READ_WRITE 
GO
