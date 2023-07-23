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
    public class CutWalllouver : IExternalCommand
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
            //Level level1 = collectorLevel.OfClass(typeof(Level)).FirstElement() as Level;
            //Level level2 = collectorLevel.OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().Cast<Level>().First(x => x.Name == "B1ST SL +0");

            //pick Walls
            Selection sel = uidoc.Selection;
            IList<Reference> listRf1 = sel.PickObjects(ObjectType.Element);

            
            //getWalltype





            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Creat Wall Pit");
                foreach (Reference rf1 in listRf1)
                {
                    Element el1 = doc.GetElement(rf1);
                    Wall wall1 = el1 as Wall;

                    LocationCurve locationCurve1 = wall1.Location as LocationCurve;
                    Curve curve1 = locationCurve1.Curve;
                    WallType wallType2 = wall1.WallType;
                    //Level level1 = Wall

                    Parameter paraBasecontraint = wall1.LookupParameter("Base Constraint");
                    //TaskDialog.Show("revit",paraBasecontraint.AsValueString());
                    Level level2 = collectorLevel.OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().Cast<Level>().First(x => x.Name == paraBasecontraint.AsValueString());




                    string wallTypeName =null;
                    if (wall1.Name.Contains("W="))
                    {
                        wallTypeName = "W=Water.P(CO)#UpperLouver";
                    }
                    else if (wall1.Name.Contains("W_C"))
                    {
                        wallTypeName = "W_C=Water.P(CO)#UpperLouver";
                    }
                    else if(wall1.Name.Contains("W_B"))
                    {
                        wallTypeName = "W_B=Water.P(CO)#UpperLouver";
                    }
                    else if (wall1.Name.Contains("W_D"))
                    {
                        wallTypeName = "W_D=Water.P(CO)#UpperLouver";
                    }
                    




                    //getWalltype
                    FilteredElementCollector collectorWalltype = new FilteredElementCollector(doc);
                    WallType wallTypeLuver = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().First(x => x.Name == wallTypeName);
                    WallType wallTypeBase = collectorWalltype.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().First(x => x.Name == "W=Stone(T30)_Entrance#");


                    Parameter paraWallBase1 = wall1.LookupParameter("Unconnected Height");
                    Parameter paraWallBase2 = wall1.LookupParameter("Base Offset");



                    double wall1Heigh = (paraWallBase1.AsDouble())*304.8;
                    double wall1Offset = (paraWallBase2.AsDouble())*304.8;
                    //TaskDialog.Show("revitr", wall1Heigh.ToString());
                    if (wall1Heigh>2700)
                    {
                        paraWallBase1.Set(2400 / 304.8);
                        
                        wall1.WallType = wallTypeBase;
                        Wall wallLuver = Wall.Create(doc, curve1, wallTypeLuver.Id, level2.Id, (wall1Heigh - 2700) / 304.8, (2700+wall1Offset) / 304.8, false, false);

                    }
                    else if(wall1Heigh == 2700)
                    {
                        paraWallBase1.Set(2400/304.8);
                        
                    }
                    else
                    {
                        TaskDialog.Show("chieu cao cua", "Kiem tra lai chieu cao tuong dang o duoi Louver");
                    }


                    


                }

                tx.Commit();
            }


           




            return Result.Succeeded;
        }
    }
}
