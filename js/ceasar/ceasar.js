const fs = require('fs');

inFile = 'test.txt';
outFile = 'output.txt';
let shift = 3;
let EngAlphLen = 26;
let RusAlphLen = 33;
let LowerEngAlph = 'abcdefghijklmnopqrstuvwxyz';
let UpperEngAlph = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
let LowerRusAlph = 'абвгдеёжзиёклмнопрстуфхцчшщъыьэюя';
let UpperRusAlph = 'абвгдеёжзиёклмнопрстуфхцчшщъыьэюя';
let message = fs.readFileSync(inFile, 'utf8');


function Encrypt(message, upperAlph, lowerAlph, alphLen, shift) {
    let outStr = "";
    for (let i = 0; i < message.length; i++) {
        ch = message[i];
        if (lowerAlph.includes(ch)) {
            let newChCode = Math.abs((lowerAlph.indexOf(ch) + shift) % alphLen);
            outStr += lowerAlph[newChCode];
        }
        else if (upperAlph.includes(ch)) {
            let newChCode = Math.abs((upperAlph.indexOf(ch) + shift) % alphLe);
            outStr = upperAlph[newChCode];
        }
        else
            outStr += ch;
    }
    return outStr;
}

function Decrypt(message, upperAlph, lowerAlph) {
    let freqTable = CalcRelativeFreq(message, upperAlph, lowerAlph);
}

function CalcRelativeFreq(str, upperAlph,lowerAlph) {
    let freqTable = new Map()
    let lettersCount = 0;
    for (let i = 0; i < str.length; i++) {
        ch=str[i];
        if (upperAlph.includes(ch) || lowerAlph.includes(ch)){
            lettersCount++;
            if (freqTable.has(ch))
                freqTable.set(ch,freqTable.get(ch)+1);
            else 
                freqTable.set(ch,1);
        }
    }

    let keys =freqTable.keys();
    for (let key of keys){
        freqTable.set(key,freqTable.get(key) / lettersCount * 100);
    }
    return freqTable;
}


console.log(CalcRelativeFreq(message,UpperEngAlph,LowerEngAlph))
//console.log(Encrypt(message, UpperEngAlph, LowerEngAlph, EngAlphLen, shift));