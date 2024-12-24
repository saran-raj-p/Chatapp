
CREATE PROCEDURE CheckUserExist 
	-- Add the parameters for the stored procedure here
	@email varchar(255),
	@password varchar(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Chatuser WHERE email=@email and password =@password)
	BEGIN
	SELECT 1;
	END
	ELSE
	BEGIN
	SELECT 0;
	END
END;
GO
