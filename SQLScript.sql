create database Report
use Report

create table Reports(
id int IDENTITY(1,1) primary key,
city nvarchar(100), 
workShop nvarchar(100),
employee nvarchar(100),
brigade nvarchar(100),
change nvarchar(100)
)

create table Brigades(
id int IDENTITY(1,1) primary key,
brigade nvarchar(100)
)

create table Cities(
id int IDENTITY(1,1) primary key,
city nvarchar(100)
)

create table WorkShops(
id int IDENTITY(1,1) primary key,
workShop nvarchar(100),
cityId int foreign key references Cities(id)
)

create table Employees(
id int IDENTITY(1,1) primary key,
employee nvarchar(100),
workShopId int foreign key references WorkShops(id)
)