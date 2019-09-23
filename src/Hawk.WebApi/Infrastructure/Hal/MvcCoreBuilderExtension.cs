﻿namespace Hawk.WebApi.Infrastructure.Hal
{
    using System;
    using System.Linq;

    using Hawk.WebApi.Infrastructure.Hal.Resource;

    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using static System.Activator;
    using static System.AppDomain;

    internal static class MvcCoreBuilderExtension
    {
        private static readonly Type Type = typeof(IResourceBuilder);

        internal static IMvcCoreBuilder AddHal(this IMvcCoreBuilder @this, Action<JsonSerializerSettings> setupAction) =>
            @this
                .AddMvcOptions(options => options
                    .OutputFormatters
                    .Add(new HalJsonOutputFormatter(
                        CurrentDomain
                            .GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .Where(type => Type.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                            .Select(type => (IResourceBuilder)CreateInstance(type))
                            .SelectMany(item => item.Builders)
                            .ToDictionary(item => item.Key, item => item.Value),
                        setupAction)));
    }
}