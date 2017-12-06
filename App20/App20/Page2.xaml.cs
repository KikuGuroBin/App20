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

            /*
            SideBounds = new ViewBounds
            {
                X = width
            };

            Side.BindingContext = SideBounds;
            */

            Side2.LayoutTo(new Rectangle(width, 0, 300, 100), 0);
            Side.LayoutTo(new Rectangle(width, 0, 300, 100), 0);
        }

        private void OnClick(object sender, EventArgs args)
        {
            Main.Children.Add(new Image { Source = "Icon.png"}, new Rectangle(0, y, 30, 20));

            y += 20;
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