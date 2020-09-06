// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;

namespace ids4.sts.Controllers
{
    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
        public string ButtonStyle { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class Providers {
        public List<ExternalProvider> ExternalProviders { get; set; }
    }
}