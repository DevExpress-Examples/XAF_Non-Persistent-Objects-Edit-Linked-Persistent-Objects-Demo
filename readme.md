<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T888962)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# XAF - How to edit a collection of persistent objects linked to a non-persistent object

When a [non\-persistent object](https://docs.devexpress.com/eXpressAppFramework/116516/concepts/business-model-design/non-persistent-objects) contains a collection of persistent business objects, you may need to edit linked objects right in the list view either in in-place or in `ListViewAndDetailView` mode. Also, if a linked persistent object is edited in a popup detail view, you may need to see changes in the source list view after they are saved.

This example demonstrates how you can perform these tasks in XAF applications.

## Implementation Details

1. The [NonPersistentObjectSpace](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace) created for the master object cannot handle linked persistent objects by default. To work with them, create a persistent object space and add it to the [AdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AdditionalObjectSpaces) collection. Set [NonPersistentObjectSpace\.AutoDisposeAdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AutoDisposeAdditionalObjectSpaces) to `true` to automatically dispose of additional object spaces when the master is disposed of. 
2. Set [NonPersistentObjectSpace\.AutoCommitAdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AutoCommitAdditionalObjectSpaces) to `true` to automatically commit changes made to linked persistent objects.
3. Set [NonPersistentObjectSpace\.AutoRefreshAdditionalObjectSpaces](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.AutoRefreshAdditionalObjectSpaces) to `true` and subscribe to the [ObjectGetting](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.NonPersistentObjectSpace.ObjectGetting) event, to refresh linked persistent objects when the non-persistent object view is refreshed. In the event handler, create a new instance of the non-persistent object and get fresh copies of linked persistent objects.
4. This example uses the `Category` property to filter the nested list view of persistent objects. To avoid  the object space modification when this property is changed, subscribe to the [BaseObjectSpace.ModifiedChanging](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.BaseObjectSpace.ModifiedChanging) event and set the `e.Cancel` parameter to `true` depending on other event arguments.

## Files to Review

* [Module.cs](./CS/EFCore/NonPersistentEditEF/NonPersistentEditEF.Module/Module.cs)
* [ProductView.cs](./CS/EFCore/NonPersistentEditEF/NonPersistentEditEF.Module/BusinessObjects/ProductView.cs)

## Documentation

- [Non-Persistent Objects](https://docs.devexpress.com/eXpressAppFramework/116516/business-model-design-orm/non-persistent-objects)

## More Examples

- [How to implement CRUD operations for Non-Persistent Objects stored remotely in eXpressApp Framework](https://github.com/DevExpress-Examples/XAF_Non-Persistent-Objects-Editing-Demo)
- [How to edit Non-Persistent Objects nested in a Persistent Object](https://github.com/DevExpress-Examples/XAF_Non-Persistent-Objects-Nested-In-Persistent-Objects-Demo)
- [How to: Display a List of Non-Persistent Objects](https://github.com/DevExpress-Examples/XAF_how-to-display-a-list-of-non-persistent-objects-e980)
- [How to filter and sort Non-Persistent Objects](https://github.com/DevExpress-Examples/XAF_Non-Persistent-Objects-Filtering-Demo)
- [How to refresh Non-Persistent Objects and reload nested Persistent Objects](https://github.com/DevExpress-Examples/XAF_Non-Persistent-Objects-Reloading-Demo)
- [How to edit a collection of Persistent Objects linked to a Non-Persistent Object](https://github.com/DevExpress-Examples/XAF_Non-Persistent-Objects-Edit-Linked-Persistent-Objects-Demo)
