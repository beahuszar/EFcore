﻿using Fasetto.Word.Web.Server.Data;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// A shorthand access class to get DI services with nice clean short code
/// </summary>
namespace Fasetto.Word.Web.Server
{
    public static class IoC
    {
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static ApplicationDbContext ApplicationDbContext => IoCContainer.Provider.GetService<ApplicationDbContext>();
    }

    /// <summary>
    /// The dependency injection container making use of the built in .Net Core service provider
    /// </summary>
    public static class IoCContainer
    {
        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static ServiceProvider Provider { get; set; }
    }
}
