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

[assembly:ExportRenderer(typeof(MyBox), typeof(MyBoxRenderer))]
namespace App20.Droid
{
    class MyBoxRenderer : BoxRenderer
    {
        // 座標
        double CX;
        double CY;

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            Touch += OnTouch;
        }

        public void OnTouch(object sender, TouchEventArgs args)
        {
            if (args.Event.Action == MotionEventActions.Move)
            {
                System.Diagnostics.Debug.WriteLine("1" + CX + ", " + CY);
                // 移動した分の座標の差分計算
                var x = args.Event.GetX() - CX;
                var y = args.Event.GetY() - CY;

                CX = args.Event.GetX();
                CY = args.Event.GetY();

                // コールバック用インスタンス生成
                var callBack = new DrugEvent
                {
                    X = x * 2,
                    Y = y * 2,
                };

                // コールバック
                var view = Element as MyBox;
                view.Drug(view, callBack);
            }
            else if (args.Event.Action == MotionEventActions.Down)
            {
                System.Diagnostics.Debug.WriteLine("2");
                // 座標格納
                CX = args.Event.GetX();
                CY = args.Event.GetX();
            }
            else if(args.Event.Action == MotionEventActions.Up)
            {
                System.Diagnostics.Debug.WriteLine("3");
                // 座標初期化
                CX = -1;
                CY = -1;
            }
        }
    }
}