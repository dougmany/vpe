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
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'10.0.2.2' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Members ( MemberID INT AUTO_INCREMENT, FirstName VARCHAR(40), LastName VARCHAR(40), Email VARCHAR(100), IsPresident BOOLEAN, IsSargent BOOLEAN, PRIMARY KEY (MemberID) );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Meetings ( MeetingID INT AUTO_INCREMENT, MeetingDate DateTime, ToastmasterMemberID INT, InspirationalMemberID INT, JokeMemberID INT, GeneralEvaluatorMemberID INT, Evaluator1MemberID INT, Evaluator2MemberID INT, TimerMemberID INT, BallotCounterMemberID INT, GrammarianMemberID INT, TableTopicsMemberID INT, Speaker1MemberID INT, Speaker2MemberID INT, PresidentMemberID INT, SargentMemberID INT, Absent1MemberID INT, Absent2MemberID INT, PRIMARY KEY (MeetingID) );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Members VALUES (1,'Dale','Rinde','',1,0); INSERT INTO Members VALUES (2,'Doug','Meeker','',0,0); INSERT INTO Members VALUES (3,'Gina','Leal','',0,0); INSERT INTO Members VALUES (4,'Gordon','Owyang','',0,0); INSERT INTO Members VALUES (5,'Hasheem','Whitmore','',0,0); INSERT INTO Members VALUES (6,'Janelle','Graham','',0,0); INSERT INTO Members VALUES (7,'Kim','Glazzard','',0,0); INSERT INTO Members VALUES (8,'Kim','Nguyen','',0,0); INSERT INTO Members VALUES (9,'Laleh','Rastegarzadeh','',0,0); INSERT INTO Members VALUES (10,'Leo','Barsukov','',0,1); INSERT INTO Members VALUES (11,'Marty','Gunn','',0,0); INSERT INTO Members VALUES (12,'Min','Wu','',0,0); INSERT INTO Members VALUES (13,'Tyler','Jennings','',0,0); INSERT INTO Members VALUES (14,'Bill','Stuart','',0,0); INSERT INTO Members VALUES (15,'Xiaoying','Zhou','',0,0);"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Meetings VALUES( 1, '2017-07-31 12:05:00', 4, 8, 13, 7, 10, 2, 1, 9, 12, 6, 15, 14, 1, 10, NULL, NULL );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; SELECT * FROM Members"

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


