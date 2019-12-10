# FlaUI.Adapter.White
This project acts as an adapter between Teststack.White and FlaUI to make it a bit easier to migrate from White to FlaUI.

# Usage
To use FlaUI, you need to add `FlaUI.Core` and `FlaUI.UIA3` (or `FlaUI.UIA2`) and this adapter (`FlaUI.Adapter.White`) to your project. You should then remove all `White` libraries. Some things might give compile errors then, you can help improve this adapter with missing things or adopt the errors to the FlaUI concepts.

## Initialization
As FlaUI is a bit more generic, it needs some things to be initialized. So call the following initializer:
```csharp
UIA3Automation automation = new UIA3Automation();
// or UIA2Automation, depending on your needs
WhiteAdapter.Initialize(automation);
```
