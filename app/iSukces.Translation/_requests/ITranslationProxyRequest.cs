using System;
using JetBrains.Annotations;

namespace iSukces.Translation
{
    public interface ITranslationProxyRequest : ITranslationRequest
    {
        string ProxyPropertyName { get; }

        [CanBeNull]
        Type ProxyType { get; }
    }
}