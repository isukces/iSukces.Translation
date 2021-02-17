using System;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public sealed partial class LocalTextSource : LocalTextSourceBase, IEquatable<LocalTextSource>
    {
        public LocalTextSource(string originalText, string key) : base(key)
        {
            if (string.IsNullOrEmpty(originalText))
                throw new ArgumentNullException(nameof(originalText));
            OriginalText = originalText;
        }

        public override string ToString()
        {
#if DEBUG
            return "?BAD BINDING? " + Value;
#else
            // awaryjnie zwracam wartość przetłumaczoną co się pojawi w WPF
            return Value;
#endif
        }

        protected override string GetCurrentTranslation()
        {
            if (!(TranslationHolder is null) && TranslationHolder.TryGetTranslation(OriginalKey, out var text))
                return text;
            return OriginalText;
        }

        [NotNull]
        public string OriginalText { get; }

        [CanBeNull]
        public string TranslationHint { get; set; }
    }
}