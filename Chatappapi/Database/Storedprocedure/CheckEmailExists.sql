USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[CheckEmailExists]    Script Date: 01-01-2025 20:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CheckEmailExists]
    @Email NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Chatuser WHERE Email = @Email)
        SELECT 1; -- Email is registered
    ELSE
        SELECT 0; -- Email is not registered
END;
