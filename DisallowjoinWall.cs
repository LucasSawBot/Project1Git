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
    public class DisallowjoinWall : IExternalCommand
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

            Selection sel1 = uidoc.Selection;
            IList<Reference> listRf1 = sel1.PickObjects(ObjectType.Element);


           
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Get Wall");
                foreach (Reference rf in listRf1)
                {
                    Element ele1 = doc.GetElement(rf);
                    Wall wall1 = ele1 as Wall;
                    WallUtils.DisallowWallJoinAtEnd(wall1, 0);
                    WallUtils.DisallowWallJoinAtEnd(wall1, 1);

                }


                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
