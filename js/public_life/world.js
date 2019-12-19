let intervalID;

let Field = new Array();

class cell {
	isAlive = false;
	x = 0;
	y = 0;
	constructor(X, Y) {
		this.x = X;
		this.y = Y;
	}
}

function drawWorld() {
	var result = "<tbody>";
	var arr = Field;
	for (var i = 0; i < arr.length; i++) {
		result += "<tr>";
		for (var j = 0; j < arr[i].length; j++) {
			result += drawCell(arr[i][j]);
		}
		result += "</tr>";
	}
	result += "</tbody>";
	return result;
}

function drawCell(cell) {
	var cl = '';
	if (cell.isAlive) {
		cl = ' class="alive"'
	}
	return '<td><div' + cl + ' x=' + cell.x + ' y=' + cell.y + ' onclick="changeCell(this);">&nbsp;</div></td>';
}

function newWorld() {
	var heig = parseInt(document.getElementById("heig").value);
	var widt = parseInt(document.getElementById("widt").value);
	initGeneration(heig, widt);
	refreshWorld();
}

function refreshWorld() {
	var table = document.getElementById("world");
	table.innerHTML = drawWorld();
}

function next() {
	let old = Field;
	Field = GetNextGeneration(old);
	refreshWorld();
}

function go() {
	stop();
	intervalID = setInterval('next()', 100);
}

function stop() {
	clearInterval(intervalID);
}

function random() {
	stop();
}

function changeCell(elem) {
	changeGeneration(parseInt(elem.getAttribute("x")), parseInt(elem.getAttribute("y")));
	refreshWorld();
}

function initGeneration(heig, widt) {
	Field = new Array(heig);
	for (let i = 0; i < heig; i++) {
		Field[i] = new Array(widt);
		for (let j = 0; j < widt; j++) {
			Field[i][j] = new cell(j, i);
		}
	}
}

function changeGeneration(x, y) {
	Field[y][x].isAlive = !Field[y][x].isAlive;
}
