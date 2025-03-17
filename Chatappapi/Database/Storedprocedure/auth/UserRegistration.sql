USE [Chatapp]
GO

/****** Object:  StoredProcedure [dbo].[UserRegistration]    Script Date: 25-12-2024 17:40:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[UserRegistration]
	@uid uniqueidentifier,
	@name varchar(255),
	@password varchar(255),
	@email varchar(255),
	@phone varchar(10),
	@otp varchar(6)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Chatuser(id,name,password,email,phone,otp) values(@uid,@name,@password,@email,@phone,@otp);
	select @email as Email,@password as Password;
END
GO


