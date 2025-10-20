function SumOfArray (numbers) {
    if (!Array.isArray(numbers)) {
        throw new Error ("The argument must be an array");
    }
    if (numbers.length == 0) {
        return 0;
    }
    
    let sum = 0;
    for (let i = 0; i < numbers.length; i++) {
        sum += numbers[i];
    }
    return sum / numbers.length;
}

console.log(SumOfArray([1,2,3,4,5]))