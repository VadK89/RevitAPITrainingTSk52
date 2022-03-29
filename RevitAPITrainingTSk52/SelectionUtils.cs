using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace RevitAPITrainingTSk52
{
    public class SelectionUtils
    {
        public static Element PickObject(ExternalCommandData commandData, string message = "Выберете элемент")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //для распаковки элемента
            Document doc = uidoc.Document;


            //переменная для выбора объекта
            var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, message);
            //распаковка референса в элемент
            var oElement = doc.GetElement(selectedObject);
            return oElement;
        }
        //для множественного выделения
        public static List<Element> PickObjects(ExternalCommandData commandData, string message = "Выберете элементы")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //для распаковки элемента
            Document doc = uidoc.Document;


            //переменная для выбора объекта
            var selectedObjects = uidoc.Selection.PickObjects(ObjectType.Element, message);
            //Преобразование в лист с  элементами
            List<Element> elementList = selectedObjects.Select(selectedObject => doc.GetElement(selectedObject)).ToList();
            return elementList;
        }
    }
}
