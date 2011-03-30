using System.Web;
using Ninject;
using Ninject.Activation;
using Trackyt.Core.Services;
using Trackyt.Core.Services.Impl;

namespace Web.Infrastructure
{
    // http://stackoverflow.com/questions/542875/how-do-i-check-if-debug-is-enabled-in-web-config

    public class TrackyNotificationServiceProvider : Provider<INotificationService> 
    {
        protected override INotificationService CreateInstance(IContext context)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return new NotificationServiceForDebug();
            }

            return new NotificationService(context.Kernel.Get<IEMailService>());
        }
    }
}
