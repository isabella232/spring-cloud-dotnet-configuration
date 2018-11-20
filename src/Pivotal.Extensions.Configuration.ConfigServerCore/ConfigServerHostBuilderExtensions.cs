﻿// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ST = Steeltoe.Extensions.Configuration.ConfigServer;

namespace Pivotal.Extensions.Configuration.ConfigServer
{
    [Obsolete("Use the Steeltoe.Extension.Configuration packages!")]
    public static class ConfigServerHostBuilderExtensions
    {
        /// <summary>
        /// Enable the application to listen on port(s) provided by the environment at runtime
        /// </summary>
        /// <param name="webHostBuilder">Your WebHostBuilder</param>
        /// <param name="runLocalPort">Set the port number with code so you don't need to set environment variables locally</param>
        /// <returns>Your WebHostBuilder, now listening on port(s) found in the environment or passed in</returns>
        /// <remarks>runLocalPort parameter will not be used if an environment variable PORT is found</remarks>
        public static IWebHostBuilder UseCloudFoundryHosting(this IWebHostBuilder webHostBuilder, int? runLocalPort = null)
        {
            return ST.ConfigServerHostBuilderExtensions.UseCloudFoundryHosting(webHostBuilder, runLocalPort);
        }

        /// <summary>
        /// Add Config Server and Cloud Foundry as application configuration sources
        /// </summary>
        /// <param name="hostBuilder"><see cref="IWebHostBuilder"/></param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
        /// <returns><see cref="IWebHostBuilder"/> with config server and Cloud Foundry Config Provider attached</returns>
        public static IWebHostBuilder AddConfigServer(this IWebHostBuilder hostBuilder, ILoggerFactory loggerFactory = null)
        {
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddConfigServer(context.HostingEnvironment, loggerFactory);
            });

            return hostBuilder;
        }
    }
}
