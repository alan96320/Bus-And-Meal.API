/****** Object:  Database [BusMeal]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE DATABASE [BusMeal]

GO

USE [BusMeal]
Go

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusMeal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusMeal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusMeal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusMeal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusMeal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusMeal] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusMeal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BusMeal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusMeal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusMeal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusMeal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusMeal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusMeal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusMeal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusMeal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusMeal] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BusMeal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusMeal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusMeal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusMeal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusMeal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusMeal] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BusMeal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusMeal] SET RECOVERY FULL 
GO
ALTER DATABASE [BusMeal] SET  MULTI_USER 
GO
ALTER DATABASE [BusMeal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusMeal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusMeal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusMeal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BusMeal] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BusMeal', N'ON'
GO
ALTER DATABASE [BusMeal] SET QUERY_STORE = OFF
GO
USE [BusMeal]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [BusMeal]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppConfiguration]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppConfiguration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LockedBusOrder] [varchar](10) NULL,
	[LockedMealOrder] [varchar](10) NULL,
 CONSTRAINT [PK_AppConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Audit]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](255) NULL,
	[UserId] [int] NOT NULL,
	[DateTime] [datetime2](7) NOT NULL,
	[KeyValues] [varchar](255) NULL,
	[NewValues] [nvarchar](max) NULL,
	[OldValues] [nvarchar](max) NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusOrder]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderEntryDate] [datetime2](7) NOT NULL,
	[DepartmentId] [int] NULL,
	[DormitoryBlockId] [int] NULL,
	[BusOrderVerificationId] [int] NULL,
	[IsReadyToCollect] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_BusOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusOrderDetail]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusOrderId] [int] NOT NULL,
	[BusTimeId] [int] NOT NULL,
	[OrderQty] [int] NOT NULL,
 CONSTRAINT [PK_BusOrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusOrderVerification]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusOrderVerification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](10) NULL,
	[Orderdate] [datetime2](7) NOT NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_BusOrderVerification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusOrderVerificationDetail]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusOrderVerificationDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusOrderVerificationId] [int] NULL,
	[BusTimeId] [int] NOT NULL,
	[SumOrderQty] [int] NOT NULL,
 CONSTRAINT [PK_BusOrderVerificationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusTime]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](6) NULL,
	[Time] [varchar](10) NULL,
	[DirectionEnum] [int] NOT NULL,
 CONSTRAINT [PK_BusTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Counter]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](3) NULL,
	[Name] [varchar](100) NULL,
	[Location] [varchar](100) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DormitoryBlock]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DormitoryBlock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](5) NULL,
	[Name] [varchar](100) NULL,
 CONSTRAINT [PK_DormitoryBlock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HrCoreNo] [varchar](10) NULL,
	[Firstname] [varchar](100) NULL,
	[Lastname] [varchar](100) NULL,
	[Fullname] [varchar](100) NULL,
	[HIDNo] [varchar](30) NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealOrder]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderEntryDate] [datetime2](7) NOT NULL,
	[DepartmentId] [int] NULL,
	[MealOrderVerificationId] [int] NULL,
	[IsReadyToCollect] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_MealOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealOrderDetail]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MealOrderId] [int] NULL,
	[MealTypeId] [int] NOT NULL,
	[OrderQty] [int] NOT NULL,
 CONSTRAINT [PK_MealOrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealOrderVerification]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrderVerification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](10) NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_MealOrderVerification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealOrderVerificationDetail]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrderVerificationDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MealOrderVerificationId] [int] NOT NULL,
	[MealTypeId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[SumOrderQty] [int] NOT NULL,
	[AdjusmentQty] [int] NOT NULL,
	[SwipeQty] [int] NOT NULL,
	[LogBookQty] [int] NOT NULL,
 CONSTRAINT [PK_MealOrderVerificationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealType]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](100) NULL,
 CONSTRAINT [PK_MealType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealVendor]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealVendor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](100) NULL,
	[ContactName] [varchar](100) NULL,
	[ContactPhone] [varchar](15) NULL,
	[ContactEmail] [varchar](100) NULL,
 CONSTRAINT [PK_MealVendor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleRight]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_ModuleRight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NULL,
	[PasswordHash] [varchar](255) NULL,
	[PasswordSalt] [varchar](255) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[FullName] [varchar](100) NULL,
	[GddbId] [varchar](100) NULL,
	[AdminStatus] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[LockTransStatus] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDepartment]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserModuleRight]    Script Date: 3/26/2020 4:05:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModuleRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleRightsId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Read] [bit] NOT NULL,
	[Write] [bit] NOT NULL,
 CONSTRAINT [PK_UserModuleRight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrder_BusOrderVerificationId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrder_BusOrderVerificationId] ON [dbo].[BusOrder]
(
	[BusOrderVerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrder_DepartmentId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrder_DepartmentId] ON [dbo].[BusOrder]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrder_DormitoryBlockId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrder_DormitoryBlockId] ON [dbo].[BusOrder]
(
	[DormitoryBlockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrder_UserId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrder_UserId] ON [dbo].[BusOrder]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrderDetail_BusOrderId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrderDetail_BusOrderId] ON [dbo].[BusOrderDetail]
(
	[BusOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrderDetail_BusTimeId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrderDetail_BusTimeId] ON [dbo].[BusOrderDetail]
(
	[BusTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrderVerificationDetail_BusOrderVerificationId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrderVerificationDetail_BusOrderVerificationId] ON [dbo].[BusOrderVerificationDetail]
(
	[BusOrderVerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BusOrderVerificationDetail_BusTimeId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusOrderVerificationDetail_BusTimeId] ON [dbo].[BusOrderVerificationDetail]
(
	[BusTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employee_DepartmentId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employee_DepartmentId] ON [dbo].[Employee]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrder_DepartmentId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrder_DepartmentId] ON [dbo].[MealOrder]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrder_MealOrderVerificationId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrder_MealOrderVerificationId] ON [dbo].[MealOrder]
(
	[MealOrderVerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrder_UserId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrder_UserId] ON [dbo].[MealOrder]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrderDetail_MealOrderId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrderDetail_MealOrderId] ON [dbo].[MealOrderDetail]
(
	[MealOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrderDetail_MealTypeId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrderDetail_MealTypeId] ON [dbo].[MealOrderDetail]
(
	[MealTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrderVerificationDetail_MealOrderVerificationId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrderVerificationDetail_MealOrderVerificationId] ON [dbo].[MealOrderVerificationDetail]
(
	[MealOrderVerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MealOrderVerificationDetail_MealTypeId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_MealOrderVerificationDetail_MealTypeId] ON [dbo].[MealOrderVerificationDetail]
(
	[MealTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserDepartment_DepartmentId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserDepartment_DepartmentId] ON [dbo].[UserDepartment]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserDepartment_UserId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserDepartment_UserId] ON [dbo].[UserDepartment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserModuleRight_ModuleRightsId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserModuleRight_ModuleRightsId] ON [dbo].[UserModuleRight]
(
	[ModuleRightsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserModuleRight_UserId]    Script Date: 3/26/2020 4:05:18 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserModuleRight_UserId] ON [dbo].[UserModuleRight]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusOrder]  WITH CHECK ADD  CONSTRAINT [FK_BusOrder_BusOrderVerification_BusOrderVerificationId] FOREIGN KEY([BusOrderVerificationId])
REFERENCES [dbo].[BusOrderVerification] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BusOrder] CHECK CONSTRAINT [FK_BusOrder_BusOrderVerification_BusOrderVerificationId]
GO
ALTER TABLE [dbo].[BusOrder]  WITH CHECK ADD  CONSTRAINT [FK_BusOrder_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[BusOrder] CHECK CONSTRAINT [FK_BusOrder_Department_DepartmentId]
GO
ALTER TABLE [dbo].[BusOrder]  WITH CHECK ADD  CONSTRAINT [FK_BusOrder_DormitoryBlock_DormitoryBlockId] FOREIGN KEY([DormitoryBlockId])
REFERENCES [dbo].[DormitoryBlock] ([Id])
GO
ALTER TABLE [dbo].[BusOrder] CHECK CONSTRAINT [FK_BusOrder_DormitoryBlock_DormitoryBlockId]
GO
ALTER TABLE [dbo].[BusOrder]  WITH CHECK ADD  CONSTRAINT [FK_BusOrder_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusOrder] CHECK CONSTRAINT [FK_BusOrder_User_UserId]
GO
ALTER TABLE [dbo].[BusOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_BusOrderDetail_BusOrder_BusOrderId] FOREIGN KEY([BusOrderId])
REFERENCES [dbo].[BusOrder] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusOrderDetail] CHECK CONSTRAINT [FK_BusOrderDetail_BusOrder_BusOrderId]
GO
ALTER TABLE [dbo].[BusOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_BusOrderDetail_BusTime_BusTimeId] FOREIGN KEY([BusTimeId])
REFERENCES [dbo].[BusTime] ([Id])
GO
ALTER TABLE [dbo].[BusOrderDetail] CHECK CONSTRAINT [FK_BusOrderDetail_BusTime_BusTimeId]
GO
ALTER TABLE [dbo].[BusOrderVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_BusOrderVerificationDetail_BusOrderVerification_BusOrderVerificationId] FOREIGN KEY([BusOrderVerificationId])
REFERENCES [dbo].[BusOrderVerification] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusOrderVerificationDetail] CHECK CONSTRAINT [FK_BusOrderVerificationDetail_BusOrderVerification_BusOrderVerificationId]
GO
ALTER TABLE [dbo].[BusOrderVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_BusOrderVerificationDetail_BusTime_BusTimeId] FOREIGN KEY([BusTimeId])
REFERENCES [dbo].[BusTime] ([Id])
GO
ALTER TABLE [dbo].[BusOrderVerificationDetail] CHECK CONSTRAINT [FK_BusOrderVerificationDetail_BusTime_BusTimeId]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department_DepartmentId]
GO
ALTER TABLE [dbo].[MealOrder]  WITH CHECK ADD  CONSTRAINT [FK_MealOrder_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[MealOrder] CHECK CONSTRAINT [FK_MealOrder_Department_DepartmentId]
GO
ALTER TABLE [dbo].[MealOrder]  WITH CHECK ADD  CONSTRAINT [FK_MealOrder_MealOrderVerification_MealOrderVerificationId] FOREIGN KEY([MealOrderVerificationId])
REFERENCES [dbo].[MealOrderVerification] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[MealOrder] CHECK CONSTRAINT [FK_MealOrder_MealOrderVerification_MealOrderVerificationId]
GO
ALTER TABLE [dbo].[MealOrder]  WITH CHECK ADD  CONSTRAINT [FK_MealOrder_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MealOrder] CHECK CONSTRAINT [FK_MealOrder_User_UserId]
GO
ALTER TABLE [dbo].[MealOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderDetail_MealOrder_MealOrderId] FOREIGN KEY([MealOrderId])
REFERENCES [dbo].[MealOrder] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MealOrderDetail] CHECK CONSTRAINT [FK_MealOrderDetail_MealOrder_MealOrderId]
GO
ALTER TABLE [dbo].[MealOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderDetail_MealType_MealTypeId] FOREIGN KEY([MealTypeId])
REFERENCES [dbo].[MealType] ([Id])
GO
ALTER TABLE [dbo].[MealOrderDetail] CHECK CONSTRAINT [FK_MealOrderDetail_MealType_MealTypeId]
GO
ALTER TABLE [dbo].[MealOrderVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderVerificationDetail_MealOrderVerification_MealOrderVerificationId] FOREIGN KEY([MealOrderVerificationId])
REFERENCES [dbo].[MealOrderVerification] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MealOrderVerificationDetail] CHECK CONSTRAINT [FK_MealOrderVerificationDetail_MealOrderVerification_MealOrderVerificationId]
GO
ALTER TABLE [dbo].[MealOrderVerificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealOrderVerificationDetail_MealType_MealTypeId] FOREIGN KEY([MealTypeId])
REFERENCES [dbo].[MealType] ([Id])
GO
ALTER TABLE [dbo].[MealOrderVerificationDetail] CHECK CONSTRAINT [FK_MealOrderVerificationDetail_MealType_MealTypeId]
GO
ALTER TABLE [dbo].[UserDepartment]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartment_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[UserDepartment] CHECK CONSTRAINT [FK_UserDepartment_Department_DepartmentId]
GO
ALTER TABLE [dbo].[UserDepartment]  WITH CHECK ADD  CONSTRAINT [FK_UserDepartment_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserDepartment] CHECK CONSTRAINT [FK_UserDepartment_User_UserId]
GO
ALTER TABLE [dbo].[UserModuleRight]  WITH CHECK ADD  CONSTRAINT [FK_UserModuleRight_ModuleRight_ModuleRightsId] FOREIGN KEY([ModuleRightsId])
REFERENCES [dbo].[ModuleRight] ([Id])
GO
ALTER TABLE [dbo].[UserModuleRight] CHECK CONSTRAINT [FK_UserModuleRight_ModuleRight_ModuleRightsId]
GO
ALTER TABLE [dbo].[UserModuleRight]  WITH CHECK ADD  CONSTRAINT [FK_UserModuleRight_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserModuleRight] CHECK CONSTRAINT [FK_UserModuleRight_User_UserId]
GO
USE [master]
GO
ALTER DATABASE [BusMeal] SET  READ_WRITE 
GO




USE [BusMeal]
GO
SET IDENTITY_INSERT [dbo].[ModuleRight] ON 
GO
INSERT [dbo].[ModuleRight] ([Id], [Code], [Description]) VALUES (1, N'0101', N'0101')
GO
INSERT [dbo].[ModuleRight] ([Id], [Code], [Description]) VALUES (2, N'1010', N'2030')
GO
INSERT [dbo].[ModuleRight] ([Id], [Code], [Description]) VALUES (3, N'4421', N'30303')
GO
INSERT [dbo].[ModuleRight] ([Id], [Code], [Description]) VALUES (4, N'4443', N'20303')
GO
SET IDENTITY_INSERT [dbo].[ModuleRight] OFF
GO




---------------------------------

USE [BusMeal]
GO
SET IDENTITY_INSERT [dbo].[AppConfiguration] ON 
GO
INSERT [dbo].[AppConfiguration] ([Id], [LockedBusOrder], [LockedMealOrder]) VALUES (1, N'10:30', N'14:30')
GO
SET IDENTITY_INSERT [dbo].[AppConfiguration] OFF
GO
SET IDENTITY_INSERT [dbo].[MealType] ON 
GO
INSERT [dbo].[MealType] ([Id], [Code], [Name]) VALUES (1, N'MT01', N'Breakfast')
GO
INSERT [dbo].[MealType] ([Id], [Code], [Name]) VALUES (2, N'MT02', N'Lunch')
GO
INSERT [dbo].[MealType] ([Id], [Code], [Name]) VALUES (3, N'MT03', N'Dinner')
GO
INSERT [dbo].[MealType] ([Id], [Code], [Name]) VALUES (4, N'MT04', N'Sapper')
GO
SET IDENTITY_INSERT [dbo].[MealType] OFF
GO

USE [BusMeal]
GO
SET IDENTITY_INSERT [dbo].[BusTime] ON 
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (1, N'010600', N'06:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (2, N'011230', N'12:30', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (3, N'011400', N'14:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (4, N'011500', N'15:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (5, N'011900', N'19:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (6, N'012200', N'22:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (7, N'012300', N'23:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (8, N'010700', N'07:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (9, N'010800', N'08:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (10, N'010830', N'08:30', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (11, N'011000', N'10:00', 1)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (12, N'021230', N'12:30', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (13, N'021500', N'15:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (14, N'021600', N'16:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (15, N'021700', N'17:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (16, N'021800', N'18:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (17, N'021830', N'18:30', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (18, N'021900', N'19:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (19, N'022100', N'21:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (20, N'022300', N'23:00', 2)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (21, N'030330', N'03:30', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (22, N'030400', N'04:00', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (23, N'030430', N'04:30', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (24, N'030500', N'05:00', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (25, N'030600', N'06:00', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (26, N'030630', N'06:30', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (27, N'030700', N'07:00', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (28, N'030030', N'00:30', 3)
GO
INSERT [dbo].[BusTime] ([Id], [Code], [Time], [DirectionEnum]) VALUES (29, N'030300', N'03:00', 3)
GO
SET IDENTITY_INSERT [dbo].[BusTime] OFF
GO
