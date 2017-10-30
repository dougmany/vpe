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

echo -e "\n--- Create database ---\n"

mysql -uroot -p$ROOTPASSWD -e "CREATE DATABASE $DBNAME"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'localhost' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "grant all privileges on $DBNAME.* to '$DBUSER'@'10.0.2.2' identified by '$DBPASSWD'"
mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Members ( MemberID INT AUTO_INCREMENT, FirstName VARCHAR(40), LastName VARCHAR(40), Email VARCHAR(100), IsPresident BOOLEAN, IsSargent BOOLEAN, PRIMARY KEY (MemberID) );"

mysql -uroot -p$ROOTPASSWD -e "USE $DBNAME; CREATE table Meetings ( MeetingID INT AUTO_INCREMENT, MeetingDate DateTime, ToastmasterMemberID INT, InspirationalMemberID INT, JokeMemberID INT, GeneralEvaluatorMemberID INT, EvaluatorIMemberID INT, EvaluatorIIMemberID INT, TimerMemberID INT, BallotCounterMemberID INT, GrammarianMemberID INT, TableTopicsMemberID INT, SpeakerIMemberID INT, SpeakerIIMemberID INT, PresidentMemberID INT, SargentMemberID INT, AbsentIMemberID INT, AbsentIIMemberID INT, PRIMARY KEY (MeetingID) );"

mysql  -uroot -p$ROOTPASSWD $DBNAME < /vagrant/MemberSeed.sql
mysql  -uroot -p$ROOTPASSWD $DBNAME < /vagrant/MeetingSeed.sql

mkdir /var/aspnetcore/
mkdir /var/aspnetcore/toastmasters.web/

dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/
dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -o /var/aspnetcore/toastmasters.web/

mkdir /var/aspnetcore/toastmasters.tex/
dotnet restore /vagrant/Toastmasters/Toastmasters.Tex/
dotnet build /vagrant/Toastmasters/Toastmasters.Tex/
dotnet publish /vagrant/Toastmasters/Toastmasters.Tex/ -o /var/aspnetcore/toastmasters.tex/

cp /vagrant/nginxDefault /etc/nginx/sites-available/default
nginx -s reload

cp /vagrant/toastmasters.service /etc/systemd/system/kestrel-toastmasters.service
systemctl enable /etc/systemd/system/kestrel-toastmasters.service
systemctl start  kestrel-toastmasters.service
systemctl status kestrel-toastmasters.service


