let exit = false;

function* move() {
    var a = 1;
    yield(a)
    a++;
    yield(a)
}
var total = move()
console.log(total.next().value);
console.log(total.next().value);