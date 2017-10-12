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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Steeltoe.Common.Http
{
    public abstract class DiscoveryHttpClientHandlerBase : HttpClientHandler
    {
        protected static Random _random = new Random();

        public DiscoveryHttpClientHandlerBase()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var current = request.RequestUri;
            try
            {
                request.RequestUri = LookupService(current);
                return await base.SendAsync(request, cancellationToken);
            }
            finally
            {
                request.RequestUri = current;
            }

        }

        public abstract Uri LookupService(Uri current);

    }
}
