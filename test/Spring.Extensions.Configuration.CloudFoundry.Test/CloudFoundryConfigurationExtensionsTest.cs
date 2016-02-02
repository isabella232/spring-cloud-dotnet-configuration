﻿//
// Copyright 2015 the original author or authors.
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

using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spring.Extensions.Configuration.CloudFoundry.Test
{
    public class CloudFoundryConfigurationExtensionsTest
    {
        [Fact]
        public void AddCloudFoundry_ThrowsIfConfigBuilderNull()
        {
            // Arrange
            IConfigurationBuilder configurationBuilder = null;

            // Act and Assert
            var ex = Assert.Throws<ArgumentNullException>(() => CloudFoundryConfigurationExtensions.AddCloudFoundry(configurationBuilder));
            Assert.Contains(nameof(configurationBuilder), ex.Message);

        }

        [Fact]
        public void AddCloudFoundry_AddsConfigServerProviderToProvidersList()
        {
            // Arrange
            var configurationBuilder = new ConfigurationBuilder();

            // Act and Assert
            configurationBuilder.AddCloudFoundry();

            CloudFoundryConfigurationProvider cloudProvider = null;
            foreach (IConfigurationProvider provider in configurationBuilder.Providers)
            {
                cloudProvider = provider as CloudFoundryConfigurationProvider;
                if (cloudProvider != null)
                    break;
            }
            Assert.NotNull(cloudProvider);

        }

    }
}