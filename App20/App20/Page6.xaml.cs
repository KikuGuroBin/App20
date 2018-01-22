using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App20
{
    /* 
     * 目的 
     *  レイアウト内にViewを追加したり、Viewを削除したり、Viewの座標を変更したときに発生する、
     *  レイアウトの初期化に対応するプログラムを探す。
     *  
     * 結果
     *  RelativeLayoutを使用し、Viewへの制約を設ければ初期化しないことが判明。
     */
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page6 : ContentPage
	{
        int[] bounds = new int[10];

        List<int> xs = new List<int>();
        List<int> ys = new List<int>();

        /* レイアウト内の子Viewの */
        Dictionary<View, ViewBounds> dic = new Dictionary<View, ViewBounds>();

        List<View> views = new List<View>();

		public Page6 ()
		{
			InitializeComponent ();

            for (var i = 0; i < 5; i++)
            {
                var label = new Label { Text = i.ToString() };

                var bound = new ViewBounds
                {
                    x = i * 10,
                    y = i * 10,
                };

                dic.Add(label, bound);

                 Main.Children.Add(label,
                      Constraint.RelativeToParent((p) =>
                      {
                          return bound.x;
                      }),
                      Constraint.RelativeToParent((p) =>
                      {
                          return bound.y;
                      }),
                      Constraint.RelativeToParent((p) =>
                      {
                          return 50;
                      }),
                      Constraint.RelativeToParent((p) =>
                      {
                          return 50;
                      })
                    //() => new Rectangle(xs[xs.Count - 1], ys[ys.Count - 1], 50, 50)
                );

                views.Add(label);
            }

            Main.Children.Add(new Label { Text = "aaaaaa" }, () => new Rectangle(300, 300, 50, 50));
		}

        int count;

        async void OnClick(object sender, EventArgs args)
        {
            /*
            var label = views[count % 5] as Label;
    
            var rc = label.Bounds;
            rc.X += 50;
            rc.Y += 50;
            await label.LayoutTo(rc, 250, Easing.CubicIn);

            xs[count % 5] += 50;
            ys[count % 5] += 50;

            count++;
            */

            var keys = dic.Keys.ToList();

            var view = keys[count++ % 5] as Label;

            dic[view].x += 50;
            dic[view].y += 50;

            var rc = view.Bounds;
            rc.X += 50;
            rc.Y += 50;
            await view.LayoutTo(rc, 250);

        }

        int y;

        void OnClick2(object sender, EventArgs args)
        {
            y += 40;

            Main.Children.Add(new Label { Text = "aaaaaa" }, () => new Rectangle(300, y, 50, 50));
        }

        class ViewBounds
        {
            public double x;
            public double y;
        }
	}
}