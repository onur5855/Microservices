using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceShared.Service
{
    public interface ISharedIdentityService
    {
        public string UserId { get; }
    }
}
