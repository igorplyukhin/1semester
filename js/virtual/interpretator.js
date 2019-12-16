/*
M0 - loop var; M1 = bool; M2... = free var;
*/
const fs = require('fs');
const readlineSync = require('readline-sync');

let file = process.argv[2];
if (!fs.existsSync(file)) {
    console.log("File doesn\'t exist");
    process.exit(-1);
}

let source = fs.readFileSync(file, 'utf8');
let lines = source.replace(/\r*\n*\s+/g, '').split(';');
let stack = new Map();
let labels = new Map();

for (let i = 0; i < lines.length - 1; i++) {
    let line = lines[i];
    if (line[0] === '@')
        labels.set(line.substring(1), i);
}

for (let i = 0; i < lines.length - 1; i++) {
    let line = lines[i];
    if (line.includes('=')) {
        let index = line.split('=')[0];
        let value = line.split('=')[1];
        PushToStack(stack, index, value);
    }
    else if (line.includes('-->')) {
        console.log(stack.get(line.split('-->')[0]))
    }
    else if (line.includes('<--')) {
        let inputVal = readlineSync.question('Type number ');
        if (isNaN(inputVal)) {
            console.log('Integer expected');
            process.exit(-1);
        }
        else
            PushToStack(stack, line.split('<--')[0], inputVal);
    }
    else if (line.includes('LT')) {
        line = line.match(/\((.*?)\)/);
        LT(stack, line[1].split(',')[0], line[1].split(',')[1]);
    }
    else if (line.includes('GT')) {
        line = line.match(/\((.*?)\)/);
        GT(stack, line[1].split(',')[0], line[1].split(',')[1]);
    }
    else if (line.includes('EQ')) {
        line = line.match(/\((.*?)\)/);
        Equal(stack, line[1].split(',')[0], line[1].split(',')[1]);
    }
    else if (line[0] === '@') {
        continue;
    }
    else if (line.includes('JMP')) {
        line = line.match(/\((.*?)\)/);
        if (stack.get('M1') === parseInt(line[1].split(',')[0]))
            i = labels.get(line[1].split(',')[1].substring(1));
    }
    else if (line.includes('GOTO')) {
        i = labels.get(line.substring(5));
    }
    else if (line.includes('EXIT')) {
        break;
    }
    else if (line.includes('SUBTR')) {
        line = line.match(/\((.*?)\)/)[1].split(',');
        Subtract(stack, line[1], line[2], line[0]);
    }
    else if (line.includes('ADD')) {
        line = line.match(/\((.*?)\)/)[1].split(',');
        Add(stack, line[1], line[2], line[0]);
    }
    else {
        console.log('Unknown sytax');
        break;
    }
}

function PushToStack(map, index, value) {
    if (!isNaN(value))
        map.set(index, parseInt(value));
    else if (map.has(value)) {
        map.set(index, map.get(value));
    }
    else {
        console.log('stack error');
        process.exit(-1);
    }
}

function Add(map, a, b, outputIndex) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    map.set(outputIndex, parseInt(a) + parseInt(b));
}

function Subtract(map, a, b, outputIndex) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    map.set(outputIndex, parseInt(a) - parseInt(b));
}

function LT(map, a, b) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    if (parseInt(a) < parseInt(b))
        stack.set('M1', 1);
    else
        stack.set('M1', 0);
}

function GT(map, a, b) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    if (parseInt(a) > parseInt(b))
        stack.set('M1', 1);
    else
        stack.set('M1', 0);
}

function Equal(map, a, b) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    if (parseInt(a) === parseInt(b))
        stack.set('M1', 1);
    else
        stack.set('M1', 0);
}
