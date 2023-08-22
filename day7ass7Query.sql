create database LibraryDB
use LibraryDB

create table Books
(BookId int primary key,
Title nvarchar(100),
Author nvarchar(100),
Genre nvarchar(100),
Quantity int)
insert into Books values (1,'.Net','Sundar','Technology',10)
insert into Books values (2,'Harry Potter','J.K.Rowling','fantasy',5)
insert into Books values (3,'A step ahead for success','Denis','Motivation',15)
select *from Books