#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/
sudo systemctl stop kestrel-toastmasters.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -o /var/aspnetcore/toastmasters/
sudo systemctl start kestrel-toastmasters.service

dotnet restore /vagrant/Toastmasters/Toastmasters.Tex/
dotnet build /vagrant/Toastmasters/Toastmasters.Tex/
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Tex/ -o /var/aspnetcore/toastmasters.Tex/

