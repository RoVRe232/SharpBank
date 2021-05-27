USE [master]
GO
/****** Object:  Database [BankDb]    Script Date: 14/05/2021 08:52:01 ******/
CREATE DATABASE [BankDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BankDb', FILENAME = N'C:\Users\Remus\BankDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BankDb_log', FILENAME = N'C:\Users\Remus\BankDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BankDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BankDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BankDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BankDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BankDb] SET  MULTI_USER 
GO
ALTER DATABASE [BankDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BankDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BankDb] SET QUERY_STORE = OFF
GO
USE [BankDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [BankDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 14/05/2021 08:52:01 ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](256) NULL,
	[County] [nvarchar](256) NULL,
	[City] [nvarchar](256) NULL,
	[Street] [nvarchar](256) NULL,
	[Number] [nvarchar](256) NULL,
	[Block] [nvarchar](256) NULL,
	[Stairway] [nvarchar](256) NULL,
	[Apartment] [nvarchar](256) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[PasswordToken] [nvarchar](256) NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[CNP] [nvarchar](256) NOT NULL,
	[CI] [nvarchar](256) NOT NULL,
	[ConfirmationKey] [nvarchar](256) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[ActionsHistory] [nvarchar](2048) NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins_RefreshTokens]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins_RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[CreatedByIp] [nvarchar](max) NULL,
	[Revoked] [datetime2](7) NULL,
	[RevokedByIp] [nvarchar](max) NULL,
	[ReplacedByToken] [nvarchar](max) NULL,
	[AdminId] [int] NOT NULL,
 CONSTRAINT [PK_Admins_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankAccounts]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccounts](
	[IBAN] [nvarchar](256) NOT NULL,
	[Type] [nvarchar](256) NULL,
	[Balance] [float] NOT NULL,
	[Currency] [nvarchar](64) NULL,
	[CustomerId] [int] NULL,
	[IsBlocked] [bit] NOT NULL,
 CONSTRAINT [PK_BankAccounts] PRIMARY KEY CLUSTERED 
(
	[IBAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cards]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cards](
	[CardNumber] [nvarchar](256) NOT NULL,
	[HolderIBAN] [nvarchar](256) NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[CVV] [nvarchar](32) NULL,
	[HolderFullName] [nvarchar](256) NULL,
	[BankAccountIBAN] [nvarchar](256) NULL,
 CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[PasswordToken] [nvarchar](256) NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[CNP] [nvarchar](256) NOT NULL,
	[CI] [nvarchar](256) NOT NULL,
	[ConfirmationKey] [nvarchar](256) NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[PhoneNumber] [nvarchar](128) NULL,
	[HomeAddressId] [int] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers_RefreshTokens]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers_RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[Expires] [datetime2](7) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[CreatedByIp] [nvarchar](max) NULL,
	[Revoked] [datetime2](7) NULL,
	[RevokedByIp] [nvarchar](max) NULL,
	[ReplacedByToken] [nvarchar](max) NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_Customers_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 14/05/2021 08:52:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [nvarchar](256) NOT NULL,
	[SenderIBAN] [nvarchar](256) NOT NULL,
	[ReceiverIBAN] [nvarchar](256) NOT NULL,
	[ReceiverFullName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Amount] [float] NOT NULL,
	[Currency] [nvarchar](64) NOT NULL,
	[BankAccountIBAN1] [nvarchar](256) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[FirstPaymentDate] [datetime2](7) NULL,
	[LastPaymentDate] [datetime2](7) NULL,
	[DaysInterval] [int] NULL,
	[IsMonthly] [bit] NULL,
	[BankAccountIBAN] [nvarchar](256) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Admins_RefreshTokens_AdminId]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Admins_RefreshTokens_AdminId] ON [dbo].[Admins_RefreshTokens]
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BankAccounts_CustomerId]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_BankAccounts_CustomerId] ON [dbo].[BankAccounts]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cards_BankAccountIBAN]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Cards_BankAccountIBAN] ON [dbo].[Cards]
(
	[BankAccountIBAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customers_HomeAddressId]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Customers_HomeAddressId] ON [dbo].[Customers]
(
	[HomeAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customers_RefreshTokens_CustomerId]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Customers_RefreshTokens_CustomerId] ON [dbo].[Customers_RefreshTokens]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Transactions_BankAccountIBAN]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_BankAccountIBAN] ON [dbo].[Transactions]
(
	[BankAccountIBAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Transactions_BankAccountIBAN1]    Script Date: 14/05/2021 08:52:01 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_BankAccountIBAN1] ON [dbo].[Transactions]
(
	[BankAccountIBAN1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BankAccounts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsBlocked]
GO
ALTER TABLE [dbo].[Admins_RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_Admins_RefreshTokens_Admins_AdminId] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Admins] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admins_RefreshTokens] CHECK CONSTRAINT [FK_Admins_RefreshTokens_Admins_AdminId]
GO
ALTER TABLE [dbo].[BankAccounts]  WITH CHECK ADD  CONSTRAINT [FK_BankAccounts_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[BankAccounts] CHECK CONSTRAINT [FK_BankAccounts_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Cards]  WITH CHECK ADD  CONSTRAINT [FK_Cards_BankAccounts_BankAccountIBAN] FOREIGN KEY([BankAccountIBAN])
REFERENCES [dbo].[BankAccounts] ([IBAN])
GO
ALTER TABLE [dbo].[Cards] CHECK CONSTRAINT [FK_Cards_BankAccounts_BankAccountIBAN]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Address_HomeAddressId] FOREIGN KEY([HomeAddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Address_HomeAddressId]
GO
ALTER TABLE [dbo].[Customers_RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_Customers_RefreshTokens_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers_RefreshTokens] CHECK CONSTRAINT [FK_Customers_RefreshTokens_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_BankAccounts_BankAccountIBAN] FOREIGN KEY([BankAccountIBAN])
REFERENCES [dbo].[BankAccounts] ([IBAN])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_BankAccounts_BankAccountIBAN]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_BankAccounts_BankAccountIBAN1] FOREIGN KEY([BankAccountIBAN1])
REFERENCES [dbo].[BankAccounts] ([IBAN])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_BankAccounts_BankAccountIBAN1]
GO
USE [master]
GO
ALTER DATABASE [BankDb] SET  READ_WRITE 
GO
