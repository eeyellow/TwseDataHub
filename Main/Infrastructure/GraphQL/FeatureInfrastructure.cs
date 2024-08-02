using GraphQL;
using GraphQL.Types;
using LC.Infrastructure._Base;
using System.Reflection;

namespace LC.Infrastructure.Graphql
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Graphql;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddGraphQL(x => x
                    .AddSchema<DataShareHub.Api.GraphQL.PublicSchema>()
                    .AddGraphTypes(Assembly.GetExecutingAssembly())
                    .AddSystemTextJson()
                );
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseGraphQL<ISchema>("/ui/graphql");
            app.UseGraphQLPlayground(
                "/graphqlui",                                // url to host Playground at
                new GraphQL.Server.Ui.Playground.PlaygroundOptions
                {
                    GraphQLEndPoint = "/ui/graphql",         // url of GraphQL endpoint
                    SubscriptionsEndPoint = "/ui/graphql",   // url of GraphQL endpoint
                });
        }
    }
}
