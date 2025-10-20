const teacher = "Homan Dmitry Homan";
const FIO = teacher.toLowerCase().split(" ");

const input = prompt("Введите ФИО");
const inputFIO = input.toLowerCase().split(" ");

if (
	(inputFIO[0] === FIO[0] && inputFIO[1] === FIO[1]) ||
	(inputFIO[1] === FIO[1] && inputFIO[2] === FIO[2]) ||
	(inputFIO[0] === FIO[0] && inputFIO.length === 1)
) {
	alert("thats right");
} else {
	alert("ERROR");
}