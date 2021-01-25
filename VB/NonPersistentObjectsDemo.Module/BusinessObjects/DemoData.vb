Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.ExpressApp

Namespace NonPersistentObjectsDemo.Module.BusinessObjects

	Friend Class DemoDataCreator
		Private ObjectSpace As IObjectSpace
		Public Sub New(ByVal objectSpace As IObjectSpace)
			Me.ObjectSpace = objectSpace
		End Sub
		Public Sub CreateDemoObjects()
			Dim rnd = New Random()
			If ObjectSpace.GetObjectsCount(GetType(Product), Nothing) = 0 Then
				Dim c1 = CreateCategory("Beverages")
				Dim c4 = CreateCategory("Cheeses")
				Dim c5 = CreateCategory("Breads")
				CreateProduct(c1, "Chai", 18.0D)
				CreateProduct(c1, "Soda", 12.0D)
				CreateProduct(c4, "Queso Cabrales", 21.5D)
				CreateProduct(c4, "Queso Manchego La Pastora", 38.0D)
				CreateProduct(c4, "Gorgonzola Telino", 12.5D)
				CreateProduct(c4, "Mascarpone Fabioli", 32.0D)
				CreateProduct(c4, "Geitost", 2.5D)
				CreateProduct(c5, "Gustaf's Knäckebröd", 21.0D)
				CreateProduct(c5, "Tunnbröd", 9.0D)
			End If
		End Sub
		Private Function CreateProduct(ByVal category As Category, ByVal model As String, ByVal price As Decimal) As Product
			Dim product = ObjectSpace.CreateObject(Of Product)()
			product.Model = model
			product.Category = category
			product.Price = price
			Return product
		End Function
		Private Function CreateCategory(ByVal name As String) As Category
			Dim category = ObjectSpace.CreateObject(Of Category)()
			category.Name = name
			Return category
		End Function
	End Class
End Namespace
