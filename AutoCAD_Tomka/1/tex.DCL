tex :dialog{   //початок діалогу  
label="Вивід напису";   //заголовок діалогового вікна
:spacer{height=1;}   //вставка пустого рядка
:boxed_column{   //відображення в колонку
label="Текст";    //заголовок колонки
:edit_box{key="txt";value="0";edit_width=10;}   //редаговане текстове поле   
}   
:spacer{height=1;}   //вставка пустого рядка
:boxed_column{   //відображення в колонку
label="Координати точки ";   //заголовок колонки
:edit_box {label="x";key="xx1";value="0";edit_width=8;}
:edit_box {label="y";key="yy1";value="0";edit_width=8;}
}
:spacer{height=1;}   //вставка пустого рядка
ok_cancel;  // додавання кнопок ОК и Отмена
}
