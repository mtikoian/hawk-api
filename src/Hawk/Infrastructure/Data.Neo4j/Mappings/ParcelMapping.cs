﻿namespace Hawk.Infrastructure.Data.Neo4j.Mappings
{
    using Hawk.Entities.Transaction.Installment;

    public class ParcelMapping
    {
        public Parcel MapFrom(Record record)
        {
            if (record == null || !record.Any())
            {
                return null;
            }

            return new Parcel(
                record.Get<int>("total"),
                record.Get<int>("number"));
        }
    }
}