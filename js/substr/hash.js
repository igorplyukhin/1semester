"use strict";
let str = "aabcacaa";
let substr = "aa";

function BruteForceFindSubstrIndices(str, substr, count) {
    let indices = new Array();
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        isNotSubstr:
        for (let j = 0; j < substr.length; j++) {
            if (str[i + j] != substr[j]) {
                break isNotSubstr;
            }
        }
        indices.push(i);
        if (count != 0 && indices.length === count)
            break;
    }
    console.log("BruteForce Method:")
    console.log(indices);
}

function GetHashSum(str, power) {
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(), power);
    }
    return hashSum;
}

function HashSumFindSubstrIndices(str, substr, power, count) {
    let indices = new Array();
    let hashSumSubstr = GetHashSum(substr, power);
    let collisions = 0;
    for (let i = 0; i < str.length - substr.length + 1; i++) {
        if (GetHashSum(str.substr(i, substr.length), power) === hashSumSubstr) {
            isNotSubstr:
            for (let j = 0; j < substr.length; j++) {
                if (str[i + j] != substr[j]) {
                    collisions += 1;
                    break isNotSubstr;
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
    console.log(indices);
    console.log("Collisions: ", collisions);
}

BruteForceFindSubstrIndices(str, substr, 0);
HashSumFindSubstrIndices(str, substr, 2, 0);



