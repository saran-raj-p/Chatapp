-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SendMessage 
    @FromId UNIQUEIDENTIFIER,
    @ToId UNIQUEIDENTIFIER,
    @SendTime DATETIME,
    @Message NVARCHAR(MAX) -- specify length
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Message (FromId, ToId, SendTime, Message) 
    VALUES (@FromId, @ToId, @SendTime, @Message);

    -- optional:
    -- SET NOCOUNT OFF;
END
GO
