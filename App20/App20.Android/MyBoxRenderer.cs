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
            if (args.Event.Action == MotionEventActions.Down)
            {
                // コールバック用インスタンス生成
                var callBack = new DrugEvent
                {
                };

                // コールバック
                var view = Element as MyBox;
                view.Drug(view, callBack);
            }
            else if (args.Event.Action == MotionEventActions.Move)
            {
                // 移動した分の座標の差分計算
                var x = args.Event.RawX - CX;
                var y = args.Event.RawY - CY;
                
                // コールバック用インスタンス生成
                var callBack = new DrugEvent
                {
                    X = x,
                    Y = y,
                };

                // コールバック
                var view = Element as MyBox;
                view.Drug(view, callBack);
            }
            else if(args.Event.Action == MotionEventActions.Up)
            {
                System.Diagnostics.Debug.WriteLine("3");
                // 座標初期化
                CX = -1;
                CY = -1;
            }

            CX = args.Event.RawX;
            CY = args.Event.RawY;
        }
    }
}