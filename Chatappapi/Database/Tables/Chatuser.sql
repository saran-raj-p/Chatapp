use Chatapp

CREATE TABLE Chatuser (
    id UNIQUEIDENTIFIER NULL,
    name VARCHAR(255) NULL,
    password VARCHAR(255) NULL,
    email VARCHAR(255) NULL,
    phone VARCHAR(10) NULL,
    otp VARCHAR(6) NULL,
    activation INT NULL,
    createdate DATE NULL,
    RefreshToken VARCHAR(255) NULL,
    ProfileUrl VARCHAR(255) NOT NULL
);

alter table Chatuser alter column id UNIQUEIDENTIFIER NOT NULL;

alter table Chatuser add primary key(id);