use Chatapp

create procedure CreateContact
    @name varchar(255),
    @email varchar(255),
    @phone varchar(255),
    @userId uniqueidentifier
as
begin
   
    if not exists (select 1 from Chatuser where email = @email)
    begin   
        raiserror('Email does not belong to a registered Chatuser', 16, 1)
        return
    end


    if exists (select 1 from Contact where email = @email and userId = @userId)
    begin       
        raiserror('Contact with this email already exists for this user', 16, 1)
        return
    end


    insert into Contact(id, name, email, phone, userId)
    values (NEWID(), @name, @email, @phone, @userId)

    select * from Contact where email = @email
end



drop procedure CreateContact;