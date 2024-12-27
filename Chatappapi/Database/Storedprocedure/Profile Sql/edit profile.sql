use Chatapp

create procedure UpdateProfileData
	@id uniqueidentifier,
	@name varchar(255),
	@email varchar(255),
	@phone varchar(255),
	@profileUrl varchar(255)
as
begin
	update Chatuser
	set
		name = @name,
		email = @email,
	    phone = @phone,
		ProfileUrl = @profileUrl
	where 
	id = @id

	return 1;
end;