function filterArray (arr1, arr2) {
    return arr1.filter(item => !arr2.includes(item));
}

const arr1 = ["String", "String", "Number", "Bool"];
const arr2 = ["String", "String"];

console.log(filterArray(arr1, arr2));