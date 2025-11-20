const simpleCache = new WeakMap();

function withCache(fn) {
    const fnCache = new Map();
    simpleCache.set(fn, fnCache);
    return function(...args) {
        const key = JSON.stringify(args);
        if (fnCache.has(key)) {
            console.log("из хэша");
            return fnCache.get(key);
        }
        console.log("вычисление...");
        const result = fn.apply(this, args);
        fnCache.set(key, result);
        return result;
    };
}

function calculation(x, y) {
    for (let i = 0; i < 1000000; i++) {}
        return x * y + x + y;
}

const cached = withCache(calculation);
console.log("первое вычисление: ", cached(5,3));
console.log("вызов из хэша: ", cached(5,3));
console.log("новые параметры: ", cached(2,4));
console.log("вызов из хэша: ", cached(2,4));