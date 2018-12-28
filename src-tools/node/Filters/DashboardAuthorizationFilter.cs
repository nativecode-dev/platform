namespace node.Filters
{
    using Hangfire.Dashboard;

    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var http = context.GetHttpContext();
            return http.User.Identity.IsAuthenticated;
        }
    }
}
