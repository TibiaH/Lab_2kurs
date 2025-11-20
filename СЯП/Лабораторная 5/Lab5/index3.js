var a = 10;
let b = 20;
function c() {
    return "Hello, world";
}
console.log(window.a);
console.log(window.b);
console.log(window.c);
window.a = 30;
console.log(a);
console.log("все обьекты window: ", Object.getOwnPropertyNames(window).length);
