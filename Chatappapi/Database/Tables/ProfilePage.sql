use Chatapp;

--GetProfileData (StoredProcedure)

create procedure GetProfileData 
	@id uniqueidentifier
as

begin
	select * from Chatuser where Id = @id;
end

--
