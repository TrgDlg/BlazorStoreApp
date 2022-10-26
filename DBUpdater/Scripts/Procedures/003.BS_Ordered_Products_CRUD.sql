USE [BlazorStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[BS_Ordered_Products_CRUD]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viacheslav Konovalov
-- Date:	26/10/2022
-- Description:	CRUD Ordered_Products
-- Returns:		Ordered_Products
-- =============================================
CREATE or ALTER PROCEDURE [dbo].[BS_Ordered_Products_CRUD]
(
	-- Add the parameters for the stored procedure here
	@status					AS INT = NULL OUTPUT,
	@sCRUD_Type				AS NVARCHAR(1) = NULL,
	@nOrderID				AS INT = NULL,	
	@sProduct				AS NVARCHAR(50) = NULL,
	@nPrice					AS INT = NULL


)
AS  
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF  @sCRUD_Type IS NULL
		AND @nOrderID IS NULL
		AND @sProduct IS NULL
		AND @nPrice IS NULL
		BEGIN
			SET @status = 99;		--NO data provided
		END;

	IF @status IS NULL		
		IF @sCRUD_Type = 'C'			
			BEGIN
				INSERT INTO dbo.OrderedProducts
				(				
				OrderId,					
				Product,												
				Price
				)
				OUTPUT INSERTED.OrderId
				VALUES
				(
				@nOrderID,
				@sProduct,
				@nPrice		
				) 
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())							
			END;				
		END;
		IF @sCRUD_Type = 'R'		
			BEGIN
				SELECT OrderedProducts.* 
				FROM OrderedProducts
				WHERE (@status IS NULL OR OrderedProducts.OrderId = @nOrderID);

				Set @status = 0;			
				RETURN (SELECT SCOPE_IDENTITY())	
			END;

		IF @sCRUD_Type = 'U'				
			BEGIN
				UPDATE OrderedProducts
				SET OrderId = ISNULL( @nOrderID, OrderId),
					Product = ISNULL( @sProduct, Product),
					Price = ISNULL( @nPrice, Price)			
				WHERE ((OrderedProducts.OrderId = @nOrderID));	
				
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())		
			END;	

		IF @sCRUD_Type = 'D'
			BEGIN
				DELETE FROM OrderedProducts
				WHERE (OrderedProducts.OrderId = @nOrderID);

				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())
			END;				
		