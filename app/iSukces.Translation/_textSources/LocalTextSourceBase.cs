using System;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public abstract class LocalTextSourceBase : TranslationNotifyPropertyChangedBase, ILocalTextSource
    {
        protected LocalTextSourceBase(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            OriginalKey = key;
        }

        public void Attach([NotNull] ITranslationHolder translationHolder)
        {
            if (!(TranslationHolder is null))
                TranslationHolder.OnChangeTranslations -= TranslationHolderOnOnChangeTranslations;
            TranslationHolder = translationHolder;
            Value              = GetCurrentTranslation();
            if (!(TranslationHolder is null))
                TranslationHolder.OnChangeTranslations += TranslationHolderOnOnChangeTranslations;
        }

        public override int GetHashCode()
        {
            return OriginalKey.GetHashCode();
        }

        protected abstract string GetCurrentTranslation();

        private void TranslationHolderOnOnChangeTranslations(object sender, EventArgs e)
        {
            Value = GetCurrentTranslation();
        }

        [NotNull]
        public string OriginalKey { get; }

        public bool IsLocalizable => true;

        /// <summary>
        ///     Bieżące tłumaczenie
        /// </summary>
        public string Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }

        protected ITranslationHolder TranslationHolder { get; private set; }
        private string _value;
    }
}