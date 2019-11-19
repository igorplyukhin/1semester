"use strict";

console.log(FindSubstrings("abaxabaz", "sz"));

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
    let indecies = new Array();
    for (let i = 0; i < str.length; i++) {
        currentState = table[currentState][str[i]] === undefined
            ? 0
            : table[currentState][str[i]];
        if (currentState === substr.length)
            indecies.push(i - substr.length + 1)
    }

    return {
        indecies,
        table
    };
}