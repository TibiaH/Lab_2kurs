function filterStudents(students) {
    return students.reduce((groups, student) => {
        if (student.age > 17) {
            groups[student.id] = groups[student.id] || [];
            groups[student.id].push(student);
        }
        return groups;
    }, {});
}

const student = [
    {name: "abc", age: 18, id: 7},
    {name: "efg", age: 16, id: 5},
    {name: "qwe", age: 22, id: 7},
    {name: "rty", age: 23, id: 9}
];

const result = filterStudents(student);
console.log(result);