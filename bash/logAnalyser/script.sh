#!/bin/bash
case "$1" in                                                                                                          
        "-h"|"--help") 
        echo "Script sums up ERROR and FATAL requests exec time"
        exit 0;;
        "") echo "1st arg should be logfile name"
        exit 1;;
esac

if [[ $1 != *.txt || ! -f $1 ]]; then
echo "expected .txt file"
exit 1
fi

filename=$1
values=$(grep 'ERROR\|FATAL' "$filename" | cut -d \| -f 5-)
echo "$values" | awk '{
    sum=0
    for (i=1; i<=NF; i++)
        sum += $i
    print sum
}'

 