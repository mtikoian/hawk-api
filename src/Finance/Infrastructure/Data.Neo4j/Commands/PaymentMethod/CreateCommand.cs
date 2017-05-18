namespace Finance.Infrastructure.Data.Neo4j.Commands.PaymentMethod
{
    using Finance.Entities.Transaction;

    using global::Neo4j.Driver.V1;

    public class CreateCommand
    {
        private readonly File file;

        public CreateCommand(File file)
        {
            this.file = file;
        }

        public virtual void Execute(Transaction entity, IStatementRunner trans)
        {
            if (string.IsNullOrWhiteSpace(entity.Payment.Method?.Name))
            {
                return;
            }

            var query = this.file.ReadAllText(@"PaymentMethod\Create.cql");
            var parameters = new
            {
                transaction = entity.Id,
                method = entity.Payment.Method.Name
            };

            trans.Run(query, parameters);
        }
    }
}