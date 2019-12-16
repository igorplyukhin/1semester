let operations = new Map([
    ["(", 0],
    [")", 0],
    ["-", 1],
    ['+', 1],
    ["*", 2],
    ["/", 2],
    ["^", 3],
]);

str=process.argv[2];

var postfix = ConvertToPostfix(str);
console.log(postfix.join(' '));
console.log(Compute(postfix));

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
                if (j === -1){
                    console.log('wrong input');
                    process.exit(-1);
                }
            }
            stack.pop();
            continue;
        }
        if (operations.has(expr[i])) {
            if (expr[i] === '-' && IsUnary(expr, i)) {
                let number = GetNumber(expr, i+1);
                output.push('\\' + number);
                i += number.length;
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
        if (stack[i] === '('){
            console.log('wrong input');
            process.exit(-1);
        }
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
                stack.push(-arr[i].substr(1))
            else
                stack.push(arr[i]);
        }
    }
    return stack[0];
}

function IsUnary(inFile, i) {
    return inFile[i - 1] === undefined
        || inFile[i - 1] === '('
        || operations.has(inFile[i - 1])
        || inFile[i - 1] === ' ';
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

