#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters.Web/Toastmasters.Web/
dotnet build /vagrant/Toastmasters.Web/Toastmasters.Web/
sudo systemctl stop kestrel-toastmasters.service
sudo dotnet publish /vagrant/Toastmasters.Web/Toastmasters.Web/ -o /var/aspnetcore/toastmasters/
sudo systemctl start kestrel-toastmasters.service