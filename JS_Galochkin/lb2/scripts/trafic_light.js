document.getElementById('button').onclick = myFunction;
var stat = "g";

function myFunction() {
    "use strict";

    var img = document.getElementById("imgg");
    if (stat == "g") {
        img.src = "images/traffic_light_yellow.jpg";
        stat = "y";
    } else if (stat == "y") {
        img.src = "images/traffic_light_red.jpg";
        stat = "r";
    } else {
        img.src = "images/traffic_light_green.jpg";
        stat = "g";
    }


}