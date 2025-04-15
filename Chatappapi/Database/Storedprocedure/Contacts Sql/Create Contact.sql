create procedure CreateContact
	@id uniqueidentifier,
	@name varachar(255),
	@email varachar(255),
	@phone varachar(255),
	@userId varachar(255)
as

begin
	insert into Contact(id,name,email,phone,userId) values (NEWID(),@name,@email,@phone,@userID);
end