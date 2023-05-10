using DevExpress.ExpressApp;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {
    class ProductViewAdapter {
        private NonPersistentObjectSpace objectSpace;
        private bool isDirty = true;

        public ProductViewAdapter(NonPersistentObjectSpace npos) {
            this.objectSpace = npos;
            objectSpace.ObjectGetting += ObjectSpace_ObjectGetting;
            objectSpace.Reloaded += ObjectSpace_Reloaded;
            objectSpace.ModifiedChanging += ObjectSpace_ModifiedChanging;
        }
        private void ObjectSpace_ModifiedChanging(object sender, ObjectSpaceModificationEventArgs e) {
            if(e.MemberInfo!= null) {
                if(e.MemberInfo.Owner.Type == typeof(ProductView) && e.MemberInfo.Name == nameof(ProductView.Category)) {
                    e.Cancel = true;
                }
            }
        }
        private void ObjectSpace_Reloaded(object sender, EventArgs e) {
            isDirty = true;
        }
        private void ObjectSpace_ObjectGetting(object sender, ObjectGettingEventArgs e) {
            if(e.SourceObject is ProductView) {
                var obj = (ProductView)e.SourceObject;
                if(isDirty) {
                    var clone = new ProductView();
                    clone.Category = objectSpace.GetObject<Category>(obj.Category);
                    e.TargetObject = clone;
                    isDirty = false;
                }
            }
        }
    }

}
