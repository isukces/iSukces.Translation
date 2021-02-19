using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace iSukces.Translation
{
    public static class TranslationUtils
    {
        public static TranslationKey GetTranslationKeyFromMember(MemberInfo member)
        {
            var attributeKey = member.GetCustomAttribute<TranslateAttribute>()?.Key;
            if (!string.IsNullOrEmpty(attributeKey))
                return (TranslationKey)attributeKey;
            var key = (TranslationKey)member.Name;
            key = ApplyMemberToTranslationKey(key, member);
            return key;
        }

        public static TranslationKey GetTranslationKeyFromType(Type type)
        {
            if (type is null)
                return null;

            var key = type.GetCustomAttribute<TranslateAttribute>()?.Key;
            if (!string.IsNullOrEmpty(key))
                return ApplyTypeToTranslationKey((TranslationKey)key, type);

            var declaringType = type.DeclaringType;
            if (declaringType is null)
                return ApplyTypeToTranslationKey(null, type);
            var parentClassKey = GetTranslationKeyFromType(declaringType);
            var translationKey = parentClassKey is null
                ? null
                : parentClassKey + (TranslationKey)type.Name;
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
                if (notTranslateAttribute != null)
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
                yield return new StaticFieldLocalTextSource(key, field, isLocalizable);
            }

            if (!deepScan) yield break;
            var nestedTypes = type.GetNestedTypes();
            foreach (var nestedType in nestedTypes)
            foreach (var key in GetTranslationsFromType(nestedType, true))
                yield return key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TranslationKey ApplyMemberToTranslationKey(TranslationKey key, MemberInfo member)
        {
            var e = MemberToTranslationKey;
            if (e == null) return key;
            var args = new MemberToTranslationKeyArgs(key, member);
            e(args);
            if (args.Key != null)
                key = args.Key;

            return key;
        }

        private static TranslationKey ApplyTypeToTranslationKey(TranslationKey key, Type type)
        {
            var e = TypeToTranslationKey;
            if (e == null) return key;
            var args = new TypeToTranslationKeyArgs(key, type);
            e(args);
            if (args.Key != null)
                key = args.Key;

            return key;
        }


        /// <summary>
        ///     Additional processing i.e. strip leading 'Str' from member name
        /// </summary>
        public static event MemberToTranslationKeyDelegate MemberToTranslationKey;

        /// <summary>
        ///     Additional processing  
        /// </summary>
        public static event TypeToTranslationKeyDelegate TypeToTranslationKey;
    }
}