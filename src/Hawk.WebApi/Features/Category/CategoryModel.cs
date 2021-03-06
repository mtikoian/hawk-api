﻿namespace Hawk.WebApi.Features.Category
{
    using Hawk.Domain.Category;

    public sealed class CategoryModel
    {
        private CategoryModel(Category entity)
        {
            this.Name = entity.Id;
            this.Transactions = entity.Transactions;
        }

        public string Name { get; }

        public uint Transactions { get; }

        internal static CategoryModel NewCategoryModel(in Category entity) => new CategoryModel(entity);
    }
}
