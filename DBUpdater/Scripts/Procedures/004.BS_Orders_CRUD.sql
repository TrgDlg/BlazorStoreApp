USE [BlazorStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[BS_Orders_CRUD]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viacheslav Konovalov
-- Date:	26/10/2022
-- Description:	CRUD Orders
-- Returns:		Products
-- =============================================
CREATE or ALTER PROCEDURE [dbo].[BS_Orders_CRUD]
(
	-- Add the parameters for the stored procedure here
	@status					AS INT = NULL OUTPUT,
	@sCRUD_Type				AS NVARCHAR(1) = NULL,
	@nOrderID				AS INT = NULL,	
	@dCreateDate			AS DATETIME = NULL,
	@dChangeDate			AS DATETIME = NULL,
	@sStatus				AS NVARCHAR(50) = NULL,
	@nAmount				AS NUMERIC(28,12) = NULL

)
AS  
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF  @sCRUD_Type IS NULL
		AND @nOrderID IS NULL
		AND @dCreateDate IS NULL
		AND @dChangeDate IS NULL
		AND @sStatus IS NULL
		AND @nAmount IS NULL
		BEGIN
			SET @status = 99;		--NO data provided
		END;

	IF @status IS NULL		
		IF @sCRUD_Type = 'C'			
			BEGIN
				INSERT INTO dbo.Orders
				(				
				OrderID,					
				CreateDate,												
				ChangeDate,				
				Status,				
				Amount			
				)
				OUTPUT INSERTED.OrderID
				VALUES
				(
				@nOrderID,
				@dCreateDate,
				@dChangeDate,
				@sStatus,									
				@nAmount		
				) 
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())							
			END;				
		END;
		IF @sCRUD_Type = 'R'		
			BEGIN
				SELECT Orders.* 
				FROM Orders
				WHERE (@status IS NULL OR Orders.OrderID = @nOrderID);

				Set @status = 0;			
				RETURN (SELECT SCOPE_IDENTITY())	
			END;

		IF @sCRUD_Type = 'U'				
			BEGIN
				UPDATE Orders
				SET OrderID = ISNULL( @nOrderID, OrderID),
					CreateDate = ISNULL( @dCreateDate, CreateDate),
					ChangeDate = ISNULL( @dChangeDate, ChangeDate),
					Status = ISNULL( @sStatus, Status),
					Amount = ISNULL( @nAmount, Amount)				
				WHERE ((Orders.OrderId = @nOrderID));	
				
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())		
			END;	

		IF @sCRUD_Type = 'D'
			BEGIN
				DELETE FROM Orders
				WHERE (Orders.OrderID = @nOrderID);

				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())
			END;				
		