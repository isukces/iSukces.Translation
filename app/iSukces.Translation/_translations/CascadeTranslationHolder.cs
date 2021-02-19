using System;
using System.Collections.Generic;
using System.Linq;

namespace iSukces.Translation
{
    public class CascadeTranslationHolder : BaseCascadeTranslationHolder
    {
        public Dictionary<string, ILocalTextSource> Translations { get; } = new Dictionary<string, ILocalTextSource>();

        public void AddFromType(Type type)
        {
            var translations = TranslationUtils.GetTranslationsFromType(type, true).ToArray();
            foreach (var i in translations)
                Translations[i.Key.Path] = i;
        }
        
   
        protected override bool TryGetTranslationInternal(string fullTransaltionKey, out string translation)
        {
            if (!Translations.TryGetValue(fullTransaltionKey, out var provider))
                return base.TryGetTranslationInternal(fullTransaltionKey, out translation);
            translation = provider.Value;
            return true;

        }
 
 
    }
}