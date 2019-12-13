#!/bin/bash
filename='conf.txt'
fileContent=$(cat $filename)
echo $fileContent

while IFS=$'\n' read -ra array; do
    for i in "${array[@]}"; do
        s=$(echo $i | awk '/mm|sm|dm|m$|km/ {print $0}')
        if [[ -n $s ]]; then
            echo $i | cut -d '=' -f 2 
            continue
        fi
        s=$(echo $i | awk '/s$|min|h$|d$/ {print $0}')
        if [[ -n $s ]]; then
            echo $i | cut -d '=' -f 2 
            continue
        fi
        s=$(echo $i | awk '/g$|kg|t$/ {print $0}')
        if [[ -n $s ]]; then
            echo $i | cut -d '=' -f 2 
            continue
        fi
        echo  $i 'unknown units'
        break 2
    done
 done <<< "$fileContent"