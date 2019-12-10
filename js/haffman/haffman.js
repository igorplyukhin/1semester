const fs = require('fs')
let inFile = process.argv[3];
let tableFile = process.argv[4];
let outFile = process.argv[5];

if (!fs.existsSync(inFile)) {
    console.log("Input file doesn't exist");
    return;
}

if (process.argv[2] === 'decode' && !fs.existsSync(tableFile)) {
    console.log("Table file doesn't exist");
    return;
}

process.argv[2] === "code"
    ? Code(inFile, tableFile, outFile)
    : Decode(inFile, tableFile, outFile);

function Code(inFile, tableFile, outFile) {
    let Node = function (count) {
        this.count = count;
    };

    function FindCode(key, table) {
        let code = "";
        while (table[key].parent != undefined) {
            code = table[key].code + code
            key = table[key].parent;
        }
        return code;
    }

    function Update(temp, min1Letter, min2Letter, min1, min2) {
        delete temp[min1Letter];
        delete temp[min2Letter];
        temp[min1Letter + min2Letter] = new Node(min1 + min2);
        table[min1Letter].parent = min1Letter + min2Letter;
        table[min2Letter].parent = min1Letter + min2Letter;
        table[min1Letter].code = 0;
        table[min2Letter].code = 1;
        table[min1Letter + min2Letter] = new Node(min1 + min2);
    }
    let s = fs.readFileSync(inFile, "utf8");
    fs.writeFileSync(tableFile, "");
    fs.writeFileSync(outFile, "");
    let letters = {};
    //Составляем словарь (буква : ее обьект)
    for (let i = 0; i < s.length; i++)
        if (letters[s[i]])
            letters[s[i]].count += 1;
        else
            letters[s[i]] = new Node(1);

    //Дублируем словарь
    let table = { ...letters };
    let temp = { ...letters };
    //Строим дерево
    while (Object.keys(temp).length > 1) {
        let min1Letter = "";
        let min2Letter = "";
        let min1 = Infinity;
        let min2 = Infinity;
        for (let key in temp)
            if (temp[key].count <= min1) {
                min1Letter = key;
                min1 = temp[key].count;
            }

        for (let key in temp)
            if (temp[key].count <= min2 && key != min1Letter) {
                min2Letter = key;
                min2 = temp[key].count;
            }

        Update(temp, min1Letter, min2Letter, min1, min2)
    }
    //Составляем коды
    for (let key in table) {
        if (key.length === 1) {
            let code = FindCode(key, table);
            fs.appendFileSync(tableFile, key + " " + code + "\n");
            table[key].code = code;
        }
    }
    //кодирование в файл
    for (let i = 0; i < s.length; i++) {
        fs.appendFileSync(outFile, table[s[i]].code);
    }
}

function Decode(inFile, tableFile, outFile) {
    let s = fs.readFileSync(inFile, "utf8");
    let strtable = fs.readFileSync(tableFile, "utf8").trim().split(/\n/);
    let table = {};
    let decodedStr = "";

    for (let i = 0; i < strtable.length; i++) {     //Парсинг table.txt
        table[strtable[i].substr(2)] = strtable[i][0];
    }

    for (let i = 0; i < s.length; i++) {    //Поиск соответствий
        let key = s[i];
        let j = i;
        while (!(key in table)) {
            if (j === s.length - 1) {
                fs.writeFileSync(outFile, decodedStr);
                console.log("Can't be fully decoded");
                return;
            }
            j++
            key += s[j];
        }
        i = j;
        decodedStr += table[key];
    }
    fs.writeFileSync(outFile, decodedStr);
}