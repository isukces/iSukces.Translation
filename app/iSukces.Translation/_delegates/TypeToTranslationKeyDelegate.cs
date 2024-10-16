using System;

namespace iSukces.Translation;

public delegate void TypeToTranslationKeyDelegate(TypeToTranslationKeyArgs args);

public sealed class TypeToTranslationKeyArgs : ToTranslationKeyArgs<Type>
{
    public TypeToTranslationKeyArgs(TranslationKey? originalKey, Type source)
        : base(originalKey, source)
    {
    }
}
