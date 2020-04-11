using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Shared;

namespace MainProduct
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MainProductAppServiceBase : ApplicationService
    {
        protected MainProductAppServiceBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }
    }
}
