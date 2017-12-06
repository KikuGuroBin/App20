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

[assembly:ExportRenderer(typeof(MyImage), typeof(MyImageRenderer))]
namespace App20.Droid
{
    class MyImageRenderer : ImageRenderer
    {
        private double CurrentX;
        private double CurrentY;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            Touch += (sender, args) =>
            {
                switch (args.Event.Action)
                {
                    case MotionEventActions.Down:
                        CurrentX = args.Event.GetX();
                        CurrentY = args.Event.GetY();
                        break;
                    case MotionEventActions.Up:
                        break;
                    case MotionEventActions.Move:
                        var x = args.Event.GetX() - CurrentX;
                        var y = args.Event.GetY() - CurrentY;

                        var callBack = new DrugEvent
                        {
                            X = x,
                            Y = y,
                        };

                        break;
                }
            };
        }

    }
}