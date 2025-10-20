function SumOfCubes (n) {
    if (n < 0) {
        throw new Error("The number cannot be negative");
    }
    let sum = 0;
    for (let i = 0; i <= n; i++) {
        sum += Math.pow(i,3);
    }
    return sum;
}

console.log(SumOfCubes(2));