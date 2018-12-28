namespace NativeCode.Core.Messaging.Extensions
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Humanizer;

    using NativeCode.Core.Messaging.Envelopes;

    public static class QueueExtensions
    {
        public static string GetExchangeName<T>(this IQueueTopic<T> topic)
            where T : IQueueMessage
        {
            return typeof(T).GetExchangeName();
        }

        public static string GetExchangeName(this Type type)
        {
            return ResolveEnvelopeType(type)
                .Name.Underscore()
                .Dasherize()
                .ToLower(CultureInfo.CurrentCulture);
        }

        public static string GetQueueName<T>(this IQueueTopic<T> topic, string identifier = default)
            where T : IQueueMessage
        {
            return typeof(T).GetQueueName(identifier);
        }

        public static string GetQueueName(this Type type, string identifier = default)
        {
            var name = ResolveEnvelopeType(type)
                .Name.Underscore()
                .Dasherize()
                .ToLower(CultureInfo.CurrentCulture);

            if (string.IsNullOrWhiteSpace(identifier))
            {
                return name;
            }

            return $"{name}:{identifier}";
        }

        public static string GetRouteName<T>(this IQueueTopic<T> topic, string route = default)
            where T : IQueueMessage
        {
            return typeof(T).GetRouteName(route);
        }

        public static string GetRouteName(this Type type, string route = default)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return type.GetQueueName();
            }

            return $"{type.GetQueueName()}@{route}".ToLower(CultureInfo.CurrentCulture);
        }

        private static Type ResolveEnvelopeType(Type type)
        {
            if (type.IsConstructedGenericType == false)
            {
                return type;
            }

            var isRequestEnvelope = type.GetGenericTypeDefinition()
                .IsAssignableFrom(typeof(RequestEnvelope<>));
            var isResponseEnvelope = type.GetGenericTypeDefinition()
                .IsAssignableFrom(typeof(ResponseEnvelope<>));

            if (isRequestEnvelope || isResponseEnvelope)
            {
                return type.GetGenericArguments()
                    .First();
            }

            return type;
        }
    }
}
