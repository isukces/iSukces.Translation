using System;

namespace iSukces.Translation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class |
                    AttributeTargets.Struct)]
    public sealed class TranslateContextAttribute : Attribute
    {
        public TranslateContextAttribute(string contextKey)
        {
            ContextKey = contextKey;
        }

        public string ContextKey { get; }
    }
}