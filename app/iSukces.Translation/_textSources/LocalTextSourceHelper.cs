using System.Collections.Generic;
using System.Linq;

namespace iSukces.Translation;

public sealed class LocalTextSourceHelper
{
    public static ILocalTextSource MakeCommaSeparated(IReadOnlyList<ILocalTextSource> list)
    {
        if (list is null)
            return new FakeTextSource(string.Empty);
        var listCount = list.Count;
        switch (listCount)
        {
            case 0:
                return new FakeTextSource(string.Empty);
            case 1:
                return list[0];
            default:
                return new JoinedLocalTextSource(", ", list.ToArray());
        }
    }
}
