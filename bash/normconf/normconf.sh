#!/bin/bash
filename='conf.txt'
content=$(cat $filename)
echo $content

while IFS=$'\n' read -ra array; do
    for i in "${array[@]}"; do
        s=$(echo $i | awk '/mm|sm|dm|m|km/ {print $0}')
        if [[ -n $s ]]; then
            echo $i | cut -d '=' -f 2
        fi
    done
 done <<< "$content"