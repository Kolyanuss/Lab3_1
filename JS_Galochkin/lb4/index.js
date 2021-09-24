function findMinLengthOfThreeWords() {
    selection = window.getSelection().toString();
    var str = document.getElementById("elen").textContent;
    var start = str.indexOf(selection);
    var end = start + selection.length;
    var rezult = str.slice(0, start) + str.slice(end);

    document.getElementById("elen").textContent = rezult;
    console.log("1.1");
}
