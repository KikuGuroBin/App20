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
	public partial class Page2 : ContentPage
	{
        int y = 40;

        ViewBounds SideBounds;

		public Page2 ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            System.Diagnostics.Debug.WriteLine("deg : onSizeAllocated " + width + ", " + height);

            /*
            SideBounds = new ViewBounds
            {
                X = width
            };

            Side.BindingContext = SideBounds;
            */

            Side2.LayoutTo(new Rectangle(Width, 0, 300, 100), 300);
            Side.LayoutTo(new Rectangle(Width, 0, 300, 100), 300);
            Scroller.LayoutTo(new Rectangle(80, 0, 200, 100));
        }

        private void OnClick(object sender, EventArgs args)
        {
            if (sender.Equals(SideButton))
            {
                Main2.Children.Add(new Image { Source = "Icon.png" }, new Rectangle(0, y, 30, 20));

                y += 20;
            }
            else
            {
                Side2.LayoutTo(new Rectangle(Width - 50, 0, 100, 100));
            }
        }

        class ViewBounds
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }
	}
}