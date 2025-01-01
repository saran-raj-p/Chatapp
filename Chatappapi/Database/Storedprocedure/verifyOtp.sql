USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[VerifyOtps]    Script Date: 01-01-2025 20:48:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[VerifyOtps]
    @Email NVARCHAR(255),
    @Otp NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the OTP is valid
    IF EXISTS (
        SELECT 1
        FROM Chatuser
        WHERE Email = @Email
          AND Otp = @Otp
    )
    BEGIN
        -- Mark OTP as used (if needed)
        UPDATE Chatuser
        SET Otp = NULL, Activation = 1 -- Clear OTP and mark user as activated
        WHERE Email = @Email AND Otp = @Otp;

        SELECT 1 AS Result; -- OTP is valid
    END
    ELSE
    BEGIN
        SELECT 0 AS Result; -- OTP is invalid or expired
    END
END;
