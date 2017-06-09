use clonestone
go

drop table tblProductPurchase
drop table tblProduct
go

-- TABLES
create table tblProduct (
	id int identity not null,

	image varbinary(max) null,
	productName nvarchar(255) not null,
	numberOfDiamonds int not null default(0),
	price decimal not null,
	active bit not null default(1)
)
go

create table tblProductPurchase(
	id int identity not null,
	
	numberOfDiamonds int not null default(0),
	price decimal not null,

	-- foreign keys
	id_user int not null,
	id_product int not null
)
go

-- PRIMARY KEYS
alter table tblProduct
add constraint PK_Product
primary key(id);
go

alter table tblProductPurchase
add constraint PK_ProductPurchase
primary key(id);
go 

-- FOREIGN KEYS
alter table tblProductPurchase
add constraint FK_ProductPurchase_User
foreign key (id_user)
references [tblUser](idUser)
go

alter table tblProductPurchase
add constraint FK_ProductPurchase_Product
foreign key (id_product)
references [tblProduct](id)
go


--- insert products
insert into tblProduct (productName, numberOfDiamonds, price, active)
values ('small bag of diamonds', 3, 10, 1)

insert into tblProduct (productName, numberOfDiamonds, price, active)
values ('medium bag of diamonds', 8, 25, 1)

insert into tblProduct (productName, numberOfDiamonds, price, active)
values ('large bag of diamonds', 20, 35, 1)