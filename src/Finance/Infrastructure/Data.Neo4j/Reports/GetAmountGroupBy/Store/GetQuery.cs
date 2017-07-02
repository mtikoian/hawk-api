namespace Finance.Infrastructure.Data.Neo4j.Reports.GetAmountGroupBy.Store
{
    using Finance.Infrastructure.Data.Neo4j.Reports.GetAmountGroupBy;
    using Finance.Infrastructure.Filter;

    using Http.Query.Filter;

    public class GetQuery : GetQueryBase
    {
        public GetQuery(
            Database database, 
            ItemMapping mapping, 
            GetScript file, 
            ILimit<int, Filter> limit, 
            ISkip<int, Filter> skip, 
            IWhere<string, Filter> where)
            : base(database, mapping, file, limit, skip, @where)
        {
        }

        protected override string GetQueryString()
        {
            return this.File.ReadAllText(@"GetAmountGroupBy.Store.Query.cql");
        }
    }
}