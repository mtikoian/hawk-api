﻿namespace Hawk.WebApi.Features.Transaction
{
    using System.Collections.Generic;
    using System.Linq;

    using Hawk.Domain.Transaction;
    using Hawk.Infrastructure.Monad;
    using Hawk.Infrastructure.Pagination;

    using static Hawk.Infrastructure.ErrorHandling.ExceptionHandler;

    public sealed class TransactionModel
    {
        public TransactionModel(Transaction entity)
            : this(
                entity.Id.ToString(),
                entity.Type,
                entity.Payment,
                entity.Store,
                entity.Tags.Select(tag => tag.Value))
        {
        }

        public TransactionModel(
            string id,
            string type,
            PaymentModel payment,
            string store,
            IEnumerable<string> tags)
        {
            this.Id = id;
            this.Type = type;
            this.Payment = payment;
            this.Store = store;
            this.Tags = tags;
        }

        public string Id { get; }

        public string Type { get; }

        public PaymentModel Payment { get; }

        public string Store { get; }

        public IEnumerable<string> Tags { get; }

        internal static Try<Page<Try<TransactionModel>>> MapTransaction(Page<Try<Transaction>> @this) => new Page<Try<TransactionModel>>(
            @this
                .Data
                .Select(item => item.Match(
                    HandleException<TransactionModel>,
                    transaction => new TransactionModel(transaction))),
            @this.Skip,
            @this.Limit);
    }
}
