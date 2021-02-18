using System;

namespace iSukces.Translation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
    public sealed class DoNotTranslateAttribute : Attribute
    {
        public DoNotTranslateAttribute(DoNotTranslateReason reason = DoNotTranslateReason.Ignore)
        {
            Reason = reason;
        }

        public DoNotTranslateReason Reason { get; }
    }

    public enum DoNotTranslateReason
    {
        Ignore,
        SameForAllLanguages
    }
}