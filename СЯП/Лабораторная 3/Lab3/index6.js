function builder(floors) {
    const tower = [];

    for (let i = 0; i < floors; i++) {
        const stars = '*'.repeat(2 * i + 1);
        tower.push(stars);
    }

    return tower;
}

console.log(builder(10).join('\n'));