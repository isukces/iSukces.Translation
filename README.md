# iSukces.Translation

Provides interfaces and classes to build translations for .NET projects. Translation repository is based on key-value structure. 

* `ITranslationHolder` interface of object, that holds translated texts. It exposes `TryGetTranslation` method for getting text and `OnChangeTranslations` event. 

* `ILocalTextSource` interface is base text provider. It exposes `Value` property that holds current translation. Classes that implement `ILocalTextSource` are also 'INotifyPropertyChanged' so `Value` property can be bind in visual models i.e. WPF.

    * `LiteLocalTextSource`: simple class, connected to `ITranslationHolder` that exposes single translated text. It reacts for language changes.

    * `ParametricLocalTextSource`: Allows to construct text similar to `string.Format` where format is connected to translation sources. It updates `Value` property each time translation or parameter (if implements `INotifyPropertyChanged`) is changed.
  
    * `FormattedLocalTextSource`: Similar to `ParametricLocalTextSource` but not react to parameters changing. It also provides default translation used when `ITranslationHolder` is not connected or doesn't contains tranlation for given key.
 

### FormattedLocalTextSource sample
`var mySrc = new FormattedLocalTextSource("Open {0}", "App.Translation.Open", description);`

This creates `mySrc` object that excposes text calculated from `"Open {0}"` template and parameter from `description` variable. If translation holder is connected and it contains text for `"App.Translation.Open"` key then translation from holder is used instead of `"Open {0}"` template.    

### Sample binding 
Assume we have `AppTranslations` with static field `StrOk`. 

```c#
public static class AppTranslations {
  /// <summary>
  /// Text: O.K.
  /// </summary>
  public static readonly ILocalTextSource StrOk 
    = new LiteLocalTextSource("Common.Ok");
}
```
It's possible to bind `TextBlock.Text` property to `Value` property exposed by StrOk.  
```xaml
<TextBlock>
  <TextBlock.Text>
      <Binding Path="Value" Source="{x:Static app:AppTranslations.StrOk}" Mode="OneWay"/>
  </TextBlock.Text>
</TextBlock>

```

## Code signing

Nuget [iSukces.Translation](https://www.nuget.org/packages/iSukces.Translation/) contains signed assemblies, but signing key is not privided in source repository. You can generate your own key or rid off signing.