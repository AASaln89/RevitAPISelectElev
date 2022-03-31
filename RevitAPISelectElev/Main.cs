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
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var levels = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Levels)
                    .WhereElementIsNotElementType();

            string msg = "";

            foreach (var level in levels)
            {
                var ducts = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_DuctCurves)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementLevelFilter(level.Id));
                int ductCount = ducts.GetElementCount();
                msg += $"{level.Name}:{ductCount}\n";
            }

            TaskDialog.Show("Duct Count", msg);
            return Result.Succeeded;
        }
    }
}
