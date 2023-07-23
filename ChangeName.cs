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
    public class ChangeName : IExternalCommand
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

            //string to replace
            string oldString = "Bar";
            string newString = "Bar2";
            //list wallType
            FilteredElementCollector collectorWallType = new FilteredElementCollector(doc);
            List<WallType> listWallType = collectorWallType.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().Cast<WallType>().ToList();

            

            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                TaskDialog.Show("revit", listWallType[0].Name);
                foreach (WallType item in listWallType)
                {
                    item.Name= item.Name.Replace(oldString, newString);
                }
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
