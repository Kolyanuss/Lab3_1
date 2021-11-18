using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using System.Threading;

namespace CSharpClassLibrary
{
    public class Class1
    {
        #region construct and destruct
        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            MessageBox.Show("Hello!");
        }

        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
        }

        #endregion

        // эта функция будет вызываться при выполнении в AutoCAD команды «CalcDot»
        [CommandMethod("CalcDot")]
        public void MyCommand1()
        {
            FormLab1 myForm = new FormLab1();
            myForm.Show();

            Thread myThread = new Thread(new ParameterizedThreadStart(waitfordot));
            myThread.Start(myForm); // запускаем поток

            //myThread.Join();
            
            var dot = myForm.getDot();

            int x = dot.Key;
            int y = dot.Value;
            int ox = 0, oy = 0;
            int radius = 2;
            double d = Math.Sqrt(Math.Pow(ox - x, 2) + Math.Pow(oy - y, 2));

            bool part1 = y > 0 && (x < 0 || x > 1);
            bool part2 = y < 0 && (x < -1 || x > 0);

            if (d <= radius && (part1 || part2))
                Rezult("Точка лежить в заданій області.");
            else
                Rezult("Точка лежить поза заданою областю.");
        }
        public static void waitfordot(object f)
        {
            FormLab1 form = (FormLab1)f;
            while (!form.hasDot)
            {
                Thread.Sleep(500);
            }
        }


        public void Rezult(string text)
        {
            MessageBox.Show(text);
        }
    }
}