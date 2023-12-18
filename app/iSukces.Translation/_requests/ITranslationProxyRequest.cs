using System;

namespace iSukces.Translation;

public interface ITranslationProxyRequest : ITranslationRequest
{
    string ProxyPropertyName { get; }

    Type? ProxyType { get; }
}
