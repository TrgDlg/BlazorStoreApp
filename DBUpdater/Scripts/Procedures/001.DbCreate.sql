if not exists (select * from sys.databases where name='BlazorStoreDB') 
CREATE DATABASE BlazorStoreDB
GO

USE BlazorStoreDB
GO
if not exists (select * from sysobjects where name='dbo.Products')
CREATE TABLE dbo.Products
    ([Name] varchar(50), [ProductType] varchar(50), [SpecialtyType] varchar(50), [Specialty] varchar(50), [Price] INT, [Pages] INT)
GO

USE BlazorStoreDB
GO
INSERT INTO dbo.Products
    ([Name], [ProductType], [SpecialtyType], [Specialty], [Price], [Pages])
VALUES
           (  'Kadignir Sanctum guide', 'Book', 'Ezoterics', '18+', '777', '1' ),
            ( 'Learn c++ within 2 hours', 'Book', 'Programming', 'C++', '99', '9001' ),
            ( 'Nuclear winter survey', 'Book', 'Cooking', 'Turtles', '100', '2042' ),
            ( 'Black magic', 'Book', 'Ezoterics', '18+', '123', '100' ),
            ( 'Learning BigData', 'Book', 'Programming', 'Python', '324', '325' ),
            ( 'Mick Gordon Doom Ost', 'CD', 'CD', 'Music', '969', '0' ),
            ( 'Additional scenes of star wars episod 4', 'CD', 'CD', 'Video', '700', '0' ),
            ( 'Capture one 21', 'CD', 'DVD', 'Software', '327', '0' ),
            ( 'All-in-one Windows 9 Half-Life 3', 'CD', 'DVD', 'Software', '1010', '0' ),
            ( 'Igorrr: Spirituality and Distortion', 'CD', 'CD', 'Music', '42', '0' )
GO


USE BlazorStoreDB
GO
if not exists (select * from sysobjects where name='dbo.Customers')
CREATE TABLE dbo.Customers
    ([Name] varchar(50), [Rights] varchar(50), [Age] INT, [Email] varchar(50), [Password] varchar(50))
GO

USE BlazorStoreDB
GO
INSERT INTO dbo.Customers
    ([Name], [Rights], [Age], [Email], [Password])
VALUES
            ( 'Doomslayer', 'Customer', '42', 'RipAndTear@hell.com', 'qwerty' ),
            ( 'Khan Maykr',  'Customer', '420', 'ConquerEarth@demons.com', '123' );



USE BlazorStoreDB
GO
if not exists (select * from sysobjects where name='dbo.Orders')
CREATE TABLE dbo.Orders
    ([OrderId] INT, [CreateDate] DATETIME, [ChangeDate] DATETIME, [Status] varchar(50), [Email] varchar(50), [Amount] NUMERIC(28, 12))
GO

USE BlazorStoreDB
GO
if not exists (select * from sysobjects where name='dbo.OrderedProducts')
CREATE TABLE dbo.OrderedProducts
    ([OrderId] INT, [Product] varchar(50), [Price] INT)
GO