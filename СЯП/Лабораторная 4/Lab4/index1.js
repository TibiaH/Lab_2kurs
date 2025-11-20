const products = new Set();

function addProduct (product) {
    if (products.has(product))
    {
        console.log(`The product ${product} has already been added to the list`);
        return false;
    }
    products.add(product);
    console.log(`Product ${product} successfully added`);
    return true;
}

function removeProduct (product) {
    const removed = products.delete(product);
    if (removed) {
        console.log(`Product ${product} successfully deleted`);
        return true;
    } else {
        console.log(`Product ${product} not found in the list`);
        return false;
    }
    return removed;
}

function hasProduct (product) {
    const search = products.has(product);
    console.log(`Product ${product} ${search ? `Founded` : `Not founded`}`);
    return search;
}

function countProduct () {
    const count = products.size;
    console.log(`Count of the product = ${count}`);
    return count;
}

function getAll () {
    return Array.from(products);
}

addProduct("macbook");
addProduct("iphone");
addProduct("imac");
removeProduct("iphone");
hasProduct("iphone");
countProduct()
console.log(`All products: ${getAll()}`);