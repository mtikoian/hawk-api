﻿namespace Hawk.Domain.Entities.Payment
{
    using System;

    using Hawk.Infrastructure;

    public sealed class Currency : IEquatable<Currency>
    {
        public Currency(string name)
        {
            Guard.NotNullNorEmpty(name, nameof(name), "Currency's name cannot be null or empty.");

            this.Name = name;
        }

        public string Name { get; }

        public static implicit operator string(Currency method)
        {
            return method.Name;
        }

        public static implicit operator Currency(string name)
        {
            return new Currency(name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Currency currency && this.Equals(currency);
        }

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Name, other.Name);
        }

        public override int GetHashCode()
        {
            return this.Name != null ? this.Name.GetHashCode() : 0;
        }
    }
}