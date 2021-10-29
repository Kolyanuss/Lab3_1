<?php
    header('Content-Type: text/html; charset=utf-8');
?>
<HTML>

<HEAD>
    <TITLE> Перша сторінка </title>
</head>
<H2> Назвіться, будь ласка </h2>
<FORM name=F1 METHOD="POST" ACTION="">
    Ім'я ....
    <INPUT TYPE="text" NAME="name"> <BR>
    Вік <INPUT TYPE="text" NAME="age">
    <P> <INPUT TYPE="submit" VALUE="ВВЕДЕННЯ" onclick="Proverka ()">
</FORM>
<SCRIPT>
    function Proverka() {
        Im = document.F1.name.value
        vozr = document.forms[0].elements[1].valuest = ""
        if (im == "") st = "ім'я \ n"
        if (vozr == "") st += "вік"
        if (st == "") document.F1.action = "Prim3_1.php"
        else // скасування передачі на веб-сервер
        {
            Str = "Введіть: \ n" + st
            alert(str)
            return false
        }
    }
</Script>

</Html>
<HTML>

<HEAD>
    <TITLE> Друга сторінка </title>
</head>

<BODY>
    <H3> П Р І В Е Т С Т В А </h3>

    <?php
    $Imja = $_POST["name"]; // прийом параметрів з форми
    $Age = $_POST["age"];
    $X = "Привіт!;
    // $Imja. ";
    if ($age > 50) echo "$x Ви включені в старшу групу.";
    elseif ($age > 30) echo "$x Ви включених групу середнього віку.";
    else echo "$x Ваше ставлення до молодіжної групи.";
    ?>
    <P>
        <A href="Prim3_1.html"> Повернення </a>
</Body>

</Html>