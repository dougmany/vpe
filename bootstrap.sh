DBNAME=toastmasters
ROOTPASSWD=test123
DBUSER=dbuser
DBPASSWD=test123

apt-get update
# MySQL setup for development purposes ONLY
echo -e "\n--- Install MySQL specific packages and settings ---\n"
debconf-set-selections <<< "mysql-server mysql-server/root_password password $ROOTPASSWD"
debconf-set-selections <<< "mysql-server mysql-server/root_password_again password $ROOTPASSWD"
apt-get -y install mysql-server python-mysqldb

echo -e "\n--- Seed Data ---\n"

mysql -uroot -p$ROOTPASSWD -e "CREATE DATABASE $DBNAME"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'localhost' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Users ( id INT PRIMARY KEY, FirstName VARCHAR(40), LastName VARCHAR(40), IsPresident BOOLEAN, IsSargent BOOLEAN );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Meetings ( id INT PRIMARY KEY, Date DateTime, Toastmaster INT, Inspirational INT, Joke INT, GeneralEvaluator INT, Evaluator1 INT, Evaluator2 INT, Timer INT, BallotCounter INT, Grammarian INT, TableTopics INT, Speaker1 INT, Speaker2 INT, President INT, Sargent INT );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Users VALUES (0,'Dale','Rinde',1,0); INSERT INTO Users VALUES (1,'Doug','Meeker',0,0); INSERT INTO Users VALUES (2,'Gina','Leal',0,0); INSERT INTO Users VALUES (3,'Gordon','Owyang',0,0); INSERT INTO Users VALUES (4,'Hasheem','Whitmore',0,0); INSERT INTO Users VALUES (5,'Janelle','Graham',0,0); INSERT INTO Users VALUES (6,'Kim','Glazzard',0,0); INSERT INTO Users VALUES (7,'Kim','Nguyen',0,0); INSERT INTO Users VALUES (8,'Laleh','Rastegarzadeh',0,0); INSERT INTO Users VALUES (9,'Leo','Barsukov',0,1); INSERT INTO Users VALUES (10,'Marty','Gunn',0,0); INSERT INTO Users VALUES (11,'Min','Wu',0,0); INSERT INTO Users VALUES (12,'Tyler','Jennings',0,0); INSERT INTO Users VALUES (13,'Bill','Stuart',0,0); INSERT INTO Users VALUES (14,'Xiaoying','Zhou',0,0);"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Meetings VALUES( 0, '2017-07-31 12:05:00', 3, 7, 12, 6, 9, 1, 0, 8, 11, 5, 14, 13, 0, 9 );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; SELECT * FROM Users"

echo -e "\n--- Install Latex ---\n"

#echo 'selected_scheme scheme-meduim' > /vagrant/temp.profile
#/vagrant/install-tl/install-tl -profile /vagrant/temp.profile
#tlmgr install meetingmins

# Download and unpack the latest TeX Live tarball
wget -q http://mirror.ctan.org/systems/texlive/tlnet/install-tl-unx.tar.gz || error_exit "Failure getting texlive tarball"
tar -xzf install-tl-unx.tar.gz || error_exit "Failure unpacking texlive tarball"
rm -f install-tl-unx.tar.gz

# Install with the meduim scheme
cd install-tl-20* || error_exit "Failure changing to texlive install directory"
echo 'selected_scheme scheme-meduim' > temp.profile
./install-tl -profile temp.profile || error_exit "Failure installing texlive core"
rm -f temp.profile

# Add the bin directory to the path
echo 'export PATH=/usr/local/texlive/2017/bin/x86_64-linux:$PATH' >> ~/.profile
source ~/.profile

install -o root -g root -m 755 -v /vagrant/latexdb-0.3/code/* /usr/local/bin/


 # full scheme (everything) 
 # medium scheme (small + more packages and languages)
 # small scheme (basic + xetex, metapost, a few languages) 
 # basic scheme (plain and latex)
 # minimal scheme (plain only)
 # ConTeXt scheme
 # GUST TeX Live scheme 
 # infrastructure-only scheme (no TeX at all)
 # teTeX scheme (more than medium, but nowhere near full) 
 # custom selection of collections


