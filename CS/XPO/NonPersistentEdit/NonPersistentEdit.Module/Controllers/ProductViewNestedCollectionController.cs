using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentObjectsDemo.Module.Controllers {
    public class ProductViewNestedCollectionController : ObjectViewController<ListView, Product> {
        public ProductViewNestedCollectionController() {
            TargetViewNesting = Nesting.Nested;
        }
        protected override void OnActivated() {
            base.OnActivated();
            var pcs = View.CollectionSource as PropertyCollectionSource;
            var linkUnlinkController = Frame.GetController<LinkUnlinkController>();
            if(linkUnlinkController != null) {
                linkUnlinkController.Active["NotInProductView"] = !(pcs != null && (pcs.MasterObject is ProductView));
            }
        }
    }
}
