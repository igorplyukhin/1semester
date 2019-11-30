'use strict';
const cnvrts = require('./convert');
module.exports = {
    Add : Add,
}

function Add(x, y) {
    let xParsed = cnvrts.FloatParse(x);
    let yParsed = cnvrts.FloatParse(y);
    let xBin = xParsed.intPart + xParsed.fracPart;
    let yBin = yParsed.intPart + yParsed.fracPart;
    let sum = 0;
    if (xParsed.shift === yParsed.shift) {
        sum = BinaryAdd(xBin,yBin);
        let shiftDelta = sum.length - 24;
        let newShift = BinaryAdd(x.substr(1,8), "00000001");
        return xParsed.sign + newShift + sum.slice(-23 - shiftDelta, -shiftDelta);
    }
}

function BinaryAdd(x,y) {
    let ans = "";
    let temp = 0
    let mem = 0
    for (let i = x.length - 1; i >= 0; i--) {
        temp = (parseInt(x[i]) + parseInt(y[i]) + mem) % 2;
        ans = temp.toString() + ans;
        if (parseInt(x[i]) + parseInt(y[i]) == 0)
            mem = 0;
        if(parseInt(x[i]) + parseInt(y[i]) == 2)
            mem = 1;
    }
    if (mem === 1)
        ans = '1' + ans;
    return ans;
}
