USE [master]
GO
/****** Object:  Database [E-LibraryDB]    Script Date: 2017-02-28 11:36:18 PM ******/
CREATE DATABASE [E-LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY                         -----Tera be carful here....make sure for the right directory location
( NAME = N'E-LibraryDB_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER2014\MSSQL\DATA\E-Library_Data.mdf' , SIZE = 688128KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'E-LibraryDB_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER2014\MSSQL\DATA\E-Library_Log.ldf' , SIZE = 1174528KB , MAXSIZE = 2048GB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [E-LibraryDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [E-LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [E-LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [E-LibraryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [E-LibraryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [E-LibraryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [E-LibraryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [E-LibraryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [E-LibraryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [E-LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [E-LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [E-LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [E-LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [E-LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [E-LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [E-LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [E-LibraryDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [E-LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [E-LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [E-LibraryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [E-LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [E-LibraryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [E-LibraryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [E-LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [E-LibraryDB] SET RECOVERY FULL 
GO
ALTER DATABASE [E-LibraryDB] SET  MULTI_USER 
GO
ALTER DATABASE [E-LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [E-LibraryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [E-LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [E-LibraryDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [E-LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'E-LibraryDB', N'ON'
GO
USE [E-LibraryDB]
GO
/****** Object:  FullTextCatalog [Text_Catalog]    Script Date: 2017-02-28 11:36:18 PM ******/
CREATE FULLTEXT CATALOG [Text_Catalog]WITH ACCENT_SENSITIVITY = ON

GO
/****** Object:  Table [dbo].[Authors]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Nationality] [nvarchar](50) NULL,
	[AuthorPicture] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Books]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Books](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[CategoryID] [int] NULL,
	[PublishDate] [date] NULL,
	[PublisherID] [int] NULL,
	[PagesCount] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [nvarchar](150) NULL,
	[ISBN] [varchar](50) NULL,
	[CoverPicture] [image] NULL,
 CONSTRAINT [PK__Books] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BooksAuthors]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BooksAuthors](
	[ID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
 CONSTRAINT [PK_BooksAuthors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TotalBooks] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data](
	[ID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[PageID] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK__Data] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Table] [varchar](50) NOT NULL,
	[Key] [int] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PicturesData]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PicturesData](
	[ID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PicturesData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publishers](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[PublisherPicture] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempTable]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempTable](
	[BookId] [int] NOT NULL,
	[PDFile] [image] NOT NULL,
 CONSTRAINT [PK_TempTable] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[SecurityLevel] [tinyint] NULL,
	[LogedIn] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WonderMen]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WonderMen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[BirthDay] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WonderMenTrigger]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WonderMenTrigger](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WonderID] [int] NULL,
	[WonderName] [nvarchar](50) NULL,
	[WonderBirthDay] [date] NULL,
	[Date] [datetime] NULL,
	[Criminer] [nvarchar](50) NULL,
	[Operation] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publishers] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publishers] ([ID])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publishers]
GO
ALTER TABLE [dbo].[BooksAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BooksAuthors_Authors] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([ID])
GO
ALTER TABLE [dbo].[BooksAuthors] CHECK CONSTRAINT [FK_BooksAuthors_Authors]
GO
ALTER TABLE [dbo].[BooksAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BooksAuthors_Books] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([ID])
GO
ALTER TABLE [dbo].[BooksAuthors] CHECK CONSTRAINT [FK_BooksAuthors_Books]
GO
ALTER TABLE [dbo].[Data]  WITH CHECK ADD  CONSTRAINT [FK_Data_Books] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([ID])
GO
ALTER TABLE [dbo].[Data] CHECK CONSTRAINT [FK_Data_Books]
GO
ALTER TABLE [dbo].[PicturesData]  WITH CHECK ADD  CONSTRAINT [FK_PicturesData_Books] FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([ID])
GO
ALTER TABLE [dbo].[PicturesData] CHECK CONSTRAINT [FK_PicturesData_Books]
GO
ALTER TABLE [dbo].[TempTable]  WITH CHECK ADD  CONSTRAINT [FK_TempTable_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([ID])
GO
ALTER TABLE [dbo].[TempTable] CHECK CONSTRAINT [FK_TempTable_Books]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddAuthor]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani Mounla
-- Create date: 2016-10-6
-- Description:	Add A category
-- =============================================
Create PROCEDURE [dbo].[sp_AddAuthor] 
	@name Nvarchar(50) ,
	@nationallity nvarchar(50),
	@authorPic image
AS
BEGIN
	SET NOCOUNT ON;
	Declare @id int = 1 
	IF EXISTS(select top 1 Id from Authors)
		select @id = MAX(Id) + 1 from Authors
	INSERT INTO Authors
	VALUES
	(@id,@name,@nationallity,@authorPic)
END



GO
/****** Object:  StoredProcedure [dbo].[sp_AddBook]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_AddBook] 
	@title nvarchar(70),
	@categoryID int ,
	@publishDate date ,
	@publisherID int ,
	@pagesCount int,
	@description nvarchar(MAX),
	@location nvarchar(150)  ,
	@isbn nvarchar(50) ,
	@coverPic image
AS
BEGIN
	SET NOCOUNT ON;
	Declare @id int = 1 
	IF EXISTS(select top 1 Id from Books)
		select @id = MAX(Id) + 1 from Books
	INSERT INTO Books
	 Values
	(@id,@title,@categoryID,@publishDate,@publisherID,@pagesCount,@description , @location ,@isbn,@coverPic)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_AddBookAuthor]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani Mounla
-- Create date: 2016-10-6
-- Description:	Add A category
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddBookAuthor] 
	@bookID int,
	@authorID int
AS
BEGIN
	SET NOCOUNT ON;
	Declare @id int = 1 
	IF EXISTS(select top 1 Id from BooksAuthors)
		select @id = MAX(Id) + 1 from BooksAuthors
	INSERT INTO BooksAuthors
	VALUES
	(@id,@bookID,@authorID)
END



GO
/****** Object:  StoredProcedure [dbo].[sp_AddBookPage]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani MOunla
-- Create date: 2016-10-17
-- Description:	Insert a book page text into the data Table and Picture Data Table
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddBookPage] 
	@bookID int, 
	@text nvarchar(MAX),
	@pictureText nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @pageID int =1

	IF Exists(Select top 1 pageID from Data where BookID = @bookID)
		select @PageID = Max(PageID) + 1 from data where BookID = @bookID

--------------------------------Text Data----------------------------------------------------

	Declare @DataID int =1,@DataPageID int = 1
	IF EXISTS(select top 1 Id from Data)
		select @DataID = MAX(Id) + 1 from Data

	Insert Into Data 
	Values
	(@DataID,@bookID , @PageID , @text)

--------------------------------Pictures Data-----------------------------------------------------

	Declare @pictureDataID int = 1,@picturePageID int = 1
	IF EXISTS(select top 1 Id from PicturesData)
		select @pictureDataID = MAX(Id) + 1 from PicturesData
	
	if @pictureText != 'No Picture' and @pictureText != 'Error getting text from Picture' and @pictureText != 'low Confidence picture'
	begin
		Insert Into PicturesData 
		Values
		(@pictureDataID,@bookID , @PageID , @pictureText)
	end 

	
END


GO
/****** Object:  StoredProcedure [dbo].[sp_AddCategory]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani Mounla
-- Create date: 2016-10-6
-- Description:	Add A category
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddCategory] 
	@name Nvarchar(50) 
AS
BEGIN
	SET NOCOUNT ON;
	Declare @id int = 1 
	IF EXISTS(select top 1 Id from Categories)
		select @id = MAX(Id) + 1 from Categories
	INSERT INTO Categories
	VALUES
	(@id,@name,0)
END



GO
/****** Object:  StoredProcedure [dbo].[sp_AddPublisher]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani Mounla
-- Create date: 2016-10-6
-- Description:	Add An Author
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddPublisher]
	@name Nvarchar(50),
	@city nvarchar(50),
	@phone varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @id int = 1 
	IF EXISTS(select top 1 Id from Publishers)
		select @id = MAX(Id) + 1 from Publishers
	INSERT INTO Publishers
	(id,name,City,Phone)
	VALUES
	(@id,@name,@city,@phone)
END



GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteBook]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteBook] 
	-- Add the parameters for the stored procedure here
	@bookID int 
AS
BEGIN
	SET NOCOUNT ON;
	delete from Data where BookID = BookID
	delete from PicturesData where BookID = BookID
	delete from BooksAuthors where BookID = @bookID
	delete from Books where ID = @bookID

END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetBookData]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetBookData] 
	@id int
AS
BEGIN
	SET NOCOUNT ON;
	select pageID ,text from Data where BookID = @id order by PageID
END



GO
/****** Object:  StoredProcedure [dbo].[sp_InsertTempBook]    Script Date: 2017-02-28 11:36:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hani
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertTempBook] 
	-- Add the parameters for the stored procedure here
	@bookID int , 
	@pdfFile image
AS
BEGIN
	SET NOCOUNT ON;

	insert into TempTable
	values
	(@bookID,@pdfFile)
END

GO
USE [master]
GO
ALTER DATABASE [E-LibraryDB] SET  READ_WRITE 
GO
