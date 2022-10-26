USE [BlazorStoreDB]
GO
/****** Object:  StoredProcedure [dbo].[BS_Customers_CRUD]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viacheslav Konovalov
-- Date:	26/10/2022
-- Description:	CRUD Customers
-- Returns:		Customers
-- =============================================
CREATE or ALTER PROCEDURE [dbo].[BS_Customers_CRUD]
(
	-- Add the parameters for the stored procedure here
	@status					AS INT = NULL OUTPUT,
	@sCRUD_Type				AS NVARCHAR(1) = NULL,
	@sName					AS NVARCHAR(50) = NULL,
	@sRights				AS NVARCHAR(50) = NULL,
	@nAge					AS INT = NULL,
	@sEmail					AS NVARCHAR(50) = NULL,
	@sPassword				AS NVARCHAR(50) = NULL

)
AS  
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF  @sCRUD_Type IS NULL
		AND @sName IS NULL
		AND @sRights IS NULL
		AND @nAge IS NULL
		AND @sEmail IS NULL
		AND @sPassword IS NULL
		BEGIN
			SET @status = 99;		--NO data provided
		END;

	IF @status IS NULL		
		IF @sCRUD_Type = 'C'			
			BEGIN
				INSERT INTO dbo.Customers
				(				
				Name,					
				Rights,												
				Age,				
				Email,				
				Password			
				)
				OUTPUT INSERTED.Name
				VALUES
				(
				@sName,
				@sRights,
				@nAge,
				@sEmail,
				@sPassword		
				) 
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())							
			END;				
		END;
		IF @sCRUD_Type = 'R'		
			BEGIN
				SELECT Customers.* 
				FROM Customers
				WHERE (@status IS NULL OR Customers.Name = @sName);

				Set @status = 0;			
				RETURN (SELECT SCOPE_IDENTITY())	
			END;

		IF @sCRUD_Type = 'U'				
			BEGIN
				UPDATE Customers
				SET Name = ISNULL( @sName, Name),
					Rights = ISNULL( @sRights, Rights),
					Age = ISNULL( @nAge, Age),
					Email = ISNULL( @sEmail, Email),
					Password = ISNULL( @sPassword, Password)			
				WHERE ((Customers.Name = @sName));	
				
				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())		
			END;	

		IF @sCRUD_Type = 'D'
			BEGIN
				DELETE FROM Customers
				WHERE (Customers.Name = @sName);

				Set @status = 0;
				RETURN (SELECT SCOPE_IDENTITY())
			END;				
		