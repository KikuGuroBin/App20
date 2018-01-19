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
        LineCanvas.Line Line1;
        LineCanvas.Line Line2;
        LineCanvas.Line Line3;

        bool First;

        bool Showing;

		public Page5 ()
		{
			InitializeComponent ();

            Box1.Drug += OnDrug;
            Box2.Drug += OnDrug;

            // Side.LayoutTo(new Rectangle(-100, 0, 100, 100));

            Showing = true;

            /* 以下の2つのメソッドどちらかで線の引き方を選ぶ */
            Line1 = Canvas.Tail(Box1, Box2);
            Line2 = Canvas.Tail(Box2, Box4);
            Line3 = Canvas.Side(true, Box2, Box3);

            Line1.Draw();
            Line2.Draw();
            Line3.Draw();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            if (!Showing)
            {

            }

            if (First)
            {
                return;
            }

            First = true;
        }

        void OnDrug(object sender, DrugEvent args)
        {
            var view = sender as View;

            var lines = Canvas.SearchLines(view);

            foreach (var line in lines)
            {
                line.Draw();
            }
        }

        private void OnClick(object sender, EventArgs args)
        {
        }
    }
}