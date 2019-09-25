function CreateNode(code, weight, name) {
    return {
        code: code,
        weight: weight,
        name: name,
        parent: null,
        used: null
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
        code,
        node1.weight + node2.weight,
        node1.name + node2.name,
    );
    UpdateNodes(node1, node2, ParentNode); 
    return ParentNode;
}


let s = toString(process.argv[2]);
for (let i = 0; i < s.length; i++) {
    
}
