#!/bin/bash
case "$1" in
    "-h"|"--help")
        echo "Script shows fodlers without opened files"
        exit 0;;
    "-d"|"--directory")
        dir=$2;;
    "")
        dir=$(pwd);;
esac

if ! [[ -d $dir ]]; then
    echo 'No such directory'
    exit 1;
fi

openedFiles=$(lsof +D "$dir" | awk ' $1!="NAME " {print $9":"}')
emptyFolderExist=0

shopt -s globstar nullglob
for folder in "$dir"/*
do
    count=0
    if ! [[ -d $folder ]]; then
        continue
    fi

    for file in "$folder"/**
    do
        if ! [[ -f $file ]]; then
            continue
        fi
        
        if echo "$openedFiles" | grep -q "$file" 
        then
            count=1
        fi
    done

    if [[ count -eq 0 ]]; then
        echo "$folder"
        emptyFolderExist=1
    fi
done

if [[ emptyFolderExist -eq 0 ]]; then
    exit 1;
fi

