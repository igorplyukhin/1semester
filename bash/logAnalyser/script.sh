#!/bin/bash
filename=$1

values=$(cat $filename | grep 'ERROR\|FATAL'  | cut -d \| -f 5-)
echo $values | awk '{
    sum=0
    for (i=1; i<=NF; i++)
        sum += $i
    print sum
}'

 