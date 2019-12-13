"use strict";
const fs = require('fs');
let strFile = process.argv[process.argv.length - 2];
let substrFile = process.argv[process.argv.length - 1];
if (!fs.existsSync(strFile) || !fs.existsSync(substrFile)) {
    console.log("File doesn't exist");
    return;
}
let str = fs.readFileSync(strFile, 'utf8');
let substr = fs.readFileSync(substrFile, 'utf8');
let method = process.argv[process.argv.length - 3];
let showCollisions = false;
let showTime = false;
let count = 0;
let keys = process.argv.slice(2, -3);
let result;
for (let i = 0; i < keys.length; i++) {
    switch (keys[i]) {
        case "-c":
            showCollisions = true;
            break;
        case "-n":
            count = Math.max(parseInt(keys[i + 1]), 0);
            break;
        case "-t":
            showTime = true;
            break;
    }
}

console.time('Elapsed time');
switch (method) {
    case "b":
        result = BruteForceFindSubstrIndices(str, substr, count);
        console.log("Bruteforce method:");
        break;
    case "h1":
        result = HashFindSubstrIndices(str, substr, GetSumHash, 1, count);
        console.log("Hashsum method:");
        break;
    case "h2":
        result = HashFindSubstrIndices(str, substr, GetSumHash, 2, count);
        console.log("SquareHashsum method:");
        break;
    case "h3":
        result = HashFindSubstrIndices(str, substr, CalcRabinKarpHash, count);
        console.log("RabinCarp mehod:");
        break;
    default:
        console.log(`Uncknown modifier \'${method}\'`);
        return;
}
if (showTime)
    console.timeEnd('Elapsed time');
console.log(result.indices.join());
if (showCollisions && method != "b")
    console.log("Collisions:", data.collisions);

function BruteForceFindSubstrIndices(str, substr, count) {
    let indices = new Array();
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        for (let j = 0; j < substr.length; j++) {
            if (str[i + j] != substr[j]) {
                continue isNotSubstr;
            }
        }
        indices.push(i);
        if (indices.length == count && count > 0)
                break;
    }
    return {
        indices
    };
}

function HashFindSubstrIndices(str, substr, Method, power, count) {
    let indices = new Array();
    let substrHash = Method(substr, power);
    let currentHash = Method(str.substr(0, substr.length), power)
    let collisions = 0;
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        if (currentHash === substrHash) {
            for (let j = 0; j < substr.length; j++) {
                if (str[i + j] != substr[j]) {
                    collisions += 1;
                    continue isNotSubstr;
                }
            }
            indices.push(i);
            if (indices.length == count && count > 0)
                break;
        }
        currentHash =
            Method === CalcRabinKarpHash
                ? UpdRabinKarpHash(str, substr, currentHash, i)
                : UpdSumHash(str[i], str[i+1], power, currentHash);
    }
    return {
        indices,
        collisions
    };
}

function UpdSumHash(symbol,nextSymbol, power, sum){
    sum-=Math.pow(symbol.charCodeAt(),power);
    sum+=Math.pow(nextSymbol.charCodeAt(),power);
    return sum;
}

function GetSumHash(str, power) {
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(), power);
    }
    return hashSum;
}

function CalcRabinKarpHash(str) {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
        hash += str[i].charCodeAt() * Math.pow(2, str.length - i - 1);
    }
    return hash;
}

function UpdRabinKarpHash(str, substr, currentHash, i) {
    return i + substr.length === str.length
        ? currentHash
        : 2 * (currentHash - str[i].charCodeAt() * Math.pow(2, substr.length - 1))
            + str[i + substr.length].charCodeAt();
}
