namespace identity
{
    using System.Collections.Generic;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using NSwag;
    using NSwag.SwaggerGeneration.Processors;
    using NSwag.SwaggerGeneration.Processors.Contexts;

    public class UnauthorizedOperationProcessor : IOperationProcessor
    {
        public Task<bool> ProcessAsync(OperationProcessorContext context)
        {
            var anonymous = context.ControllerType.GetCustomAttribute<AllowAnonymousAttribute>()
                            ?? context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>();

            if (anonymous != null)
            {
                return Task.FromResult(true);
            }

            context.OperationDescription.Operation.Responses.Add(
                new KeyValuePair<string, SwaggerResponse>(
                    "401",
                    new SwaggerResponse { Description = HttpStatusCode.Unauthorized.ToString(), }));

            return Task.FromResult(true);
        }
    }
}
