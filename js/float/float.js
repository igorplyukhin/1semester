'use strict'
const cnvrts = require('./convert');
const oprtns = require('./sum');

let input = '0+0';
let parsedInput = input[0] === '-'
    ? input.split(/-|\+/).splice(1, 2)
    : input.split(/-|\+/);
let a = input[0] === '-'
    ? '-' + parsedInput[0]
    : parsedInput[0];
let b = parsedInput[1];

if (b === undefined || b ===''){
    console.log(cnvrts.GetBinaryNumber(a));
}
else 
{
    let aBin = cnvrts.GetBinaryNumber(a);
    let bBin = cnvrts.GetBinaryNumber(b);
    let sumBin = oprtns.Add(aBin,bBin);
    console.log(sumBin);
    console.log(cnvrts.GetDecimalNumber(cnvrts.FloatParse(sumBin)));
}
//сумма разных порядков
//разность
//разобраться с нулем

