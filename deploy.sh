#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/ -c Release
sudo systemctl stop kestrel-toastmasters.service

sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.web/

sudo systemctl stop kestrel-toastmasters-carborators.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.carborators.web/

sed -i 's/5000/5002/g' /var/aspnetcore/toastmasters.carborators.web/hosting.json
sed -i 's/database=toastmasters;/database=toastmastersCarborators;/g' /var/aspnetcore/toastmasters.carborators.web/appsettings.json
sed -i 's/"CookieName": "toastmasters"/"CookieName": "toastmasterscarborators"/g' /var/aspnetcore/toastmasters.carborators.web/appsettings.json
sed -i 's/\\home\\www-data\\log\\/\\home\\www-data\\log-carborators\\/g' /var/aspnetcore/toastmasters.carborators.web/nlog.config

sudo systemctl stop kestrel-toastmasters-trashtalkers.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.trashtalkers.web/

sed -i 's/5000/5003/g' /var/aspnetcore/toastmasters.trashtalkers.web/hosting.json
sed -i 's/database=toastmasters;/database=toastmastersTrashTalkers;/g' /var/aspnetcore/toastmasters.trashtalkers.web/appsettings.json
sed -i 's/"CookieName": "toastmasters"/"CookieName": "toastmasterstrashtalkers"/g' /var/aspnetcore/toastmasters.trashtalkers.web/appsettings.json
sed -i 's/\\home\\www-data\\log\\/\\home\\www-data\\log-trashtalkers\\/g' /var/aspnetcore/toastmasters.trashtalkers.web/nlog.config

sudo systemctl start kestrel-toastmasters.service
sudo systemctl start kestrel-toastmasters-carborators.service
sudo systemctl start kestrel-toastmasters-trashtalkers.service

sudo chown :www-data /vagrant/agenda/AgendaTemplate.tex
sudo chmod g+w /vagrant/agenda/AgendaTemplate.tex
sudo chown :www-data /vagrant/agenda/EmailTemplate.tex
sudo chmod g+w /vagrant/agenda/EmailTemplate.tex
