USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[VerifyOTP]    Script Date: 09-01-2025 20:56:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[VerifyOTP]
    @email nvarchar(255),
    @otp varchar(6)
AS
BEGIN
    SELECT CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM Chatuser 
            WHERE Email = @email 
            AND OTP = @otp
        ) 
        THEN 1 
        ELSE 0 
    END
END