using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public abstract class TranslationNotifyPropertyChangedBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler is null) return;
            var arg = new PropertyChangedEventArgs(propertyName);
            handler(this, arg);
        }

        protected void OnPropertyChanged([NotNull] PropertyChangedEventArgs args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));
            var handler = PropertyChanged;
            // ReSharper disable once UseNullPropagation
            if (handler is null) return;
            handler(this, args);
        }

        protected bool SetAndNotify<T>(ref T backField, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backField, value))
                return false;
            backField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}