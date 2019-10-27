/*
M0 - loop var; M1 = COMP; M2 = inputvalue; M3... = free var;
*/
let fs = require('fs');
let file = 'nod.txt';
let inputValue = 10;
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
        PushToStack(stack, line.split('<--')[0], inputValue);
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
    else {
        console.log('Unknown sytax');
        break;
    }
}
console.log(lines);

function PushToStack(map, index, value) {
    if (!isNaN(value))
        map.set(index, parseInt(value));
    else if (value.includes('+')) {
        map.set(index, Add(map, value));
    }
    else if (value.includes('-')) {
        map.set(index, Subtract(map, value));
    }
    else {
        map.set(index, map.get(value));
    }
}

function Add(map, str) {
    str = str.split('+');
    let a = map.has(str[0]) ? map.get(str[0]) : str[0];
    let b = map.has(str[1]) ? map.get(str[1]) : str[1];
    return parseInt(a) + parseInt(b);
}

function Subtract(map, str) {
    str = str.split('-');
    let a = map.has(str[0]) ? map.get(str[0]) : str[0];
    let b = map.has(str[1]) ? map.get(str[1]) : str[1];
    return parseInt(a) - parseInt(b);
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
