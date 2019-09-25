@echo off
setlocal
if "%1"=="/?" (
    echo Program says hello
    echo.
    echo %0 [username]
    exit /b
)
if "%1"=="" (
set /p username=who are you?
) else (
    set username=%1
)
echo hello, %username%
endlocal