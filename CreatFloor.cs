#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using Autodesk.Revit.UI.Selection;
#endregion

namespace DoallVietnam
{
    [Transaction(TransactionMode.Manual)]
    public class CreatFloor : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;



            FilteredElementCollector colFloor
              = new FilteredElementCollector(doc)
                .OfClass(typeof(FloorType));

            //FloorType floorType1 = colFloor.FirstElement() as FloorType;
            FloorType floorType1 = colFloor.Cast<FloorType>().First();

            XYZ a1 = new XYZ(0, 0, 0);
            XYZ a2 = new XYZ(20, 0, 0);
            XYZ a3 = new XYZ(20, 15, 0);
            XYZ a4 = new XYZ(0, 15, 0);


            FilteredElementCollector collectorLevel = new FilteredElementCollector(doc);
            Level level1 = collectorLevel.OfClass(typeof(Level)).FirstElement() as Level;



            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");

                CurveArray profile = new CurveArray();

                profile.Append(Line.CreateBound(a1, a2));
                profile.Append(Line.CreateBound(a2, a3));
                profile.Append(Line.CreateBound(a3, a4));
                profile.Append(Line.CreateBound(a4, a1));

                XYZ normal1 = XYZ.BasisZ;
                TaskDialog.Show("revit", floorType1.Name);

                doc.Create.NewFloor(profile, floorType1, level1, false, normal1);

                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
