"use strict";
const fs = require('fs');
let str = fs.readFileSync(process.argv[process.argv.length - 2], 'utf8');
let substr = fs.readFileSync(process.argv[process.argv.length - 1], 'utf8');
let method = process.argv[process.argv.length - 3];
let showCollisions = false;
let showTime = false;
let count = 0;
let keys = process.argv.slice(2, -3);
for (let i = 0; i < keys.length; i++) {
    switch (keys[i]) {
        case "-c":
            showCollisions = true;
            break;
        case "-n":
            count = parseInt(keys[i + 1]);
            break;
        case "-t":
            showTime = true;
            break;
    }
}

console.time('Elapsed time');

switch (method) {
    case "b":
        BruteForceFindSubstrIndices(str, substr, count);
        break;
    case "h1":
        HashSumFindSubstrIndices(str, substr, 1, count, showCollisions);
        break;
    case "h2":
        HashSumFindSubstrIndices(str, substr, 2, count, showCollisions);
        break;
    case "h3":
        RabinKarpFindSubstrIndices(str, substr, count, showCollisions);
        break;
}

if (showTime)
    console.timeEnd('Elapsed time');

//str = "aabcacaa";
//substr = "aa";
//BruteForceFindSubstrIndices(str, substr, 1);
//RabinKarpFindSubstrIndices(str, substr);
//HashSumFindSubstrIndices(str, substr, 2, 0);
//HashSumFindSubstrIndices(str, substr, 1);

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
        if (count > 0 && indices.length === count)
            break;
    }
    console.log("BruteForce Method:")
    console.log(indices.join());
}

function GetHashSum(str, power) {
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(), power);
    }
    return hashSum;
}

function HashSumFindSubstrIndices(str, substr, power, count, showCollisions) {
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
            if (count != 0 && count === indices.length)
                break;
        }
    }
    power === 1
        ? console.log("HashSum Method:")
        : console.log("Square HashSum Method:");
    console.log(indices.join());
    if (showCollisions)
        console.log("Collisions: ", collisions);
}

function CalcRabinKarpHash(str) {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
        hash += str[i].charCodeAt() * Math.pow(2, str.length - i - 1);
    }
    return hash;
}

function RabinKarpFindSubstrIndices(str, substr, count, showCollisions) {
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
            currenttHash = 2 * (currenttHash - str[i].charCodeAt() * Math.pow(2, substr.length - 1)) + str[i + substr.length].charCodeAt();
        if (count != 0 && count === indices.length)
            break;
    }
    console.log("RabinKarp Method:");
    console.log(indices.join());
    if (showCollisions)
        console.log("Collisions: ", collisions);
}


