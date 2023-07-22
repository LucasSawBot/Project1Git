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
    public class CreatWallPit : IExternalCommand
    {
        
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


            WallType symbolWallType = null;
            


            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Creat Wall Pit");
                Form_CreatWallPit fmCreatWall = new Form_CreatWallPit();
                fmCreatWall.listWall = listStrWallSymbol;
               
                

                if (fmCreatWall.ShowDialog() == DialogResult.OK)
                {

                    
                    foreach (WallType itemWalltype in listWallSymbol)
                    {
                        if (itemWalltype.Name == fmCreatWall.ItemWallType)
                        {
                            symbolWallType = itemWalltype as WallType;
                            
                        }
                    }

                    double widthSymbol = symbolWallType.Width;
                    foreach (Reference floorItem in listRf1)
                    {
                        Element floor1 = doc.GetElement(floorItem) as Floor;
                        Level levelCurrent = floor1.Document.GetElement(floor1.LevelId) as Level;
                        Parameter paraHightOffsetFloor = floor1.LookupParameter("Height Offset From Level");
                        double hightOffsetFloor = paraHightOffsetFloor.AsDouble();


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
                                CurveLoop curveLoopItem1 = CurveLoop.CreateViaOffset(curveLoopItem, -widthSymbol/2, new XYZ(0, 0, 1));

                                foreach (Curve item2 in curveLoopItem1)
                                {
                                    Wall wall1 = Wall.Create(doc, item2, symbolWallType.Id, levelCurrent.Id, (fmCreatWall.unHeight)/304.8, hightOffsetFloor, false, false);
                                }
                            }
                        }

                    }
                }
                
                tx.Commit();
            }



                return Result.Succeeded;
        }
    }
}
