let expr = '1+1';
let operations = ['(', ')', '+', '-', '*', '/', '^'];
let output = '';
let stack = new Array();

function IsUnary(str, i) {
    return str[i - 1] === undefined
        || str[i - 1] === '('
        || operations.includes(str[i - 1]);
}

for (let i = 0; i < expr.length; i++) {
    if (operations.includes(expr[i]))
        output+=expr[i];

}

console.log(operations.includes('('));