#!/bin/bash

for path in $PATH; do
    if [ -f "$path/$1" -a -x "$path/$1" ]; then
    echo "path/$1";
    fi
done