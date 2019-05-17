@echo off
title Gaussian TXT2CSV
set "str=%~nx1"
set "szr=%~n1
if "%str%"=="" (goto B) else goto A
pause

:A
color 02
echo 转换中
(for /f "delims=" %%a in (%str%)do echo;&for %%b in (%%a)do set/p=%%b,)<nul>%szr%_New.csv
echo 转换完毕
pause
exit

:B
color 04
echo 请将txt文件拖到程序图标上！
pause
exit