using System;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Collections.Generic;
using Autodesk.AutoCAD.EditorInput;

namespace CSharpClassLibrary2
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

        /*[CommandMethod("Test", CommandFlags.Modal)]
        public void Test()
        {
            Document doc = acad.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptDistanceOptions pdo = new PromptDistanceOptions("\nFillet radius: ");
            pdo.AllowZero = false;
            pdo.AllowNegative = false;
            PromptDoubleResult pdr = ed.GetDistance(pdo);
            if (pdr.Status != PromptStatus.OK)
                return;

            double radius = pdr.Value;
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect a polyline: ");
            peo.SetRejectMessage("Not a polyline.");
            peo.AddAllowedClass(typeof(Polyline), true);
            PromptEntityResult per = ed.GetEntity(peo);
            if (per.Status != PromptStatus.OK)
                return;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Polyline pline = (Polyline)tr.GetObject(per.ObjectId, OpenMode.ForWrite);
                FilletAll(pline, radius);
                tr.Commit();
            }
        }

        private void FilletAll(Polyline pline, double radius)
        {
            int n = pline.Closed ? 1 : 2;
            for (int i = 0; i < pline.NumberOfVertices - n; i++)
                i += Fillet(pline, radius, i, i + 1);
            if (pline.Closed)
                Fillet(pline, radius, pline.NumberOfVertices - 1, 0);
        }*/

        [CommandMethod("LabTwo")]
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

            Draw();
        }

        public void Draw()
        {
            DocumentCollection acDocMgr = acad.DocumentManager;
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу блоков документа
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // открываем пространство модели (Model Space) - оно является одной из записей в таблице блоков документа
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    #region Polyline
                    // создаем полилинию
                    Polyline acPolyline = new Polyline();

                    // устанавливаем параметры созданного объекта равными параметрам по умолчанию
                    acPolyline.SetDatabaseDefaults();

                    // добавляем к полилинии вершины
                    acPolyline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                    acPolyline.AddVertexAt(1, new Point2d(0, squareHight), 0, 0, 0);

                    acPolyline.AddVertexAt(2, new Point2d((squareWidth - upperWidthSlot) / 2, squareHight), 0, 0, 0);
                    acPolyline.AddVertexAt(3, new Point2d((squareWidth - lowerWidthSlot) / 2, squareHight - hightSlot), 0, 0, 0);
                    acPolyline.AddVertexAt(4, new Point2d(squareWidth - (squareWidth - lowerWidthSlot) / 2, squareHight - hightSlot), 0, 0, 0);
                    acPolyline.AddVertexAt(5, new Point2d(squareWidth - (squareWidth - upperWidthSlot) / 2, squareHight), 0, 0, 0);

                    acPolyline.AddVertexAt(6, new Point2d(squareWidth, squareHight), 0, 0, 0);
                    acPolyline.AddVertexAt(7, new Point2d(squareWidth, 0), 0, 0, 0);
                    acPolyline.AddVertexAt(8, new Point2d(0, 0), 0, 0, 0);

                    // закругляем куты
                    Fillet(acPolyline, roundingRadiusSlot, 3, 4);
                    Fillet(acPolyline, roundingRadiusSlot, 2, 3);

                    // добавляем созданный объект в пространство модели
                    acBlkTblRec.AppendEntity(acPolyline);

                    // также добавляем созданный объект в транзакцию
                    acTrans.AddNewlyCreatedDBObject(acPolyline, true);
                    #endregion

                    #region circle
                    // 1) создаем окружность - границу штриховки
                    Circle acCircle = new Circle();

                    // 2) устанавливаем параметры границы штриховки
                    acCircle.SetDatabaseDefaults();
                    acCircle.Center = new Point3d(squareWidth / 2, hightToCenterCircle, 0);
                    acCircle.Radius = circleDiameter / 2;


                    // 3) добавляем созданную границу штриховки в пространство модели и в транзакцию
                    acBlkTblRec.AppendEntity(acCircle);
                    acTrans.AddNewlyCreatedDBObject(acCircle, true);
                    #endregion

                    // фиксируем изменения
                    acTrans.Commit();
                }
            }
            // Set the new document current
            acDocMgr.MdiActiveDocument = acDoc;
        }

        private int Fillet(Polyline pline, double radius, int index1, int index2)
        {
            if (pline.GetSegmentType(index1) != SegmentType.Line ||
                pline.GetSegmentType(index2) != SegmentType.Line)
                return 0;
            LineSegment2d seg1 = pline.GetLineSegment2dAt(index1);
            LineSegment2d seg2 = pline.GetLineSegment2dAt(index2);
            Vector2d vec1 = seg1.StartPoint - seg1.EndPoint;
            Vector2d vec2 = seg2.EndPoint - seg2.StartPoint;
            double angle = vec1.GetAngleTo(vec2) / 2.0;
            double dist = radius / Math.Tan(angle);
            if (dist > seg1.Length || dist > seg2.Length)
                return 0;
            Point2d pt1 = seg1.EndPoint.TransformBy(Matrix2d.Displacement(vec1.GetNormal() * dist));
            Point2d pt2 = seg2.StartPoint.TransformBy(Matrix2d.Displacement(vec2.GetNormal() * dist));
            double bulge = Math.Tan((Math.PI / 2.0 - angle) / 2.0);
            if (Clockwise(seg1.StartPoint, seg1.EndPoint, seg2.EndPoint))
                bulge = -bulge;
            pline.AddVertexAt(index2, pt1, bulge, 0.0, 0.0);
            pline.SetPointAt(index2 + 1, pt2);
            return 1;
        }

        private bool Clockwise(Point2d p1, Point2d p2, Point2d p3)
        {
            return ((p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X)) < 1e-8;
        }

        public void PrintMsg(string text)
        {
            MessageBox.Show(text);
        }
    }
}