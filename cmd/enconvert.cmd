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
set directory=%1
set tmpname="tmp"
if exist "%~d0\%~p0\%tmpname%.txt" (  ::~d0 current disk ~p0 ctalog without disk and executing file
         echo tmpfile exists!!
         exit /b
    )
for /r %directory% %%f in (*.txt) do (
    type %%f> %tmpname%.txt
    cmd /u /c type %tmpname%.txt> %%f
    echo %%f was converted
)
echo success
del %tmpname%.txt
endlocal
