const fs = require('fs');
let entropy = 0;
let fileContent = fs.readFileSync(process.argv[2], "utf8");
let count = new Map();

function getBaseLog(x, y) {
    return Math.log(y) / Math.log(x);
}

for (let i = 0; i < fileContent.length; i++) {
    if (count.has(fileContent[i]))
        count.set(fileContent[i], parseInt(count.get(fileContent[i])) + 1);
    else
        count.set(fileContent[i], 1);
}

let x = count.size;
if (process.argv[3]) {
    if (Number(process.argv[3]))
        x = process.argv[3];
}

for (let key of count.keys()) {
    console.log(key, ":", count.get(key));
    count.set(key, count.get(key) / count.size);
    entropy = count.get(key) + getBaseLog(x, (1 / count.get(key)));
}

console.log(entropy.toFixed(2));

