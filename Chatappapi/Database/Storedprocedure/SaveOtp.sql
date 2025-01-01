ALTER PROCEDURE SaveOtp
    @Email NVARCHAR(255),
    @Otp NVARCHAR(6) -- The generated OTP
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the email exists in the Chatuser table
    IF EXISTS (SELECT 1 FROM Chatuser WHERE Email = @Email)
    BEGIN
        -- Update the OTP for the user with the given email
        UPDATE Chatuser
        SET Otp = @Otp,
            Activation = 0, -- Reset activation status (if applicable)
            Createdate = GETDATE() -- Update timestamp
        WHERE Email = @Email;

        SELECT 1 AS Result; -- OTP saved successfully
    END
    ELSE
    BEGIN
        SELECT 0 AS Result; -- Email not found
    END
END;
