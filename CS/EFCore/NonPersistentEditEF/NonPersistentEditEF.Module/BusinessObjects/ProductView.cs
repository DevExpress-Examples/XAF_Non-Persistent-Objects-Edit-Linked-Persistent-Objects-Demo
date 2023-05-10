using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {

    [DefaultClassOptions]
    [DomainComponent]
    public class ProductView : NonPersistentBaseObject {

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

}
