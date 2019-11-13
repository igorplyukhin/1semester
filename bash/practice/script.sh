data=$(cat $1 | cut -d " " -f 1)
marks=$(cat $1 | cut -d " " -f 2)
for ((i=1;i<3;i++))
do 
printf "%s\t" "$(echo $data | cut -d \n -f $1)"
printf "$marks"
done