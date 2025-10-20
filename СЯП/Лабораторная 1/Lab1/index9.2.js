const week = {
    1: 'пн',
    2: 'вт',
    3: 'ср',
    4: 'чт',
    5: 'пт',
    6: 'сб',
    7: 'вс'
};

const input = prompt('Введите номер дня недели: ');

if (week[input]) {
    alert(week[input]);
} else {
    alert("неверный номер");
}