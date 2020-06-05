Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

Namespace NonPersistentObjectsDemo.Module.BusinessObjects

	<DefaultClassOptions>
	Public Class Product
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private _Model As String
		Public Property Model() As String
			Get
				Return _Model
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)(NameOf(Model), _Model, value)
			End Set
		End Property
		Private _Category As Category
		Public Property Category() As Category
			Get
				Return _Category
			End Get
			Set(ByVal value As Category)
				SetPropertyValue(Of Category)(NameOf(Category), _Category, value)
			End Set
		End Property
		<PersistentAlias("Concat(iif(Category is null, '*', Category.Name), ' - ', Model)")>
		Public ReadOnly Property DisplayName() As String
			Get
				Return CStr(EvaluateAlias(NameOf(DisplayName)))
			End Get
		End Property
		Private _Price As Decimal
		Public Property Price() As Decimal
			Get
				Return _Price
			End Get
			Set(ByVal value As Decimal)
				SetPropertyValue(Of Decimal)(NameOf(Price), _Price, value)
			End Set
		End Property
		Protected Overrides Sub OnChanged(ByVal propertyName As String, ByVal oldValue As Object, ByVal newValue As Object)
			MyBase.OnChanged(propertyName, oldValue, newValue)
			If propertyName = NameOf(Category) OrElse propertyName = NameOf(Model) Then
				OnChanged(NameOf(DisplayName))
			End If
		End Sub
	End Class

	<DefaultClassOptions>
	Public Class Category
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)(NameOf(Name), _Name, value)
			End Set
		End Property
	End Class
End Namespace
