const fs = require('fs');
const escSymbol = "#";

function EscCode(inf, outf) {
    function Code() {
        if (symbol != escSymbol) {
            while (symbolsCount > 259) {
                encodedStr += escSymbol + String.fromCharCode(255) + symbol;
                symbolsCount -= 259;
            }
            if (symbolsCount > 3)
                encodedStr += escSymbol + String.fromCharCode(symbolsCount - 4) + symbol;
            else
                while (symbolsCount > 0) {
                    encodedStr += symbol;
                    symbolsCount--;
                }
        }
        else {
            while (symbolsCount > 256) {
                encodedStr += escSymbol + String.fromCharCode(255) + symbol;
                symbolsCount -= 256;
            }
            encodedStr += escSymbol + String.fromCharCode(symbolsCount - 1) + symbol;
        }
    }
    let s = fs.readFileSync(inf, "utf8");
    let encodedStr = new String;    //Закодированная строка
    let symbol = s[0];  //Текущий символ
    let symbolsCount = 1;   //Количество идущих подряд символов
    for (i = 1; i < s.length; i++)
        if (s[i] == s[i - 1])
            symbolsCount++;
        else {
            Code();
            symbol = s[i];
            symbolsCount = 1;
        }
    Code();
    fs.writeFileSync(outf, encodedStr);
}

function EscDecode(inf, outf) {
    let s = fs.readFileSync(inf, "utf8");
    let decodedStr = new String;
    let symbolsAmmount = 1;
    for (i = 0; i < s.length; i++) {
        if (s[i] == escSymbol) {
            if (s[i + 2] == escSymbol)
                symbolsAmmount = s[i + 1].charCodeAt(0) + 1;
            else
                symbolsAmmount = s[i + 1].charCodeAt(0) + 4;
            while (symbolsAmmount > 0) {
                decodedStr += s[i + 2];
                symbolsAmmount--;
            }
            i = i + 2;
        }
        else
            decodedStr = decodedStr + s[i];
    }
    fs.writeFileSync(outf, decodedStr);
}

function JumpCode(inf, outf) {
    function Code() {
        
    }
            
    const lengthLimit = 128; //
    let s = fs.readFileSync(inf, "utf8");
    let encodedStr = new String;    //Закодированная строка
    let difSymbolsSeq = new String; //Последовательность не повторяюшихся символов
    let symbol = s[0];  //Текущий символ
    let symbolsCount = 1;   //Количество идущих подряд символов
    for (i = 1; i < s.length; i++) {
        if (s[i] == s[i - 1]) {
            symbolsCount++;
        }
        else {
            Code();
            symbol = s[i];
            symbolsCount = 1;
        }
    }
    Code();
    fs.writeFileSync(outf, encodedStr);
}

function JumpDecode(inf, outf) {
    let s = fs.readFileSync(inf, "utf8");
    let symbolsCount = 0;
    let decodedString = new String;
    for (let i = 0; i < s.length; i++) {
        if (s[i].charCodeAt(0) < 129){
            symbolsCount = s[i].charCodeAt(0);
            while (symbolsCount > 0){
                decodedString += s[i+1]
                symbolsCount --;
            }
            i++;
        }
        else {
            symbolsCount = s[i].charCodeAt(0) - 128;
            decodedString += s.substr(s[i], symbolsCount);
            i += symbolsCount;
        }
    }
    fs.writeFileSync(outf, decodedString);
}

inf = process.argv[4];
outf = process.argv[5];
if (process.argv[2] == "code" && process.argv[3] == "escape")
    EscCode(inf, outf);
else if (process.argv[2] == "decode" && process.argv[3] == "escape")
    EscDecode(inf, outf);
else if (process.argv[2] == "code" && process.argv[3] == "jump")
    JumpCode(inf, outf);
else if (process.argv[2] == "decode" && process.argv[3] == "jump")
    JumpDecode(inf, outf);
else console.log("Something went wrong");