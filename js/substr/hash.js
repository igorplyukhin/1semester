let str = "aabacaa";
let substr = "aa";

function BruteForceFindSubstrIndices(str, substr) {
    let indices = new Array();
    let isSubstr;
    for (let i = 0; i < str.length; i++) {
        isSubstr = true;
        for (let j = 0; j < substr.length; j++) {
            if (str[i + j] != substr[j]) {
                isSubstr = false;
                break;
            }
        }
        if (isSubstr)
            indices.push(i);
    }
    return indices;
}

function GetHashSum(str, exponent) {
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(), exponent);
    }
    return hashSum;
}


function RabinKarpFindSubstrIndices(str, substr, count) {
    let indices = new Array();
    let collisions = 0;
    let substrHash = CalcRabinKarpHash(substr);
    let currenttHash = CalcRabinKarpHash(str.substr(0, substr.length));
    isNotSubstr:
    for (let i = 0; i < str.length - substr.length; i++) {
        if (currenttHash == substrHash) {
            for (let j = 0; j < substr.length; j++) {
                if (str[i + j] != substr[j]) {  
                collisions++;          
                break isNotSubstr;
                }
            }
            indices.push(i);
        }
        currenttHash = 2*(currenttHash - str[i].charCodeAt()*Math.pow(2, substr.length - 1)) + str[i+substr.length].charCodeAt();
    }
    console.log(indices);
    console.log(collisions);
}

function CalcRabinKarpHash(str) {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
        hash += str[i].charCodeAt() * Math.pow(2, str.length - i - 1);
    }
    return hash;
}


RabinKarpFindSubstrIndices(str,substr);



