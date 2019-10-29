#!/bin/bash
if [ "$1" = "-h" ] || [ "$1" = "--help" ]; then 
    echo "$0 [-h]"
    echo "This script adds text to the rigth bottom corner of the pic"
    echo "arg1 - inputDir; arg2 - your text; arg3 - outputdir"
    exit 0;
fi

if [ $# -lt 3 ]
  then
    echo "There should be at least 3 arg"
    exit 0;
fi

inputdir=$1
text=$2
outputdir=$3
suffics="_annotated"
f=$(find "$inputdir" -name \*jpg)
for file in $f
do
filename=$(basename -- "$file")
filename="${filename%.*}"
echo "$filename"" was converted"
convert "$file" -fill white -gravity SouthEast \
    -pointsize 30 -annotate +0+0  "$text" "$outputdir$filename$suffics.jpg" 
done
