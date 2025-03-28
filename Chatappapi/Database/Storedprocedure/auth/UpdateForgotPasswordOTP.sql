USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[UpdateForgotPasswordOTP]    Script Date: 09-01-2025 20:54:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateForgotPasswordOTP]
    @email nvarchar(255),
    @otp varchar(6)
AS
BEGIN
    UPDATE Chatuser
    SET OTP = @otp
    WHERE Email = @email
END