using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Threading;

namespace YANFOE.Factories.UI
{
    public static class YANFOEApplication
    {
        // Use of this class is bad, it should be removed as soon as possible.
        // TODO: Get rid of this class and its methods.
        //
        //

        //[SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        public static object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;

            return null;
        }
    }
}
