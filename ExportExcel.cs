#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq;
#endregion

namespace DoallVietnam
{
    [Transaction(TransactionMode.Manual)]
    public class ExportExcel : IExternalCommand
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

            Excel.Application xlApp = new Excel.Application();

            Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            

            // Picks element contain Groups

            Selection sel = uidoc.Selection;
            IList<Reference> listRefEle1 = sel.PickObjects(ObjectType.Element, "Pick groups");

            //find all wall in project
            FilteredElementCollector collectorWall = new FilteredElementCollector(doc);
            List<Wall> listWall = collectorWall.OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().Cast<Wall>().ToList();

            List<ElementId> listWrongtype= new List<ElementId>();
            List<ElementId> listRighttype= new List<ElementId>();


            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");

                foreach (Wall itemWall in listWall)
                {
                    IList<ElementId> listInserted = itemWall.FindInserts(true, false, true, true);
                    foreach (ElementId itemId in listInserted)
                    {
                        Element eleInserted = doc.GetElement(itemId);
                        if (eleInserted.Name.Contains("_C"))
                        {
                            if (itemWall.Name.Contains("DUMMY")||itemWall.Name.Contains("Dummy")||itemWall.Name.Contains("dummy"))
                            {
                                listRighttype.Add(eleInserted.Id);
                            }
                            else
                            {
                                listWrongtype.Add(eleInserted.Id);
                            }
                        }
                        else if (eleInserted.Name.Contains("_B"))
                        {
                            if (itemWall.Name.Contains("Brick") || itemWall.Name.Contains("BRICK") || itemWall.Name.Contains("CMU"))
                            {
                                listRighttype.Add(eleInserted.Id);
                            }
                            else
                            {
                                listWrongtype.Add(eleInserted.Id);
                            }
                        }
                        else if (eleInserted.Name.Contains("_D"))
                        {
                            if (itemWall.Name.Contains("Dry") || itemWall.Name.Contains("DRY") || itemWall.Name.Contains("Insulation")||itemWall.Name.Contains("INSULATION"))
                            {
                                listRighttype.Add(eleInserted.Id);
                            }
                            else
                            {
                                listWrongtype.Add(eleInserted.Id);
                            }
                        }
                    }

                }

                //TaskDialog.Show("soluongcua", "cua rightType: " + listRighttype.Count.ToString() + "  " + "cua WrongType: " + listWrongtype.Count.ToString());
                //foreach (ElementId item in listWrongtype)
                //{
                //    TaskDialog.Show("revit1", item.ToString());
                //}
                int sheet = 1;
                foreach (Reference itemRef in listRefEle1)
                {
                    Element ele2 = doc.GetElement(itemRef);
                    if (ele2.ToString().Contains("Group"))
                    {
                        Group group2 = ele2 as Group;
                        String grName = group2.Name;
                        int lengGroupName = grName.Length;
                        grName = grName.Replace("*", "_");
                        grName = grName.Replace("/", "_");
                        grName = grName.Replace("#", "_");
                        Excel.Sheets sheet1 = xlWorkBook.Worksheets;
                        var xlNewSheet = (Excel.Worksheet)sheet1.Add(sheet1[sheet], Type.Missing, Type.Missing, Type.Missing);

                        //TaskDialog.Show("ten group", grName.Length.ToString());

                        if (lengGroupName<31)
                        {
                            xlNewSheet.Name = grName;

                        }
                        else
                        {
                            xlNewSheet.Name = grName.Substring(0, 30);
                        }
                        
                        IList<ElementId> memberIds = group2.GetMemberIds();
                        int count1 = memberIds.Count;

                        xlNewSheet.Cells[1, 1] = grName;
                        xlNewSheet.Cells[1, 1].Interior.ColorIndex = 7;
                        xlNewSheet.Cells[4, 1] = "Family Category";
                        xlNewSheet.Cells[4, 1].Interior.ColorIndex = 40;
                        xlNewSheet.Cells[4, 2] = "Name";
                        xlNewSheet.Cells[4, 2].Interior.ColorIndex = 40;
                        xlNewSheet.Cells[4, 3] = "Id";
                        xlNewSheet.Cells[4, 3].Interior.ColorIndex = 40;


                        int sub = 5;
                        foreach (ElementId item in memberIds)
                        {

                            Element elemember = doc.GetElement(item);


                            //Parameter para1 = elemember.LookupParameter("Mark");
                            //Parameter para1 = elemember.get_Parameter(BuiltInParameter.LEVEL_PARAM);
                            //Parameter para2 = elemember.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                            //Parameter para3 = elemember.get_Parameter(BuiltInParameter.RBS_HVACLOAD_FLOOR_AREA_PARAM);
                            //Parameter para4 = elemember.get_Parameter(BuiltInParameter.RBS_HVACLOAD_WALL_AREA_PARAM);


                            //Parameter para5 = elemember.LookupParameter("Unconnected Height");

                            string strNameEle = elemember.ToString();
                            if (strNameEle.Contains("ModelLine") || strNameEle.Contains("Sketch") || strNameEle.Contains("Dimension") || strNameEle.Contains("AnalyticalModel") || strNameEle.Contains("ReferencePlane"))
                            {

                            }
                            else
                            {
                                xlNewSheet.Cells[sub, 1] = elemember.ToString();
                                //xlNewSheet.Cells[sub, 1].Interior.ColorIndex = 36;
                                xlNewSheet.Cells[sub, 2] = elemember.Name.ToString();
                                xlNewSheet.Cells[sub, 3] = elemember.Id.ToString();
                                foreach (ElementId idWrongtype in listWrongtype)
                                {
                                    if (elemember.Id == idWrongtype)
                                    {
                                        xlNewSheet.Cells[sub, 3].Interior.ColorIndex = 36;
                                        xlNewSheet.Tab.Color = 3;
                                    }
                                }
                                //if (para1 != null)
                                //{
                                //    xlNewSheet.Cells[sub, 4] = para1.AsValueString();

                                //}

                                //if (para2 != null)
                                //{
                                //    xlNewSheet.Cells[sub, 5] = para2.AsDouble().ToString();

                                //}
                                //if (para2 != null)
                                //{
                                //    xlNewSheet.Cells[sub, 6] = para2.AsDouble().ToString();

                                //}
                                //if (para2 != null)
                                //{
                                //    xlNewSheet.Cells[sub, 7] = para2.AsDouble().ToString();

                                //}
                                //xlNewSheet.Cells[sub, 3] = para1.AsDouble().ToString();
                                //xlNewSheet.Cells[sub, 4] = para2.AsValueString();


                                sub++;
                            }

                        }
                        sheet++;
                    }
                    else
                    {
                        //TaskDialog.Show("ten group", ele2.ToString());
                    }

                    
                }
                TaskDialog.Show("revit", "There are " + (sheet-1) + " Group(s) will be exported");

                xlWorkBook.SaveAs("d:\\ExportQS.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);
                //xlApp.Quit();
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}
