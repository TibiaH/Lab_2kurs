const students = new Set();

function createStudent(studentId, group, fullName) {
    return {studentId, group, fullName};
}

function addStudent(studentId, group, fullName) {
    const newStudent = createStudent(studentId, group, fullName);
    for (const student of students) {
        if (student.studentId == studentId) {
            console.log("student with this record book number already exist");
            return false;
        }
    }
    students.add(newStudent);
    console.log("student successfully added");
    return true;
}

function removeStudent(studentId) {
    let removed = null;
    for (const student of students) {
        if (student.studentId == studentId) {
            removed = student;
            break;
        }
    }

    if (removed) {
        students.delete(removed);
        console.log("student deleted");
        return true;
    } else {
        console.log("student with this record book number not fiunded");
        return false;
    }
}

function filter(group) {
    const result = [];
    for (const student of students) {
        if (student.group === group) {
            result.push(student);
        }
    }
    console.log(`student of ${group} group: `, result);
    return result;
}

function sort() {
    const studentArray = Array.from(students);
    return studentArray.sort((a,b) => a.studentId - b.studentId);
}

function allStudent() {
    return Array.from(students);
}

addStudent(1234, 7, "Homan Dmitry Pavlovich 1");
addStudent(1235, 7, "Homan Dmitry Pavlovich 2");
addStudent(1236, 8, "Homan Dmitry Pavlovich 3");
filter(7);
console.log(sort());
removeStudent(1234);
console.log(allStudent());