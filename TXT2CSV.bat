@echo off
title Gaussian TXT2CSV
set "str=%~nx1"
set "szr=%~n1
if "%str%"=="" (goto B) else goto A
pause

:A
color 02
echo ת����
(for /f "delims=" %%a in (%str%)do echo;&for %%b in (%%a)do set/p=%%b,)<nul>%szr%_New.csv
echo ת�����
pause
exit

:B
color 04
echo �뽫txt�ļ��ϵ�����ͼ���ϣ�
pause
exit