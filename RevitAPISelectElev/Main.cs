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
                Element element = doc.GetElement(levelId);
                if (element is Duct)
                {

                }
            }
            TaskDialog.Show("123", $"{levels.ToString()} {Environment.NewLine} {duct.ToString()}");

            return Result.Succeeded;
        }
    }
}
