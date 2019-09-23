/*
function User(name, age) {
    this.name = name;
    this.age = age;
}

let u1 = new User("Vova", 13);
let u2 = new User("Dima", 15);
console.log(u1.name);
*/

let ar1 = new Array();
let ar2 = new Array(1,2);
//ar1.__proto__.xyz = "xyz";
//console.log(ar2.__proto__ == Array.prototype);
console.log(Array.__proto__ == Function.prototype);
let ar3 = ar1.__proto__.constructor();
ar3 = new Array();
console.log(typeof(Array.__proto__));
//let ob1 = new Object();
//ob1.qwe = "qwe";