using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using App20;
using App20.iOS;

[assembly:ExportRenderer(typeof(MyBox), typeof(MyBoxRenderer))]
namespace App20.iOS
{
    public class MyBoxRenderer : BoxRenderer
    {
    }
}