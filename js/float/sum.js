'use strict';
const cnvrts = require('./convert');
module.exports = {
    Add: Add,
}

function Add(x, y) {
    let xParsed = cnvrts.FloatParse(x);
    let yParsed = cnvrts.FloatParse(y);
    let xBin = (xParsed.intPart + xParsed.fracPart).replace(/^0+/, '');
    let yBin = (yParsed.intPart + yParsed.fracPart).replace(/^0+/, '');
    let sum = 0;
    let newShift;
    let delta = xParsed.shift - yParsed.shift;
    if (delta < 0) {
        xBin = ('0'.repeat(Math.abs(delta)) + xBin).slice(0,delta);
        xParsed.shift = yParsed.shift;
        newShift = y.substr(1,8);
    }
    else if(delta > 0) {
        yBin = ('0'.repeat(delta) + yBin).slice(0,-delta);
        yParsed.shift = xParsed.shift;
        newShift = x.substr(1,8);
    }
    else 
        newShift = BinaryAdd(x.substr(1,8), "1");
    sum = BinaryAdd(xBin, yBin);
    let shiftDelta = sum.length - xBin.length;
    let m = sum.slice(-23 - shiftDelta, sum.length - shiftDelta);
    return xParsed.sign + newShift + m;
}

function Normalize(x, y) {
    if (x.length < y.length) {
        x = '0'.repeat(y.length - x.length) + x;
    }
    else {
        y = '0'.repeat(x.length - y.length) + y;
    }
    return { x, y }
}

function BinaryAdd(x, y) {
    let ans = "";
    let temp = 0;
    let mem = 0;
    let norm = Normalize(x, y);
    x = norm.x;
    y = norm.y;
    for (let i = x.length - 1; i >= 0; i--) {
        temp = (parseInt(x[i]) + parseInt(y[i]) + mem) % 2;
        ans = temp.toString() + ans;
        if (parseInt(x[i]) + parseInt(y[i]) == 0)
            mem = 0;
        if (parseInt(x[i]) + parseInt(y[i]) == 2)
            mem = 1;
    }
    if (mem === 1)
        ans = '1' + ans;
    return ans;
}

function BinarySubstract(x, y) {
    let ans ='';
    let mem =0;
    let norm = Normalize(x, y);
    x = norm.x;
    y = norm.y;
    for (let i = x.length - 1; i >= 0; i--) {
        let temp = (parseInt(x[i]) - parseInt(y[i]) - mem) % 2;
        if (parseInt(x[i]) - parseInt(y[i]) === 1)
            mem =0;
        if (temp != -1)
            ans = temp + ans;
        else {
            mem = 1;
            ans = '1' + ans;
        }
    }
    return ans;
}

