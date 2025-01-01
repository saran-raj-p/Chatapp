USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 01-01-2025 20:46:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ResetPassword]
    @Email NVARCHAR(255),
    @NewPassword NVARCHAR(255) -- Use a suitable size for hashed passwords
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the email exists
    IF EXISTS (SELECT 1 FROM Chatuser WHERE Email = @Email)
    BEGIN
        -- Update the password
        UPDATE Chatuser
        SET Password = @NewPassword -- Ensure @NewPassword is hashed before passing
        WHERE Email = @Email;

        SELECT 1 AS Result; -- Password reset successful
    END
    ELSE
    BEGIN
        SELECT 0 AS Result; -- Email not found
    END
END;
