CREATE PROCEDURE UserActivation
    @Email VARCHAR(255),
    @Otp VARCHAR(255)
AS
BEGIN
    DECLARE @StoredOtp VARCHAR(255);

    SELECT @StoredOtp = otp FROM Chatuser WHERE email = @Email;

    
    IF (@Otp = @StoredOtp)
    BEGIN
        -- Update the user to mark them as activated (activation = 1)
        UPDATE Chatuser
        SET activation = 1
        WHERE email = @Email;
    END
    ELSE
    BEGIN
        -- Handle the case where the OTP does not match
        RAISERROR('Invalid OTP', 16, 1);
    END
END;
