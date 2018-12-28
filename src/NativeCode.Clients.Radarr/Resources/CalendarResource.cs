namespace NativeCode.Clients.Radarr.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Clients.Radarr.Requests;
    using NativeCode.Clients.Radarr.Responses;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Serialization;

    using RestSharp;

    public class CalendarResource : ResourceBase, IResourceQuery<QueryCalendar, Movie>
    {
        public CalendarResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<Movie>> Find(QueryCalendar request)
        {
            var builder = new UriBuilder(this.Client.BaseUrl) { Path = "calendar" };

            if (request.End.HasValue)
            {
                builder.AddQueryParam("end", request.End.ToString());
            }

            if (request.Start.HasValue)
            {
                builder.AddQueryParam("start", request.Start.ToString());
            }

            return this.GetCollection<Movie>(builder.Uri.PathAndQuery);
        }
    }
}
