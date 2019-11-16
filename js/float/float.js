'use strict';

function BinaryNumber(sign, shift, mantissa) {
    this.sign = sign;
    this.shift = shift;
    this.mantissa = mantissa;
}

function ParseNumber(str) {
    let parts = str.split('.');
    let intPart = parts[0] === undefined ? 0n : BigInt(parts[0]);
    let fracPart = parts[1] === undefined ? 0n : BigInt(parts[1]);
    return {
        intPart,
        fracPart
    }
}

function IntToBinary(num) {
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
    while (num > 0 && i != 20) {
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

function GetBinaryNumber(str) {
    let binaryNumber = new BinaryNumber;
    binaryNumber.sign = str[0] === '-' ? 1 : 0;
    str = str[0] === '-' ? str.substring(1) : str;
    let parsedNumber = ParseNumber(str);
    binaryNumber.mantissa = (IntToBinary(parsedNumber.intPart) + FracToBinary(parsedNumber.fracPart)).substring(1);

    return binaryNumber;
}


console.log(GetBinaryNumber("-6.75"));

