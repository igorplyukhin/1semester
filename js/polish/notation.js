let expr = '(1.5 + 2) * (3 ^ 2 - 4) / 2 + 10 ^ 2';
let operations = new Map([
    ["(", 0],
    [")", 0],
    ["-", 1],
    ['+', 1],
    ["*", 2],
    ["/", 2],
    ["^", 3],
]);
let output = new Array();
let stack = new Array();

function IsUnary(str, i) {
    return str[i - 1] === undefined
        || str[i - 1] === '('
        || operations.has(str[i - 1]);
}

function GetNumber(str, position) {
    let number = '';
    for (let i = position; i < str.length; i++) {
        if (operations.has(str[i]) || str[i] === ' ')
            break;
        number += str[i];
    }
    return number;
}

for (let i = 0; i < expr.length; i++) {
    if (expr[i] === ' ')
        continue;
    if (expr[i] === ')') {
        let j = stack.length - 1;
        while (stack[j] !== '(') {
            output.push(stack.pop());
            j = stack.length - 1;
        }
        stack.pop();
        continue;
    }
    if (operations.has(expr[i])) {
        if (expr[i] === '-' && IsUnary(expr, i)) {
            output.push('\\' + expr[i + 1]);
            i++;
            continue;
        }
        let j = stack.length - 1;
        while (expr[i] !== '(' && (operations.get(stack[j]) >= operations.get(expr[i]))) {
            output.push(stack.pop());
            j = stack.length - 1;
        }
        stack.push(expr[i]);
    }
    else {
        let number = GetNumber(expr, i);
        output.push(number);
        i += number.length - 1;
    }
}

for (let i = stack.length - 1; i >= 0; i--) {
    output.push(stack[i]);
}

console.log(output.join(''))
