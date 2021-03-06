﻿namespace Hawk.Domain.Configuration.Data.Neo4J.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Hawk.Domain.Configuration;
    using Hawk.Domain.Configuration.Queries;
    using Hawk.Domain.Shared.Queries;
    using Hawk.Infrastructure.Data.Neo4J;
    using Hawk.Infrastructure.Filter;
    using Hawk.Infrastructure.Monad;
    using Hawk.Infrastructure.Pagination;

    using Http.Query.Filter;

    using static System.IO.Path;

    using static Hawk.Domain.Configuration.Data.Neo4J.ConfigurationMapping;
    using static Hawk.Infrastructure.Data.Neo4J.CypherScript;

    internal sealed class GetConfigurations : Query<GetAllParam, Page<Try<Configuration>>>, IGetConfigurations
    {
        private static readonly Option<string> StatementOption = ReadCypherScript(Combine("Configuration", "Data.Neo4J", "Queries", "GetConfigurations.cql"));
        private readonly Neo4JConnection connection;
        private readonly ILimit<int, Filter> limit;
        private readonly ISkip<int, Filter> skip;

        public GetConfigurations(
            Neo4JConnection connection,
            ILimit<int, Filter> limit,
            ISkip<int, Filter> skip)
        {
            this.connection = connection;
            this.limit = limit;
            this.skip = skip;
        }

        protected override async Task<Try<Page<Try<Configuration>>>> GetResult(GetAllParam param)
        {
            var parameters = new
            {
                email = param.Email.Value,
                skip = this.skip.Apply(param.Filter),
                limit = this.limit.Apply(param.Filter),
            };

            var data = await this.connection.ExecuteCypher(
                record => MapConfiguration(record),
                StatementOption,
                parameters);

            return data.Select(items => new Page<Try<Configuration>>(items, parameters.skip, parameters.limit));
        }
    }
}
