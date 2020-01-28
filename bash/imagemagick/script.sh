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

if [[ ! -d $1 ]]; then
	echo "input dir doesn\`t exist"
	exit 1;
fi

if [[ ! -d $3 ]]; then
	mkdir $3
fi

if ! [[ -w $3 && -x $3 ]]; then    
   echo "access to output folder denied"
   exit 1;
fi

inputdir=$1
text=$2
outputdir=$3

if [[ -n $4 && "$4" =~ ^[0-9]+$ ]]; then
    fontSize=$4
else 
    fontSize=55
fi

if [[ -n $5 ]]; then
    fontName=$5
else 
    fontName='Waree-Oblique'
    
fi

suffics="_annotated"
f=$(find "$inputdir" -name \*.jpg)
for file in $f
do
    filename=$(basename "$file")
    filename="${filename%.*}"
    convert "$file" -fill white -gravity SouthEast \
        -pointsize $fontSize -font $fontName -annotate +0+0 "$text" "$outputdir/$filename$suffics.jpg" 
    echo "$filename"" was converted"
done
