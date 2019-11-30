'use strict'
const cnvrts = require('./convert');
const oprtns = require('./sum');

let a = cnvrts.GetBinaryNumber('45672431');
let b = cnvrts.GetBinaryNumber('45672431');
let c = oprtns.Add(a,b);
console.log(c);
let d = cnvrts.FloatParse(c);
console.log(cnvrts.GetDecimalNumber(d.intPart, d.fracPart, d.sign));