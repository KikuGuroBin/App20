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
	public partial class Page1 : ContentPage
	{
        private ViewBounds ShadowSize;
        private ViewBounds ShadowPosition;

        private int y = 200;

        private bool DialogShow;

        private bool First;

        private ImitationDialog Dialog;

		public Page1 ()
		{
			InitializeComponent ();

            // 初期設定の挙動を見せないために画面透過
            Opacity = 0;
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!First)
            {
                ShadowSize = new ViewBounds
                {
                    Width = width / 2 - 50,
                    Height = height / 2 - 50
                };

                ShadowPosition = new ViewBounds
                {
                    X = 0,
                    Y = height
                };

                // DialogShadow.BindingContext = ShadowPosition;
                // ImitationDialog.BindingContext = ShadowSize;

                // DialogShadow.LayoutTo(new Rectangle(0, height, width, height), 0);

                Shadow.LayoutTo(new Rectangle(0, height, width, height));
                Shadow.Opacity = 0;

                ImitationDialog.LayoutTo(new Rectangle(width / 2 - 50, height, 100, 100));
                ImitationDialog.Opacity = 0;

                System.Diagnostics.Debug.WriteLine("deg : " + height + ", ");

                var rc = CustomDialog.Bounds;
                rc.X = -1000;
                CustomDialog.LayoutTo(rc, 0);

                Dialog = new ImitationDialog
                {
                    Shadow = Shadow,
                    Dialog = CustomDialog,
                };

                Opacity = 1;
            }
        }

        private async void OnClick(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("deg : Height / 2 = " + Height / 2);

            // 表示フラグ反転
            DialogShow = !DialogShow;

            // 影の座標、サイズを取得し座標を変更し移動
            // var rc = Shadow.Bounds;
            // rc.Y = 0;
            // await Shadow.LayoutTo(rc, 0);

            // rc = ImitationDialog.Bounds;
            // rc.Y = Height / 2 - 50;
            // await ImitationDialog.LayoutTo(rc);

            // MovingView(ImitationDialog);

            // await DisplayAlert("a", "a", "a", "a");

            MainLayout.Children.Add(new Label { Text = "fuck meeee" }, () => new Rectangle(0, y, 40, 20));

            y += 20;
        }

        private async void ScrollToLabel(object sender, EventArgs args)
        {
            await Scroller.ScrollToAsync(Label1.Width, 0, true);
        }

        private async void ScrollToLabel2(object sender, EventArgs args)
        {
            await Scroller.ScrollToAsync(50, 0, true);
        }

        private void ScrollerTapped(object sender, EventArgs args)
        {

        }

        private void CustomDialogShow(object sender, EventArgs args)
        {
            //Dialog.ShowUp(Height / 2 + 50);

            y += 20;

            MainLayout.Children.Add(new Label { Text = "fuck meeeee"}, () => new Rectangle(0, y, 100, 20));
        }

        private void OnTap(object sender, EventArgs args)
        {
            // 表示フラグ反転
            // DialogShow = !DialogShow;

            // ダイアログの影の座標、サイズ取得し座標を変更し移動
            // var rc = Shadow.Bounds;
            // rc.Y = Height;
            // Shadow.LayoutTo(rc, 0);
            // Shadow.Opacity = 0;

            // ダイアログの座標、サイズ取得し座標を変更し移動
            // rc = ImitationDialog.Bounds;
            // rc.Y = Height;
            // ImitationDialog.LayoutTo(rc);

            Dialog.Hide();

            ImitationDialog.Opacity = 0;
        }

        /// <summary>
        /// ビューの移動アニメーションもどき
        /// </summary>
        /// <param name="view">
        /// アニメーションを適用するviewインスタンス
        /// </param>
        private async void MovingView(View view)
        {
            for (var cnt = 0; cnt < 5; cnt++)
            {
                System.Diagnostics.Debug.WriteLine("deg : " + view.Bounds.Y);

                view.Opacity += 0.2;

                Shadow.Opacity += 0.1;

                var rc = view.Bounds;
                rc.Y -= 10;
                await view.LayoutTo(rc, 100);
            }
        }

        private class ViewBounds
        {
            public double Width { get; set; }
            public double Height { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
        }
	}
}