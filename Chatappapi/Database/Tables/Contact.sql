use Chatapp

create table Contact(
	id int primary key identity,
	name varchar(255),
	email varchar(255),
	phone varchar(225),
	userId vachar(255) not null,
	foreign key (userId) references Chatuser(id)
);