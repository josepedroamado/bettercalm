USE [master]
GO
/****** Object:  Database [BetterCalmDB]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE DATABASE [BetterCalmDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BetterCalmDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BetterCalmDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BetterCalmDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BetterCalmDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BetterCalmDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BetterCalmDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BetterCalmDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BetterCalmDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BetterCalmDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BetterCalmDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BetterCalmDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BetterCalmDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BetterCalmDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BetterCalmDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BetterCalmDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BetterCalmDB] SET  MULTI_USER 
GO
ALTER DATABASE [BetterCalmDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BetterCalmDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BetterCalmDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BetterCalmDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BetterCalmDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BetterCalmDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BetterCalmDB] SET QUERY_STORE = OFF
GO
USE [BetterCalmDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/6/2021 6:38:24 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[PatientId] [int] NULL,
	[PsychologistId] [int] NULL,
	[IllnessId] [int] NULL,
	[ScheduleId] [int] NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryContent]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryContent](
	[CategoriesId] [int] NOT NULL,
	[ContentsId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryContent] PRIMARY KEY CLUSTERED 
(
	[CategoriesId] ASC,
	[ContentsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryPlaylist]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryPlaylist](
	[CategoriesId] [int] NOT NULL,
	[PlayListsId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryPlaylist] PRIMARY KEY CLUSTERED 
(
	[CategoriesId] ASC,
	[PlayListsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContentPlaylist]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentPlaylist](
	[ContentsId] [int] NOT NULL,
	[PlayListsId] [int] NOT NULL,
 CONSTRAINT [PK_ContentPlaylist] PRIMARY KEY CLUSTERED 
(
	[ContentsId] ASC,
	[PlayListsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contents]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ContentLength] [time](7) NOT NULL,
	[ArtistName] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[AudioUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Contents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Illnesses]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Illnesses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Illnesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IllnessPsychologist]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IllnessPsychologist](
	[IllnessesId] [int] NOT NULL,
	[PsychologistsId] [int] NOT NULL,
 CONSTRAINT [PK_IllnessPsychologist] PRIMARY KEY CLUSTERED 
(
	[IllnessesId] ASC,
	[PsychologistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BirthDate] [datetime2](7) NOT NULL,
	[EMail] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlists]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Playlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Psychologists]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Psychologists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Format] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Psychologists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUser]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUser](
	[RolesId] [int] NOT NULL,
	[UsersId] [int] NOT NULL,
 CONSTRAINT [PK_RoleUser] PRIMARY KEY CLUSTERED 
(
	[RolesId] ASC,
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PsychologistId] [int] NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/6/2021 6:38:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EMail] [nvarchar](450) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210426125056_Sessions', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210429010426_AudioUrlMigration', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210430213450_PsychologistIllnessRelation', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210501152313_ScheduleMigration', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210501175137_AddressToAppointment', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210502004108_UserImprovent', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210502004439_RemoveRoles', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210502012825_RoleBasedUsers', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210502022213_AddNameToUser', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210503234226_UserConstraints', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504021824_PalylistConstraints', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504024501_ContentConstraints', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504194639_PsychologistConstraints', N'5.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210504235056_PatientConstraints', N'5.0.4')
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([Id], [Date], [PatientId], [PsychologistId], [IllnessId], [ScheduleId], [Address]) VALUES (2067, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2), 1002, 2011, 3, 1026, N'http://bettercalm.com.uy/meeting_id/5f2232bf-a756-4541-b0af-1a19a0c2734e')
INSERT [dbo].[Appointments] ([Id], [Date], [PatientId], [PsychologistId], [IllnessId], [ScheduleId], [Address]) VALUES (2068, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2), 1003, 2012, 4, 1027, N'1st Street')
INSERT [dbo].[Appointments] ([Id], [Date], [PatientId], [PsychologistId], [IllnessId], [ScheduleId], [Address]) VALUES (2069, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2), 1004, 2011, 10, 1026, N'http://bettercalm.com.uy/meeting_id/574f7c54-0409-4f86-9058-cd43b627393a')
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1002, N'Música')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1003, N'Cuerpo')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1004, N'Dormir')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1005, N'Meditar')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[CategoryContent] ([CategoriesId], [ContentsId]) VALUES (1002, 4025)
INSERT [dbo].[CategoryContent] ([CategoriesId], [ContentsId]) VALUES (1002, 4026)
INSERT [dbo].[CategoryContent] ([CategoriesId], [ContentsId]) VALUES (1002, 4027)
INSERT [dbo].[CategoryContent] ([CategoriesId], [ContentsId]) VALUES (1002, 4028)
GO
INSERT [dbo].[CategoryPlaylist] ([CategoriesId], [PlayListsId]) VALUES (1002, 3027)
GO
INSERT [dbo].[ContentPlaylist] ([ContentsId], [PlayListsId]) VALUES (4025, 3026)
INSERT [dbo].[ContentPlaylist] ([ContentsId], [PlayListsId]) VALUES (4026, 3026)
INSERT [dbo].[ContentPlaylist] ([ContentsId], [PlayListsId]) VALUES (4027, 3027)
GO
SET IDENTITY_INSERT [dbo].[Contents] ON 

INSERT [dbo].[Contents] ([Id], [Name], [ContentLength], [ArtistName], [ImageUrl], [AudioUrl]) VALUES (4025, N'Beat It', CAST(N'00:04:30' AS Time), N'Michael Jackson', N'http://images.com/image.jpg', N'http://audio.com/audio.mp3')
INSERT [dbo].[Contents] ([Id], [Name], [ContentLength], [ArtistName], [ImageUrl], [AudioUrl]) VALUES (4026, N'Billie Jean', CAST(N'00:03:30' AS Time), N'Michael Jackson', N'http://images.com/image2.jpg', N'http://audio.com/audio2.mp3')
INSERT [dbo].[Contents] ([Id], [Name], [ContentLength], [ArtistName], [ImageUrl], [AudioUrl]) VALUES (4027, N'Beat It extended', CAST(N'00:06:30' AS Time), N'Michael Jackson', N'http://images.com/image3.jpg', N'http://audio.com/audio3.mp3')
INSERT [dbo].[Contents] ([Id], [Name], [ContentLength], [ArtistName], [ImageUrl], [AudioUrl]) VALUES (4028, N'Beat It extended 23', CAST(N'02:35:01' AS Time), N'Michael Jackson', N'http://images.com/imageMan.jpg', N'http://audio.com/audioMan.mp3')
SET IDENTITY_INSERT [dbo].[Contents] OFF
GO
SET IDENTITY_INSERT [dbo].[Illnesses] ON 

INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (3, N'depresión')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (4, N'estrés')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (5, N'ansiedad')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (6, N'autoestima')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (7, N'enojo')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (8, N'relaciones')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (9, N'duelo')
INSERT [dbo].[Illnesses] ([Id], [Name]) VALUES (10, N'otros')
SET IDENTITY_INSERT [dbo].[Illnesses] OFF
GO
INSERT [dbo].[IllnessPsychologist] ([IllnessesId], [PsychologistsId]) VALUES (3, 2011)
INSERT [dbo].[IllnessPsychologist] ([IllnessesId], [PsychologistsId]) VALUES (4, 2012)
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id], [BirthDate], [EMail], [Phone], [FirstName], [LastName]) VALUES (1002, CAST(N'2020-10-15T00:00:00.0000000' AS DateTime2), N'dsca@ba.com', N'123456789', N'Jose Perez', N'Recoba')
INSERT [dbo].[Patients] ([Id], [BirthDate], [EMail], [Phone], [FirstName], [LastName]) VALUES (1003, CAST(N'2020-10-15T00:00:00.0000000' AS DateTime2), N'vela@patient.com', N'123456789', N'Ramon', N'Velazquez')
INSERT [dbo].[Patients] ([Id], [BirthDate], [EMail], [Phone], [FirstName], [LastName]) VALUES (1004, CAST(N'2020-10-15T00:00:00.0000000' AS DateTime2), N'marta@patient.com', N'123456789', N'Marta', N'Fernandez')
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Playlists] ON 

INSERT [dbo].[Playlists] ([Id], [Name], [Description], [ImageUrl]) VALUES (3026, N'Best of Michael Jackson', N'Best songs of Michael Jackson', NULL)
INSERT [dbo].[Playlists] ([Id], [Name], [Description], [ImageUrl]) VALUES (3027, N'Trending', N'Trending music', NULL)
SET IDENTITY_INSERT [dbo].[Playlists] OFF
GO
SET IDENTITY_INSERT [dbo].[Psychologists] ON 

INSERT [dbo].[Psychologists] ([Id], [Address], [Format], [CreatedDate], [FirstName], [LastName]) VALUES (2011, N'1st Street', 0, CAST(N'2021-05-06T12:20:56.1411029' AS DateTime2), N'Orestes', N'Fiandra Pérez')
INSERT [dbo].[Psychologists] ([Id], [Address], [Format], [CreatedDate], [FirstName], [LastName]) VALUES (2012, N'1st Street', 1, CAST(N'2021-05-06T12:21:09.3253341' AS DateTime2), N'Doctor', N'Lecter')
SET IDENTITY_INSERT [dbo].[Psychologists] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name], [Description]) VALUES (1, N'Administrator', N'Control total')
INSERT [dbo].[Role] ([Id], [Name], [Description]) VALUES (1002, N'Client', N'Acceso limitado')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[RoleUser] ([RolesId], [UsersId]) VALUES (1, 1)
INSERT [dbo].[RoleUser] ([RolesId], [UsersId]) VALUES (1002, 1003)
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([Id], [PsychologistId], [Date]) VALUES (1026, 2011, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedule] ([Id], [PsychologistId], [Date]) VALUES (1027, 2012, CAST(N'2021-05-07T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [Token], [UserId]) VALUES (3013, N'cc222b37-9ee4-446b-b8e5-c97c98929787', 1)
SET IDENTITY_INSERT [dbo].[Sessions] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [EMail], [Password], [Name]) VALUES (1, N'admin@admin.com', N'admin1234', N'The Boss')
INSERT [dbo].[User] ([Id], [EMail], [Password], [Name]) VALUES (1003, N'user@user.com', N'user1234', N'Jose Martinez')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [IX_Appointments_IllnessId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_IllnessId] ON [dbo].[Appointments]
(
	[IllnessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_PatientId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_PatientId] ON [dbo].[Appointments]
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_PsychologistId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_PsychologistId] ON [dbo].[Appointments]
(
	[PsychologistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_ScheduleId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_ScheduleId] ON [dbo].[Appointments]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryContent_ContentsId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryContent_ContentsId] ON [dbo].[CategoryContent]
(
	[ContentsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryPlaylist_PlayListsId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryPlaylist_PlayListsId] ON [dbo].[CategoryPlaylist]
(
	[PlayListsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContentPlaylist_PlayListsId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_ContentPlaylist_PlayListsId] ON [dbo].[ContentPlaylist]
(
	[PlayListsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_IllnessPsychologist_PsychologistsId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_IllnessPsychologist_PsychologistsId] ON [dbo].[IllnessPsychologist]
(
	[PsychologistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleUser_UsersId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleUser_UsersId] ON [dbo].[RoleUser]
(
	[UsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedule_PsychologistId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schedule_PsychologistId] ON [dbo].[Schedule]
(
	[PsychologistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sessions_UserId]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_UserId] ON [dbo].[Sessions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_EMail]    Script Date: 5/6/2021 6:38:24 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_EMail] ON [dbo].[User]
(
	[EMail] ASC
)
WHERE ([EMail] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contents] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Contents] ADD  DEFAULT (N'') FOR [ArtistName]
GO
ALTER TABLE [dbo].[Contents] ADD  DEFAULT (N'') FOR [AudioUrl]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT (N'') FOR [EMail]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT (N'') FOR [Phone]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT (N'') FOR [FirstName]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT (N'') FOR [LastName]
GO
ALTER TABLE [dbo].[Playlists] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Playlists] ADD  DEFAULT (N'') FOR [Description]
GO
ALTER TABLE [dbo].[Psychologists] ADD  DEFAULT (N'') FOR [Address]
GO
ALTER TABLE [dbo].[Psychologists] ADD  DEFAULT (N'') FOR [FirstName]
GO
ALTER TABLE [dbo].[Psychologists] ADD  DEFAULT (N'') FOR [LastName]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (N'') FOR [Name]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Illnesses_IllnessId] FOREIGN KEY([IllnessId])
REFERENCES [dbo].[Illnesses] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Illnesses_IllnessId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patients_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Patients_PatientId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Psychologists_PsychologistId] FOREIGN KEY([PsychologistId])
REFERENCES [dbo].[Psychologists] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Psychologists_PsychologistId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Schedule_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedule] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Schedule_ScheduleId]
GO
ALTER TABLE [dbo].[CategoryContent]  WITH CHECK ADD  CONSTRAINT [FK_CategoryContent_Categories_CategoriesId] FOREIGN KEY([CategoriesId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryContent] CHECK CONSTRAINT [FK_CategoryContent_Categories_CategoriesId]
GO
ALTER TABLE [dbo].[CategoryContent]  WITH CHECK ADD  CONSTRAINT [FK_CategoryContent_Contents_ContentsId] FOREIGN KEY([ContentsId])
REFERENCES [dbo].[Contents] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryContent] CHECK CONSTRAINT [FK_CategoryContent_Contents_ContentsId]
GO
ALTER TABLE [dbo].[CategoryPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_CategoryPlaylist_Categories_CategoriesId] FOREIGN KEY([CategoriesId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryPlaylist] CHECK CONSTRAINT [FK_CategoryPlaylist_Categories_CategoriesId]
GO
ALTER TABLE [dbo].[CategoryPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_CategoryPlaylist_Playlists_PlayListsId] FOREIGN KEY([PlayListsId])
REFERENCES [dbo].[Playlists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryPlaylist] CHECK CONSTRAINT [FK_CategoryPlaylist_Playlists_PlayListsId]
GO
ALTER TABLE [dbo].[ContentPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_ContentPlaylist_Contents_ContentsId] FOREIGN KEY([ContentsId])
REFERENCES [dbo].[Contents] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContentPlaylist] CHECK CONSTRAINT [FK_ContentPlaylist_Contents_ContentsId]
GO
ALTER TABLE [dbo].[ContentPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_ContentPlaylist_Playlists_PlayListsId] FOREIGN KEY([PlayListsId])
REFERENCES [dbo].[Playlists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContentPlaylist] CHECK CONSTRAINT [FK_ContentPlaylist_Playlists_PlayListsId]
GO
ALTER TABLE [dbo].[IllnessPsychologist]  WITH CHECK ADD  CONSTRAINT [FK_IllnessPsychologist_Illnesses_IllnessesId] FOREIGN KEY([IllnessesId])
REFERENCES [dbo].[Illnesses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IllnessPsychologist] CHECK CONSTRAINT [FK_IllnessPsychologist_Illnesses_IllnessesId]
GO
ALTER TABLE [dbo].[IllnessPsychologist]  WITH CHECK ADD  CONSTRAINT [FK_IllnessPsychologist_Psychologists_PsychologistsId] FOREIGN KEY([PsychologistsId])
REFERENCES [dbo].[Psychologists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IllnessPsychologist] CHECK CONSTRAINT [FK_IllnessPsychologist_Psychologists_PsychologistsId]
GO
ALTER TABLE [dbo].[RoleUser]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser_Role_RolesId] FOREIGN KEY([RolesId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUser] CHECK CONSTRAINT [FK_RoleUser_Role_RolesId]
GO
ALTER TABLE [dbo].[RoleUser]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser_User_UsersId] FOREIGN KEY([UsersId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUser] CHECK CONSTRAINT [FK_RoleUser_User_UsersId]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Psychologists_PsychologistId] FOREIGN KEY([PsychologistId])
REFERENCES [dbo].[Psychologists] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Psychologists_PsychologistId]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_User_UserId]
GO
USE [master]
GO
ALTER DATABASE [BetterCalmDB] SET  READ_WRITE 
GO
