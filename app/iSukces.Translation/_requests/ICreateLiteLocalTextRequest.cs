using System;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public interface ICreateLiteLocalTextRequest : ITranslationTextSourceRequest
    {
        string FieldName { get; }

        [CanBeNull]
        Type FieldHostingType { get; }
    }
}