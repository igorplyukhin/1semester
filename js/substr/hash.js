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
let data;
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
        data = BruteForceFindSubstrIndices(str, substr);
        console.log("Bruteforce method:");
        break;
    case "h1":
        data = HashSumFindSubstrIndices(str, substr, 1);
        console.log("Hashsum method:");
        break;
    case "h2":
        data = HashSumFindSubstrIndices(str, substr, 2);
        console.log("SquareHashsum method:");
        break;
    case "h3":
        data = RabinKarpFindSubstrIndices(str, substr);
        console.log("RabinCarp mehod:");
        break;
    default:
        console.log(`Uncknown modifier \'${method}\'`);
        return;
}
if (count > 0)
    data.indices = data.indices.slice(0, count);
console.log(data.indices.join());
if (showCollisions && method != "b")
    console.log("Collisions:", data.collisions);
if (showTime)
    console.timeEnd('Elapsed time');

function BruteForceFindSubstrIndices(str, substr) {
    let indices = new Array();
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        for (let j = 0; j < substr.length; j++) {
            if (str[i + j] != substr[j]) {
                continue isNotSubstr;
            }
        }
        indices.push(i);
    }
    return {
        indices
    };
}

function GetHashSum(str, power) {
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(), power);
    }
    return hashSum;
}

function HashSumFindSubstrIndices(str, substr, power) {
    let indices = new Array();
    let hashSumSubstr = GetHashSum(substr, power);
    let collisions = 0;
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        if (GetHashSum(str.substr(i, substr.length), power) === hashSumSubstr) {
            for (let j = 0; j < substr.length; j++) {
                if (str[i + j] != substr[j]) {
                    collisions += 1;
                    continue isNotSubstr;
                }
            }
            indices.push(i);
        }
    }
    return {
        indices,
        collisions
    };
}

function CalcRabinKarpHash(str) {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
        hash += str[i].charCodeAt() * Math.pow(2, str.length - i - 1);
    }
    return hash;
}

function RabinKarpFindSubstrIndices(str, substr) {
    let indices = new Array();
    let collisions = 0;
    let substrHash = CalcRabinKarpHash(substr);
    let currenttHash = CalcRabinKarpHash(str.substr(0, substr.length));
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        if (currenttHash == substrHash) {
            for (let j = 0; j < substr.length; j++) {
                if (str[i + j] != substr[j]) {
                    collisions++;
                    continue isNotSubstr;
                }
            }
            indices.push(i);
        }
        if (i + substr.length <= str.length - 1)
            currenttHash = 2 * (currenttHash - str[i].charCodeAt() * Math.pow(2, substr.length - 1))
                + str[i + substr.length].charCodeAt();
    }
    return {
        indices,
        collisions
    };
}