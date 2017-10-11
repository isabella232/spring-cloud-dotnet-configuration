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

using Steeltoe.Extensions.Configuration.CloudFoundry;
using Pivotal.Extensions.Configuration.ConfigServer;

namespace Pivotal.Extensions.Configuration
{
    /// <summary>
    /// Extension methods for adding <see cref="ConfigServerConfigurationProvider"/>.
    /// </summary>

    public static class ConfigServerConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddConfigServer(this IConfigurationBuilder configurationBuilder, string environment, ILoggerFactory logFactory = null) =>
            configurationBuilder.AddConfigServer(new ConfigServerClientSettings() { Environment = environment }, logFactory);

        public static IConfigurationBuilder AddConfigServer(this IConfigurationBuilder configurationBuilder, ConfigServerClientSettings defaultSettings, ILoggerFactory logFactory = null)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (defaultSettings == null)
            {
                throw new ArgumentNullException(nameof(defaultSettings));
            }

            configurationBuilder.Add(new CloudFoundryConfigurationSource());
            configurationBuilder.Add(new ConfigServerConfigurationProvider(defaultSettings, logFactory));
            return configurationBuilder;
        }
    }
}
