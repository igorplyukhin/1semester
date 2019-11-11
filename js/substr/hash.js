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

function GetHashSum(str, exponent){
    let hashSum = 0;
    for (let i = 0; i < str.length; i++) {
        hashSum += Math.pow(str[i].charCodeAt(),exponent);
    }
    return hashSum;
}

function HashSumFindSubstrIndices(str, substr) {
    let hashSumStr = 0;
    let hashSumSubstr = GetHashSum(substr, 1);
    let isSubstr = true;
    let collisions = 0;
    
}

function SquareHashSumFindSubstrIndices(str, substr) {

}


HashSumFindSubstrIndices(str, substr);

