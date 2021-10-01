function computeAreaOfARectangle(a, b) {
    return "1.1\n" + a * b;
}
var output = computeAreaOfARectangle(4, 8);
console.log(output);

function computeAreaOfACircle(r) {
    return "1.2\n" + 3.14 * r * r;
}
var output = computeAreaOfACircle(4);
console.log(output);

function computePower(x, e) {
    return "1.3\n" + Math.exp(x); // i dont know
}
var output = computePower(4);
console.log(output);

function computeSquareRoot(x) {
    return "1.4\n" + Math.sqrt(x);
}
var output = computeSquareRoot(9);
console.log(output);

function getLengthOfThreeWords(a, b, c) {
    return "1.5\n" + (a.length + b.length + c.length);
}
var output = getLengthOfThreeWords("first", "twic", "three"); //14
console.log(output);

function joinArrays(arr1, arr2) {
    return "1.6\n[" + arr1.concat(arr2) + ']';
}
var output = joinArrays([1, 2, 3], [5, 6, 7]);
console.log(output);

function getProductOfAllEIementsAtProperty(obj, key) {
    var rez = 1;
    if (!Array.isArray(obj[key]) || key == '') {
        return "1.7\n0";
    }
    for (let i = 0; i < obj[key].length; i++) {
        rez *= obj[key][i];
    }
    return "1.7\n" + rez;
}
var obj = {
    key: [1, 2, 3, 4]
}
var output = getProductOfAllEIementsAtProperty(obj, 'key');
console.log(output);

function sumDigits(x) {
    var rez = x;
    var iter = 0;
    while (rez != 0) {
        rez = parseInt(rez / 10);
        iter++;
    }
    rez = 0;
    var xx = x;
    for (let i = 1; i <= iter; i++) {
        rez += xx % (10);
        xx = parseInt(xx / 10);
    }

    if (x < 0) {
        rez = -rez;
        rez += 2 * parseInt(x / (10 ** (iter - 1)));
    }
    return "1.8\n" + rez;
}
var output = sumDigits(-316);
console.log(output);

function findShortestWordAmongMixedElements(arr) {
    if (arr.length == 0) return "1.9\n";

    var minWord = null;
    function isStr(x) {
        return !Number.isInteger(x) && !Number.isFinite(x) && !Number.isNaN(x) && !Array.isArray(x);
    }

    for (let i = 0; i < arr.length; i++) {

        if (isStr(arr[i])) {
            minWord = arr[i];
            break;
        }
    }
    if (minWord == null) return "1.9\n";

    for (let i = 0; i < arr.length; i++) {
        if (isStr(arr[i]) && arr[i].length < minWord.length) {
            minWord = arr[i];
        }
    }
    return ("1.9\n" + minWord);
}
var output = findShortestWordAmongMixedElements([7, 'x', 5, 2, "asasins"]);
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
    return ("1.10\n" + minVal);
}
var output = findSmallestNumberAmongMixedElements([7, 'x', 5, 2, "asasins"]);
console.log(output);

function modulo(x, y) {
    var rez = x / y;
    rez = (rez - parseInt(rez)) * y;
    return '1.11\n' + rez;
}
var output = modulo(25, 4);
console.log(output);

function flipEveryNChars(str, n) {
    var rez = '';
    for (let i = 0; i < str.length; i += n) {
        var tmp = '';
        for (let k = 0; k < n && i + k < str.length; k++) {
            tmp += str[i + k];
        }
        for (let j = 0; j < n && j < tmp.length; j++) {
            rez += tmp[tmp.length - 1 - j];
        }
    }
    return '1.12\n' + rez;
}
var output = flipEveryNChars('a short example_ab-', 5);
console.log(output);

function detectOutlierValue(str) {
    var arr = str.split(" ");

    for (let i = 0; i < arr.length; i++) {
        arr[i] = parseInt(arr[i]);
    }
    if (arr.length < 1) {
        return '1.13\nNo number';
    }

    var par = 0, nepar = 0, idPar, idNepar;

    for (let i = 0; i < arr.length; i++) {
        if (arr[i] % 2 == 0) {
            par++;
            idPar = i;
        } else {
            nepar++;
            idNepar = i;
        }
    }
    if (par > 1 && nepar > 1 || par == nepar) {
        return '1.13\nNo unicue';
    }

    if (par == 1) {
        return '1.13\n' + arr[idPar];
    } else return '1.13\n' + arr[idNepar];
}
var output = detectOutlierValue("2 4 7 8 10");
console.log(output);

function findPairForSum(mas, sum) {
    var rez = [];
    for (let i = 0; i < mas.length; i++) {
        for (let j = i + 1; j < mas.length; j++) {
            if (mas[i] + mas[j] == sum) {
                rez.push(mas[i]);
                rez.push(mas[j]);
            }
        }
    }
    return '1.14\n[' + rez + ']';
}
var output = findPairForSum([3, 34, 4, 12, 5, 2], 9);
console.log(output);

function checkDzerkal2Str(str1, str2) {
    var rez = false;
    if (str1.length != str2.length) {
        return '1.15\n' + rez;
    }
    for (let i = 0; i < str2.length; i++) {
        if (str2[i] == str1[0]) {
            rez = true;
            for (let j = 0, l = i; j < str1.length; j++, l++) {
                if (l == str2.length) {
                    l = 0;
                }
                if (str2[l] != str1[j]) {
                    rez = false;
                }
            }
        }
    }
    return '1.15\n' + rez;
}
var output = checkDzerkal2Str("hello world", "orldhello w");
console.log(output);

function binarySerch(arr, num) {
    var index = 'no number';
    var mId = parseInt(arr.length / 2);
    for (let i = 0; i < arr.length; i++) {
        if (arr[mId] == num) {
            index = mId;
            break;
        }
        if (arr[mId] > num) {
            mId = parseInt(mId / 2);
        } else
            mId = parseInt(mId + (mId / 2));
    }
    return '1.16\nid ' + index;
}
var arr = [1, 3, 16, 22, 31, 33, 34];
var output = binarySerch(arr, 31);
console.log(output);

function chekIzogram(str) {
    var rez = true;
    for (let i = 0; i < str.length; i++) {
        for (let j = i + 1; j < str.length; j++) {
            if (str[i] == str[j]) {
                rez = false;
                break;
            }
        }
    }
    return '1.17\n' + rez;
}
var output = chekIzogram("qwe rty");
console.log(output);

function chekPalindrom(str) {
    var rez = true;
    for (let i = 0; i < parseInt(str.length / 2); i++) {
        if (str[i] != str[str.length - 1 - i]) {
            rez = false;
            break;
        }
    }
    return '1.18\n' + rez;
}
var output = chekPalindrom("xyzzyx");
console.log(output);