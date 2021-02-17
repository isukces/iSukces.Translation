using System;

namespace iSukces.Translation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
    public sealed class TranslateAttribute : Attribute
    {
        public TranslateAttribute(string key = null)
        {
            Key = key;
        }

        public string Key      { get; }
        public string Language { get; set; }
    }


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