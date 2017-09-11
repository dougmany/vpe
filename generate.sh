#!/bin/bash
#generate.sh

dotnet /var/aspnetcore/toastmasters.tex/Toastmasters.Tex.dll
latex2rtf /vagrant/agenda/variableTest1.tex