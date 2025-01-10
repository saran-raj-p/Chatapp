USE [chatapp]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByEmail]    Script Date: 09-01-2025 20:49:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetUserByEmail]
    @email nvarchar(255)
AS
BEGIN
    SELECT *
    FROM Chatuser
    WHERE Email = @email
END