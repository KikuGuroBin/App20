using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App20
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Label label = new Label
            {
                Text = "Hello Xamarin!!",
            };

            Label label2 = new Label
            {
                Text = "Hello Xamarin2!!",
            };

            // 追加方法１
            MainLayout.Children.Add(label, 
                Constraint.RelativeToParent(parent => (parent.Width / 2) - (label.Width / 2)), 
                Constraint.RelativeToParent(parent => (parent.Height / 2) - (label.Height / 2)),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.Constant(50));

            // 追加方法２
            Expression<Func<Rectangle>> ex = () => new Rectangle(200, 200, 50, 50);
            MainLayout.Children.Add(label2, ex);
            
            var box = new MyBox
            {
                Color = Color.Red,
                WidthRequest = 50,
                HeightRequest = 50,
            };

            MainLayout.Children.Add(box, () => new Rectangle(50, 50, 50, 50));
            
            var list = new List<int>
            {
                10, 33, 123, 2, 1, 30,
            };

            var result = from x in list
                         where x % 2 == 0
                         orderby x descending
                         select x;

            label.Text = "";

            foreach (var s in result.ToArray())
            {
                label.Text += s + ", ";
            }
		}
	}
}
