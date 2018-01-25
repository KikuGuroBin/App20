using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App20
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page7 : ContentPage
	{
        private LineCanvas Canvas = new LineCanvas();

		public Page7 ()
		{
			InitializeComponent ();

            Main.Touch += Touch;

            AddCanvas();
		}

        private void Touch(object sender, DrugEvent args)
        {
            MoveCanvas(args.X, args.Y);
        }

        private void AddCanvas()
        {
            Canvas.Padding = new Thickness(20, 20, 20, 20);

            Canvas.BackgroundColor = Color.Black;

            Main.Children.Add(Canvas,
                Constraint.RelativeToParent((p) =>
                {
                    return Canvas.X;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return Canvas.Y;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return Canvas.Width;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return Canvas.Height;
                })
            );

            Canvas.LayoutTo(new Rectangle(100, 100, 50, 50), 0);
        }

        private void AppendCanvas(View view)
        {
            Canvas.Children.Add(view,
                Constraint.RelativeToParent((p) =>
                {
                    return view.X;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return view.Y;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return view.Width;
                }),
                Constraint.RelativeToParent((p) =>
                {
                    return view.Height;
                })
            );

            view.LayoutTo(new Rectangle(50, 50, 100, 100), 0);

            var rc = Canvas.Bounds;

            /* Canvasの拡張 */
            if (rc.Width < view.X + view.Width)
            {
                rc.Width += view.X + view.Width;
            }

            if (rc.Height < view.Y + view.Height)
            {
                rc.Height += view.Y + view.Height;
            }
        }

        private async void MoveCanvas(double x, double y)
        {
            var rc = Canvas.Bounds;
            rc.X += x;
            rc.Y += y;
            await Canvas.LayoutTo(rc, 0);
        }
	}
}