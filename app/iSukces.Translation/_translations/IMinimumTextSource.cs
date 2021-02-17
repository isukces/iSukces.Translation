namespace iSukces.Translation
{
    public interface IMinimumTextSource
    {
        string Text     { get; }
        string Language { get; }
        string Key      { get; }
        string Hint     { get; }
    }
}