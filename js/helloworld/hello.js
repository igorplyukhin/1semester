const fs = require('fs');

//Вывод данных с консоли
/*
console.log("Hello world!");
console.log(process.argv[0]);
*/
console.log(process.argv[1]);


//Синхронно читаем содержимое файла
let fileContent = fs.readFileSync('file.txt', 'utf8');
console.log(fileContent);

fs.writeFileSync('file2.txt', 'test')