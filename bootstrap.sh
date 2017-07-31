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
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Users VALUES (0,'Doug','Meeker'); INSERT INTO Users VALUES (1,'Dale','Rinde'); INSERT INTO Users VALUES (2,'Leo','Barsukov');"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; SELECT * FROM Users"

install -o root -g root -m 755 -v /vagrant/latexdb-0.3/code/* /usr/local/bin/
