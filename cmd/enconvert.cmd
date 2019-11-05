@echo off
setlocal
if "%1"=="/?" (
    echo Program converts cp886 file into utf8 in specified directory
    echo Type directory
    exit /b
)

if "%1"=="" (
    echo Type directory
    exit /b
)

set isEmpty=0
set directory=%1
set tmpname="tmp"
if exist "%~d0\%~p0\%tmpname%.txt" (  
        echo tmpfile exists!!
        exit /b
    )
for /r %directory% %%f in (*.txt) do (
    type %%f> %tmpname%.txt
    if %errorlevel% neq 0 (
        echo Error while writing tmpfile
    )
    cmd /u /c type %tmpname%.txt> %%f
    if %errorlevel% neq 0 (
        echo Error while rewriting source file
    )
    isEmpty=1
)

if %isEmpty% equ 0 (
    echo No txt files in this directiory
) else (
    del %tmpname%.txt
    echo success
)

