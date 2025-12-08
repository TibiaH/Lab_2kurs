let user = {
    name: 'Masha',
    age: 21
};
let copy1 = {...user}
//----------------------------
let numbers = [1, 2, 3]
let copy2 = [...numbers]
//----------------------------
let user1 = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
};
let copy3 = {
    ...user1,
    location: {...user1.location}
}
//----------------------------
let user2 = {
    name: 'Masha',
    age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};
let copy4 = {
    ...user2,
    skills: [...user2.skills]
}
//----------------------------
let array = [
    {id: 1, name: 'Vasya', group: 10},
    {id: 2, name: 'Ivan', group: 11},
    {id: 3, name: 'Masha', group: 12},
    {id: 4, name: 'Petya', group: 10},
    {id: 5, name: 'Kira', group: 11},
]
let copy5 = array.map(item => ({...item}))
let copy11 = [...array];
copy11[0].name= 'Dima';
console.log(copy11);
console.log(array)
//----------------------------
let user4 = {
    name: 'Masha',
    age: 19,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        exams: {
            math: true,
            programming: false
        }
    }
};
let copy6 = {
    ...user4,
    studies: {
        ...user4.studies,
        exams: {...user4.studies.exams}
    }
}
//----------------------------
let user5 = {
    name: 'Masha',
    age: 22,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {math: true, mark: 8},
            {programming: true, mark: 4},
        ]
    }
};
let copy7 = {
    ...user5,
    studies: {
        ...user5.studies,
        department: {...user5.studies.department},
        exams: user5.studies.exams.map(exam => ({...exam}))
    }
}
//----------------------------
let user6 = {
    name: 'Masha',
    age: 21,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                math: true,
                mark: 8,
                professor: {
                    name: 'Ivan Ivanov ',
                    degree: 'PhD'
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Petrov',
                    degree: 'PhD'
                }
            },
        ]
    }
};
let copy8 = {
    ...user6,
    studies: {
        ...user6.studies,
        department: {...user6.studies.department},
        exams: user6.studies.exams.map(exam => ({
            ...exam,
            professor: {...exam.professor}
        }))
    }
}
//----------------------------
let user7 = {
    name: 'Masha',
    age: 20,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                math: true,
                mark: 8,
                professor: {
                    name: 'Ivan Petrov',
                    degree: 'PhD',
                    articles: [
                        {title: "About HTML", pageNumber: 3},
                        {title: "About CSS", pageNumber: 5},
                        {title: "About JavaScript", pageNumber: 1},
                    ]
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Ivanov',
                    degree: 'PhD',
                    articles: [
                        {title: "About HTML", pageNumber: 3},
                        {title: "About CSS", pageNumber: 5},
                        {title: "About JavaScript", pageNumber: 1},
                    ]
                }
            },
        ]
    }
}
let copy9 = {
    ...user7,
    studies: {
        ...user7.studies,
        department: {...user7.studies.department},
        exams: user7.studies.exams.map(exam => ({
            ...exam,
            professor: {
                ...exam.professor,
                articles: exam.professor.articles.map(article => ({...article}))
            }
        }))
    }
}
//----------------------------
let store = {
    state: {
        profilePage: {
            posts: [
                {id: 1, message: "Hi", likesCount: 12},
                {id: 2, message: 'By', likesCount: 1},
            ],
            newPostText: "About me",
        },
        dialogPage: {
            dialogs: [
                {id: 1, name: 'Valera'},
                {id: 2, name: 'Andrey'},
                {id: 3, name: 'Sasga'},
                {id: 4, name: 'Viktor'},
            ],
            message: [
                {id: 1, message: 'hi'},
                {id: 2, message: 'hi hi'},
                {id: 3, message: 'hi hi hi'},
            ],
        },
        sidebar: [],
    },
}
let copy10 = {
    state: {
        profilePage: {
            ...store.state.profilePage,
            posts: store.state.profilePage.posts.map(post => ({...post}))
        },
        dialogPage: {
            ...store.state.dialogPage,
            dialogs: store.state.dialogPage.dialogs.map(dialog => ({...dialog})),
            message: store.state.dialogPage.message.map(msg => ({...msg}))
        },
        sidebar: [...store.state.sidebar]
    }
}

//1)
copy1.name = "rename"
console.log(user.name)

//2)
copy7.studies.department.group = 12
copy7.studies.exams[1].mark = 10
console.log(copy7.studies.exams[1].mark)
console.log(user5.studies.exams[1].mark)

//3)
copy8.studies.exams[0].professor.name = 'new Name'
console.log(copy8.studies.exams[0].professor.name)
console.log(user6.studies.exams[0].professor.name)

//4
copy9.studies.exams[1].professor.articles.forEach(article => {
    if (article.title === "About CSS") {
        article.pageNumber = 3
    }
})
copy9.studies.exams[1].professor.articles.forEach(article => {
    console.log(article.pageNumber)
})
user7.studies.exams[1].professor.articles.forEach(article => {
    console.log(article.pageNumber)
})
//5
copy10.state.profilePage.posts.forEach(post => {
    post.message = "Hello"
})
copy10.state.profilePage.newPostText = "Hello"
copy10.state.dialogPage.message.forEach(msg => {
    msg.message = "Hello"
})
copy10.state.profilePage.posts.forEach(post => {
    console.log(post.message)
})
console.log(store.state.profilePage.posts[0].message)