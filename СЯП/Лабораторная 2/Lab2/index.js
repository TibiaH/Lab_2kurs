function basicOperation (operator, operand1, operand2) {
    switch (operator) {
        case '+': 
        return operand1 + operand2;
        case '-':
            return operand1 - operand2;
        case '*':
            return operand1 * operand2;
        case '/': 
            if (operand2 == 0) {
                throw new Error("Division bu zero is impossible");
            }
            return operand1 / operand2;
        default:
            throw new Error(`Wrong Operator: ${operator}`);
    }
}

console.log(basicOperation('*', 5,5));