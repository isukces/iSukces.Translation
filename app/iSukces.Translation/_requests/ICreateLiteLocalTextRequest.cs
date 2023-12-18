using System;

namespace iSukces.Translation;

public interface ICreateLiteLocalTextRequest : ITranslationTextSourceRequest
{
    string FieldName { get; }

    Type? FieldHostingType { get; }
}
