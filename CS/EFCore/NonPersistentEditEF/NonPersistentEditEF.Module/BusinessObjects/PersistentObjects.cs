using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {

    [DefaultClassOptions]
    public class Product : BaseObject {

        public virtual string Model { get; set; }
        public virtual Category Category { get; set; }
        public virtual decimal Price { get; set; }
    }

    [DefaultClassOptions]
    public class Category : BaseObject {

        public virtual string Name { get; set; }
    }
}
