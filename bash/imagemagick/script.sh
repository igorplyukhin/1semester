#!/bin/bash
if [ "$1" = "-h" ] || [ "$1" = "--help" ]; then 
    echo "$0 [-h]"
    echo "This script adds text to the rigth bottom corner of the pic"
    echo "arg1 - inputDir; arg2 - your text; arg3 - outputdir; arg4 - fontSize; arg5 - fontName"
    exit 0;
fi

if [ $# -lt 3 ] || [ $# -gt 5 ] 
  then
    echo "There should be from 3 to 5 arg"
    exit 1;
fi

inputdir=$1
text=$2
outputdir=$3
fontSize=$4
fontName=$5
suffics="_annotated"
f=$(find "$inputdir" -name \*jpg)
for file in $f
do
filename=$(basename -- "$file")
filename="${filename%.*}"
echo "$filename"" was converted"
convert "$file" -fill white -gravity SouthEast \
    -pointsize $fontSize -font $fontName -annotate +0+0 "$text" "$outputdir$filename$suffics.jpg" 
done
