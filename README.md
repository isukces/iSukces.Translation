# iSukces.Translation

[Polska wersja README](README-pl.md)

Interfaces and helpers for building key-based translations in .NET. The library focuses on simple composition of translation sources and safe data binding for UI frameworks.

## Installation
- NuGet: `dotnet add package iSukces.Translation`
- Targets: `net6.0`, `net8.0`, `net9.0`, `net10.0`

## Core building blocks
- `TranslationKey` stores a dot-separated key, supports concatenation (`+`) and absolute paths.
- `ITranslationHolder` provides translations via `TryGetTranslation` and notifies listeners with `OnChangeTranslations`.
  - `CascadeTranslationHolder` lets you stack holders (`BaseHolder`) and populate them with `AddFromType`.
- `ILocalTextSource` exposes a translated `Value` and implements `INotifyPropertyChanged` for UI binding.
  - `LiteLocalTextSource` binds a single key and reacts to holder changes.
  - `LocalTextSource` keeps an original text and optional hint; shows the original when a translation is missing.
  - `FormattedLocalTextSource` formats text using parameters; switches to the translated format when available.
  - `ParametricLocalTextSource` wraps another source and re-runs `string.Format` when parameters or translations change.
  - `CombinedLocalTextSource`, `JoinedLocalTextSource` and `LocalTextSourceHelper.MakeCommaSeparated` help compose multiple sources; `FakeTextSource` represents static/non-localizable text.

## Working with translations
```csharp
[Translate("MyTexts")]
public class AppTexts
{
    public static string Master = "Master text";

    public class Nested
    {
        public static string Slave = "Slave text";
        [DoNotTranslate(DoNotTranslateReason.Ignore)] public static string Skip = "Not exported";
    }
}

var holder = new CascadeTranslationHolder();
holder.AddFromType(typeof(AppTexts)); // scans static string fields, builds keys MyTexts.Master, MyTexts.Nested.Slave

var text = new LiteLocalTextSource("MyTexts.Master");
text.Attach(holder); // keeps Value in sync with holder changes
```

### Binding example (WPF)
```csharp
public static class AppTranslations
{
    public static readonly ILocalTextSource StrOk = new LiteLocalTextSource("Common.Ok");
}
```
```xaml
<TextBlock>
  <TextBlock.Text>
    <Binding Path="Value" Source="{x:Static app:AppTranslations.StrOk}" />
  </TextBlock.Text>
</TextBlock>
```

### Customizing keys
- `[Translate]` on fields/classes sets the key prefix; `[DoNotTranslate]` can skip or mark strings as non-localizable.
- `TranslationUtils.MemberToTranslationKey` and `TypeToTranslationKey` events let you normalize keys (e.g., strip `Str` prefix).

### Grammar helpers
- `PolishGrammar.NounForm(number, form1, form2, form3)` returns the correct Polish noun form for the given count.
```csharp
// patyk, patyki, patyków (forms for 1, 2-4, many)
var text1 = PolishGrammar.NounForm(1,  "patyk",  "patyki", "patyków");  // patyk
var text2 = PolishGrammar.NounForm(3,  "patyk",  "patyki", "patyków");  // patyki
var text3 = PolishGrammar.NounForm(12, "patyk",  "patyki", "patyków");  // patyków
var text4 = PolishGrammar.NounForm(22, "patyk",  "patyki", "patyków");  // patyki
```

## Code signing
NuGet package [iSukces.Translation](https://www.nuget.org/packages/iSukces.Translation/) ships signed assemblies; the signing key is not included in this repository. Generate your own key or disable signing if you build locally.
