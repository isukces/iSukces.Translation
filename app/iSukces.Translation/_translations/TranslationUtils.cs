﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace iSukces.Translation;

public static class TranslationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER
    [return: NotNullIfNotNull(nameof(key))]
#endif
    private static TranslationKey? ApplyMemberToTranslationKey(TranslationKey? key, MemberInfo member)
    {
        var e = MemberToTranslationKey;
        if (e == null) return key;
        var args = new MemberToTranslationKeyArgs(key, member);
        e(args);
        if (args.Key is not null)
            key = args.Key;

        return key;
    }

    private static TranslationKey? ApplyTypeToTranslationKey(TranslationKey? key, Type type)
    {
        var e = TypeToTranslationKey;
        if (e == null) return key;
        var args = new TypeToTranslationKeyArgs(key, type);
        e(args);
        if (args.Key is not null)
            key = args.Key;

        return key;
    }

    public static TranslationKey? GetTranslationKeyFromMember(MemberInfo member)
    {
        var attributeKey = member.GetCustomAttribute<TranslateAttribute>()?.Key;
        if (!string.IsNullOrEmpty(attributeKey))
            return (TranslationKey?)attributeKey;
        var key = (TranslationKey?)member.Name;
        key = ApplyMemberToTranslationKey(key, member);
        return key;
    }

    public static TranslationKey? GetTranslationKeyFromType(Type? type)
    {
        if (type is null)
            return null;

        var key = type.GetCustomAttribute<TranslateAttribute>()?.Key;
        if (!string.IsNullOrEmpty(key))
            return ApplyTypeToTranslationKey((TranslationKey?)key, type);

        var declaringType = type.DeclaringType;
        if (declaringType is null)
            return ApplyTypeToTranslationKey(null, type);
        var parentClassKey = GetTranslationKeyFromType(declaringType);
        TranslationKey? translationKey;
        if (parentClassKey is null)
            translationKey = null;
        else
        {
            var typeName = (TranslationKey?)type.Name;
            translationKey = parentClassKey + typeName;
        }

        return ApplyTypeToTranslationKey(translationKey, type);
    }

    public static IEnumerable<StaticFieldLocalTextSource> GetTranslationsFromType(Type type, bool deepScan)
    {
        var typeKey = GetTranslationKeyFromType(type);
        if (typeKey is null) yield break;
        var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fieldInfos)
        {
            if (field.FieldType != typeof(string))
                continue;
            var isLocalizable         = true;
            var notTranslateAttribute = field.GetCustomAttribute<DoNotTranslateAttribute>();
            if (notTranslateAttribute is not null)
                switch (notTranslateAttribute.Reason)
                {
                    case DoNotTranslateReason.Ignore:
                        continue;
                    case DoNotTranslateReason.SameForAllLanguages:
                        isLocalizable = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            var memberKey = GetTranslationKeyFromMember(field);
            if (memberKey is null)
                continue;
            var key = typeKey + memberKey;
            if (key is not null) // should be always true
                yield return new StaticFieldLocalTextSource(key, field, isLocalizable);
        }

        if (!deepScan) yield break;
        var nestedTypes = type.GetNestedTypes();
        foreach (var nestedType in nestedTypes)
        foreach (var key in GetTranslationsFromType(nestedType, true))
            yield return key;
    }


    /// <summary>
    ///     Additional processing i.e. strip leading 'Str' from member name
    /// </summary>
    public static event MemberToTranslationKeyDelegate? MemberToTranslationKey;

    /// <summary>
    ///     Additional processing
    /// </summary>
    public static event TypeToTranslationKeyDelegate? TypeToTranslationKey;
}
