function reverseString (str) {
    if (typeof str !== 'string') {
        throw new Error ("Argument must be string");
    }

    const Echar = str.split('')
    .filter(char => /[a-zA-Z]/.test(char));

    return Echar.reverse().join('');
}

console.log(reverseString("JavaScript5"));