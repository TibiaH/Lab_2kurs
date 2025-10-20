const fioString = (name = "Dmitry", surname, secondname) => {
    console.log(String().concat(name,surname,secondname));
};
fioString(undefined, surname = "Homan", prompt("Enter the secondname: "));