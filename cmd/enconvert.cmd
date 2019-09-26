@echo off
if "%1"=="/?" (
    echo Program converts cp886 file into utf8 in specified directory
    echo Type directory as 1 arg
    exit /b
)

if "%1"=="" (
    echo Type directory
    exit /b
)
set directory=%1
set tmpname="tmp"
for /r %directory% %%f in (*.txt) do (
    
    if exist "%directory%\%tmpname%.txt" (  
         set tmpname=%random%
         echo %tmpname%
    )
    type %%f> %tmpname%.txt
    cmd /U /C type %tmpname%.txt> %%f
    del %tmpname%.txt
    echo %%f was converted
)

