"use strict";
const fs = require("fs");

let strFile = process.argv[process.argv.length - 2];
let substrFile = process.argv[process.argv.length - 1];
let keys = process.argv.slice(2,-2);
let showTime = false;
let showTable = false;
let indicesCount = 0;

if (!fs.existsSync(strFile) || !fs.existsSync(substrFile)) {
    console.log("Check files");
    return;
}

for (let i=0; i < keys.length; i++) {
    switch (keys[i]){
        case "-t":
            showTime = true;
            break;
        case "-a":
            showTable = true;
            break;
        case "-n":
            indicesCount = Math.max(parseInt(keys[i + 1]), 0);
            break;
    }
}

let str = fs.readFileSync(strFile, "utf8");
let substr = fs.readFileSync(substrFile, "utf8");
console.time("Elapsed time");
let indicesAndTable = FindSubstrings(str,substr);
if (showTime)
    console.timeEnd("Elapsed time");
console.log(indicesAndTable.indices.join());
if (showTable)
    console.log(indicesAndTable.table);

function BuildTable(str) {
    let len = str.length;
    let alph = new Array();
    let table = new Array(len + 1);
    for (let i = 0; i < len; i++) {
        alph[str[i]] = 0;
    }

    for (let i = 0; i < len + 1; i++)
        table[i] = new Array();

    for (let letter in alph)
        table[0][letter] = 0;

    for (let i = 0; i < len; i++) {
        let prev = table[i][str[i]];
        table[i][str[i]] = i + 1;
        for (let letter in alph)
            table[i + 1][letter] = table[prev][letter];
    }

    return table;
}

function FindSubstrings(str, substr) {
    let table = BuildTable(substr);
    let currentState = 0;
    let indices = new Array();
    for (let i = 0; i < str.length; i++) {
        currentState = table[currentState][str[i]] === undefined
            ? 0
            : table[currentState][str[i]];
        if (currentState === substr.length)
            indices.push(i - substr.length + 1)
        if (indices.length === indicesCount && indicesCount > 0)
            break;
    }

    return {
        indices,
        table
    };
}