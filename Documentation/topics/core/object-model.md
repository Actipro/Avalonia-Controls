---
title: "Object Model"
page-title: "Object Model - Core Library Reference"
order: 10
---
# Object Model

Base classes are included for implementing observable and disposable objects.

## Observable Objects

The abstract [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) class implements both the `INotifyPropertyChanging` and `INotifyPropertyChanged` interfaces.  These interfaces allow objects inheriting the base class to publish property change notifications.

> [!NOTE]
> The `INotifyPropertyChanged` implementation is especially important since it allows plain CLR objects to be used as data binding sources.

### Setting Property Values

Classes inheriting [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) should invoke the protected [SetProperty](xref:ActiproSoftware.ObservableObjectBase.SetProperty*) method from property setters.  This method is passed:
- The backing field (by-reference) to update that holds the property value.
- The new value.
- The optional name of the property being updated, if different from the caller property.

The [OnPropertyChanging](xref:ActiproSoftware.ObservableObjectBase.OnPropertyChanging*) method is called immediately before a property's backing field is updated.  The default implementation of that method raises the [PropertyChanging](xref:ActiproSoftware.ObservableObjectBase.PropertyChanging) event.

Similarly, the [OnPropertyChanged](xref:ActiproSoftware.ObservableObjectBase.OnPropertyChanged*) method is called immediately after a property's backing field is updated.  The default implementation of that method raises the [PropertyChanged](xref:ActiproSoftware.ObservableObjectBase.PropertyChanged) event.

### Inheriting the Base Class

The following example shows how to create an observable object by inheriting the [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) class:

```csharp
public class ViewModel : ObservableObjectBase {

	private bool? _isActive;

	public bool? IsActive {
		get => _isActive;
		set => SetProperty(ref _isActive, value);
	}

}
```

## Disposable Objects

The abstract [DisposableObjectBase](xref:ActiproSoftware.DisposableObjectBase) class implements the `IDisposable` interface and the disposable object pattern.

The base class has a finalizer that calls a protected abstract [Dispose](xref:ActiproSoftware.DisposableObjectBase.Dispose*) method overload.  Its implementation of the `IDisposable` interface's public `Dispose` method also calls into the protected abstract `Dispose` method overload and suppresses finalization.

### Inheriting the Base Class

The following example shows how to create a disposable object by inheriting the [DisposableObjectBase](xref:ActiproSoftware.DisposableObjectBase) class and overriding the protected [Dispose](xref:ActiproSoftware.DisposableObjectBase.Dispose*) method:

```csharp
public class MyDisposableObject : DisposableObjectBase {

	protected override void Dispose(bool disposing) {
		if (disposing) {
			// Free any managed objects here
		}

		// Free any unmanaged objects here
	}

}
```
