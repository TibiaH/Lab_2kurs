let exit = false;
function* move (x = 0, y = 0) {
    while (true) {
        let direction = prompt("Enter the command (left), (right), (up) (dowm): ");
        for (let i = 0; i < 10; i++) {
            switch(direction) {
                case "left": x--; break;
                case "right": x++; break;
                case "up": y++; break;
                case "down": y--; break;
                case "exit": exit = true; return;
                default: console.log("Unknown command"); i = 10; break;
            }
            if (direction !== "exit" && !exit) {
                yield { x, y };
            }
        }
    }
}
let gen = move();
while (!exit) {
    console.log(gen.next().value);
}
