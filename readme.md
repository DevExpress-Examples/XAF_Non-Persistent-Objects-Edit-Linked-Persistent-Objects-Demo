*Files to look at*:

* [Module.cs](./CS/NonPersistentObjectsDemo.Module/Module.cs)
* [ProductView.cs](./CS/NonPersistentObjectsDemo.Module/BusinessObjects/ProductView.cs)
* [NonPersistentObjectBase.cs](./CS/NonPersistentObjectsDemo.Module/BusinessObjects/NonPersistentObjectBase.cs)


# How to edit a collection of Persistent Objects linked to a Non-Persistent Object

## Scenario

When a [Non\-Persistent Object](https://docs.devexpress.com/eXpressAppFramework/116516/concepts/business-model-design/non-persistent-objects?v=20.1) contains a collection of persistent business objects, we want to edit linked objects right in the list view either in in-place mode or in ListViewAndDetailView mode. Also, when a linked persistent object is edited in a popup detail view, we want to see changes in the source list view after they are saved.

## Solution

The [NonPersistentObjectSpace](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace) created for the master object cannot handle linked persistent objects by default. To work with them, create a persistent object space and add it to the [AdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AdditionalObjectSpaces) collection. Set [NonPersistentObjectSpace\.AutoDisposeAdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AutoDisposeAdditionalObjectSpaces?v=20.1) to *true* to automatically dispose of additional object spaces when the master is disposed. 

To automatically commit changes made to linked persistent objects, set **NonPersistentObjectSpace.AutoCommitAdditionalObjectSpaces** to *true*.

To refresh linked persistent objects when the non-persistent object view is refreshed, set [NonPersistentObjectSpace\.AutoRefreshAdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AutoRefreshAdditionalObjectSpaces) to *true* and subscribe to the [ObjectGetting](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.ObjectGetting) event. In the event handler, create a new non-persistent object instance and get fresh copies of linked persistent objects.

The Category property in this example is used to filter the nested list view of persistent objects. To avoid modifying the object space when this property is changed, subscribe to the [BaseObjectSpace.ModifiedChanging](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.BaseObjectSpace.ModifiedChanging) event and set the *e.Cancel* parameter to *true* depending on other event arguments.
