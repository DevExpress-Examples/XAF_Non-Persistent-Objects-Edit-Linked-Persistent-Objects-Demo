Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base

Namespace NonPersistentObjectsDemo.Module.BusinessObjects

	<DefaultClassOptions>
	<DevExpress.ExpressApp.DC.DomainComponent>
	Public Class ProductView
		Inherits NonPersistentObjectBase

		Private _AllProducts As IList(Of Product)
		Public ReadOnly Property AllProducts() As IList(Of Product)
			Get
				If _AllProducts Is Nothing Then
					_AllProducts = ObjectSpace.GetObjects(Of Product)()
				End If
				Return _AllProducts
			End Get
		End Property
		Private _Category As Category
		<ImmediatePostData>
		Public Property Category() As Category
			Get
				Return _Category
			End Get
			Set(ByVal value As Category)
				If value IsNot Category Then
					_Category = value
					OnPropertyChanged(NameOf(Category))
					UpdateProducts()
					OnPropertyChanged(NameOf(Products))
				End If
			End Set
		End Property
		Private Sub UpdateProducts()
			If _Products IsNot Nothing Then
				ObjectSpace.ApplyCriteria(_Products, CriteriaOperator.Parse("? is null or Category = ?", Category, Category))
			End If
		End Sub
		Private _Products As IList(Of Product)
		Public ReadOnly Property Products() As IList(Of Product)
			Get
				If _Products Is Nothing Then
					_Products = ObjectSpace.GetObjects(Of Product)()
					UpdateProducts()
				End If
				Return _Products
			End Get
		End Property
	End Class

	Friend Class ProductViewAdapter
		Private objectSpace As NonPersistentObjectSpace
		Private isDirty As Boolean = True

		Public Sub New(ByVal npos As NonPersistentObjectSpace)
			Me.objectSpace = npos
			AddHandler objectSpace.ObjectGetting, AddressOf ObjectSpace_ObjectGetting
			AddHandler objectSpace.Reloaded, AddressOf ObjectSpace_Reloaded
			AddHandler objectSpace.ModifiedChanging, AddressOf ObjectSpace_ModifiedChanging
		End Sub
		Private Sub ObjectSpace_ModifiedChanging(ByVal sender As Object, ByVal e As ObjectSpaceModificationEventArgs)
			If e.MemberInfo IsNot Nothing Then
				If e.MemberInfo.Owner.Type Is GetType(ProductView) AndAlso e.MemberInfo.Name = NameOf(ProductView.Category) Then
					e.Cancel = True
				End If
			End If
		End Sub
		Private Sub ObjectSpace_Reloaded(ByVal sender As Object, ByVal e As EventArgs)
			isDirty = True
		End Sub
		Private Sub ObjectSpace_ObjectGetting(ByVal sender As Object, ByVal e As ObjectGettingEventArgs)
			If TypeOf e.SourceObject Is ProductView Then
				Dim obj = CType(e.SourceObject, ProductView)
				If isDirty Then
					Dim clone = New ProductView()
					clone.Category = objectSpace.GetObject(Of Category)(obj.Category)
					e.TargetObject = clone
					isDirty = False
				End If
			End If
		End Sub
	End Class

End Namespace
