const fs = require('fs');
const LowerEngAlph = 'abcdefghijklmnopqrstuvwxyz';
const LowerRusAlph = 'абвгдеёжзиёклмнопрстуфхцчшщъыьэюя';

let inFile = process.argv[3];
let outFile = process.argv[4];
if (!fs.existsSync(inFile)){
    console.log('file doesn\'t exist');
    process.exit(-1);
}
if (!fs.existsSync('RuTrainingSample.txt') || !fs.existsSync('EnTrainingSample.txt')){
    console.log('RuTrainingSample.txt && EnTrainingSample.txt required');
    process.exit(-1);
}

let trainFile = '';
let message = fs.readFileSync(inFile, 'utf8');
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
                trainFile = fs.readFileSync('RuTrainingSample.txt', 'utf8');
                let obj = Decrypt(message, LowerRusAlph);
                console.log(obj.shift)
                fs.writeFileSync(outFile, obj.decryptedMessage);
                break;
            case 'en':
                trainFile = fs.readFileSync('EnTrainingSample.txt', 'utf8');
                let result = Decrypt(message, LowerEngAlph);
                console.log(result.shift)
                fs.writeFileSync(outFile, result.decryptedMessage);
                break;
            default:
                console.log('wrong lang');
                process.exit(-1);
        }
        break;
}

function Encrypt(message, lowerAlph, shift) {
    let outStr = "";
    let upperAlph = lowerAlph.toUpperCase();
    shift=shift % lowerAlph.length;
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

function CalcShift(message, lowerAlph){

    let alphFreqTable = CalcRelativeFreq(trainFile, lowerAlph);
    let textFreqTable = CalcRelativeFreq(message, lowerAlph);
    let matchTable = new Map();
    let textKeys = textFreqTable.keys();

    for (let k of textKeys){
        let min = Infinity;
        let alphKeys = alphFreqTable.keys();
        let shift = 0;
        for (let k1 of alphKeys){
            let textSymb = textFreqTable.get(k);
            let alphSymb = alphFreqTable.get(k1);
            if (Math.abs(textSymb - alphSymb) < min){
                min = Math.abs(textSymb - alphSymb);
                shift = 
                (lowerAlph.length + lowerAlph.indexOf(k) - lowerAlph.indexOf(k1)) % lowerAlph.length;
            }
        }
        if (matchTable.has(shift)){
            matchTable.set(shift, matchTable.get(shift)+1);
        }
        else {
            matchTable.set(shift, 1);
        }
    }
    return FindMostOftenKey(matchTable);
}

function FindMostOftenKey(map){
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
    let shift = CalcShift(message, lowerAlph)
    let decryptedMessage = "";
    for (let i=0; i < message.length; i++){
        let ch = message[i];
        if (lowerAlph.includes(ch)){
            decryptedMessage += lowerAlph[(lowerAlph.length + lowerAlph.indexOf(ch) - shift) % lowerAlph.length];
        }
        else if (upperAlph.includes(ch)){
            decryptedMessage += upperAlph[(lowerAlph.length + upperAlph.indexOf(ch) - shift) % upperAlph.length];
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
        let ch=str[i];
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
