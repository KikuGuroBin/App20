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

using App20.Droid;
using App20;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(MyRelative), typeof(MyRelativeRenderer))]
namespace App20.Droid
{
    public class MyRelativeRenderer : ViewRenderer<MyRelative, Android.Views.View>
    {
        private double CurrentX;
        private double CurrentY;

        protected override void OnElementChanged(ElementChangedEventArgs<MyRelative> e)
        {
            base.OnElementChanged(e);

            Touch += OnTouch;
        }

        private void OnTouch(object sender, TouchEventArgs args)
        {
            /* イベント種別フラグ */
            var flag = -1;

            /* 呼び出し元へのコールバック用 */
            var callback = new DrugEvent
            {
                EventFlag = flag,
            };

            /* 現在の座標 */
            var nowX = args.Event.RawX;
            var nowY = args.Event.RawX;

            switch (args.Event.Action)
            {
                case MotionEventActions.Down:
                    flag = 1;
                    
                    callback.X = nowX;
                    callback.Y = nowY;

                    break;
                case MotionEventActions.Move:
                    flag = 2;

                    /* 差分を計算 */
                    var x = nowX - CurrentX;
                    var y = nowY - CurrentY;

                    callback.X = x;
                    callback.Y = y;

                    break;
                case MotionEventActions.Up:
                    flag = 3;

                    callback.X = nowX;
                    callback.Y = nowY;

                    break;
            }

            /* コールバック */
            var view = Element as MyRelative;
            view.OnTouch(view, callback);

            /* フィールド更新 */
            CurrentX = nowX;
            CurrentY = nowY;
        }
    }
}