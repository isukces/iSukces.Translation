using System.Reflection;

namespace iSukces.Translation
{
    public delegate void MemberToTranslationKeyDelegate(MemberToTranslationKeyArgs args);

    public sealed class MemberToTranslationKeyArgs : ToTranslationKeyArgs<MemberInfo>
    {
        public MemberToTranslationKeyArgs(TranslationKey originalKey, MemberInfo source)
            : base(originalKey, source)
        {
        }
    }
}