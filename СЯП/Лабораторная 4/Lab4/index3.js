const Products = new Map();
let nextId = 1;

function addProduct(name, count, price) {
    const id = nextId++;
    Products.set(id, {id, name, count: Number(count), price: Number(price)});
    return id;
}

function removeProductId(id) {
    return Products.delete(id);
}

function removeProductsName(name) {
    let removedCount = 0;
    for (let [id, product] of Products) {
        if (product.name === name) {
            Products.delete(id);
            removedCount++;
        }
    }
    return removedCount;
}

function updateCount(id, newCount) {
    const product = Products.get(id);
    if (product) {
        product.count = Number(newCount);
        return true;
    }
    return false;
}

function updatePrice (id, newPrice) {
    const product = Products.get(id);
    if (product) {
        product.price = Number(newPrice);
        return true;
    }
    return false;
}

function productSize() {
    return Products.size;
}

function productCost() {
    let total = 0;
    for (let product of Products.values()) {
        total += Products.count * Products.price;
    }
    return total;
}

function allProducts() {
    return Array.from(Products.values());
}


/////Приминение функций/////

const product1 = addProduct("apple", 4, 4000);
const product2 = addProduct("samsung", 5, 3500);
const product3 = addProduct("xiaomi", 6, 3800);

updateCount(product1, 10);
updatePrice(product2, 3900);
console.log("Изменение количества\n", allProducts());
removeProductId(2);
removeProductsName("xiaomi");
console.log("Удаление двух товаров \n", allProducts());