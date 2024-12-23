use Chatapp;

create database Chatapp;

create Table Chatuser (id uniqueidentifier,name varchar(255),password varchar(255),email varchar(255),phone varchar(10),otp varchar(6),activation int,createdate date);

