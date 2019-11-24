@echo off

del /S /Q /F *.csproj.user
del /S /Q /F .\*.suo
rd /S /Q .\.vs
rd /S /Q .\Anguista\obj
rd /S /Q .\AnguistaLib\obj

cd .\bin\Release
del *.exe.config *.pdb *.exe.manifest log4net.xml *.vshost.exe
cd ..\..
cd .\bin\Debug
del *.exe.config *.pdb *.exe.manifest log4net.xml *.vshost.exe
cd ..\..

