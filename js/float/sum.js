'use strict';
const cnvrts = require('./convert');
module.exports = {
    Add : Add,
}

function Add(x, y) {
    let xParsed = cnvrts.FloatParse(x);
    let yParsed = cnvrts.FloatParse(y);
    let xIntPart = BigInt(xParsed.intPart + xParsed.fracPart);
    let yIntPart = BigInt(yParsed.intPart + yParsed.fracPart);
    let sum =0;
    if (xParsed.shift === yParsed.shift) {
        sum = GetRidOfTwos(xIntPart + yIntPart);
        let shiftDelta = sum.length - 24;
        let newShift = GetRidOfTwos(BigInt(x.substr(1,8)) + 1n);
        return xParsed.sign + newShift  + sum.slice(shiftDelta, -1);
    }
}

function BinaryAdd(x,y) {
    let ans = "";
    let temp = 0
    let mem = 0
    for (let i = x.length - 1; i >= 0; i--) {
        let a =parseInt(x[i]);
        temp = (parseInt(x[i]) + parseInt(y[i]) + mem) % 2
        mem = 0
        ans = temp.toString() + ans
        if(Number(x[i]) + Number(y[i]) == 2)
            mem = 1
    }
    return ans;
}

console.log(BinaryAdd('111',"001"));