'use strict'
const cnvrts = require('./convert');
const oprtns = require('./sum');

let input = process.argv[2];
let splitedStr;
let firstChIsMinus = false;
let isAddition = false;
if (input[0] === "+")
    input = input.substr(1);
if (input[0] === '-') {
    input = input.substr(1);
    firstChIsMinus = true;
}
if (input.indexOf('+') !== -1) {
    isAddition = true;
    splitedStr = input.split('+');
}
else if (input.lastIndexOf('-') !== -1) {
    splitedStr = input.split('-');
    splitedStr[1] = '-' + splitedStr[1];
}
else {
    if (firstChIsMinus)
        input = '-' + input;
    splitedStr = [input];
}
let a = firstChIsMinus
    ? '-' + splitedStr[0]
    : splitedStr[0];
let b = splitedStr[1];


if (b === undefined || b === '') {
    console.log(cnvrts.GetBinaryNumber(a));
}
else if (isAddition) {
    ShowResult(a, b, oprtns.BinaryAdd);
}
else {
    ShowResult(a, b, oprtns.BinarySubtract);
}

function ShowResult(a, b, operation) {
    let aBin = cnvrts.GetBinaryNumber(a);
    let bBin = cnvrts.GetBinaryNumber(b);
    let sumBin = oprtns.Add(aBin, bBin, operation);
    console.log(sumBin);
    console.log(cnvrts.GetDecimalNumber(sumBin));
}




