﻿//
// Copyright 2017 the original author or authors.
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
//

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pivotal.Extensions.Configuration.ConfigServer;
using System.Reflection;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace Pivotal.Extensions.Configuration
{

    public static class ConfigServerConfigurationBuilderExtensionsAutofac
    {

        private const string DEFAULT_ENVIRONMENT = "Production";

        public static IConfigurationBuilder AddConfigServer(this IConfigurationBuilder configurationBuilder, ILoggerFactory logFactory = null)
        {
            return configurationBuilder.AddConfigServer(DEFAULT_ENVIRONMENT, Assembly.GetEntryAssembly()?.GetName().Name);
        }

        public static IConfigurationBuilder AddConfigServer(this IConfigurationBuilder configurationBuilder, string environment, ILoggerFactory logFactory = null)
        {

            return configurationBuilder.AddConfigServer(environment, Assembly.GetEntryAssembly()?.GetName().Name);
        }

        public static IConfigurationBuilder AddConfigServer(this IConfigurationBuilder configurationBuilder, string environment, string applicationName, ILoggerFactory logFactory = null)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new CloudFoundryConfigurationSource());

            var settings = new ConfigServerClientSettings()
            {
                Name = applicationName ?? Assembly.GetEntryAssembly()?.GetName().Name,
                Environment = environment ?? DEFAULT_ENVIRONMENT
            };

            configurationBuilder.Add(new ConfigServerConfigurationProvider(settings, logFactory));
            return configurationBuilder;
        }
    }
}
