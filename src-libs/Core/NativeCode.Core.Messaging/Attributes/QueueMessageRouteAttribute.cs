namespace NativeCode.Core.Messaging.Attributes
{
    using System;
    using Extensions;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class QueueMessageRouteAttribute : Attribute
    {
        public QueueMessageRouteAttribute(Type type, string route = default, int priority = 0)
            : this(type.GetRouteName(route), priority)
        {
        }

        public QueueMessageRouteAttribute(string route, int priority = 0)
        {
            this.Priority = priority;
            this.Route = route;
        }

        /// <summary>
        ///     Gets or sets the priority for this route. Higher priority routes are executed
        ///     first. Priorities with the same value are ordered by route alphabetically.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        ///     Gets or sets the route key to identify the message delivery endpoint.
        /// </summary>
        public string Route { get; }
    }
}
