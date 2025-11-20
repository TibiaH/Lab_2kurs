function sumArray (arr) {
    let sum = 0;

    for (let i = 0; i < arr.length; i++) {
        if (Array.isArray(arr[i])) {
            sum += sumArray(arr[i]);
        } else if (typeof arr[i] === 'number') {
            sum += arr[i];
        }
    }
    return sum;
}

const array1 = [1, [2,3, [4,5]],6,[7]];
const sum = sumArray(array1);

console.log(sum);