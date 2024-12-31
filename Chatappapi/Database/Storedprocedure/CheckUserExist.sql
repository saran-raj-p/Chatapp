USE [Chatapp]
GO

/****** Object:  StoredProcedure [dbo].[CheckUserExist]    Script Date: 26-12-2024 22:20:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[CheckUserExist] 
	-- Add the parameters for the stored procedure here
	@email varchar(255),
	@password varchar(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT id as UserId,email as Email FROM Chatuser WHERE email=@email and password =@password)
	BEGIN
	SELECT id as UserId,email as Email FROM Chatuser WHERE email=@email and password =@password;
	END
	ELSE
	BEGIN
	SELECT null;
	END
END;
GO


