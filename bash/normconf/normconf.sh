#/bin/bash
case "$1" in                                                                                                          
        "-h"|"--help") 
        echo -e "Script makes your config normal\n1st arg should be config file name"
        exit 0;;
        "") echo "no file spicified"
        exit 1;;
esac

filename=$1
if [[ ! -f $1 ]]; then
	echo "file does not exist"
	exit 1;
fi
if [[ ! -r $1 ]]; then
    echo "read access denied"
    exit 1;
fi

fileContent=$(cat "$filename")

declare -A unitsValues=( ['s']=1 ['min']=60 ['h']=3600 ['d']=24*3600 ['mm']=0.001 ['sm']=0.01 
['dm']=0.1 ['m']=1 ['km']=1000 ['mg']=0.000001 ['g']=0.001 ['kg']=1 ['t']=1000 )

declare -A unitsGroups=( ['s']=1 ['min']=1 ['h']=1 ['d']=1 ['mm']=2 ['sm']=2 
['dm']=2 ['m']=2 ['km']=2 ['mg']=3 ['g']=3 ['kg']=3 ['t']=3 )

Evaluate(){
    res=0
    isSubtraction=false
    group=-1
    while IFS=$' ' read -ra array; do
        for i in "${array[@]}"; do
            if [[ $i == "-" ]]; then
                isSubtraction=true
            elif [[ $i == "+" ]]; then
                continue
            else 
                value=${i//[^0-9]/}
                unit=${i//[0-9]/}

                if [[ ${unitsValues[$unit]} == "" ]]; then
                    echo "unknown units $unit; line: $2"
                    return 
                fi

                if [[ $group -eq -1 ]]; then
                    group=${unitsGroups[$unit]}
                elif [[ group -ne ${unitsGroups[$unit]} ]]; then
                        echo "incompatible units $unit; line: $2"
                        return
                    fi

                if [[ "$isSubtraction" == true ]]; then
                    res=$(echo "scale=10; x=$res-$value*${unitsValues[$unit]}; if(x<1 && x>0) print 0; x" | bc -q )
                    isSubtraction=false
                else
                    res=$(echo "scale=10; x=$res+$value*${unitsValues[$unit]}; if(x<1 && x>0) print 0; x" | bc -q )
                fi
            fi
        done
    done <<< "$1"
    echo "$res"
}

lineNumber=-1
while IFS=$'\n' read -ra array; do
    for i in "${array[@]}"; do
        lineNumber=$(( lineNumber + 2 ))
        expr=$(echo "$i" | cut -d '=' -f 2)
        name=$(echo "$i" | cut -d '=' -f 1)
        echo "$name=$(Evaluate "$expr" "$lineNumber")"
    done
done <<< "$fileContent"

