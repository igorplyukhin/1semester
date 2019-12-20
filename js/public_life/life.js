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

function ProceedCell(oldCell, field){
    let cell = new Cell(oldCell.x, oldCell.y)
    let aliveNeighCount = 0;
    for (let dir of Directions) {
        let xNeigh = SetCoordinates(oldCell.x, dir[0], field[0].length);
        let yNeigh = SetCoordinates(oldCell.y, dir[1], field.length);
        if (field[yNeigh][xNeigh].isAlive)
            aliveNeighCount++;
    }

    if (oldCell.isAlive)  {
        if (aliveNeighCount === 2 || aliveNeighCount === 3)
            cell.isAlive = true;
    }
    else if (aliveNeighCount === 3)
            cell.isAlive = true;

    return cell;
}

function SetCoordinates(oldX, deltaX, border){
    let x = oldX + deltaX;
    if (x < 0)
        return border - 1;
    else if (x >= border)
        return 0;
    return x;
}