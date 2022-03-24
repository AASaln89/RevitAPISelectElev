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
        public object DuctLeveL { get; private set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var ductsList = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            var duct1level = new List<Duct>();
            var duct2Level = new List<Duct>();

            string Level = string.Empty;
            foreach (Duct duct in ductsList)
            {
                Parameter DuctLevel = duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM);
                string L = DuctLeveL.AsValueString().ToString();
                if (L=="Level 1")
                {
                    duct1level.Add(duct);
                }
                if (L == "Level 2")
                {
                    duct2Level.Add(duct);
                }
            }
            Level += $"Количество 1 этаж: {duct1level.Count}{Environment.NewLine} Количество 2 этаж: {duct2Level.Count}";
            Level += $"ObschKol-vo: {ductsList.Count}";
            TaskDialog.Show("Kol-vo", Level);
            return Result.Succeeded;
        }
    }
}
