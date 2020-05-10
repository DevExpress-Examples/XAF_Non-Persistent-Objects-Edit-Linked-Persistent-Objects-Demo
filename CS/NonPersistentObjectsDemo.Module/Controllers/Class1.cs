using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using NonPersistentObjectsDemo.Module.BusinessObjects;

namespace NonPersistentObjectsDemo.Module.Controllers {
    public class MyController : ObjectViewController<DetailView, Order> {
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            var ti = typesInfo.FindTypeInfo(typeof(Order));
            var mi = ti.FindMember("DynValue");
            if(mi == null) {
                mi = ti.CreateMember("DynValue", typeof(String));
            }
        }
        public MyController() {
            var action = new SimpleAction(this, "Abracadabra", PredefinedCategory.View);
            action.Execute += Action_Execute;
        }

        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e) {
            var dv = View as DetailView;
            var editorNode = dv.Model.Items["newValuePropertyEditor"] as IModelPropertyEditor;
            var containerLayoutGroup = dv.Model.Layout["Main"] as IModelLayoutGroup;
            var litem = containerLayoutGroup["DynValue"] as IModelLayoutViewItem;
            var os = this.ObjectSpace;
            if(Frame.SetView(null, false, null, false)) {
                if(litem != null) {
                    litem.Remove();
                    litem = null;
                }
                if(editorNode != null) {
                    editorNode.Remove();
                    editorNode = null;
                }

                editorNode = dv.Model.Items.AddNode<IModelPropertyEditor>();
                ((IModelViewItem)editorNode).Id = "newValuePropertyEditor";
                editorNode.PropertyEditorType = ReflectionHelper.FindType("StringPropertyEditor");
                editorNode.AllowEdit = true;
                editorNode.ImmediatePostData = true;
                editorNode.PropertyName = "DynValue";
                //((IModelViewItem)editorNode).Id = "DynValue";
                //((IModelLayoutElement)editorNode).Id = "DynValue";
                ((IModelCommonMemberViewItem)editorNode).Caption = "Please enter value";
                editorNode.RowCount = 35;//modelNode.RowCount;
                editorNode.MaxLength = 3500;
                //editorNode.PredefinedValues = modelNode.PredefinedValues;
                //editorNode.EditMaskType = modelNode.EditMaskType;
                //editorNode.EditMask = modelNode.EditMask;
                //editorNode.DisplayFormat = modelNode.DisplayFormat;
                //editorNode.DataSourceCriteriaProperty = modelNode.DataSourceCriteriaProperty;
                //editorNode.DataSourceCriteria = modelNode.DataSourceCriteria;
                //editorNode.DataSourcePropertyIsNullMode = modelNode.DataSourcePropertyIsNullMode;
                //editorNode.DataSourcePropertyIsNullCriteria = modelNode.DataSourcePropertyIsNullCriteria;
                //editorNode.LookupEditorMode = modelNode.LookupEditorMode;
                //editorNode.ToolTip = modelNode.ToolTip;
                //editorNode.AllowClear = modelNode.AllowClear;
                //editorNode.LookupProperty = modelNode.LookupProperty;

                litem = containerLayoutGroup.AddNode<IModelLayoutViewItem>();
                litem.ShowCaption = true;
                litem.ViewItem = editorNode;
                litem.MaxSize = new System.Drawing.Size(0, 0);
                litem.MinSize = new System.Drawing.Size(0, 0);
                litem.Index = 0;
                litem.SizeConstraintsType = DevExpress.ExpressApp.Layout.XafSizeConstraintsType.Default;
                litem.VerticalAlign = DevExpress.ExpressApp.Editors.StaticVerticalAlign.Top;
                litem.RelativeSize = 100;
                litem.CaptionLocation = Locations.Top;
                litem.CaptionHorizontalAlignment = DevExpress.Utils.HorzAlignment.Default;
                litem.CaptionWordWrap = DevExpress.Utils.WordWrap.Default;

                dv.LoadModel(true);
                Frame.SetView(dv, true, Frame, false);
            }
        }
    }
}
