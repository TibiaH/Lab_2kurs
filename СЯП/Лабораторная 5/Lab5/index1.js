function square(h) {
    return function (a) {
        return function (b) {
            return a * b * h;
        };
    };
}
let v8 = square(8);
console.log(v8(5)(5));