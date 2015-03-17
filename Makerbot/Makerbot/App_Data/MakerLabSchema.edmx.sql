
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/17/2015 10:52:00
-- Generated from EDMX file: \\psf\Dropbox\IHA\4. semester\semesterprojekt4\Makerbot\Makerbot\Models\MakerLabSchema.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeFirstNewDatabaseSample.BloggingContext];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserUserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_UserUserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_BookingUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingSet] DROP CONSTRAINT [FK_BookingUser];
GO
IF OBJECT_ID(N'[dbo].[FK_BookingPrinter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingSet] DROP CONSTRAINT [FK_BookingPrinter];
GO
IF OBJECT_ID(N'[dbo].[FK_UserBookingPrintError]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingPrintErrorSet] DROP CONSTRAINT [FK_UserBookingPrintError];
GO
IF OBJECT_ID(N'[dbo].[FK_BookingBookingPrintError]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookingPrintErrorSet] DROP CONSTRAINT [FK_BookingBookingPrintError];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[UserRoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoleSet];
GO
IF OBJECT_ID(N'[dbo].[BookingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingSet];
GO
IF OBJECT_ID(N'[dbo].[PrinterSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrinterSet];
GO
IF OBJECT_ID(N'[dbo].[BookingPrintErrorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingPrintErrorSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserRoleId] int  NOT NULL,
    [StudieNumber] nvarchar(max)  NOT NULL,
    [AccessCard] int  NOT NULL,
    [UserRole_Id] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [CanCreateBooking] bit  NOT NULL
);
GO

-- Creating table 'Booking'
CREATE TABLE [dbo].[Booking] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrinterId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [User_Id] int  NOT NULL,
    [Printer_Id] int  NOT NULL
);
GO

-- Creating table 'Printer'
CREATE TABLE [dbo].[Printer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BookingPrintError'
CREATE TABLE [dbo].[BookingPrintError] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [BookingId] int  NOT NULL,
    [ErrorType] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [User_Id] int  NOT NULL,
    [Booking_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Booking'
ALTER TABLE [dbo].[Booking]
ADD CONSTRAINT [PK_Booking]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Printer'
ALTER TABLE [dbo].[Printer]
ADD CONSTRAINT [PK_Printer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookingPrintError'
ALTER TABLE [dbo].[BookingPrintError]
ADD CONSTRAINT [PK_BookingPrintError]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserRole_Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UserUserRole]
    FOREIGN KEY ([UserRole_Id])
    REFERENCES [dbo].[UserRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRole'
CREATE INDEX [IX_FK_UserUserRole]
ON [dbo].[User]
    ([UserRole_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Booking'
ALTER TABLE [dbo].[Booking]
ADD CONSTRAINT [FK_BookingUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingUser'
CREATE INDEX [IX_FK_BookingUser]
ON [dbo].[Booking]
    ([User_Id]);
GO

-- Creating foreign key on [Printer_Id] in table 'Booking'
ALTER TABLE [dbo].[Booking]
ADD CONSTRAINT [FK_BookingPrinter]
    FOREIGN KEY ([Printer_Id])
    REFERENCES [dbo].[Printer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingPrinter'
CREATE INDEX [IX_FK_BookingPrinter]
ON [dbo].[Booking]
    ([Printer_Id]);
GO

-- Creating foreign key on [User_Id] in table 'BookingPrintError'
ALTER TABLE [dbo].[BookingPrintError]
ADD CONSTRAINT [FK_UserBookingPrintError]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBookingPrintError'
CREATE INDEX [IX_FK_UserBookingPrintError]
ON [dbo].[BookingPrintError]
    ([User_Id]);
GO

-- Creating foreign key on [Booking_Id] in table 'BookingPrintError'
ALTER TABLE [dbo].[BookingPrintError]
ADD CONSTRAINT [FK_BookingBookingPrintError]
    FOREIGN KEY ([Booking_Id])
    REFERENCES [dbo].[Booking]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingBookingPrintError'
CREATE INDEX [IX_FK_BookingBookingPrintError]
ON [dbo].[BookingPrintError]
    ([Booking_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------