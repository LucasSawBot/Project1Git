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
    public class PlaceParking : IExternalCommand
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

            Selection selectin2 = uidoc.Selection;
            Reference refer2 = selectin2.PickObject(ObjectType.Element, "Pick a Host Floor");
            Element elem2 = doc.GetElement(refer2);
            Floor floorHost = elem2 as Floor;
            //Level levelFloor = doc.GetElement(floorHost.Id) as Level;
            Level levelFloor = doc.GetElement(floorHost.LevelId) as Level;

            double ele = levelFloor.Elevation;

            Parameter param = floorHost.get_Parameter(BuiltInParameter.FLOOR_ATTR_THICKNESS_PARAM);
            double thickness = 0;
            if (param != null)
            {
                thickness = param.AsDouble();
            }


            Selection selectin1 = uidoc.Selection;
            Reference refer = selectin1.PickObject(ObjectType.Element, "Select a link Cad");
            Element elem = doc.GetElement(refer);

            GeometryElement geeoElem = elem.get_Geometry(new Options());


            //creat family instance
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_GenericModel);

            FilteredElementCollector collector2 = new FilteredElementCollector(doc);

            FamilySymbol symbol = null;


            IList<Element> listSymbol = collector.OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_GenericModel).WhereElementIsElementType().ToElements();

            List<string> name = new List<string>();
            List<string> nameBlock = new List<string>();
            List<string> nameBlock1 = new List<string>();

            foreach (FamilySymbol sym in listSymbol)
            {
                name.Add(sym.Name);
            }

            foreach (GeometryInstance geoObj in geeoElem)
            {
                GeometryInstance instance = geoObj as GeometryInstance;
                foreach (GeometryObject instObj in instance.SymbolGeometry)
                {

                    if (instObj.GetType().Name == "GeometryInstance")
                    {
                        GeometryInstance geo1 = instObj as GeometryInstance;
                        nameBlock.Add(geo1.Symbol.Name.ToString());
                        //nameBlock.Add(geo1.ToString());

                    }
                }
            }

            nameBlock1 = nameBlock.Distinct().ToList();
            nameBlock1.Add(nameBlock[0]);
            nameBlock1.Remove(nameBlock1[0]);


            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                Form_PlaceParkingLot fm1 = new Form_PlaceParkingLot();

                fm1.listtileblock = name;
                fm1.listBlock = nameBlock1;
                TaskDialog.Show("revit", nameBlock1.Count.ToString());
                int demso = 0;
                int demgeo = 0;

                if (fm1.ShowDialog() == DialogResult.OK)
                {
                    foreach (FamilySymbol sym in listSymbol)
                    {
                        if (sym.Name == fm1.ItemFamilyInstance)
                        {

                            symbol = sym as FamilySymbol;

                        }

                    }
                    foreach (GeometryInstance geoObj in geeoElem)
                    {
                        GeometryInstance instance = geoObj as GeometryInstance;
                        foreach (GeometryObject instObj in instance.SymbolGeometry)
                        {


                            if (instObj.GetType().Name == "GeometryInstance")
                            {
                                GeometryInstance geo1 = instObj as GeometryInstance;
                                demgeo++;
                                
                                if (geo1.Symbol.Name == fm1.ItemBlock)
                                {
                                    demso++;
                                    XYZ vectorTrans = geo1.Transform.OfVector(geo1.Transform.BasisX.Normalize());
                                    double rot = geo1.Transform.BasisX.AngleOnPlaneTo(vectorTrans, geo1.Transform.BasisZ.Normalize());
                                    

                                    FamilyInstance family1 = doc.Create.NewFamilyInstance(new XYZ(geo1.Transform.Origin.X, geo1.Transform.Origin.Y, ele + thickness), symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                                    //doc.Create.PlaceGroup(new XYZ(geo1.Transform.Origin.X, geo1.Transform.Origin.Y, ele + thickness), grtype2);

                                    XYZ center = new XYZ(geo1.Transform.Origin.X, geo1.Transform.Origin.Y, 0);
                                    Line Axist = Line.CreateBound(center, center + XYZ.BasisZ);
                                    ElementTransformUtils.RotateElement(doc, family1.Id, Axist, rot);

                                }
                                //TaskDialog.Show("revit",geo1.Symbol)
                            }
                        }
                    }
                    //TaskDialog.Show("revit1", "co " + demgeo + " Geo");

                    //TaskDialog.Show("revit1", fm1.ItemBlock);


                    TaskDialog.Show("revit1", "There are " + demso + " created");

                }


                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}

