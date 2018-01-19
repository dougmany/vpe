#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/ -c Release
sudo systemctl stop kestrel-toastmasters-training.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.web.training/
sudo systemctl start kestrel-toastmasters-training.service

sed -i 's/5000/5001/g' /var/aspnetcore/toastmasters.web.training/hosting.json
sed -i 's/database=toastmasters;/database=toastmastersTraining;/g' /var/aspnetcore/toastmasters.web.training/appsettings.json

sudo chown :www-data /vagrant/agenda/AgendaTemplate.tex
sudo chmod g+w /vagrant/agenda/AgendaTemplate.tex
sudo chown :www-data /vagrant/agenda/EmailTemplate.tex
sudo chmod g+w /vagrant/agenda/EmailTemplate.tex
