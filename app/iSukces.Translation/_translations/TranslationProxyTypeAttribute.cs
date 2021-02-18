using System;

namespace iSukces.Translation
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class TranslationProxyTypeAttribute : Attribute
    {
        public TranslationProxyTypeAttribute(Type proxyType)
        {
            ProxyType = proxyType;
        }

        public Type ProxyType { get; }
    }
}