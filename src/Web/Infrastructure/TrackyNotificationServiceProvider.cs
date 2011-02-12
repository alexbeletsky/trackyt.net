using System.Web;
using Ninject;
using Ninject.Activation;
using Trackyt.Core.Services;

namespace Web.Infrastructure
{
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
