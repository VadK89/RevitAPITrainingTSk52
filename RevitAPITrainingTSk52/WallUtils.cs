using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;

namespace RevitAPITrainingTSk52
{
    public class WallUtils
    {

        //8метод для учета трубопроводных стистем
        public static List<WallType> GetWallTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //для распаковки элемента
            Document doc = uidoc.Document;


            //var walls = new FilteredElementCollector(doc)
            //  .OfCategory(BuiltInCategory.OST_Walls)
            //  .WhereElementIsNotElementType()
            //  .Cast<Wall>()
            //  .ToList();

            List<WallType> wallListTypes = new FilteredElementCollector(doc)
               .OfClass(typeof(WallType))
               .Cast<WallType>()
               .ToList();

            return wallListTypes;
        }
    }
}
