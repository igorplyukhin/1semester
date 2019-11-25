'use strict';
const MantissaSuffix = "00000000000000000000000";
const ShiftPrefix = "00000000";
const MantissaLength = 23;
const ShiftOffset = 127;
const floatRegExp = /[^/.0-9]/gim;

console.log(GetBinaryNumber('dfh'));

function GetBinaryNumber(str) {
    let binaryNumber = new BinaryNumber;
    binaryNumber.sign = str[0] === '-' ? 1 : 0;
    str = str[0] === '-' ? str.substring(1) : str;
    if (str.replace(floatRegExp,'') === "") {//NaN
        binaryNumber.shift = "11111111";
        binaryNumber.mantissa = ('1' + MantissaSuffix).substr(0,MantissaLength);

        return binaryNumber;
    }
    let parsedNumber = ParseNumber(str);
    if (parsedNumber.intPart > 340282346638528859811704183484516925439n) { //Infinity
        binaryNumber.shift = "11111111";
        binaryNumber.mantissa = MantissaSuffix;

        return binaryNumber;
    }

    
    let binIntPart = IntToBinary(parsedNumber.intPart);
    let binFracPart = FracToBinary(parsedNumber.fracPart)
    binaryNumber.shift = (ShiftPrefix + CalcShift(binIntPart, binFracPart, parsedNumber.lostZeros)).slice(-8);
    binaryNumber.mantissa = (binIntPart + binFracPart + MantissaSuffix).replace(/^0+/, '').substr(1, MantissaLength);

    return binaryNumber;
}

function BinaryNumber(sign, shift, mantissa) {
    this.sign = sign;
    this.shift = shift;
    this.mantissa = mantissa;
}

function ParseNumber(str) {
    let parts = str.split('.');
    parts[0] = parts[0] === undefined ? '0' : parts[0];
    parts[1] = parts[1] === undefined ? '0' : parts[1];
    let intPart = BigInt(parts[0]);
    let fracPart = BigInt(parts[1]);
    let lostZeros = parts[1].length - CalcNumLength(fracPart);
    return {
        intPart,
        fracPart,
        lostZeros
    }
}

function IntToBinary(num) {
    if (num === 0n)
        return "0";
    let binaryNum = "";
    while (num > 0n) {
        binaryNum = (num % 2n).toString() + binaryNum;
        num = (num - num % 2n) / 2n;
    }
    return binaryNum;
}

function FracToBinary(num) {
    let binaryNum = "";
    let i = 0;
    while (num > 0 && i != 40) { 
        i++;
        let oldLen = CalcNumLength(num);
        num = 2n * num;
        let newLen = CalcNumLength(num);
        if (newLen > oldLen) {
            binaryNum += 1;
            num -= RaiseToPow(10n, oldLen);
        }
        else {
            binaryNum += "0";
        }
    }
    return binaryNum;
}

function CalcNumLength(num) {
    let len = (num == 0n) ? 1 : 0;
    while (num != 0n) {
        len++;
        num = (num - num % 10n) / 10n;
    }
    return len;
}

function RaiseToPow(num, pow) {
    let ans = 1n;
    while (pow > 0) {
        ans *= num;
        pow -= 1;
    }
    return ans;
}

function CalcShift(intPart, fracPart, lostZeros) {
    if (intPart != "0")
        return IntToBinary(BigInt(intPart.length - 1 + ShiftOffset));
    else {
        let strWithoutZeros = fracPart.replace(/^0+/, ''); //Delete first zeros
        return IntToBinary(BigInt(strWithoutZeros.length - fracPart.length - 1 - lostZeros + ShiftOffset));
    }
}



