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

for folder in $(find "$dir" -maxdepth 1 -not -name "."  -type d)
do
count=0
    for file in $(find "$folder" -maxdepth 1 -type f)
    do
        absFilePath=$(realpath "$file")
        str=$(echo "$openedFiles" | grep "$absFilePath")
        if [[ -n $str ]]; then 
            count=1
            emptyFolderExist=1
        fi
    done
    
if [[ count -eq 0 ]]; then
    echo "$(realpath "$folder") Doesn't have opened files"
fi
done

if [[ emptyFolderExist -eq 0 ]]; then
    exit 1;
fi