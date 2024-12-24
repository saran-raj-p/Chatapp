
alter PROCEDURE UserRegistration 
	@name varchar(255),
	@password varchar(255),
	@email varchar(255),
	@phone varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Chatuser(name,password,email,phone) values(@name,@password,@email,@phone);
	select @email as Email,@password as Password;
END
GO
