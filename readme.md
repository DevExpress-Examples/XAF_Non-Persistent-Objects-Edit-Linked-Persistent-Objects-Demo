*Files to look at*:

* [Module.cs](./CS/NonPersistentObjectsDemo.Module/Module.cs)
* [ProductView.cs](./CS/NonPersistentObjectsDemo.Module/BusinessObjects/ProductView.cs)
* [NonPersistentObjectBase.cs](./CS/NonPersistentObjectsDemo.Module/BusinessObjects/NonPersistentObjectBase.cs)


# How to edit a collection of Persistent Objects linked to Non-Persistent Objects

## Scenario

When a [Non\-Persistent Object](https://docs.devexpress.com/eXpressAppFramework/116516/concepts/business-model-design/non-persistent-objects?v=20.1) contains a collection of persistent business objects, it would be convenient to edit linked objects right in the list view in in-place mode or in ListViewAndDetailView mode. Also, when a linked persistent object is edited in a popup detail view, we want to see changes in the source list view after they are saved.

## Solution

NonPersistentObjectSpace created for the mater object cannot handle linked persistent objects by default. To work with them, create a perssitent object space and add it to the AdditionalObjectSpaces collection. To automatically dispose of additional object spaces, set AutoDisposeAdditionalObjectSpaces to true. 

To automatically commit changes made to linked persistent objects, set AutoCommitAdditionalObjectSpaces to true.

To enable the Save action in a non-persistent object view when a linked persistent object is modified, subscribe to the ObjectChanged event of the persistent object space and call the NonPersistentObjectSpace.SetModified(null) method.

To refresh linked persistent objects when the non-persistent object view is refreshed, set AutoRefreshAdditionalObjectSpaces to true and subscribe to the ObjectGetting event. In the event handler, create a new non-persistent object instance and get fresh copies of linked persistent objects.

