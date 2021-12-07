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

        private Point2d point0 { get; set; }
        private Point2d point1 { get; set; }
        private Point2d point2 { get; set; }
        private Point2d point3 { get; set; }
        private Point2d point4 { get; set; }
        private Point2d point5 { get; set; }
        private Point2d point6 { get; set; }
        private Point2d point7 { get; set; }

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
                    acCurDb.Clayer = acLyrTbl["LayerKreslennya"];
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
                    point0 = new Point2d(0, 0);
                    point1 = new Point2d(0, squareHight);
                    point2 = new Point2d((squareWidth - upperWidthSlot) / 2, squareHight);
                    point3 = new Point2d((squareWidth - lowerWidthSlot) / 2, squareHight - hightSlot);
                    point4 = new Point2d(squareWidth - (squareWidth - lowerWidthSlot) / 2, squareHight - hightSlot);
                    point5 = new Point2d(squareWidth - (squareWidth - upperWidthSlot) / 2, squareHight);
                    point6 = new Point2d(squareWidth, squareHight);
                    point7 = new Point2d(squareWidth, 0);

                    acPolyline.AddVertexAt(0, point0, 0, 0, 0);
                    acPolyline.AddVertexAt(1, point1, 0, 0, 0);

                    acPolyline.AddVertexAt(2, point2, 0, 0, 0);
                    acPolyline.AddVertexAt(3, point3, 0, 0, 0);
                    acPolyline.AddVertexAt(4, point4, 0, 0, 0);
                    acPolyline.AddVertexAt(5, point5, 0, 0, 0);

                    acPolyline.AddVertexAt(6, point6, 0, 0, 0);
                    acPolyline.AddVertexAt(7, point7, 0, 0, 0);
                    acPolyline.AddVertexAt(8, point0, 0, 0, 0);

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
            delLayer("Defpoints");
            delLayer("LayerSizes");
            DocumentCollection acDocMgr = acad.DocumentManager;
            Document acDoc = acad.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Editor ed = acDoc.Editor;

            using (DocumentLock acLckDoc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    #region create Sizes layer
                    LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    createLayer("LayerSizes", acTrans, acLyrTbl, acCurDb);
                    acCurDb.Clayer = acLyrTbl["LayerSizes"];
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

                    #region square hight
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d(point0.X, point0.Y, 0);
                        acRotDim.XLine2Point = new Point3d(point1.X, point1.Y, 0);
                        acRotDim.Rotation = Math.PI / 2; //90ang
                        acRotDim.DimLinePoint = new Point3d(-(point1.Y / 4), point1.X / 4, 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region square width
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d(point0.X, point0.Y, 0);
                        acRotDim.XLine2Point = new Point3d(point7.X, point7.Y, 0);
                        acRotDim.DimLinePoint = new Point3d(point7.Y / 4, -(point7.X / 4), 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region upper slot width
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d(point2.X, point2.Y, 0);
                        acRotDim.XLine2Point = new Point3d(point5.X, point5.Y, 0);
                        acRotDim.DimLinePoint = new Point3d(0, squareHight + (point5.X / 8), 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region lower slot width
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d(point3.X, point3.Y, 0);
                        acRotDim.XLine2Point = new Point3d(point4.X, point4.Y, 0);
                        acRotDim.DimLinePoint = new Point3d(0, squareHight - hightSlot - (point4.X / 8), 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region slot hight
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d(point4.X, point4.Y, 0);
                        acRotDim.XLine2Point = new Point3d(point5.X, point5.Y, 0);
                        acRotDim.Rotation = Math.PI / 2; //90ang
                        acRotDim.DimLinePoint = new Point3d(squareWidth - (squareWidth - lowerWidthSlot) / 2 + (hightSlot / 8), 0, 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region hight to center circle
                    using (RotatedDimension acRotDim = new RotatedDimension())
                    {
                        acRotDim.XLine1Point = new Point3d((squareWidth - circleDiameter) / 2, 0, 0);
                        acRotDim.XLine2Point = new Point3d((squareWidth - circleDiameter) / 2, hightToCenterCircle, 0);
                        acRotDim.Rotation = Math.PI / 2; //90ang
                        acRotDim.DimLinePoint = new Point3d((squareWidth - circleDiameter) / 2 - circleDiameter / 4, 0, 0);
                        acRotDim.Dimasz = 2; // розмір стілки
                        acRotDim.Dimtxt = 1; // розмір тексту

                        acRotDim.DimensionStyle = acCurDb.Dimstyle;
                        // Add the new object to Model space and the transaction
                        acBlkTblRec.AppendEntity(acRotDim);
                        acTrans.AddNewlyCreatedDBObject(acRotDim, true);
                    }
                    #endregion

                    #region circle
                    using (DiametricDimension acDiamDim = new DiametricDimension())
                    {
                        double angle1 = Math.PI - 0.3; // in radian
                        double angle2 = -0.3; // in radian
                        double cirRad = (circleDiameter / 2);

                        acDiamDim.FarChordPoint = new Point3d(squareWidth / 2 + cirRad * Math.Cos(angle1), hightToCenterCircle + cirRad * Math.Sin(angle1), 0);
                        acDiamDim.ChordPoint = new Point3d(squareWidth / 2 + cirRad * Math.Cos(angle2), hightToCenterCircle + cirRad * Math.Sin(angle2), 0);
                        acDiamDim.LeaderLength = circleDiameter / 6;
                        acDiamDim.Dimcen = -1;
                        acDiamDim.Dimasz = 2; // розмір стілки
                        acDiamDim.Dimtxt = 1; // розмір тексту
                        acDiamDim.Dimtih = true; // вирівняти текст 
                        acDiamDim.Dimtofl = true; // лінії миж точками замість маркера по центру

                        acDiamDim.DimensionStyle = acCurDb.Dimstyle;

                        acBlkTblRec.AppendEntity(acDiamDim);
                        acTrans.AddNewlyCreatedDBObject(acDiamDim, true);
                    }
                    #endregion

                    #region to do RadialDimension
                    PromptSelectionResult selRes = ed.SelectAll();
                    ObjectId[] ids = selRes.Value.GetObjectIds();
                    Polyline myPoli = null;
                    foreach (ObjectId id in ids)
                    {
                        if (((Entity)acTrans.GetObject(id, OpenMode.ForRead)).GetType() == typeof(Polyline))
                        {
                            myPoli = (Polyline)acTrans.GetObject(id, OpenMode.ForRead);
                            break;
                        }
                    }
                    if (myPoli != null)
                    {
                        myPoli.GetPoint2dAt(4);
                        Point3d dPol = myPoli.GetPoint3dAt(4);
                        double angle = Math.PI + Math.PI / 4; // in radian
                        using (RadialDimension acRadDim = new RadialDimension())
                        {
                            acRadDim.Center = new Point3d(dPol.X, dPol.Y + roundingRadiusSlot, 0);
                            acRadDim.ChordPoint = new Point3d(dPol.X + roundingRadiusSlot * Math.Cos(angle), dPol.Y + roundingRadiusSlot + roundingRadiusSlot * Math.Sin(angle), 0);
                            acRadDim.LeaderLength = 5;
                            acRadDim.DimensionStyle = acCurDb.Dimstyle;
                            acRadDim.Dimasz = 2; // розмір стілки
                            acRadDim.Dimtxt = 1; // розмір тексту
                            acRadDim.Dimtmove = 2;
                            acRadDim.TextPosition = new Point3d(dPol.X, dPol.Y + roundingRadiusSlot + 2, 0);

                            // Add the new object to Model space and the transaction
                            acBlkTblRec.AppendEntity(acRadDim);
                            acTrans.AddNewlyCreatedDBObject(acRadDim, true);
                        }
                    }

                    #endregion

                    #endregion

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
                    #region layer 1 (kreslennya)
                    acCurDb.Clayer = acLyrTbl["LayerKreslennya"];

                    if (lineThicknessLayer1.Length > 0)
                    {
                        foreach (ObjectId id in ids)
                        {
                            Entity entForIf = (Entity)tr.GetObject(id, OpenMode.ForRead);
                            if (entForIf.Layer == "LayerKreslennya")
                            {
                                if (entForIf.GetType() == typeof(Polyline))
                                {
                                    Polyline entity = (Polyline)tr.GetObject(id, OpenMode.ForRead);

                                    entity.UpgradeOpen();
                                    entity.ConstantWidth = double.Parse(lineThicknessLayer1);
                                    entity.Closed = true; //not work
                                }

                                if (entForIf.GetType() == typeof(Circle))
                                {
                                    Circle entity = (Circle)tr.GetObject(id, OpenMode.ForRead);

                                    entity.UpgradeOpen();
                                    entity.Thickness = double.Parse(lineThicknessLayer1);

                                }
                            }

                        }
                    }
                    if (lineColorLayer1.Length > 0)
                    {
                        changeColorEntity("LayerKreslennya", lineColorLayer1, tr, ids);
                    }
                    #endregion

                    #region layer 2 (sizes)
                    acCurDb.Clayer = acLyrTbl["LayerSizes"];
                    if (lineThicknessLayer2.Length > 0)
                    {
                        //changeLineWidthEntity("LayerSizes", lineThicknessLayer2, tr, ids);
                    }
                    if (lineColorLayer2.Length > 0)
                    {
                        changeColorEntity("LayerSizes", lineColorLayer2, tr, ids);
                    }
                    #endregion
                    #endregion

                    #region finish tranzaction
                    // Set the new document current
                    acad.DocumentManager.MdiActiveDocument = acDoc;
                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
            #endregion
        }

        #region other func
        private void changeColorEntity(string layer, string lineColor, Transaction tr, ObjectId[] ids)
        {
            // "пробегаем" по всем полученным объектам
            foreach (ObjectId id in ids)
            {
                Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);
                if (entity.Layer == layer)
                {
                    entity.UpgradeOpen();

                    // изменяем цвет
                    //entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 128, 0);
                    switch (lineColor)
                    {
                        case "red":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 0, 0);
                            break;
                        case "blue":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(0, 0, 255);
                            break;
                        case "yellow":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 255, 0);
                            break;
                        case "green":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(0, 255, 0);
                            break;
                        case "orange":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 165, 0);
                            break;
                        case "pink":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 0, 255);
                            break;
                        case "purple":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(128, 0, 128);
                            break;
                        case "black":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(0, 0, 0);
                            break;
                        case "white":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 255, 255);
                            break;
                        case "gray":
                            entity.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(128, 128, 128);
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        private void createLayer(string Layer, Transaction tr, LayerTable acLyrTbl, Database acCurDb)
        {
            try
            {
                LayerTableRecord acLyrTblRec = new LayerTableRecord();
                acLyrTblRec.Name = Layer;

                acLyrTbl.Add(acLyrTblRec);
                tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                acCurDb.Clayer = acLyrTbl[Layer];
                //PrintMsg("Успішно створено шар " + Layer);  //debug
            }
            catch (Autodesk.AutoCAD.Runtime.Exception Ex)
            {
                acad.ShowAlertDialog("Помилка створення шару " + Layer + ":\n" + Ex.Message);
            }
        }

        public void delLayer(string layer)
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
                    //PrintMsg(layer + " поінт 0");
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                    if (acLyrTbl.Has(layer) == false)
                    {
                        return;
                    }

                    acCurDb.Clayer = acLyrTbl[layer];
                    #region del all entity
                    Editor ed = acDoc.Editor;
                    PromptSelectionResult selRes = ed.SelectAll();

                    // если произошла ошибка - сообщаем о ней
                    if (selRes.Status != PromptStatus.OK)
                    {
                        ed.WriteMessage("\nВиникла помилка при виборі обєктів! Пропускаю дію \n");
                    }
                    else
                    {
                        // получаем массив ID объектов
                        ObjectId[] ids = selRes.Value.GetObjectIds();

                        foreach (ObjectId id in ids)
                        {
                            Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);
                            if (entity.Layer == layer) // видаляємо тільки обєкти на нашому слою
                            {
                                try
                                {
                                    entity.UpgradeOpen();
                                    entity.Erase();
                                }
                                catch (Autodesk.AutoCAD.Runtime.Exception)
                                {
                                    ed.WriteMessage("\nSomething went wrong...\n");
                                }
                            }
                        }
                    }

                    #endregion

                    // устанавливаем нулевой слой в качестве текущего
                    acCurDb.Clayer = acLyrTbl["0"];

                    // убеждаемся, что на удаляемый слой не ссылаются другие объекты
                    ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                    acObjIdColl.Add(acLyrTbl[layer]);
                    acCurDb.Purge(acObjIdColl);

                    //PrintMsg(layer + " поінт 1"); //debug

                    if (acObjIdColl.Count > 0)
                    {
                        // получаем запись слоя для изменения
                        LayerTableRecord acLyrTblRec = tr.GetObject(acObjIdColl[0], OpenMode.ForWrite) as LayerTableRecord;

                        try
                        {
                            // удаляем слой
                            acLyrTblRec.Erase(true);
                            tr.Commit();
                            //PrintMsg("Успішно видалено шар " + layer);  //debug
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception Ex)
                        {
                            acad.ShowAlertDialog("Неможливо видалити шар " + layer + ":\n" + Ex.Message);
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