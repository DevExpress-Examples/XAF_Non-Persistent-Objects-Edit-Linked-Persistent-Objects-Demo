using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentEditEF.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        CreateDemoObjects();
        ObjectSpace.CommitChanges();
    }
    public void CreateDemoObjects() {
        var rnd = new Random();
        if(ObjectSpace.GetObjectsCount(typeof(Product), null) == 0) {
            var c1 = CreateCategory("Beverages");
            var c4 = CreateCategory("Cheeses");
            var c5 = CreateCategory("Breads");
            CreateProduct(c1, "Chai", 18.0m);
            CreateProduct(c1, "Soda", 12.0m);
            CreateProduct(c4, "Queso Cabrales", 21.5m);
            CreateProduct(c4, "Queso Manchego La Pastora", 38.0m);
            CreateProduct(c4, "Gorgonzola Telino", 12.5m);
            CreateProduct(c4, "Mascarpone Fabioli", 32.0m);
            CreateProduct(c4, "Geitost", 2.5m);
            CreateProduct(c5, "Gustaf's Knäckebröd", 21.0m);
            CreateProduct(c5, "Tunnbröd", 9.0m);
        }
    }
    private Product CreateProduct(Category category, string model, decimal price) {
        var product = ObjectSpace.CreateObject<Product>();
        product.Model = model;
        product.Category = category;
        product.Price = price;
        return product;
    }
    private Category CreateCategory(string name) {
        var category = ObjectSpace.CreateObject<Category>();
        category.Name = name;
        return category;
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
