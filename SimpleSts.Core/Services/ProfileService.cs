using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace ids4.sts.Services
{
    /// <summary>
    /// Default profile service implementation.
    /// This implementation sources all claims from the current subject (e.g. the cookie).
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultProfileService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ProfileService(ILogger<ProfileService> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);
            context.AddRequestedClaims(context.Subject.Claims);
            context.LogIssuedClaims(Logger);

            return Task.CompletedTask;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}