using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISelectElev
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        private ElementId levelId;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector levels = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType();

            var duct = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_DuctCurves)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementLevelFilter(levelId));

            foreach (var level in levels)
            {
                duct = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_DuctCurves)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementLevelFilter(levelId));
            }


            //var duct1level = new List<Duct>();
            //var duct2Level = new List<Duct>();

            //string Level = string.Empty;
            //foreach (Duct duct in ductsList)
            //{
            //    Parameter DuctLevel = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM);
            //    string L = DuctLeveL.AsValueString().ToString();
            //    if (L == "Level 1")
            //    {
            //        duct1level.Add(duct);
            //    }
            //    if (L == "Level 2")
            //    {
            //        duct2Level.Add(duct);
            //    }
            //}
            //Level += $"Количество 1 этаж: {duct1level.Count}{Environment.NewLine} Количество 2 этаж: {duct2Level.Count}";
            //Level += $"ObschKol-vo: {ductsList.Count}";
            TaskDialog.Show("123", $"{levels.ToString()} {Environment.NewLine} {duct.ToString()}");

            return Result.Succeeded;
        }
    }
}
