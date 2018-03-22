using WebViewJavascript.iOS.Render;
using Xamarin.Forms;
using UIKit;
using WebViewJavascript;
using System;
using System.Threading.Tasks;
using Foundation;


[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace WebViewJavascript.iOS.Render
{
	using UIKit;
	using Xamarin.Forms.Platform.iOS;

	public class WebViewRender : WebViewRenderer
	{

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null && e.NewElement != null)
                InitializeCommands((WebViewer)e.NewElement);


            //NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject("myuseragentgoeshere"), NSObject.FromObject("UserAgent"));
            //NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

            var webView = e.NewElement as WebViewer;

            if (webView != null)
            {
                /*
                var userAgent = this.Control.Settings.UserAgentString;
                if (!userAgent.Contains("coltrack_mobile"))
                {
                    userAgent += " coltrack_mobile";
                    // set useragent
                    this.Control.Settings.UserAgentString = userAgent;
                }*/

                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(this.EvaluateJavascript(js));
                };
            }
        }

		private void InitializeCommands(WebViewer element)
		{
            /*
			element.RefreshCommand = new Command(() =>
			{
				((UIWebView)NativeView).Reload();
			});

			element.GoBackCommand = new Command(() =>
			{
				var control = ((UIWebView)NativeView);
				if (control.CanGoBack)
				{
					element.IsBackNavigating = true;
					control.GoBack();
				}
			});*/

			element.CanGoBackFunction = () =>
			{
				return ((UIWebView)NativeView).CanGoBack;
			};

			var ctl = ((UIWebView)NativeView);

			ctl.ScalesPageToFit = true;

		}

	}

}
