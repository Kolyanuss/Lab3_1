using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CSharpClassLibrary
{
    public class Class1
    {
        private FormLab1 myForm;
        private float x = 2;
        private float y = 2;
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
        public void MyCommand()
        {
            //Drawpoint();
            this.myForm = new FormLab1(this);
            myForm.Show();
        }

        public void onButtonClick(float dx, float dy)
        {
            this.x = dx;
            this.y = dy;
            Drawpoint();
            Calculate();
        }

        private void Calculate()
        {
            //logic
            int ox = 0, oy = 0;
            int radius = 2;
            double d = Math.Sqrt(Math.Pow(ox - x, 2) + Math.Pow(oy - y, 2));

            bool part1 = x > 1 || y > 1;
            bool part2 = x < 0 && y > 0;
            bool part3 = x < -1 || y < -1;
            bool part4 = x > 0 && y < 0;

            if (d <= radius && (part1 || part2 || part3 || part4))
                PrintMsg("Точка лежить в заданій області.");
            else
                PrintMsg("Точка не попадає в область.");
        }

        public void Drawpoint()
        {
            DocumentCollection acDocMgr = acad.DocumentManager;
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (DBPoint acPoint = new DBPoint(new Point3d(x, y, 0)))
                    {
                        acBlkTblRec.AppendEntity(acPoint);
                        acTrans.AddNewlyCreatedDBObject(acPoint, true);
                    }
                    acCurDb.Pdmode = 0;
                    acCurDb.Pdsize = 4;

                    acTrans.Commit();
                }
            }
            // Set the new document current
            acDocMgr.MdiActiveDocument = acDoc;
        }

        public void PrintMsg(string text)
        {
            MessageBox.Show(text);
        }
    }
}