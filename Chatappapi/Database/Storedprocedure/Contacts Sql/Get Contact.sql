use Chatapp

create procedure GetProfile
	@userId varchar(255)
as
begin
	select * from Chatuser where id = @userId;
end