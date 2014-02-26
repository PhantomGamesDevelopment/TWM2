@echo off

echo Removing DSO files...
echo;

cd ..
del /s /q *.dso
cls

cd /d %~dp0
cd ../
start Tribes2.exe -online -dedicated -mod TWM2
cls
exit