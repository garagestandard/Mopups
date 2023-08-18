using Microsoft.Maui.Handlers;
using Mopups.Pages;
using UIKit;

namespace Mopups.Platforms.iOS
{
    public class PopupPageHandler : PageHandler
    {
        private readonly UIGestureRecognizer _tapGestureRecognizer;

        public PopupPageHandler()
        {
            this.SetMauiContext(MauiUIApplicationDelegate.Current.Application.Windows[0].Handler.MauiContext); //Still a hack?

            _tapGestureRecognizer = new UITapGestureRecognizer(OnTap)
            {
                CancelsTouchesInView = false
            };
        }

        private void OnTap(UITapGestureRecognizer e)
        {
            var view = e.View;
            var location = e.LocationInView(view);
            var subview = view.HitTest(location, null);

            if (Equals(subview, view))
            {
                ((PopupPage)VirtualView).SendBackgroundClick();
            }
        }

        protected override Microsoft.Maui.Platform.ContentView CreatePlatformView()
        {
            return base.CreatePlatformView();
        }

        protected override void ConnectHandler(Microsoft.Maui.Platform.ContentView nativeView)
        {
            base.ConnectHandler(nativeView);
            nativeView?.AddGestureRecognizer(_tapGestureRecognizer);
        }

        protected override void DisconnectHandler(Microsoft.Maui.Platform.ContentView nativeView)
        {
            nativeView?.RemoveGestureRecognizer(_tapGestureRecognizer);
            base.DisconnectHandler(nativeView);
        }
    }
}
