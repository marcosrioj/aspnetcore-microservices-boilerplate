using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace MainProduct.Controllers
{
    public abstract class MainProductControllerBase: AbpController
    {
        protected MainProductControllerBase()
        {
            LocalizationSourceName = SharedConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
