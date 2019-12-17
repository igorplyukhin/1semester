const fs = require('fs');
const LowerEngAlph = 'abcdefghijklmnopqrstuvwxyz';
const LowerRusAlph = 'абвгдеёжзиёклмнопрстуфхцчшщъыьэюя';

let inFile = process.argv[3];
let outFile = process.argv[4];
if (!fs.existsSync(inFile)){
    console.log('file doesn\'t exist');
    process.exit(-1);
}
if (!fs.existsSync('trainingSample.txt')){
    console.log('trainingSample.txt required');
    process.exit(-1);
}

let message = fs.readFileSync(inFile, 'utf8');
let trainFile = fs.readFileSync('trainingSample.txt', 'utf8')
let language = '';
let shift = 0;

let mode = process.argv[2];
switch (mode){
    case 'code':
        shift = parseInt(process.argv[5]);
        language = process.argv[6];
        switch (language){
            case 'ru':
                fs.writeFileSync(outFile, Encrypt(message, LowerRusAlph, shift));
                break;
            case 'en':
                fs.writeFileSync(outFile, Encrypt(message, LowerEngAlph, shift));
                break;
            default:
                console.log('wrong lang');
                process.exit(-1);
        }
        break;
    case 'decode':
        language = process.argv[5];
        switch (language){
            case 'ru':
                fs.writeFileSync(outFile, Decrypt(message, LowerRusAlph).decryptedMessage);
                break;
            case 'en':
                fs.writeFileSync(outFile, Decrypt(message, LowerEngAlph).decryptedMessage);
                break;
            default:
                console.log('wrong lang');
                process.exit(-1);
        }
        break;
}

// let inFile = 'input.txt';
// let outFile = 'output.txt';
// let trainFile = fs.readFileSync('trainingSample.txt', 'utf8')
// let message = fs.readFileSync(inFile, 'utf8');
// d=Encrypt(message, LowerEngAlph, 6);
// fs.writeFileSync(outFile, d)
// message = fs.readFileSync('output.txt', 'utf8');
// Decrypt(message, LowerEngAlph);

function Encrypt(message, lowerAlph, shift) {
    let outStr = "";
    let upperAlph = lowerAlph.toUpperCase();
    for (let i = 0; i < message.length; i++) {
        ch = message[i];
        if (lowerAlph.includes(ch)) {
            let newChCode = Math.abs((lowerAlph.length + lowerAlph.indexOf(ch) + shift) % lowerAlph.length);
            outStr += lowerAlph[newChCode];
        }
        else if (upperAlph.includes(ch)) {
            let newChCode = Math.abs((lowerAlph.length + upperAlph.indexOf(ch) + shift) % lowerAlph.length);
            outStr += upperAlph[newChCode];
        }
        else
            outStr += ch;
    }
    return outStr;
}

function FindMostOftenLetter(map){
    let keys = map.keys();
    let max = 0;
    let maxKey = 0;
    for (let k of keys)
    {
        let e = map.get(k);
        if ( e > max){
            max = e;
            maxKey = k;
        }
    }
    return maxKey;
}

function Decrypt(message, lowerAlph) {
    let upperAlph = lowerAlph.toUpperCase();
    let alphFreqTable = CalcRelativeFreq(trainFile, lowerAlph);
    console.log(alphFreqTable);
    let textFreqTable = CalcRelativeFreq(message, lowerAlph);
    console.log(textFreqTable);
    let alphMostFreqLetter = FindMostOftenLetter(alphFreqTable);
    let textMostFreqLetter = FindMostOftenLetter(textFreqTable);
    let shift = 
        (lowerAlph.length + lowerAlph.indexOf(alphMostFreqLetter) 
        - lowerAlph.indexOf(textMostFreqLetter)) % lowerAlph.length;    
    let decryptedMessage = "";
    for (let i=0; i < message.length; i++){
        let ch = message[i];
        if (lowerAlph.includes(ch)){
            decryptedMessage+=lowerAlph[(lowerAlph.indexOf(ch) + shift) % lowerAlph.length];
        }
        else if (upperAlph.includes(ch)){
            decryptedMessage+=upperAlph[(upperAlph.indexOf(ch) + shift) % lowerAlph.length]
        }
        else {
            decryptedMessage+=ch;
        }
    }
    return {
        shift,
        decryptedMessage,
    }
}

function CalcRelativeFreq(str, lowerAlph) {
    let freqTable = new Map()
    let lettersCount = 0;
    let upperAlph = lowerAlph.toUpperCase();
    for (let i = 0; i < str.length; i++) {
        ch=str[i];
        if (upperAlph.includes(ch) || lowerAlph.includes(ch)){
            ch=ch.toLowerCase();
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
