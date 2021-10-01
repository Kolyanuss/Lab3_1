function str_del() {
    selection = window.getSelection().toString();
    var str = document.getElementById("elen").textContent;
    var start = str.indexOf(selection);
    var end = start + selection.length;
    var rezult = str.slice(0, start) + str.slice(end);

    document.getElementById("elen").textContent = rezult;
}

///
function getProperty(obj, key) {
    return obj[key];
}
var obj = {
    key1: 'key1_val',
    key2: 'key2_val',
    key3: 'key3_val',
};
var output = getProperty(obj, 'key2');
console.log(output);

///
function addProperty(myObj, newkey) {
    myObj.key = newkey;
    return myObj[newkey] = newkey + '_val';
}
var myObj = {};
addProperty(myObj, "myProperty");
addProperty(myObj, "myProperty2");
console.log(myObj.myProperty2);

///
function removeProperty(obj, keyForDel) {
    delete obj[keyForDel];
}
var obj = {
    name: 'Sam',
    age: 20
}
removeProperty(obj, 'name');
console.log(obj.name);

///
function getFullName(name, surname) {
    return name + ' ' + surname;
}
var output = getFullName('Joe', 'Smith');
console.log(output);

///
function getLengthOfWord(str) {
    return str.length;
}
var output = getLengthOfWord('some');
console.log(output);

///
function getLengthOfTwoWords(word1, word2) {
    return word1.length + word2.length;
}
var output = getLengthOfTwoWords('some', 'words');
console.log(output);

///
function isGreaterThan(x, y) {
    return x > y;
}
var output = isGreaterThan(11, 10);
console.log(output);

///
function isEven(x) {
    return x % 2 == 0;
}
var output = isEven(11);
console.log(output);

///
function isSameLength(word1, word2) {
    return word1.length == word2.length;
}
var output = isSameLength('words', 'super');
console.log(output);

///
function isEvenAndGreaterThanTen(x) {
    return x % 2 == 0 && x > 10;
}
var output = isEvenAndGreaterThanTen(13);
console.log(output);

///
function computeAreaOfATriangle(x, y) {
    return 0.5 * x * y;
}
var output = computeAreaOfATriangle(4, 6);
console.log(output);