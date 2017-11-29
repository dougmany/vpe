#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/ -c Release
sudo systemctl stop kestrel-toastmasters.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.web/
#sudo cp /vagrant/Toastmasters/Toastmasters.Web/bin/Release/netcoreapp1.0/* /var/aspnetcore/toastmasters.web/
sudo systemctl start kestrel-toastmasters.service

sudo chown :www-data /vagrant/agenda/AgendaTemplate.tex
sudo chmod g+w /vagrant/agenda/AgendaTemplate.tex
sudo chown :www-data /vagrant/agenda/EmailTemplate.tex
sudo chmod g+w /vagrant/agenda/EmailTemplate.tex