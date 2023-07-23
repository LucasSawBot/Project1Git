#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace DoallVietnam
{
    [Transaction(TransactionMode.Manual)]
    public class FindHost : IExternalCommand
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
           
            IList<Reference> listRf1 = sel.PickObjects(ObjectType.Element);
            // Retrieve elements from database

            

            

            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                foreach (Reference item in listRf1)
                {
                    Element el1 = doc.GetElement(item);
                    Wall wall1 = el1 as Wall;
                    IList<ElementId> listEleId = wall1.FindInserts(true, false, true, true);
                    TaskDialog.Show("revit", listEleId.Count.ToString());
                }
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
