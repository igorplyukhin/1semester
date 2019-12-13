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
if [[ -z $4 ]]; then
    fontSize=55
    else 
    fontSize=$4
fi

if [[ -z $5 ]]; then
    fontName='Waree-Oblique'
    else 
    fontSize=$5
fi

suffics="_annotated"
f=$(find "$inputdir" -name \*jpg)
for file in $f
do
    filename=$(basename "$file")
    filename="${filename%.*}"
    convert "$file" -fill white -gravity SouthEast \
        -pointsize $fontSize -font $fontName -annotate +0+0 "$text" "$outputdir/$filename$suffics.jpg" 
    echo "$filename"" was converted"
done
