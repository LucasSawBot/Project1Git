#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
using System.Linq;
#endregion

namespace DoallVietnam
{
    [Transaction(TransactionMode.Manual)]
    public class CreatAllFloor : IExternalCommand
    {
        double unconectedHeight2;

        public double unHeight
        {
            get { return unconectedHeight2; }
            set { unconectedHeight2 = value; }
        }
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            FilteredElementCollector collectorLevel = new FilteredElementCollector(doc);
            Level level1 = collectorLevel.OfClass(typeof(Level)).FirstElement() as Level;

            Selection sel = uidoc.Selection;
            IList<Reference> listRf1 = sel.PickObjects(ObjectType.Element);
            FilteredElementCollector collectorWalltype = new FilteredElementCollector(doc);

            //get all walltype into the list
            List<WallType> listWallSymbol = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().ToList();

            List<string> listStrWallSymbol = new List<string>();

            foreach (WallType itemWallType in listWallSymbol)
            {
                listStrWallSymbol.Add(itemWallType.Name);
            }

            WallType symbolWallType = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().First(x => x.Name == "W=WF_ExWall#");
            //IList<Element> listWalltype = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements();
            //Element wallType2 = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().FirstElement();

            //double unconectedHeight;


            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Creat Wall Pit");
                double widthSymbol = symbolWallType.Width;

                CurveArray curve1 = new CurveArray();
                foreach (Reference floorItem in listRf1)
                {
                    Element floor1 = doc.GetElement(floorItem) as Floor;
                    Level levelCurrent = floor1.Document.GetElement(floor1.LevelId) as Level;
                    Autodesk.Revit.DB.HostObject fl1 = floor1 as HostObject;
                    IList<Reference> list1 = HostObjectUtils.GetTopFaces(fl1);
                   
                    
                    foreach (Reference item in list1)
                    {
                        GeometryObject geoObject = doc.GetElement(item).GetGeometryObjectFromReference(item);
                        PlanarFace plFace1 = geoObject as PlanarFace;
                        IList<CurveLoop> lisCurveLoop = plFace1.GetEdgesAsCurveLoops();
                        foreach (CurveLoop curveLoopItem in lisCurveLoop)
                        {
                            //CurveLoop curveLoopItem1 = CurveLoop.CreateViaOffset(curveLoopItem, 50/304.8, new XYZ(0, 0, 1));
                            //CurveLoop curveLoopItem1 = CurveLoop.CreateViaOffset(curveLoopItem, -widthSymbol / 2, new XYZ(0, 0, 1));
                            CurveLoop curveLoopItem1 = CurveLoop.CreateViaOffset(curveLoopItem, -widthSymbol/2, new XYZ(0, 0, 1));



                            foreach (Curve item2 in curveLoopItem1)
                            {
                                //Wall wall1 = Wall.Create(doc, item2, symbolWallType.Id, levelCurrent.Id, 5000 / 304.8, 0 / 304.8, false, false);
                                doc.Create.NewDetailCurve(doc.ActiveView, item2);
                                //curve1.Append(item2);

                            }

                            //doc.Create.NewFloor(curve1, false);
                        }
                    }

                }

                //doc.Create.NewFloor(curve1, false);

                //Line line1 = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(1000, 1000, 0));
                //doc.Create.NewDetailCurve(doc.ActiveView, line1);
                //TaskDialog.Show("reviot", line1.Length.ToString());
                tx.Commit();
            }



                return Result.Succeeded;
        }
    }
}
