let str = "ABC";
let ascii = "";

for (let i = 0; i < str.length; i++) {
    ascii += str.charCodeAt(i);
}

console.log(ascii);

const ascii2 = ascii.replace(/7/g, '1');
console.log(ascii2);

let result = ascii - ascii2;
console.log(result);