using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using App20;
using App20.Droid;

[assembly : ExportRenderer(typeof(MyRelative), typeof(MyRelativeRenderer))]
namespace App20.Droid
{
    public class MyRelativeRenderer : ViewRenderer<MyRelative, Android.Views.View>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MyRelative> e)
        {
            base.OnElementChanged(e);
            
        }

        private void OnTouch(object sender, TouchEventArgs args)
        {

        }
    }
}