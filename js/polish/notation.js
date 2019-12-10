const fs = require('fs');
let operations = new Map([
    ["(", 0],
    [")", 0],
    ["-", 1],
    ['+', 1],
    ["*", 2],
    ["/", 2],
    ["^", 3],
]);

let inFile = 'input.txt';
if (!fs.existsSync(inFile)) {
    console.log('file doesn\'t exist');
    process.exit(-1);
}

let str = fs.readFileSync(inFile, 'utf8');
if (CheckBrakets(str)){
    var postfix = ConvertToPostfix(str);
    console.log(postfix.join(' '));
    console.log(Compute(postfix));
}
else {
    console.log('Wrong brackets');
}

function ConvertToPostfix(expr) {
    let output = new Array();
    let stack = new Array();
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
            output.push(parseFloat(number));
            i += number.length - 1;
        }
    }

    for (let i = stack.length - 1; i >= 0; i--) {
        output.push(stack[i]);
    }
    return output;
}

function Compute(arr) {
    let stack = new Array();
    for (let i = 0; i < arr.length; i++) {
        if (operations.has(arr[i])) {
            let res;
            let len = stack.length;
            switch(arr[i]){
                case "+":
                    res = stack[len-2] + stack[len-1];
                    break;
                case "-":
                    res = stack[len-2] - stack[len-1];
                    break;
                case "*":
                    res = stack[len-2] * stack[len-1];
                    break;
                case "/":
                    res = stack[len-2] / stack[len-1];
                    break;
                case "^":
                    res = Math.pow(stack[len-2], stack[len-1]);
                    break;
            }
            stack.pop();
            stack.pop();
            stack.push(res);
        }
        else {
            if (arr[i][0]==='\\')
                stack.push(-arr[i+1])
            else
                stack.push(arr[i]);
        }
    }
    return stack[0];
}

function CheckBrakets(inFile) {
    let balance = 0;
    for (let i=0 ; i< inFile.length; i++) {
        if (inFile[i]==='(')
            balance++;
        else if (inFile[i]===")")
            balance--;
        if (balance < 0) {
            return false;
        }
    }
    return true;
}

function IsUnary(inFile, i) {
    return inFile[i - 1] === undefined
        || inFile[i - 1] === '('
        || operations.has(inFile[i - 1]);
}

function GetNumber(inFile, position) {
    let number = '';
    for (let i = position; i < inFile.length; i++) {
        if (operations.has(inFile[i]) || inFile[i] === ' ')
            break;
        number += inFile[i];
    }
    return number;
}


