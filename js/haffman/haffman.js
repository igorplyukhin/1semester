const fs = require('fs')

function Code(s, tableFile, outFile) {
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

    function UpdateTemp(temp, min1Letter, min2Letter, min1, min2) {
        delete temp[min1Letter];
        delete temp[min2Letter];
        temp[min1Letter + min2Letter] = new Node(min1 + min2);
        table[min1Letter].parent = min1Letter + min2Letter;
        table[min2Letter].parent = min1Letter + min2Letter;
        table[min1Letter].code = 0;
        table[min2Letter].code = 1;
        table[min1Letter + min2Letter] = new Node(min1 + min2);
    }

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
        let min1Letter;
        let min2Letter;
        let min1 = Infinity;
        let min2 = Infinity;
        for (let key in temp) {
            if (temp[key].count <= min1) {
                min2Letter = min1Letter;
                min1Letter = key;
                min2 = min1;
                min1 = temp[key].count;
            }
        }
        UpdateTemp(temp, min1Letter, min2Letter, min1, min2)
    }
    //Составляем коды
    for (let key in table) {
        if (key.length === 1) {
            fs.appendFileSync(tableFile, key + " " + FindCode(key, table) + "\n");
            table[key].code = FindCode(key, table);
        }
    }
    //кодирование в файл
    for (let i = 0; i < s.length; i++) {
        fs.appendFileSync(outFile, table[s[i]].code);
    }
}

function Decode(s, tableFile, outFile) {
    let strtable = fs.readFileSync(tableFile, "utf8");
    let table = {};

    for (let i = 0; i < strtable.split(/[\n ]+/).length - 1; i + 1) {
        //table[strtable[i]] = strtable[i + 1];
    }

}

let inFile = process.argv[3];
let tableFile = process.argv[4];
let outFile = process.argv[5];
let s = fs.readFileSync(inFile, "utf8");

Code(s, tableFile, outFile);
