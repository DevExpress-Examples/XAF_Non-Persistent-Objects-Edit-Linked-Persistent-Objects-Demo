Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.ExpressApp.Xpo
Imports NonPersistentObjectsDemo.Module.BusinessObjects

Namespace NonPersistentObjectsDemo.Module
	' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
	Public NotInheritable Partial Class NonPersistentObjectsDemoModule
		Inherits ModuleBase

		Public Sub New()
			InitializeComponent()
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
			Return New ModuleUpdater() { updater }
		End Function
		Public Overrides Sub Setup(ByVal application As XafApplication)
			MyBase.Setup(application)
			' Manage various aspects of the application UI and behavior at the module level.
			AddHandler application.SetupComplete, AddressOf Application_SetupComplete
		End Sub
		Private Sub Application_SetupComplete(ByVal sender As Object, ByVal e As EventArgs)
			AddHandler Application.ObjectSpaceCreated, AddressOf Application_ObjectSpaceCreated
			NonPersistentObjectSpace.UseKeyComparisonToDetermineIdentity = True
		End Sub
		Private Sub Application_ObjectSpaceCreated(ByVal sender As Object, ByVal e As ObjectSpaceCreatedEventArgs)
			Dim npos = TryCast(e.ObjectSpace, NonPersistentObjectSpace)
			If npos IsNot Nothing Then
				If Not npos.AdditionalObjectSpaces.Any(Function(os) os.IsKnownType(GetType(BaseObject))) Then
					Dim persistentObjectSpace As IObjectSpace = Application.CreateObjectSpace(GetType(BaseObject))
					npos.AdditionalObjectSpaces.Add(persistentObjectSpace)
				End If
				npos.AutoDisposeAdditionalObjectSpaces = True
				npos.AutoRefreshAdditionalObjectSpaces = True
				npos.AutoCommitAdditionalObjectSpaces = True
				Dim tempVar As New ProductViewAdapter(npos)
			End If
		End Sub
		Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
			MyBase.CustomizeTypesInfo(typesInfo)
			CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
		End Sub
	End Class
End Namespace
