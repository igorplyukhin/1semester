const fs = require('fs')

let Node = function (count) {
    this.count = count;
};

let inFile = process.argv[3];
let s = fs.readFileSync(inFile, "utf8");
let letters = new Map();

for (let i = 0; i < s.length; i++)
    if (letters.has(s[i]))
        letters.get(s[i]).count += 1;
    else
        letters.set(s[i], new Node(1));


let table = {};
for(var key in letters)
{
    table[key] = letters[key];
}
let temp = Object.assign(letters, {});
console.log(table);

function GetTwoMin(dict){
    let min1 = Infinity; let min1Letter;
    let min2 = Infinity; let min2Letter;
    function Min(value, key, map) {
        if (value.count <= min1) {
            min2Letter = min1Letter;
            min1Letter = key;
            min2 = min1;
            min1 = value.count;
        }
    }
    dict.forEach(Min);
    return [min1Letter, min2Letter, min1, min2];
}

while (temp.size > 1){
    let minInfo = GetTwoMin(temp);
    let min1Letter = minInfo[0];
    let min2Letter = minInfo[1];
    let min1 = minInfo[2];
    let min2 = minInfo[3];
    temp.delete(min1Letter);
    temp.delete(min2Letter);
    //table.set(min1Letter + min2Letter, new Node(min1 + min2));
    console.log(table);
    // table.get(min1Letter).code = 0;
    // table.get(min2Letter).code = 1;
    // table.get(min1Letter).parent = min1Letter + min2Letter;
    // table.get(min2Letter).parent = min1Letter + min2Letter;
    temp.set(min1Letter + min2Letter, new Node(min1 + min2));
}






