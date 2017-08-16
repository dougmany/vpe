DBNAME=toastmasters
ROOTPASSWD=test123
DBUSER=dbuser
DBPASSWD=test123

apt-get update
# MySQL setup for development purposes ONLY
echo -e "\n--- Install MySQL specific packages and settings ---\n"
debconf-set-selections <<< "mysql-server mysql-server/root_password password $ROOTPASSWD"
debconf-set-selections <<< "mysql-server mysql-server/root_password_again password $ROOTPASSWD"

sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys B02C46DF417A0893
apt-get update

apt-get -y install mysql-server latex2rtf dotnet-dev-1.0.4 nginx

echo -e "\n--- Seed Data ---\n"

mysql -uroot -p$ROOTPASSWD -e "CREATE DATABASE $DBNAME"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'localhost' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'10.0.2.2' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Members ( MemberID INT AUTO_INCREMENT, FirstName VARCHAR(40), LastName VARCHAR(40), Email VARCHAR(100), IsPresident BOOLEAN, IsSargent BOOLEAN, PRIMARY KEY (MemberID) );"

mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Meetings ( MeetingID INT AUTO_INCREMENT, MeetingDate DateTime, ToastmasterMemberID INT, InspirationalMemberID INT, JokeMemberID INT, GeneralEvaluatorMemberID INT, EvaluatorIMemberID INT, EvaluatorIIMemberID INT, TimerMemberID INT, BallotCounterMemberID INT, GrammarianMemberID INT, TableTopicsMemberID INT, SpeakerIMemberID INT, SpeakerIIMemberID INT, PresidentMemberID INT, SargentMemberID INT, AbsentIMemberID INT, AbsentIIMemberID INT, PRIMARY KEY (MeetingID) );"

mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Members VALUES (1,'Dale','Rinde','Dale.Rinde@LC.CA.GOV',1,0);INSERT INTO Members VALUES (2,'Doug','Meeker','Doug.Meeker@arb.ca.gov',0,0);INSERT INTO Members VALUES (3,'Gina','Leal','Gina.Leal@DMHC.CA.GOV',0,0);
INSERT INTO Members VALUES (4,'Gordon','Owyang','Gordon.Owyang@gartner.com',0,0);INSERT INTO Members VALUES (5,'Hasheem','Whitmore','hasheemw@gmail.com',0,0);INSERT INTO Members VALUES (6,'Janelle','Graham','Janelle.Graham@dmhc.ca.gov',0,0);INSERT INTO Members VALUES (7,'Kim','Glazzard','Kim.Glazzard@calepa.ca.gov',0,0);INSERT INTO Members VALUES (8,'Kim','Nguyen','kim.nguyen@arb.ca.gov',0,0);INSERT INTO Members VALUES (9,'Laleh','Rastegarzadeh','Laleh.Rastegarzadeh@waterboards.ca.gov',0,0);INSERT INTO Members VALUES (10,'Leo','Barsukov','Leonid.Barsukov@arb.ca.gov',0,1);INSERT INTO Members VALUES (11,'Marty','Gunn','martin.gunn@arb.ca.gov',0,0);INSERT INTO Members VALUES (12,'Min','Wu','Min.Wu@dtsc.ca.gov',0,0);INSERT INTO Members VALUES (13,'Tyler','Jennings','arachulaeus@gmail.com',0,0);INSERT INTO Members VALUES (14,'Bill','Stuart','will@wstuart.net',0,0);INSERT INTO Members VALUES (15,'Xiaoying','Zhou','Xiaoying.Zhou@dtsc.ca.gov',0,0);"

mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; INSERT INTO Meetings VALUES( 1, '2017-07-31 12:05:00', 4, 8, 13, 7, 10, 2, 1, 9, 12, 6, 15, 14, 1, 10, NULL, NULL );"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; SELECT * FROM Members"


cp /vagrant/nginxDefault /etc/nginx/sites-available/default
nginx -s reload
#cp /vagrant/kestral-toastmasters.service /etc/systemd/system/kestral-toastmasters.service
#systemctl enable kestrel-toastmasters.service



