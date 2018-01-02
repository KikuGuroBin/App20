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
	public partial class Page3 : ContentPage
	{
        bool Flag;

        bool First;

		public Page3 ()
		{
			InitializeComponent ();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            System.Diagnostics.Debug.WriteLine("deg : OnSizeAllocated");

            if (!First)
            {
                Label1.TranslateTo(50, 50, 1000);
                Label2.TranslateTo(100, 50);
                Label3.TranslateTo(200, 50);

                First = true;
            }
            /*
                         Label1.LayoutTo(new Rectangle(40, 0, 30, 30));
            Label2.LayoutTo(new Rectangle(80, 0, 30, 30));
            Label3.LayoutTo(new Rectangle(100, 0, 30, 30));
             */
        }

        private void OnClick(object sender, EventArgs args)
        {
            ShowPanel();
        }

        private void Click (object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("deg : Page3.Click");
        }

        private async void ShowPanel()
        {
            Flag = !Flag;

            if (Flag)
            {
                await this.LayoutTo(new Rectangle(10, 0, Width + 10, Height));
            }
            else
            {
                await this.LayoutTo(new Rectangle(-10, 0, Width - 10, Height));
            }
        }
    }
}