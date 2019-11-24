REM https://msdn.microsoft.com/ja-jp/library/ccec7sz1(v=vs.110).aspx#Strong

set PATH=C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools
resgen.exe .\Resources.en-US.resx /str:c#,Anguista.Main.Properties,Resources,Resources.en-US.Designer.cs /publicClass

@echo *******************************************************
@echo *   Replace 'internal Resources()' with 'public ..'   *
@echo *******************************************************

@echo off
pause
