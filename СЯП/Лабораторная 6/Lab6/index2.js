const user = {
    name: "Dmitry",
    age: 18
};
const admin = {
    admin: true,
    ...user
};
console.log(admin);