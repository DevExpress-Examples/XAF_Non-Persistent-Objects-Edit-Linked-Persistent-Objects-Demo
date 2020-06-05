Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.SystemModule
Imports NonPersistentObjectsDemo.Module.BusinessObjects

Namespace NonPersistentObjectsDemo.Module.Controllers
	Public Class NonPersistentObjectActivatorController
		Inherits WindowController

		Private showNavigationItemController As ShowNavigationItemController
		Public Sub New()
			TargetWindowType = WindowType.Main
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			showNavigationItemController = Frame.GetController(Of ShowNavigationItemController)()
			If showNavigationItemController IsNot Nothing Then
				AddHandler showNavigationItemController.CustomShowNavigationItem, AddressOf ShowNavigationItemController_CustomShowNavigationItem
			End If
		End Sub
		Protected Overrides Sub OnDeactivated()
			If showNavigationItemController IsNot Nothing Then
				RemoveHandler showNavigationItemController.CustomShowNavigationItem, AddressOf ShowNavigationItemController_CustomShowNavigationItem
			End If
			MyBase.OnDeactivated()
		End Sub
		Private Sub ShowNavigationItemController_CustomShowNavigationItem(ByVal sender As Object, ByVal e As CustomShowNavigationItemEventArgs)
			Dim args = e.ActionArguments
			Dim shortcut = TryCast(args.SelectedChoiceActionItem.Data, ViewShortcut)
			If shortcut IsNot Nothing Then
				Dim model = Application.FindModelView(shortcut.ViewId)
				If TypeOf model Is IModelDetailView AndAlso String.IsNullOrEmpty(shortcut.ObjectKey) Then
					Dim objectType = DirectCast(model, IModelDetailView).ModelClass.TypeInfo.Type
					If GetType(ProductView).IsAssignableFrom(objectType) Then
						Dim objectSpace = Application.CreateObjectSpace(objectType)
						Dim obj = objectSpace.CreateObject(objectType)
						objectSpace.RemoveFromModifiedObjects(obj)
						Dim detailView = Application.CreateDetailView(objectSpace, shortcut.ViewId, True, obj)
						detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit
						args.ShowViewParameters.CreatedView = detailView
						args.ShowViewParameters.TargetWindow = TargetWindow.Current
						e.Handled = True
					End If
				End If
			End If
		End Sub
	End Class
End Namespace
