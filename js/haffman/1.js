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

let result = Object.assign({},letters);
let temp = Object.assign({},letters);

console.log(temp, result);

function  GetTwoMin(dictionary) {
    let min1 = 6875565; let min1Letter;
    let min2 = 6436436; let min2Letter;

    dictionary.forEach(e => {
        if (dictionary.get(e).count < min1)
        {
            min2Letter = min1Letter;
            min1Letter = dictionary.keys()[i];
            min2 = min1;
            min1 = dictionary.get(e).count;
        }
    });
    
    console.log(min1, min2);
    return [min1Letter, min2Letter];
}




