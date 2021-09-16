document.getElementById('button1').onclick = myFunction;
document.getElementById('button2').onclick = myFunction;
var turnOn = false;

function myFunction() {
    "use strict";

    var img1 = document.getElementById('img1');
    var img2 = document.getElementById('img2');
    if(!turnOn)
    {
        img1.style.display = "none";
        img2.style.display = "inline-block";
        turnOn = true;
    }else{
        img1.style.display = "inline-block";
        img2.style.display = "none";
        turnOn = false;
    }


}