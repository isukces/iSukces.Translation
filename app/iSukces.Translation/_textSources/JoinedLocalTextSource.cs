namespace iSukces.Translation
{
    public sealed class JoinedLocalTextSource : AbstractCombinedTextSource
    {
        public JoinedLocalTextSource(string separator, ILocalTextSource[] list)
            : base(list)
        {
            _separator = separator;
        }

        protected override string GetCurrentValue(ILocalTextSource[] parameters)
        {
            var args = parameters.MapToArray(a => a.Value);
            return string.Join(_separator, args);
        }

        private readonly string _separator;
    }
}