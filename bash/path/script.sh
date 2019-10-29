#!/bin/bash
if [ "$1" = "-h"] || ["$1" = "--help" ]; then 
    echo "$0 [-h]"
    echo "This script shows folders with executable files in spicified dir"
    echo "arg1 you may optionally spicify the dir"
    exit 0;
fi

if [ "$1" ];
then
    dir=$1
else 
    dir=$PATH
fi

emptyFoldersCount=0
IFS=:

for folder in $dir
do
    k=0 
    for file in "$folder"/*
    do
    if [ -x "$file" ]
    then    
        k=1
    fi
    done

    if [ $k -eq 1 ]
    then
    echo "$folder"
    else
    emptyFoldersCount=$((emptyFoldersCount+1))
    fi
done

echo "$emptyFoldersCount Folders was empty"