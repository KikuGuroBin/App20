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
	public partial class Page5 : ContentPage
	{
        LineCanvas.Line Line;

        bool First;

		public Page5 ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            Canvas.WidthRequest = width;
            Canvas.HeightRequest = height;

            Main.WidthRequest = width;
            Main.HeightRequest = height;

            if (First)
            {
                return;
            }

            First = true;

            Line = Canvas.Side();

            //Line = Canvas.Tail();
        }

        private void OnClick(object sender, EventArgs args)
        {
            //Line.DrawTail(Box1, Box2);

            Line.DrawSide(Box1, Box2, true);
        }
    }
}