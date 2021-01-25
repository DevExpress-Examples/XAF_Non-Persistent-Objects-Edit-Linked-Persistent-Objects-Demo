using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentObjectsDemo.Module {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class NonPersistentObjectsDemoModule : ModuleBase {
        public NonPersistentObjectsDemoModule() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
            application.SetupComplete += Application_SetupComplete;
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
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}
