USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 09-01-2025 20:56:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdatePassword]
    @email nvarchar(255),
    @password nvarchar(max)
AS
BEGIN
    UPDATE Chatuser
    SET 
        Password = @password,
        OTP = NULL
    WHERE Email = @email
END