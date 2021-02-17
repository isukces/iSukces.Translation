using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace iSukces.Translation
{
    public sealed class CombinedLocalTextSource : TranslationNotifyPropertyChangedBase, ILocalTextSource
    {
        public CombinedLocalTextSource(ILocalTextSource first, string second) :
            this(first, new FakeTextSource(second))
        {
        }

        public CombinedLocalTextSource(string first, ILocalTextSource second) :
            this(new FakeTextSource(first), second)
        {
        }

        public CombinedLocalTextSource(params ILocalTextSource[] items)
        {
            var sources = CombineItems(items, out var isLocalizable);
            IsLocalizable = isLocalizable;

            for (var index = sources.Count - 1; index >= 0; index--)
                if (sources[index] is INotifyPropertyChanged npc)
                    npc.PropertyChanged += NpcOnPropertyChanged;

            _items = sources;
            UpdateValue();
        }

        private static IReadOnlyList<ILocalTextSource> CombineItems(ILocalTextSource[] items, out bool isLocalizable)
        {
            isLocalizable = false;
            var sources = new List<ILocalTextSource>();

            void AddOrUpdate(ILocalTextSource source, ref bool isLocalizable1)
            {
                if (source.IsLocalizable)
                    isLocalizable1 = true;

                switch (source)
                {
                    case CombinedLocalTextSource combinedLocalTextSource:
                    {
                        foreach (var i in combinedLocalTextSource._items)
                            AddOrUpdate(i, ref isLocalizable1);
                        return;
                    }
                    case FakeTextSource fts:
                    {
                        if (sources.LastOrDefault() is FakeTextSource last)
                        {
                            sources[sources.Count - 1] = new FakeTextSource(last.Value + fts.Value);
                            return;
                        }

                        break;
                    }
                }

                sources.Add(source);
            }

            foreach (var source in items)
                AddOrUpdate(source, ref isLocalizable);

            return sources;
        }

        private void NpcOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Value))
                UpdateValue();
        }

        private void UpdateValue()
        {
            Value = string.Join("", _items.Select(a => a.Value));
        }

        public bool IsLocalizable { get; }

        public string Value
        {
            get => _value;
            set => SetAndNotify(ref _value, value);
        }

        private readonly IReadOnlyList<ILocalTextSource> _items;
        private string _value;
    }
}