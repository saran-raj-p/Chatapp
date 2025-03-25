
CREATE PROCEDURE TokenValidation
	-- Add the parameters for the stored procedure here
	@token varchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if EXISTS(select 1 from Chatuser where RefreshToken=@token)
	BEGIN
		RETURN 1
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
GO
