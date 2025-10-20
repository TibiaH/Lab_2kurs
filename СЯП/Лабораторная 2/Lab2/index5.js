function repeatString (n,s) {
    if (typeof n !== 'number' || n < 0) {
        throw new Error("the second argument must be positive number");
    }
    if (typeof s !== 'string') {
        throw new Error("the first argument must be string");
    }
    return s.repeat(n);
}

console.log(repeatString(3, "String"));