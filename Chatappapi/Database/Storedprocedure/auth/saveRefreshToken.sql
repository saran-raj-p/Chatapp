-- ================================================

CREATE PROCEDURE saveRefreshToken
@uid uniqueidentifier,
@refreshtoken varchar(255)
AS
BEGIN 
	update Chatuser set RefreshToken = @refreshtoken where id=@uid;
	IF EXISTS(select RefreshToken from Chatuser where id=@uid )
	BEGIN
	SELECT 1 From Chatuser
	END
	ELSE
	BEGIN
	SELECT 0 FROM Chatuser
	END
END
GO