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

        Public Property Model As String
            Get
                Return _Model
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Product.Model), _Model, value)
            End Set
        End Property

        Private _Category As Category

        Public Property Category As Category
            Get
                Return _Category
            End Get

            Set(ByVal value As Category)
                SetPropertyValue(NameOf(Product.Category), _Category, value)
            End Set
        End Property

        <PersistentAlias("Concat(iif(Category is null, '*', Category.Name), ' - ', Model)")>
        Public ReadOnly Property DisplayName As String
            Get
                Return CStr(EvaluateAlias(NameOf(Product.DisplayName)))
            End Get
        End Property

        Private _Price As Decimal

        Public Property Price As Decimal
            Get
                Return _Price
            End Get

            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)(NameOf(Product.Price), _Price, value)
            End Set
        End Property

        Protected Overrides Sub OnChanged(ByVal propertyName As String, ByVal oldValue As Object, ByVal newValue As Object)
            MyBase.OnChanged(propertyName, oldValue, newValue)
            If Equals(propertyName, NameOf(Product.Category)) OrElse Equals(propertyName, NameOf(Product.Model)) Then
                OnChanged(NameOf(Product.DisplayName))
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

        Public Property Name As String
            Get
                Return _Name
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Category.Name), _Name, value)
            End Set
        End Property
    End Class
End Namespace
