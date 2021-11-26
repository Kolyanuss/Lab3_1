using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Collections.Generic;

namespace CSharpClassLibrary
{
    public class Class1
    {
        private FormLab1 myForm;
        private int squareWidth { get; set; }
        private int squareHight { get; set; }
        private int upperWidthSlot { get; set; }
        private int lowerWidthSlot { get; set; }
        private int roundingRadiusSlot { get; set; }
        private int hightSlot { get; set; }
        private int hightToCenterCircle { get; set; }
        private int circleDiameter { get; set; }

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

        [CommandMethod("Lb2Draw")]
        public void MyCommand()
        {
            this.myForm = new FormLab1(this);
            myForm.Show();
        }

        public void onButtonClick(List<int> list)
        {
            squareWidth = list[0];
            squareHight = list[1];
            upperWidthSlot = list[2];
            lowerWidthSlot = list[3];
            roundingRadiusSlot = list[4];
            hightSlot = list[5];
            hightToCenterCircle = list[6];
            circleDiameter = list[7];
            

            //Draw();
        }

        /*public void Draw()
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
        }*/

        public void PrintMsg(string text)
        {
            MessageBox.Show(text);
        }
    }
}