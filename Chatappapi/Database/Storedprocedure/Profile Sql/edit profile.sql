use Chatapp

create procedure UpdateProfileData
    @id UNIQUEIDENTIFIER,
    @name VARCHAR(255),
    @email VARCHAR(255),
    @phone VARCHAR(255),
    @profileUrl VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

   
    update Chatuser
    set
        name = @name,
        email = @email,
        phone = @phone,
        ProfileUrl = @profileUrl
    where id = @id;

    -- Check if any rows were affected and return the result
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 1 AS result;  -- Update successful
    END
    ELSE
    BEGIN
        SELECT 0 AS result;  -- No rows affected (id not found)
    END
END;

