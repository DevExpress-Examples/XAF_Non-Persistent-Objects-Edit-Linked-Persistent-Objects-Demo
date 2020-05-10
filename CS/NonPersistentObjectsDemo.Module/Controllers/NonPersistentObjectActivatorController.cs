using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentObjectsDemo.Module.Controllers {
    public class NonPersistentObjectActivatorController : WindowController {
        ShowNavigationItemController showNavigationItemController;
        public NonPersistentObjectActivatorController() {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            if(showNavigationItemController != null) {
                showNavigationItemController.CustomShowNavigationItem += ShowNavigationItemController_CustomShowNavigationItem;
            }
        }
        protected override void OnDeactivated() {
            if(showNavigationItemController != null) {
                showNavigationItemController.CustomShowNavigationItem -= ShowNavigationItemController_CustomShowNavigationItem;
            }
            base.OnDeactivated();
        }
        private void ShowNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e) {
            var args = e.ActionArguments;
            var shortcut = args.SelectedChoiceActionItem.Data as ViewShortcut;
            if(shortcut != null) {
                var model = Application.FindModelView(shortcut.ViewId);
                if(model is IModelDetailView && string.IsNullOrEmpty(shortcut.ObjectKey)) {
                    var objectType = ((IModelDetailView)model).ModelClass.TypeInfo.Type;
                    if(typeof(ProductView).IsAssignableFrom(objectType)) {
                        var objectSpace = Application.CreateObjectSpace(objectType);
                        var obj = objectSpace.CreateObject(objectType);
                        objectSpace.RemoveFromModifiedObjects(obj);
                        var detailView = Application.CreateDetailView(objectSpace, shortcut.ViewId, true, obj);
                        detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                        args.ShowViewParameters.CreatedView = detailView;
                        args.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
