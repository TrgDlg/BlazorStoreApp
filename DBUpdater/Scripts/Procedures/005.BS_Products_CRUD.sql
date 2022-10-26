USE [BlazorStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[BS_Products_CRUD]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viacheslav Konovalov
-- Date:	26/10/2022
-- Description:	CRUD Products
-- Returns:		Products
-- =============================================
CREATE or ALTER PROCEDURE [dbo].[BS_Products_CRUD]
(
	-- Add the parameters for the stored procedure here
	@status					AS INT = NULL OUTPUT,
	@sCRUD_Type				AS NVARCHAR(1) = NULL,
	@sProductName			AS NVARCHAR(50) = NULL,
	@sProductType			AS NVARCHAR(50) = NULL,
	@sSpecialtyType		AS NVARCHAR(50) = NULL,
	@sSpecialty			AS NVARCHAR(50) = NULL,
	@nPrice					AS INT = NULL,
	@nAmount				AS INT = NULL

)
AS  
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF  @sCRUD_Type IS NULL
		AND @sProductName IS NULL
		AND @sProductType IS NULL
		AND @sSpecialtyType IS NULL
		AND @sSpecialty IS NULL
		AND @nPrice IS NULL
		AND @nAmount IS NULL
		BEGIN
			SET @status = 99;		--NO data provided
		END;

	IF @status IS NULL		
		IF @sCRUD_Type = 'C'			
			BEGIN
				INSERT INTO dbo.Products
				(				
				ProductName,					
				ProductType,												
				SpecialtyType,				
				Specialty,				
				Price,				
				Pages				
				)
				OUTPUT INSERTED.ProductName
				VALUES
				(
				@sProductName,
				@sProductType,
				@sSpecialtyType,
				@sSpecialty,
				@nPrice,					
				@nAmount		
				) 
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())							
			END;				
		END;
		IF @sCRUD_Type = 'R'		
			BEGIN
				SELECT Products.* 
				FROM Products
				WHERE (@status IS NULL OR Products.ProductName = @sProductName);

				Set @status = 0;			
				RETURN (SELECT SCOPE_IDENTITY())	
			END;

		IF @sCRUD_Type = 'U'				
			BEGIN
				UPDATE Products
				SET ProductName = ISNULL( @sProductName, ProductName),
					ProductType = ISNULL( @sProductType, ProductType),
					SpecialtyType = ISNULL( @sSpecialtyType, SpecialtyType),
					Specialty = ISNULL( @sSpecialty, Specialty),
					Price = ISNULL( @nPrice, Price),
					Pages = ISNULL( @nAmount, Pages)				
				WHERE ((Products.ProductName = @sProductName));	
				
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())		
			END;	

		IF @sCRUD_Type = 'D'
			BEGIN
				DELETE FROM Products
				WHERE (Products.ProductName = @sProductName);

				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())
			END;				
		