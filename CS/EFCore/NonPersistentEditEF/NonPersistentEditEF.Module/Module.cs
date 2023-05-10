using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.BaseImpl.EF;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentEditEF.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class NonPersistentEditEFModule : ModuleBase {
    public NonPersistentEditEFModule() {
		// 
		// NonPersistentEditEFModule
		// 
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.SetupComplete += Application_SetupComplete;
        // Manage various aspects of the application UI and behavior at the module level.
    }
    private void Application_SetupComplete(object sender, EventArgs e) {
        Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
        NonPersistentObjectSpace.UseKeyComparisonToDetermineIdentity = true;
    }
    private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
        var npos = e.ObjectSpace as NonPersistentObjectSpace;
        if(npos != null) {
            if(!npos.AdditionalObjectSpaces.Any(os => os.IsKnownType(typeof(BaseObject)))) {
                IObjectSpace persistentObjectSpace = Application.CreateObjectSpace(typeof(BaseObject));
                npos.AdditionalObjectSpaces.Add(persistentObjectSpace);
            }
            npos.AutoDisposeAdditionalObjectSpaces = true;
            npos.AutoRefreshAdditionalObjectSpaces = true;
            npos.AutoCommitAdditionalObjectSpaces = true;
            new ProductViewAdapter(npos);
        }
    }
}
