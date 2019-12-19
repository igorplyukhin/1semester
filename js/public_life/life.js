const tuple = (...args) => Object.freeze(args);

let Directions = [
    tuple(0, 1), tuple(1, 0),
    tuple(1, 1), tuple(0, -1),
    tuple(-1, 0), tuple(-1, -1),
    tuple(1, -1), tuple(-1, 1),
]

function GetNextGeneration(field) {
    let newField = new Array(field.length)
    for (let i = 0; i < field.length; i++) {
        newField[i] = new Array(field[i].length);
        for (let j = 0; j < field[i].length; j++) {
            newField[i][j] = ProceedCell(field[i][j], field);
        }
    }
    return newField;
}

function ProceedCell(cell, field){
    let x = cell.x;
    let y = cell.y;
    let aliveNeighCount = 0;
    for (let dir of Directions) {
        let xNeigh = SetCoordinates(x, dir[0], field[0].length);
        let yNeigh = SetCoordinates(y, dir[1], field.length);
        if (field[yNeigh][xNeigh].isAlive)
            aliveNeighCount++;
    }

    if (cell.isAlive)  {
        console.log('alive');
        if (aliveNeighCount < 2 || aliveNeighCount > 3)
            cell.isAlive = false;
    }
    else{
        console.log('dead');
        if (aliveNeighCount === 3)
            cell.isAlive = true;
    }
    return cell;
}

function SetCoordinates(oldX, deltaX, border){
    let x = oldX + deltaX;
    if (x < 0)
        return border - 1;
    else if (x >= border)
        return 0;;
    return x;
}