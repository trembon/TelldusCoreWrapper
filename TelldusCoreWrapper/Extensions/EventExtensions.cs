using System;
using System.Collections.Generic;
using System.Text;

namespace TelldusCoreWrapper.Extensions
{
    internal static class EventExtensions
    {
        internal static void Trigger(this EventHandler eventHandler, object sender, EventArgs eventArgs)
        {
            if (eventHandler != null)
            {
                eventHandler.Invoke(sender, eventArgs);
            }
        }

        internal static void Trigger<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler.Invoke(sender, eventArgs);
            }
        }
    }
}
