function merge(...object) {
    const mergedObject = {};

    Object.assign(mergedObject, ...object);
    return mergedObject;
}

const obj1 = {a: 1, b: 2};
const obj2 = {c: 3, d: 4};
const obj3 = {a: 5, e: 6};

const result = merge(obj1, obj2, obj3);
console.log(result);