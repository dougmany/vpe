use texdb
create table AdvancedUsers ( id INT PRIMARY KEY, Title VARCHAR(10), 
  First VARCHAR(40), Last VARCHAR(40), Street VARCHAR(40), 
  Zip VARCHAR(10), Town VARCHAR(40), Country VARCHAR(40) );
create table Products ( id INT PRIMARY KEY, Name VARCHAR(50),
  Price REAL);
create table Orders ( id INT PRIMARY KEY, UserID INT, ProductID INT,
  Date DATE );

insert into AdvancedUsers values 
  (0, "Mr", "Hans-Georg", "Eßer", "33 Test Street", "47384",
  "New SQLshire", "Germany");
insert into AdvancedUsers values
  (1, "Mrs", "Joanna", "Dumbledore", "42 Owl Fields", "99911",
  "Hogwardstown", "United Kingdom");

insert into Products values
  (0, "The LaTeXDB Book. A complete reference", 19.95);

insert into Products values
  (1, "Magic owls and how to tame them", 12.95);

insert into Orders values (0, 0, 0, "2003-08-15");
insert into Orders values (1, 0, 1, "2003-08-16");
insert into Orders values (2, 1, 1, "2003-08-16");

select Title, First, Last, Street, Zip, Town, Country, Name, Price, Date from AdvancedUsers, Products, Orders where UserID=AdvancedUsers.id and ProductID=Products.id;

