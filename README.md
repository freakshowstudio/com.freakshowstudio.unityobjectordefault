# Unity Object Or Default

The UnityObjectOrDefault extension method handles Unity's unique null 
semantics, providing a consistent way to check if objects are valid. 
Unity overloads the equality operators for UnityEngine.Object types, 
meaning destroyed objects can appear "null" in Unity's terms while 
still being non-null C# references. This extension method resolves 
that ambiguity.

This lets us avoid casting components to check for null, and 
allows for using null coalescing operators and other standard C# 
null-handling patterns.

### Examples

```csharp
var c = GetComponent<MyComponent>();
// We can use null coalescing operator here and have correct null checks
c.UnityObjectOrDefault()?.SomeMethod();
var a = c.UnityObjectOrDefault() ?? GetOtherThing();

// Works with interfaces too
IInterface i = GetThing(); // returns a MonoBehaviour that implements IInterface
i.UnityObjectOrDefault()?.SomeMethod(); 
var t = i.UnityObjectOrDefault() ?? GetOtherThing();
if (i.UnityObjectOrDefault() == null) { }
```
