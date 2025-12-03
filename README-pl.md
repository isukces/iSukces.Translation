# iSukces.Translation

[English README](README.md)

Interfejsy i klasy pomocnicze do budowania tłumaczeń kluczowych w .NET. Biblioteka ułatwia składanie źródeł tłumaczeń i bezpieczne bindowanie w UI.

## Instalacja
- NuGet: `dotnet add package iSukces.Translation`
- Wspierane platformy: `net6.0`, `net8.0`, `net9.0`, `net10.0`

## Główne elementy
- `TranslationKey` przechowuje klucz (jako nazwy oddzielone kropką), wspiera konkatenację (`+`) i ścieżki absolutne.
- `ITranslationHolder` udostępnia tłumaczenia przez `TryGetTranslation` i powiadamia zdarzeniem `OnChangeTranslations`.
  - `CascadeTranslationHolder` pozwala łączyć holdery (`BaseHolder`) i zasilać je metodą `AddFromType`.
- `ILocalTextSource` wystawia przetłumaczony `Value` i implementuje `INotifyPropertyChanged`, więc nadaje się do bindowania w UI.
  - `LiteLocalTextSource` wiąże pojedynczy klucz i reaguje na zmiany w holderze.
  - `LocalTextSource` trzyma tekst oryginalny i opcjonalną wskazówkę; gdy brak tłumaczenia, wyświetla oryginał.
  - `FormattedLocalTextSource` formatuje tekst z parametrami; przełącza się na przetłumaczony format, gdy ten jest dostępny.
  - `ParametricLocalTextSource` opakowuje inne źródło i ponownie uruchamia `string.Format`, gdy zmieniają się parametry lub tłumaczenia.
  - `CombinedLocalTextSource`, `JoinedLocalTextSource` oraz `LocalTextSourceHelper.MakeCommaSeparated` ułatwiają składanie wielu źródeł; 
  - `FakeTextSource` reprezentuje tekst statyczny/nietłumaczalny.
  - `StaticFieldLocalTextSource` wystawia statyczne pola string odnalezione przez refleksję.

## Praca z tłumaczeniami
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
holder.AddFromType(typeof(AppTexts)); // skanuje statyczne pola, tworzy klucze MyTexts.Master, MyTexts.Nested.Slave

var text = new LiteLocalTextSource("MyTexts.Master");
text.Attach(holder); // Value pozostaje w synchronizacji ze zmianami holdera
```

### Przykład bindowania (WPF)
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

### Dostosowywanie kluczy
- `[Translate]` na polach/klasach ustawia prefiks klucza; `[DoNotTranslate]` może pominąć lub oznaczyć string jako nietłumaczalny.
- Zdarzenia `TranslationUtils.MemberToTranslationKey` i `TypeToTranslationKey` pozwalają normalizować klucze (np. usunąć prefiks `Str`).

### Pomocnik gramatyczny
- `PolishGrammar.NounForm(number, form1, form2, form3)` zwraca poprawną formę rzeczownika dla podanej liczby.
```csharp
// patyk, patyki, patyków (formy dla 1, 2-4, wielu)
var text1 = PolishGrammar.NounForm(1,  "patyk",  "patyki", "patyków");  // patyk
var text2 = PolishGrammar.NounForm(3,  "patyk",  "patyki", "patyków");  // patyki
var text3 = PolishGrammar.NounForm(12, "patyk",  "patyki", "patyków");  // patyków
var text4 = PolishGrammar.NounForm(22, "patyk",  "patyki", "patyków");  // patyki
```

## Podpisywanie kodu
Paczka NuGet [iSukces.Translation](https://www.nuget.org/packages/iSukces.Translation/) zawiera podpisane asemblii; klucz nie jest dołączony do repozytorium. Lokalnie możesz wygenerować własny klucz albo wyłączyć podpisywanie.
