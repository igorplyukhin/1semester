'use strict'
const cnvrts = require('./convert');
const oprtns = require('./sum');

let input = '100000000+2';
let splitedStr;
let isMinus = false;
if (input[0] === "+")
    input = input.substr(1);
if (input[0] === '-') {
    input = input.substr(1);
    isMinus = true;
}
if (input.indexOf('+') !== -1) {
    splitedStr = input.split('+');
}
else if (input.lastIndexOf('-') !== -1) {
    splitedStr = input.split('-');
    splitedStr[1] = '-' + splitedStr[1];
}
else {
    if (isMinus)
        input = '-' + input;
    splitedStr = [input];
}
let a = isMinus
    ? '-' + splitedStr[0]
    : splitedStr[0];
let b = splitedStr[1];


if (b === undefined || b === '') {
    console.log(cnvrts.GetBinaryNumber(a));
}
else {
    let aBin = cnvrts.GetBinaryNumber(a);
    let bBin = cnvrts.GetBinaryNumber(b);
    let sumBin = oprtns.Add(aBin, bBin);
    console.log(sumBin);
    console.log(cnvrts.GetDecimalNumber(cnvrts.FloatParse(sumBin)));
}
//сумма разных порядков
//разность
//разобраться с нулем
//includes

