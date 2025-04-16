use Chatapp

create procedure GetContact
	@userId uniqueidentifier
as
begin
	select * from Contact where userId = @userId;
end

drop procedure GetContact