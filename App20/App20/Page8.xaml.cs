using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App20
{
    /*
     * インデックス
     *  フィールド
     *
     *  メソッド
     *  
     *  コンストラクタ
     *   index-Page8
     **/

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page8 : ContentPage
	{
        double i = 70;
        
        /* index-Page8 */
        public Page8 ()
		{
			InitializeComponent ();
            
            Initialize();
		}

        private void OnClick(object sender, EventArgs args)
        {
            var label = new Label
            {
                Text = "12345",
            };

            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                var l = s as Label;
                l.Text = count++.ToString();
            };

            label.GestureRecognizers.Add(tap);

            MainAdd(label, i += 50);
        }

        private void Drug(object sender, DrugEvent args)
        {
            var view = sender as View;


        }

        int count;

        private void Initialize()
        {
            var tap = new TapGestureRecognizer();
            tap.Tapped += async (s, e) =>
            {
                var view = s as View;

                var bounds = Main.Views[view];

                bounds.X += 50;
                bounds.Y += 50;

                var rc = view.Bounds;
                rc.X += 50;
                rc.Y += 50;
                await view.LayoutTo(rc);
            };

            var label1 = new Label
            {
                Text = "abcde",
            };

            var label2 = new Label
            {
                Text = "fghij",
            };

            var label3 = new Label
            {
                Text = "mnlop",
            };

            label1.GestureRecognizers.Add(tap);
            label2.GestureRecognizers.Add(tap);
            label3.GestureRecognizers.Add(tap);

            MainAdd(label1, i);
            MainAdd(label2, i += 50);
            MainAdd(label3, i += 50);
        }

        private void MainAdd(View view, double i)
        {
            var bounds = new ViewBounds
            {
                X = i,
                Y = i,
                Width = 50,
                Height = 50,
            };

            Main.Views.Add(view, bounds);

            Main.Children.Add(view,
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