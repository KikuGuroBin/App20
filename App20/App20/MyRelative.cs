using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App20
{
    public class MyRelative : RelativeLayout
    {
        public bool Druging;

        public EventHandler<DrugEvent> Touch;

        public Dictionary<View, ViewBounds> Views;

        public MyRelative()
        {
            Touch = OnTouch;

            ChildRemoved += Remove;

            Views = new Dictionary<View, ViewBounds>();
        }
        
        public void OnTouch(object sender, DrugEvent args)
        {
            switch (args.EventFlag)
            {
                /* タッチした時 */
                case 1:
                    Druging = true;
                    
                    break;

                /* ドラッグした時 */
                case 2:
                    break;

                /* 指を離した時 */
                case 3:
                    Druging = false;

                    break;
            }
        }

        private void Remove(object sender, EventArgs args)
        {
            var view = sender as View;

            Views.Remove(view);
        }

        private void ChildrenAdd(View view, double x = 0, double y = 0, double widthh = 0, double height = 0)
        {
            var bounds = new ViewBounds
            {
                X = x,
                Y = y,
                Width = 50,
                Height = 50,
            };

            Views.Add(view, bounds);

            this.Children.Add(view,
                Constraint.RelativeToParent((p) =>
                {
                    return bounds.X;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return bounds.Y;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return bounds.Width;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return bounds.Height;
                })
            );
        }
    }
}
