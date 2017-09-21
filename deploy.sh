#!/bin/bash
# file: deploy.sh
dotnet restore /vagrant/Toastmasters/Toastmasters.Web/
dotnet build /vagrant/Toastmasters/Toastmasters.Web/ -c Release
sudo systemctl stop kestrel-toastmasters.service
sudo dotnet publish /vagrant/Toastmasters/Toastmasters.Web/ -c Release -o /var/aspnetcore/toastmasters.web/
#sudo cp /vagrant/Toastmasters/Toastmasters.Web/bin/Release/netcoreapp1.0/* /var/aspnetcore/toastmasters.web/
sudo systemctl start kestrel-toastmasters.service

dotnet restore /vagrant/Toastmasters/Toastmasters.Tex/
dotnet build /vagrant/Toastmasters/Toastmasters.Tex/ -c Release
#dotnet publish /vagrant/Toastmasters/Toastmasters.Tex/ -c Release
sudo cp /vagrant/Toastmasters/Toastmasters.Tex/bin/Release/netcoreapp1.0/* /var/aspnetcore/toastmasters.tex/
