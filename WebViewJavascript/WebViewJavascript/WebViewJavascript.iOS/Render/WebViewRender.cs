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


            //NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject("coltrack_mobile"), NSObject.FromObject("UserAgent"));
            //NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

            var webView = e.NewElement as WebViewer;

            if (webView != null)
            {
                //var w = webView. e.NewElement as WKWebView;

                //string newUserAgent = await webView.EvaluateJavascript("navigator.userAgent");

                NSObject agent = NSUserDefaults.StandardUserDefaults["UserAgernt"];

                string newUserAgent = "";

                if (agent != null)
                {
                    newUserAgent = agent.ToString();
                }

                if (!newUserAgent.Contains("coltrack_mobile"))
                {
                    newUserAgent += " coltrack_mobile";

                    var dictionary = new NSDictionary("UserAgent", newUserAgent);
                    NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

                }

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
