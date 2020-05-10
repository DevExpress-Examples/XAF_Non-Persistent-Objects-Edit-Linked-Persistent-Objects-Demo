using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {

    [DefaultClassOptions]
    public class Product : BaseObject {
        public Product(Session session) : base(session) { }

        private string _Model;
        public string Model {
            get { return _Model; }
            set { SetPropertyValue<string>(nameof(Model), ref _Model, value); }
        }
        private Category _Category;
        public Category Category {
            get { return _Category; }
            set { SetPropertyValue<Category>(nameof(Category), ref _Category, value); }
        }
        [PersistentAlias("Concat(iif(Category is null, '*', Category.Name), ' - ', Model)")]
        public string DisplayName {
            get { return (string)EvaluateAlias(nameof(DisplayName)); }
        }
        private decimal _Price;
        public decimal Price {
            get { return _Price; }
            set { SetPropertyValue<decimal>(nameof(Price), ref _Price, value); }
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            if(propertyName == nameof(Category) || propertyName == nameof(Model)) {
                OnChanged(nameof(DisplayName));
            }
        }
    }

    [DefaultClassOptions]
    public class Category : BaseObject {
        public Category(Session session) : base(session) { }

        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }
    }
}
