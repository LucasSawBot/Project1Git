#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
#endregion

namespace DoallVietnam
{
    [Transaction(TransactionMode.Manual)]
    public class ViewCheckDoor : IExternalCommand
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

            // Access current selection

            Selection sel = uidoc.Selection;
            //Reference refer = sel.PickObject(ObjectType.Element);

            FilteredElementCollector collectorWall = new FilteredElementCollector(doc);
            List<Wall> listWall = collectorWall.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().Cast<Wall>().ToList();

            ViewFamilyType viewFamilyType = (from v in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
                                             where v.ViewFamily == ViewFamily.ThreeDimensional
                                             select v).First();

            // Modify document within a transaction
            int i = 0;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                foreach (Wall iteWall in listWall)
                {
                    IList<ElementId> listInsert = iteWall.FindInserts(true, false, true, true);
                    foreach (ElementId eleId in listInsert)
                    {
                        Element ele = doc.GetElement(eleId);
                        BoundingBoxXYZ bounEle = ele.get_BoundingBox(doc.ActiveView);
                        bounEle.Min = new XYZ(bounEle.Min.X - 1, bounEle.Min.Y - 1, bounEle.Min.Z - 1);
                        bounEle.Max = new XYZ(bounEle.Max.X + 1, bounEle.Max.Y + 1, bounEle.Max.Z - 1);

                        View3D view = View3D.CreateIsometric(doc, viewFamilyType.Id);
                        view.SetSectionBox(bounEle);
                        view.DisplayStyle = DisplayStyle.Shading;
                        view.Name = ele.Id.ToString()+"--" +i.ToString() +"  View Door-Window";
                        i++;
                    }
                }
                
                
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
