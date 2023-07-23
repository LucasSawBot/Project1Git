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
    public class CreatWallBIMBOM : IExternalCommand
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


            // type of item
            string nameFloor=null;
            string nameWall=null;
            string nameGroup=null;


            //Creat list item of Group
            ICollection<ElementId> listElementIdGroup;
            List<ElementId> listId = new List<ElementId>();


            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Creat Wall BIMBOM");
                Form_CreatWallPit fmCreatWall = new Form_CreatWallPit();
                
                if (fmCreatWall.ShowDialog() == DialogResult.OK)
                {

                    foreach (Reference floorItem in listRf1)
                    {
                        Element floor1 = doc.GetElement(floorItem) as Floor;
                        nameFloor = floor1.Name;
                        if (floor1.Name == "@AC-F-N")//
                        {
                            nameWall = "@AC-W-D";
                            nameGroup = "Air Conditioner";
                        }
                        else if(floor1.Name == "@BAL-F-N")//
                        {
                            nameWall = "@BAL-W-D";
                            nameGroup = "Balcony";
                        }
                        else if (floor1.Name == "@BR1-F-N")//
                        {
                            nameWall = "@BR1-W-D";
                            nameGroup = "Bedroom1";
                        }
                        else if (floor1.Name == "@BR2-F-N")//
                        {
                            nameWall = "@BR2-W-D";
                            nameGroup = "Bedroom2";
                        }
                        else if (floor1.Name == "@BR3-F-N")//
                        {
                            nameWall = "@BR3-W-D";
                            nameGroup = "Bedroom3";
                        }
                        else if (floor1.Name == "@BR4-F-N")//
                        {
                            nameWall = "@BR4-W-D";
                            nameGroup = "Bedroom4";
                        }
                        else if (floor1.Name == "@BTR-F-N")//
                        {
                            nameWall = "@BTR-W-D";
                            nameGroup = "Bathroom";
                        }
                        else if (floor1.Name == "@COR-F-N")//
                        {
                            nameWall = "@COR-W-D-BW";
                            nameGroup = "Corridor";
                        }
                        else if (floor1.Name == "@COR-F-N-STAIR")//
                        {
                            nameWall = "@COR-W-D-BW";
                            nameGroup = "Corridor-Stair";
                        }
                        else if (floor1.Name == "@DR-F-N")//
                        {
                            nameWall = "@DR-W-D";
                            nameGroup = "Dressroom";
                        }
                        else if (floor1.Name == "@ENT-F-N")//
                        {
                            nameWall = "@ENT-W-D";
                            nameGroup = "Entrance";
                        }
                        else if (floor1.Name == "@ENTS-F-N")//
                        {
                            nameWall = "@ENTS-W-D";
                            nameGroup = "Entrrance Storage";
                        }
                        else if (floor1.Name == "@EVC-F-N")//
                        {
                            nameWall = "@EVC-W-D";
                            nameGroup = "Evacuation Room";
                        }
                        else if (floor1.Name == "@KIT-F-N")//
                        {
                            nameWall = "@KIT-W-D";
                            nameGroup = "Kitchen";
                        }
                        else if (floor1.Name == "@LR-F-N")//
                        {
                            nameWall = "@LR-W-D-FW";
                            nameGroup = "Living Room";
                        }
                        else if (floor1.Name == "@MBTR-F-N")//
                        {
                            nameWall = "@MBTR-W-D";
                            nameGroup = "Master Bathroom";
                        }
                        else if (floor1.Name == "@OBAL-F-N")//
                        {
                            nameWall = "@OBAL-W-D";
                            nameGroup = "Open Balcony";
                        }
                        else if (floor1.Name == "@PNT-F-N")//
                        {
                            nameWall = "@PNT-W-D";
                            nameGroup = "Pentry Room";
                        }
                        else if (floor1.Name == "@PR-F-N")//
                        {
                            nameWall = "@PR-W-D";
                            nameGroup = "Powder Room";
                        }
                        else if (floor1.Name == "@SKIT-F-N-Lower Step")//
                        {
                            nameWall = "@SKIT-W-D";
                            nameGroup = "Sub Kitchen lower";
                        }
                        else if (floor1.Name == "@SKIT-F-N-Top Step")//
                        {
                            nameWall = "@SKIT-W-D";
                            nameGroup = "Sub Kitchen top";
                        }
                        else if (floor1.Name == "@TRC-F-N")
                        {
                            nameWall = "@TRC-W-D";
                            nameGroup = "Terrace";
                        }

                        listId.Add(floor1.Id);
                        //WallType wallTypeBase = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().First(x => x.Name == nameWall);

                        foreach (WallType item in listWallSymbol)
                        {
                            if (nameWall == item.Name )
                            {
                                symbolWallType = item as WallType;
                            }
                        }

                        double widthSymbol = symbolWallType.Width;
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
                                    listId.Add(wall1.Id);
                                }
                            }
                        }

                        listElementIdGroup = listId;
                        Group group = doc.Create.NewGroup(listElementIdGroup);
                        //TaskDialog.Show("revit2", nameGroup);
                        group.GroupType.Name = nameGroup;
                        


                    }
                }
                
                tx.Commit();
            }



                return Result.Succeeded;
        }
    }
}
