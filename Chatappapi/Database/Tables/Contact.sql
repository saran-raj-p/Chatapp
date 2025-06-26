use Chatapp

create table Contact(
	id uniqueidentifier primary key,
	name varchar(255),
	email varchar(255),
	phone varchar(225),
	userId uniqueidentifier not null,
	foreign key (userId) references Chatuser(id)
);

drop table Contact