const fs = require("fs");
import{Queue} from './node_c:/Users/Igor/Desktop/repos/js/haffman/node_modules/js-queue/queue.jsmodules/js-queue/'
function CreateNode(weight, name) {
    return {
        code: undefined,
        weight: weight,
        name: name,
        parent: undefined,
        used: undefined
    }
}

function UpdateNodes(node1, node2, ParentNode) {
    node1.parent = ParentNode;
    node2.parent = ParentNode;
    node1.code = 0;
    node2.code = 1;
}

function ConnectNodes(node1, node2) {
    let ParentNode = CreateNode(
        node1.weight + node2.weight,
        node1.name + node2.name,
    );
    UpdateNodes(node1, node2, ParentNode);
    return ParentNode;
}

let letterCount = new Map();
let letters = new Array();
let inFile = process.argv[3];
if (!fs.existsSync(inFile)) {
    console.log("Input file doesn't exist");
    return
}
//let outFile = fs.readFileSync(process.argv[4], "utf8")

let s = fs.readFileSync(inFile, "utf8");
for (let i = 0; i < s.length; i++) {
    if (letterCount.has(s[i])) {
        letterCount.set(s[i], CreateNode(letterCount.get(s[i]).weight + 1, s[i]));
    }
    else {
        letterCount.set(s[i], CreateNode(1, s[i]));
    }
}



