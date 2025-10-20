const week = ["пн","вт","ср","чт","пн","сб", "вс"];
const input = prompt("Enter the number of the day of the week");
if (input > 0 && input < 8) {
    alert(week.at(input - 1));
} else {
    alert('Incorrect number');
}

