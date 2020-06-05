Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports NonPersistentObjectsDemo.Module.BusinessObjects

Namespace NonPersistentObjectsDemo.Module.Controllers
	Public Class ProductViewNestedCollectionController
		Inherits ObjectViewController(Of ListView, Product)

		Public Sub New()
			TargetViewNesting = Nesting.Nested
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			Dim pcs = TryCast(View.CollectionSource, PropertyCollectionSource)
			Dim linkUnlinkController = Frame.GetController(Of LinkUnlinkController)()
			If linkUnlinkController IsNot Nothing Then
				linkUnlinkController.Active("NotInProductView") = Not (pcs IsNot Nothing AndAlso (TypeOf pcs.MasterObject Is ProductView))
			End If
		End Sub
	End Class
End Namespace
