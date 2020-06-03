using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {

    [DefaultClassOptions]
    [DevExpress.ExpressApp.DC.DomainComponent]
    public class ProductView : NonPersistentObjectBase {

        private IList<Product> _AllProducts;
        public IList<Product> AllProducts {
            get {
                if(_AllProducts == null) {
                    _AllProducts = ObjectSpace.GetObjects<Product>();
                }
                return _AllProducts;
            }
        }
        private Category _Category;
        [ImmediatePostData]
        public Category Category {
            get { return _Category; }
            set {
                if(value != Category) {
                    _Category = value;
                    OnPropertyChanged(nameof(Category));
                    UpdateProducts();
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        private void UpdateProducts() {
            if(_Products != null) {
                ObjectSpace.ApplyCriteria(_Products, CriteriaOperator.Parse("? is null or Category = ?", Category, Category));
            }
        }
        private IList<Product> _Products;
        public IList<Product> Products {
            get {
                if(_Products == null) {
                    _Products = ObjectSpace.GetObjects<Product>();
                    UpdateProducts();
                }
                return _Products;
            }
        }
    }

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
