let fs = require('fs');
let file = 'fib.txt';
let inputValue = process.argv[2];
let source = fs.readFileSync(file, 'utf8');
let lines = source.replace(/\r*\n*\s+/g, '').split(';');
let stack = new Map();
let labels = new Map();
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
    else if (line.includes('CMP')) {
        line = line.match(/\((.*?)\)/);
        Compare(stack, line[1].split(',')[0], line[1].split(',')[1]);
    }
    else if (line[0] === '@') {
        labels.set(line.substring(1), i);
    }
    else if (line.includes('GOTO')) {
        if (stack.get('M1') === -1)
            i = labels.get(line.substring(5));
    }
    else {
        console.log('Unknown sytax');
        break;
    }
}

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

function Compare(map, a, b) {
    a = map.has(a) ? map.get(a) : a;
    b = map.has(b) ? map.get(b) : b;
    if (a > b)
        stack.set('M1', 1);
    else if (a < b)
        stack.set('M1', -1);
    else
        stack.set('M1', 0);
}
