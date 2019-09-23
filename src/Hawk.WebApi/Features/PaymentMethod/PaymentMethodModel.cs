﻿namespace Hawk.WebApi.Features.PaymentMethod
{
    using System.Linq;

    using Hawk.Domain.PaymentMethod;
    using Hawk.Infrastructure;
    using Hawk.Infrastructure.Monad;
    using Hawk.WebApi.Infrastructure.ErrorHandling.TryModel;
    using Hawk.WebApi.Infrastructure.Pagination;

    using static Infrastructure.ErrorHandling.ErrorHandler;

    public sealed class PaymentMethodModel
    {
        public PaymentMethodModel(string name, uint total)
        {
            this.Name = name;
            this.Total = total;
        }

        public string Name { get; }

        public uint Total { get; }

        internal static TryModel<PageModel<TryModel<PaymentMethodModel>>> MapFrom(Page<Try<(PaymentMethod Method, uint Count)>> @this) => new PageModel<TryModel<PaymentMethodModel>>(
            @this
                .Data
                .Select(item => item.Match(
                    HandleError<PaymentMethodModel>,
                    paymentMethod => new PaymentMethodModel(paymentMethod.Method, paymentMethod.Count))),
            @this.Skip,
            @this.Limit);
    }
}