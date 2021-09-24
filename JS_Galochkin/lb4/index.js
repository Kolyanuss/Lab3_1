function findMinLengthOfThreeWords(word1, word2, word3) {
    var rez = word1.length < word2.length ?
        (word1.length < word3.length ? word1 : word3) :
        (word2.length < word3.length ? word2 : word3);
    return ("1.1\n" + rez.length);
}
var output = findMinLengthOfThreeWords('a', 'bb', 'ccc');
console.log(output);

function filterOddElements(arr) {
    var rez = [];
    for (let i = 0; i < arr.length; i++) {
        if (arr[i] % 2 != 0) {
            rez = rez.concat(arr[i]);
        }
    }
    return ("1.2\n[" + rez + ']');
}
var output = filterOddElements([1, 2, 3, 4, 5, 6, 7]);
console.log(output);

function getLengthOfShortestElement(arr) {
    var minlenght = arr[0].length;
    for (let i = 0; i < arr.length; i++) {
        if (arr[i].length < minlenght) {
            minlenght = arr[i].length;
        }
    }
    return ("1.3\n" + minlenght);
}
var output = getLengthOfShortestElement(['a', 'bb', 'ccc']);
console.log(output);

function joinArrayOfArrays(arr) {
    var rez = [];
    for (let i = 0; i < arr.length; i++) {
        rez = rez.concat(arr[i]);
    }
    return ("1.4\n[" + rez + ']');
}
var output = joinArrayOfArrays([[1, 3], [true, false], ['ccc', 'aaa']]);
console.log(output);

function findSmallestNumberAmongMixedElements(arr) {
    if (arr.length == 0) return 0;

    var minVal = null;
    for (let i = 0; i < arr.length; i++) {
        if (Number.isInteger(arr[i])) {
            minVal = arr[i];
            break;
        }
    }
    if (minVal == null) return 0;

    for (let i = 0; i < arr.length; i++) {
        if (Number.isInteger(arr[i]) && arr[i] < minVal) {
            minVal = arr[i];
        }
    }
    return ("1.5\n" + minVal);
}
var output = findSmallestNumberAmongMixedElements([7, 'x', 5, 2, "asasins"]);
console.log(output);

function computeSummationToN(N) {
    var rez = 0;
    for (let i = 1; i <= N; i++) {
        rez += i;
    }
    return ("1.6\n" + rez);
}
var output = computeSummationToN(4);
console.log(output);

function convertScoreToGrade(score) {
    var rez;

    switch (true) {
        case score >= 0 && score <= 34:
            rez = 'F';
            break;
        case score >= 35 && score <= 59:
            rez = 'Fx';
            break;
        case score >= 60 && score <= 68:
            rez = 'E';
            break;
        case score >= 69 && score <= 74:
            rez = 'D';
            break;
        case score >= 75 && score <= 81:
            rez = 'CA';
            break;
        case score >= 82 && score <= 89:
            rez = 'B';
            break;
        case score >= 90 && score <= 100:
            rez = 'A';
            break;

        default:
            rez = 'INVALID SCORE';
            break;
    }

    return ("1.7\n" + rez);
}
var output = convertScoreToGrade(100);
console.log(output);

function getLongestOfThreeWords(word1, word2, word3) {
    var rez = word1.length > word2.length ?
        (word1.length > word3.length ? word1 : word3) :
        (word2.length > word3.length ? word2 : word3);
    return ("1.8\n" + rez);
}
var output = getLongestOfThreeWords('a', 'bb', 'ccc');
console.log(output);

function multiply(x, y) {
    var res = 0;
    while (y != 0) {
        if ((y & 1) == 1) res += x;
        x <<= 1;
        y >>= 1;
    }
    return '1.9\n' + res;
}
var output = multiply(10, 10);
console.log(output);

function computeSumBetween(x, y) {
    var rez = 0;
    if (y < x) return ("1.10\n" + rez);
    for (let i = x; i < y; i++) {
        rez += i;
    }
    return ("1.10\n" + rez);
}
var output = computeSumBetween(1, 4);
console.log(output);

