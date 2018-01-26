using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App20
{
    /* 
     * インデックス
     *  フィールド
     *   index.Canvas
     *  
     *  メソッド
     *   index.Touch
     *   index.OnClick
     *   index.InitializeCanvas
     *   index.CanvasAppend
     *   index.MoveCanvas
     *  
     *  コンストラクタ
     *   index.Page7
     *  
     */

    /// <summary>
    /// 
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page7 : ContentPage
	{
        private LineCanvas Canvas;

        double x;
        double y;

        /* index.Page7 */
        public Page7 ()
		{
			InitializeComponent ();

            InitializeCanvas();

            Main.Touch += Touch;
		}

        /* index.Touch */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Touch(object sender, DrugEvent args)
        {
           // MoveCanvas(args.X, args.Y);
        }

        /* index.OnClick */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnClick(object sender, EventArgs args)
        {
            var label = new Label
            {
                Text = "abcde",
            };

            if ((x / 10) % 2 == 0)
            {
            //    CanvasAppend(label, x, 0, 50, 50);
            }
            else
            {
             //   CanvasAppend(label, 0, x, 50, 50);
            }

            var box = new MyBox
            {
                BackgroundColor = Color.Black,
            };

            CanvasAppend(box, 40, 40, 50, 50);

            x += 50;
            y += 50;
        }

        int ss;

        private void OnClick2(object sender, EventArgs args)
        {
            Main.Children.Add(new Label { Text = "12345"}, () => new Rectangle(200, ss, 50, 50));

            ss += 50;
        }
        
        /* index.InitializeCanvas */
        /// <summary>
        /// フィールドCanvasの初期化
        /// </summary>
        private void InitializeCanvas()
        {
            Canvas = new LineCanvas()
            {
                BackgroundColor = Color.Lime,
            };

            /* 制約付きでフィールドMainにCanvas追加 */
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

            var rc = Canvas.Bounds;
            rc.X += 20;
            rc.Y += 60;
            rc.Width += 100;
            rc.Height += 100;
            Canvas.LayoutTo(rc, 0);
        }

        /* index.CanvasAppend */
        /// <summary>
        /// フィールドCanvasへのViewの追加。
        /// </summary>
        /// <param name="view">Canvasに追加するViewインスタンス。</param>
        /// <param name="x">ViewインスタンスのX座標。</param>
        /// <param name="y">ViewインスタンスのY座標。</param>
        /// <param name="width">Viewインスタンスの幅。</param>
        /// <param name="height">Viewインスタンスの高さ。</param>
        private void CanvasAppend(View view, double x = 0, double y = 0, double width = 0, double height = 0)
        {
            /* 制約付きでViewを追加 */
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

            /* 引数で指定した座標、サイズにする */
            var rc = view.Bounds;
            rc.X = x;
            rc.Y = y;
            rc.Width = width;
            rc.Height = height;
            view.LayoutTo(rc, 0);

            rc = Canvas.Bounds;

            /* Canvasの拡張 */
            if (rc.Width < x + width)
            {
                rc.Width = x + width;
            }

            if (rc.Height < y + height)
            {
                rc.Height = y + height;
            }

            Canvas.LayoutTo(rc, 0);
        }
        
        /* index.MoveCanvas */
        /// <summary>
        /// フィールドCanvasを非同期で動かす。
        /// </summary>
        /// <param name="x">Canvasの新しいX座標。</param>
        /// <param name="y">Canvasの新しいY座標。</param>
        private async void MoveCanvas(double x, double y)
        {
            var rc = Canvas.Bounds;
            rc.X += x;
            rc.Y += y;
            await Canvas.LayoutTo(rc, 0);
        }
	}
}