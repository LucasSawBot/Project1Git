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
    public class CreatRoom : IExternalCommand
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
            //pick all Floors
            Selection selFloor = uidoc.Selection;
            IList<Reference> listRf1 = selFloor.PickObjects(ObjectType.Element);

            //get Wall Type and add to list string
            FilteredElementCollector collectorWall = new FilteredElementCollector(doc);
            List<WallType> listWallType = collectorWall.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().ToList();
            List<string> listStrWallType = new List<string>();
            foreach (WallType itemWallType in listWallType)
            {
                listStrWallType.Add(itemWallType.Name);
            }

            //getFloorType of Floor and add to list string
            FilteredElementCollector collectorFloor = new FilteredElementCollector(doc);
            IList<FloorType> listFloorType = collectorFloor.OfCategory(BuiltInCategory.OST_Floors).WhereElementIsElementType().Cast<FloorType>().ToList();
            List<string> listStrFloorType = new List<string>();
            foreach (FloorType itemFloorType in listFloorType)
            {
                listStrFloorType.Add(itemFloorType.Name);
            }

            //get Ceiling type of Floor and add to list string
            List<string> listStrCeilingType = new List<string>();
            foreach (FloorType itemCeilingType in listFloorType)
            {
                listStrCeilingType.Add(itemCeilingType.Name);
            }


            //Creat Walltype and FloorType and CeilingType
            WallType symbolWallType = null;
            FloorType symbolFloorType = null;
            FloorType symbolCeilingType = null;

            ICollection<ElementId> listElementId;
            List<ElementId> listId = new List<ElementId>();

            


            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Creat Room");
                Form_CreatRoom fmCreatRoom = new Form_CreatRoom();
                fmCreatRoom.listWallType = listStrWallType;
                fmCreatRoom.listFloorType = listStrFloorType;
                fmCreatRoom.listCelingType = listStrCeilingType;

                if (fmCreatRoom.ShowDialog() == DialogResult.OK)
                {

                    //binding data list type to the form
                    foreach (WallType itemWalltype in listWallType)
                    {
                        if (itemWalltype.Name == fmCreatRoom.ItemWallType)
                        {
                            symbolWallType = itemWalltype as WallType;
                            
                        }
                    }

                    foreach (FloorType itemFloortypeBase in listFloorType)
                    {
                        if (itemFloortypeBase.Name ==fmCreatRoom.ItemFloorType)
                        {
                            symbolFloorType = itemFloortypeBase as FloorType;
                        }
                    }

                    foreach (FloorType itemFloortypeCeiling in listFloorType)
                    {
                        if (itemFloortypeCeiling.Name == fmCreatRoom.ItemCeilingType)
                        {
                            symbolCeilingType = itemFloortypeCeiling as FloorType;
                        }
                    }

                    //creat model wall and ceiling
                    
                    double widthWallType = symbolWallType.Width;
                    foreach (Reference floorRf in listRf1)
                    {
                        Element floor1 = doc.GetElement(floorRf) as Floor;
                        Level levelCurrent = floor1.Document.GetElement(floor1.LevelId) as Level;
                        Parameter paraHightOffsetFloor = floor1.LookupParameter("Height Offset From Level");
                        double hightOffsetFloor = paraHightOffsetFloor.AsDouble();

                        if (fmCreatRoom.checkFloor)
                        {
                            listId.Add(floor1.Id);
                        }
                        else
                        {
                            Floor floor2 = floor1 as Floor;
                            floor2.FloorType = symbolFloorType;
                            listId.Add(floor2.Id);
                        }
                        

                        



                        //copy floor to ceiling
                        ICollection<ElementId> listCeling = Autodesk.Revit.DB.ElementTransformUtils.CopyElement(doc, floor1.Id, new XYZ(0, 0, (fmCreatRoom.hightRoom)/304.8));
                        foreach (ElementId item in listCeling)
                        {
                            Floor ceilingCopy = doc.GetElement(item) as Floor;
                            listId.Add(ceilingCopy.Id);
                            ceilingCopy.FloorType = symbolCeilingType;
                            
                        }
                        
                        



                        //creat Wall
                        Autodesk.Revit.DB.HostObject fl1 = floor1 as HostObject;
                        IList<Reference> listFace = HostObjectUtils.GetTopFaces(fl1);

                        foreach (Reference item in listFace)
                        {
                            GeometryObject geoObject = doc.GetElement(item).GetGeometryObjectFromReference(item);
                            PlanarFace plFace = geoObject as PlanarFace;
                            IList<CurveLoop> listCurveLoop = plFace.GetEdgesAsCurveLoops();
                            foreach (CurveLoop curveLoopItem in listCurveLoop)
                            {
                                CurveLoop curveLoopOffset = CurveLoop.CreateViaOffset(curveLoopItem, -widthWallType / 2, new XYZ(0, 0, 1));
                                foreach (Curve itemCurve in curveLoopOffset)
                                {
                                    Wall wall1 = Wall.Create(doc, itemCurve, symbolWallType.Id, levelCurrent.Id, (fmCreatRoom.hightRoom) / 304.8,hightOffsetFloor, false, false);
                                    listId.Add(wall1.Id);
                                }
                            }
                        }

                        




                        //creat group
                        listElementId = listId;
                        
                        Group group = doc.Create.NewGroup(listElementId);


                    }

                    
                }



                tx.Commit();
            }


                return Result.Succeeded;
        }
    }
}
