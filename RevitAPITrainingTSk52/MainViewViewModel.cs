using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingTSk52
{
    class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }

        public List<Element> PickedObjects { get; } = new List<Element>();

        public List<WallType> WallTypes { get; } = new List<WallType>();

        public DelegateCommand SelectCommand { get; }

        public WallType SelectedWallTypes { get; set; }

       
        public MainViewViewModel(ExternalCommandData commandData)
        {
            //для взаимодействия с UI docment
            _commandData = commandData;
            //SelectCommand = new DelegateCommand(OnSelectCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);
            WallTypes = WallUtils.GetWallTypes(commandData);
        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            //для распаковки элемента
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedWallTypes == null)
                return;
            //Создание транзакции
            using (var ts = new Transaction(doc, "Set System type"))
            {
                ts.Start();

                foreach (var picketObject in PickedObjects)
                {
                    //Проверка на принаджлежность
                    if (picketObject is Wall)
                    {
                        var oWall = picketObject as Wall;
                        WallType wallType = SelectedWallTypes as WallType;
                        oWall.WallType = wallType;
                    }
                }
                ts.Commit();
            }
            RaiseCloseRequest();
        }
        public event EventHandler CloseRequest;
        //метод для закрытия окна
        private void RaiseCloseRequest()
        {//Для запуска методов привзязанных к запросу закрытия
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
