# Option Types

This package is made to implement an `Option<T>` type for use in Unity, based on the [rust option](https://doc.rust-lang.org/std/option/).

The package implements a lot of basic functions using an `out` parameter and turns them into Options, as well as a few common use cases where `Option<T>` may be useful in Unity.

I'm sure there are plenty I have missed and they will likely be added at some point, but for now this is the base that we have to work with.

If you plan to make a lot of use of the `Option<T>` varaints, it may be useful to include the following in your using statements in a Unity template script.

```csharp
using static SafedGames.Options.OptionUtilities;
```

