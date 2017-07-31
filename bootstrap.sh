DBNAME=toastmasters
ROOTPASSWD=test123
DBUSER=dbuser
DBPASSWD=test123

apt-get update
# MySQL setup for development purposes ONLY
echo -e "\n--- Install MySQL specific packages and settings ---\n"
debconf-set-selections <<< "mysql-server mysql-server/root_password password $ROOTPASSWD"
debconf-set-selections <<< "mysql-server mysql-server/root_password_again password $ROOTPASSWD"
apt-get -y install mysql-server python-mysqldb texlive

mysql -uroot -p$ROOTPASSWD -e "CREATE DATABASE $DBNAME"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'localhost' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Users ( id INT PRIMARY KEY, FirstName VARCHAR(40), LastName VARCHAR(40) );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Meetings ( id INT PRIMARY KEY, Date DateTime, Toastmaster INT, Inspirational INT, Joke INT, GeneralEvaluator INT, Evaluator1 INT, Evaluator2 INT, Timer INT, BallotCounter INT, Grammarian INT, TableTopics INT, Speaker1 INT, Speaker2 INT );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; IINSERT INTO Users VALUES (0,'Dale',' Rinde'); INSERT INTO Users VALUES (1,'Doug',' Meeker'); INSERT INTO Users VALUES (2,'Gina',' Leal'); INSERT INTO Users VALUES (3,'Gordon'' Owyang'); INSERT INTO Users VALUES (4,'Hasheem',' Whitmore'); INSERT INTO Users VALUES (5,'Janelle',' Graham'); INSERT INTO Users VALUES (6,'Kim',' Glazzard'); INSERT INTO Users VALUES (7,'Kim',' Nguyen'); INSERT INTO Users VALUES (8,'Laleh',' Rastegarzadeh'); INSERT INTO Users VALUES (9,'Leo',' Barsukov'); INSERT INTO Users VALUES (10,'Marty','Gunn'); INSERT INTO Users VALUES (11,'Min',' Wu'); INSERT INTO Users VALUES (12,'Tyler',' Jennings'); INSERT INTO Users VALUES (13,'Bill',' Stuart'); INSERT INTO Users VALUES (14,'Xiaoying',' Zhou');"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Meetings VALUES( 0, '2017-07-31 12:05:00', 3, 7, 12, 6, 9, 1, 0, 8, 11, 5, 14, 13 );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; SELECT * FROM Users"

install -o root -g root -m 755 -v /vagrant/latexdb-0.3/code/* /usr/local/bin/



SELECT a.Date, tod.FirstName Toastmaster, i.FirstName Inspirational , j.FirstName Joke, ge.FirstName GeneralEvaluator, e1.FirstName Evaluator1, e2.FirstName Evaluator2, t.FirstName Timer, bc.FirstName BallotCounter, g.FirstName Grammarian, tt.FirstName TableTopics, s1.FirstName Speaker1, s2.FirstName Speaker2
FROM  Meetings a 
LEFT JOIN Users tod On a.Toastmaster = tod.id
LEFT JOIN Users i On a.Inspirational = i.id
LEFT JOIN Users j On a.Joke = j.id
LEFT JOIN Users ge On a.GeneralEvaluator = ge.id
LEFT JOIN Users e1 On a.Evaluator1 = e1.id
LEFT JOIN Users e2 On a.Evaluator2 = e2.id
LEFT JOIN Users t On a.Timer = t.id
LEFT JOIN Users bc On a.BallotCounter = bc.id
LEFT JOIN Users g On a.Grammarian = g.id
LEFT JOIN Users tt On a.TableTopics = tt.id
LEFT JOIN Users s1 On a.Speaker1 = s1.id
LEFT JOIN Users s2 On a.Speaker2 = s2.id;


