
enum Stages {
    Data = 0,
    Grid = 1,
    Dices = 2,
}

let stage: Stages = Stages.Data;

let width: number;
let height: number;
let rounds: number;

let grid: number[][];

function rollDice(min: number, max: number): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

let gridIdx = 0;

function onLineRead(line: string) {
    // console.log(`>${line}`)
    if (stage === Stages.Data) {
        const data = line.split(" ").map(Number);
        width = data[0];
        height = data[1];
        rounds = data[2];
        grid = Array(height).fill([]).map(() => []);
        stage = Stages.Grid;
    } else if (stage === Stages.Grid) {
        gridIdx++
        const rowData = line.split(" ").map(Number).map(x=> x== 0 ? -1 : 0);
        grid[gridIdx] = rowData;
        if (gridIdx === height) {
            stage = Stages.Dices;
        }
    } else if (stage === Stages.Dices) {
        const rollData = line.split(" ").map(Number);
        const first = rollData[1];
        const second = rollData[2];
        let x: number=0, y: number=0, limit = 5000;
  
        while(true){
            if(!((grid[y][x] !== 0 || grid[y][x] === -1) && limit !== 0)) break;
            x = rollDice(0, width / 2-1);
            y = rollDice(0, height );
        }
        grid[y][x] = first;
        // console.log(`--- GRID --- ${grid.map(x=>x.join(" ").replaceAll("-1", "X")).join("\n")}`)
        console.log(`p ${first} ${x},${y-1}`);
    }
}


async function readStdin(onLineRead) {
    const stream = await Bun.stdin.stream();
    const decoder = new TextDecoder();

    let remainingData = "";

    for await (const chunk of stream) {
        const str = decoder.decode(chunk);

        remainingData += str; // Append the chunk to the remaining data

        // Split the remaining data by newline character
        let lines = remainingData.split(/\r?\n/);
        // Loop through each line, except the last one
        while (lines.length > 1) {
            // Remove the first line from the array and pass it to the callback
            onLineRead(lines.shift());
        }
        // Update the remaining data with the last incomplete line
        remainingData = lines[0];
    }
}
readStdin(onLineRead);