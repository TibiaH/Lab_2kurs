const person = {
    name: 'Дмитрий',
    age: 18,

    greet: function() {
        return `Привет от ${this.name}`;
    },

    ageAfterYear: function(year) {
        return this.age + year
    }
};

const car = {
    model: 'BMW 7-series',
    year: 2014,

    getinfo: function() {
        return `Модель - ${this.model}, год - ${this.year}`;
    }
};

function Book(title, author) {
    this.title = title
    this.author = author

    this.getTitle = function() {
        return this.title
    };

    this.getAuthor = function() {
        return this.author
    };
}

const team = {
    players: [
        {name: 'name1', number: '1'},
        {name: 'name2', number: '2'},
        {name: 'name3', number: '3'}
    ],

    showPlayers: function() {
        this.players.forEach((player) => {
            console.log(`Имя - ${player.name}, номер - ${player.number}`)
        })
    }
}

const counter = (function() {
    let count = 0;
    return {
        increment: function() {
            count++;
            return count;
        },
        decrement: function() {
            count--;
            return count;
        },
        getCount: function() {
            return count;
        }
    }
})();

const item = {
    price: 100
}
Object.defineProperty(item, 'price', {
    value: 100,
    writable: false,
    configurable: false
})

const circle = {
    radius: 0,

    get area() {
        return Math.PI * this.radius * this.radius
    }
}

const car2 = {
    make: "BMW",
    model: "7-series",
    year: 2020
};
Object.defineProperties(car2, {
    make: {value: car2.make, writable: false, configurable: false },
    model: {value: car2.model, writable: false, configurable: false},
    year: {value: car2.year, writable: false, configurable: false}
})

const numbers = [1, 2, 3]
Object.defineProperty(numbers, 'sum', {
    get: function() {
        return this[0] + this[1] + this[2]
    },
})

const rectangle = {
    width: 0,
    height: 0,

    get area() {
        return this.width * this.height
    },
}

const user = {
    firstName: 'Дмитрий',
    lastName: 'Гоман',

    get fullName() {
        return `${this.lastName} ${this.firstName}`
    },

    set fullName(value) {
        const parts = value.split(' ')
        this.lastName = parts[0] || ''
        this.firstName = parts[1] || ''
    }
}

//1)
console.log(person.greet())
console.log(person.ageAfterYear(5))

//2)
console.log(car.getinfo())

//3)
const book = new Book('Ведьмак', 'Анджей Сапковский')
console.log(book.getTitle())
console.log(book.getAuthor())

//4)
team.showPlayers()

//5)
console.log(counter.increment());
console.log(counter.increment());
console.log(counter.decrement());
//6)
item.price = 200
delete item.price
console.log(item.price)

//7
circle.radius = 5
console.log(circle.area)

//8
car2.make = 'Mercedes'
console.log(car2.make)

//9
console.log(numbers.sum)

//10
rectangle.width = 10
rectangle.height = 5
console.log(rectangle.area)

//11
user.fullName = 'Гоман2 Дмитрий2'
console.log(user.fullName)