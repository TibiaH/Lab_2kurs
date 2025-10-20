const array1 = [1,2,3];
const array2 = [4,5,6];

const mergedArray = [array1, array2].reduce((accumulator, currentValue) => {
    return accumulator.concat(currentValue);
}, []);

console.log(mergedArray);

