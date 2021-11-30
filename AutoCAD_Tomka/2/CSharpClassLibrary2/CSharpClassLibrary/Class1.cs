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
        #region param
        private FormLab1 myForm;
        private int squareWidth { get; set; }
        private int squareHight { get; set; }
        private int upperWidthSlot { get; set; }
        private int lowerWidthSlot { get; set; }
        private int roundingRadiusSlot { get; set; }
        private int hightSlot { get; set; }
        private int hightToCenterCircle { get; set; }
        private int circleDiameter { get; set; }

        private int drawState;
        #endregion

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

            DrawKres();
            DrawSizes();
            drawState = 3; // 3 - all layer draw
        }

        #region draw func
        private void DrawKres()
        {
            delLayer("LayerKreslennya");
            DocumentCollection acDocMgr = acad.DocumentManager;
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    #region create Kres layer
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    createLayer("LayerKreslennya", acTrans, acLyrTbl, acCurDb);
                    #endregion

                    #region var for draw
                    // открываем таблицу блоков документа
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // открываем пространство модели (Model Space) - оно является одной из записей в таблице блоков документа
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    #endregion

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

        private void DrawSizes()
        {
            delLayer("LayerSizes");
            DocumentCollection acDocMgr = acad.DocumentManager;
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    #region create Sizes layer
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    createLayer("LayerSizes", acTrans, acLyrTbl, acCurDb);
                    #endregion

                    #region var for draw
                    // открываем таблицу блоков документа
                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // открываем пространство модели (Model Space) - оно является одной из записей в таблице блоков документа
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    #endregion

                    #region draw sizes

                    #endregion

                    // фиксируем изменения
                    acTrans.Commit();
                }
            }
            // Set the new document current
            acDocMgr.MdiActiveDocument = acDoc;
        }
        #endregion

        public void onButtonEditClick(int whatDraw, string lineThicknessLayer1, string lineColorLayer1, string lineThicknessLayer2, string lineColorLayer2)
        {
            checkLayer(whatDraw);

            #region init var for tranzaction
            // получаем текущий документ и его БД
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor ed = acDoc.Editor;

            PromptSelectionResult selRes = ed.SelectAll();

            // если произошла ошибка - сообщаем о ней
            if (selRes.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\nError!\n");
                return;
            }

            // получаем массив ID объектов
            ObjectId[] ids = selRes.Value.GetObjectIds();

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    #endregion

                    #region edit object
                    #region layer 1 (kreslenya)
                    acCurDb.Clayer = acLyrTbl["LayerKreslennya"];
                    if (lineThicknessLayer1.Length > 0)
                    {
                        foreach (ObjectId id in ids)
                        {
                            // приводим каждый из них к типу Entity
                            Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);

                            // открываем объект на запись
                            entity.UpgradeOpen();

                            // изменяем цвет
                            entity.LinetypeScale = double.Parse(lineThicknessLayer1); // чого не міняється розмір лінії
                        }
                    }
                    if (lineColorLayer1.Length > 0)
                    {
                        PrintMsg(lineColorLayer1);
                        // "пробегаем" по всем полученным объектам
                        foreach (ObjectId id in ids)
                        {
                            // приводим каждый из них к типу Entity
                            Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);

                            // открываем объект на запись
                            entity.UpgradeOpen();

                            // изменяем цвет
                            //entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 128, 0);
                            entity.Color = Autodesk.AutoCAD.Colors.Color. FromNames(lineColorLayer1, lineColorLayer1); // добавити вибір кольору
                        }
                    }
                    acCurDb.Clayer = acLyrTbl["LayerSizes"];
                    #endregion
                    #region layer 2 (sizes)
                    #endregion
                    #endregion

                    #region finish tranzaction
                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
            #endregion
        }

        #region other func

        private void createLayer(string currentLayer, Transaction tr, LayerTable acLyrTbl, Database acCurDb)
        {
            try
            {
                // создаем новый слой и задаем ему имя
                LayerTableRecord acLyrTblRec = new LayerTableRecord();
                acLyrTblRec.Name = currentLayer;

                // заносим созданный слой в таблицу слоев
                acLyrTbl.Add(acLyrTblRec);

                // добавляем созданный слой в документ
                tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                acCurDb.Clayer = acLyrTbl[currentLayer];
            }
            catch (Autodesk.AutoCAD.Runtime.Exception Ex)
            {
                PrintMsg("Не можу створити шар " + currentLayer);
                acad.ShowAlertDialog("Ошибка:\n" + Ex.Message);
            }
        }

        private void delLayer(string layer)
        {
            // получаем текущий документ и его БД
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                    if (acLyrTbl.Has(layer) == false)
                    {
                        return;
                    }

                    // устанавливаем нулевой слой в качестве текущего
                    acCurDb.Clayer = acLyrTbl["0"];

                    #region del all entity
                    // прокачати провірку: чи є обєкти на слою, якщо є - видалити

                    Editor ed = acDoc.Editor;

                    PromptSelectionResult selRes = ed.SelectAll();

                    // если произошла ошибка - сообщаем о ней
                    if (selRes.Status != PromptStatus.OK)
                    {
                        ed.WriteMessage("\nError!\n");
                        return;
                    }

                    // получаем массив ID объектов
                    ObjectId[] ids = selRes.Value.GetObjectIds();

                    // "пробегаем" по всем полученным объектам
                    foreach (ObjectId id in ids)
                    {
                        // приводим каждый из них к типу Entity
                        Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);

                        try
                        {
                            // открываем приговоренный объект на запись
                            entity.UpgradeOpen();

                            // удаляем объект
                            entity.Erase();
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception)
                        {
                            ed.WriteMessage("\nSomething went wrong...\n");
                        }
                    }

                    #endregion

                    // убеждаемся, что на удаляемый слой не ссылаются другие объекты
                    ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                    acObjIdColl.Add(acLyrTbl[layer]);
                    acCurDb.Purge(acObjIdColl);

                    if (acObjIdColl.Count > 0)
                    {
                        // получаем запись слоя для изменения
                        LayerTableRecord acLyrTblRec = tr.GetObject(acObjIdColl[0], OpenMode.ForWrite) as LayerTableRecord;

                        try
                        {
                            // удаляем слой
                            acLyrTblRec.Erase(true);
                            // фиксируем транзакцию
                            tr.Commit();
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception Ex)
                        {
                            PrintMsg("Не можу видалити шар " + layer);
                            // если произошла ошибка - значит, слой удалить нельзя
                            acad.ShowAlertDialog("Ошибка:\n" + Ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        private void checkLayer(int whatDraw)
        {
            if (drawState == whatDraw)
            {
                return;
            }
            #region init var for tranzaction
            // получаем текущий документ и его БД
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    #endregion

                    switch (whatDraw)
                    {
                        case 0:
                            onOrOffLayer("LayerKreslennya", false, acLyrTbl, tr);
                            onOrOffLayer("LayerSizes", false, acLyrTbl, tr);
                            break;
                        case 1:
                            onOrOffLayer("LayerKreslennya", true, acLyrTbl, tr);
                            onOrOffLayer("LayerSizes", false, acLyrTbl, tr);
                            break;
                        case 2:
                            onOrOffLayer("LayerKreslennya", false, acLyrTbl, tr);
                            onOrOffLayer("LayerSizes", true, acLyrTbl, tr);
                            break;
                        case 3:
                            onOrOffLayer("LayerKreslennya", true, acLyrTbl, tr);
                            onOrOffLayer("LayerSizes", true, acLyrTbl, tr);
                            break;
                        default:
                            break;
                    }

                    #region finish tranzaction
                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
            #endregion
            drawState = whatDraw;
        }

        private void onOrOffLayer(string nameLayer, bool state, LayerTable acLyrTbl, Transaction tr)
        {
            // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
            if (acLyrTbl.Has(nameLayer) == false)
            {
                return;
            }
            // получаем запись слоя для изменения
            LayerTableRecord acLyrTblRec = tr.GetObject(acLyrTbl[nameLayer], OpenMode.ForWrite) as LayerTableRecord;
            // скрываем и блокируем слой
            acLyrTblRec.IsOff = !state;
            //acLyrTblRec.IsLocked = !state;
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
        #endregion
    }
}