using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App20
{
    /// <summary>
    /// View同士を線で結ぶサンプル。
    /// 
    /// 線を引く手法として、Width, Heightのどちらかを2px程度にしたBoxViewを生成し、
    /// それを複数繋ぎ合わせて線のようにしている。
    /// 
    /// 2つのViewインスタンスA, Bをしっぽと頭を繋ぐメソッドと
    /// 右端または左端と頭を繋ぐメソッドの2種類を実装している。
    /// 
    /// 上記の前者のメソッドは以下の3パターン(+左右反転)が描画される。
    /// この時の描画に使用するBoxViewを管理するのがBoxArray。
    /// 線に付属している番号は配列のインデックス。
    /// 
    /// a.  _____         b.                       c.   __3___
    ///    |  A  |                                     |4     |
    ///    |_____|                    __3__          __|__    |
    ///      0|                      |     |        |  B  |   |
    ///       |_1_                   |     |4       |_____|   |2
    ///           |         _____    |2  __|__                |
    ///           |2       |  A  |   |  |  B  |      _____    |
    ///         __|__      |_____|   |  |_____|     |  A  |   |
    ///        |  B  |        |0     |              |_____|   |
    ///        |_____|        |___1__|                0|___1__|
    ///  
    /// 後者のメソッドは以下の2パターン(+左右反転)が描画される。
    /// この時の描画に使用するBoxViewを管理するのがBoxArray2。
    /// 線に付属している番号は配列のインデックス。
    /// 
    /// d. _____             e.            __2__        f.   ___2___
    ///   |  A  |__0__                    |     |3        _3|__     |  
    ///   |_____|     |                   |1  __|__      |  B  |    |
    ///               |3                  |  |  B  |     |_____|    |1
    ///             __|__      _____      |  |_____|      _____     |
    ///            |  B  |    |  A  |__0__|              |  A  |_0__|
    ///            |_____|    |_____|                    |_____|
    /// 
    /// 
    /// 
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page4 : ContentPage
	{
        /// <summary>
        /// しっぽから頭をつなぐ描画用のBoxViewを管理する配列。
        /// </summary>
        BoxView[] BoxArray = new BoxView[5];

        /// <summary>
        /// 右端または左端から頭をつなぐ描画用のBoxViewを管理する配列。
        /// </summary>
        BoxView[] BoxArray2 = new BoxView[4];

        int Count;

		public Page4 ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            System.Diagnostics.Debug.WriteLine("deg : OnSize");
        }

        private void Line1(View a, View b, bool flag = false)
        {
            /* AインスタンスのしっぽのY座標を取得 */
            var aty = a.TranslationY + a.Height;
            
            /* Bインスタンスの頭のY座標を取得 */
            var bhy = b.TranslationY;
            
            /* 両インスタンスの中心のX座標を取得 */
            var acx = a.TranslationX + a.Width / 2;
            var bcx = b.TranslationX + b.Width / 2;

            /* 位置関係を判定 */
            if (bhy - aty > 20)
            {
                /* 両インスタンスの間隔を計算 */
                var both = (bhy - aty) / 2;

                /* 線の描画 */
                var box1 = new BoxView
                {
                    TranslationX = acx,
                    TranslationY = aty,
                    WidthRequest = 2,
                    HeightRequest = both,
                    BackgroundColor = Color.Blue,
                };

                var box2 = new BoxView
                {
                    BackgroundColor = Color.Cyan,
                    IsVisible = false,
                };

                var box3 = new BoxView
                {
                    TranslationX = bcx - acx > 0 ? acx : bcx,
                    TranslationY = aty + both,
                    WidthRequest = Math.Abs(bcx - acx),
                    HeightRequest = 2,
                    BackgroundColor = Color.Lime,
                };

                var box4 = new BoxView
                {
                    BackgroundColor = Color.Cyan,
                    IsVisible = false,
                };

                var box5 = new BoxView
                {
                    TranslationX = bcx,
                    TranslationY = bhy - both,
                    WidthRequest = 2,
                    HeightRequest = both,
                    BackgroundColor = Color.Yellow,
                };
                
                Main.Children.Add(box1);
                Main.Children.Add(box2);
                Main.Children.Add(box3);
                Main.Children.Add(box4);
                Main.Children.Add(box5);

                /* 管理用配列に格納 */
                BoxArray[0] = box1;
                BoxArray[1] = box2;
                BoxArray[2] = box3;
                BoxArray[3] = box4;
                BoxArray[4] = box5;
            }
            else
            {
                /* AインスタンスのしっぽのX座標取得 */
                var atx = a.TranslationX + a.Width;

                /* Bインスタンスの頭のX座標取得 */
                var bhx = b.TranslationX;

                var both = (bhx - atx) / 2;

                /* 両インスタンスのしっぽの位置関係を計算 */
                var diy = bhy + b.Height - aty;

                var box1 = new BoxView
                {
                    TranslationX = acx,
                    TranslationY = aty,
                    WidthRequest = 2,
                    HeightRequest = (diy > 0 ? diy : 0) + 20,
                    BackgroundColor = Color.Blue,
                };

                /* 両インスタンスの領域の差分の絶対値を計算 */
                var abs = Math.Abs(bcx - acx) - a.Width / 2 - b.Width / 2;

                /* box1の幅を計算 */
                var width = abs > 40 ? abs / 2 : abs + b.Width / 2 + 20;

                var box2 = new BoxView
                {
                    TranslationX = bcx - acx > 0 ? acx : acx - width,
                    TranslationY = aty + box1.HeightRequest,
                    WidthRequest = width,
                    HeightRequest = 2,
                    BackgroundColor = Color.Aqua,
                };

                var work = bcx - acx > 0 ? width : -width;

                var box3 = new BoxView
                {
                    TranslationX = acx + work,
                    TranslationY = bhy - 20,
                    WidthRequest = 2,
                    HeightRequest = (diy > 0 ? 0 : diy) + b.Height + 40,
                    BackgroundColor = Color.Lime,
                };

                work = bcx - acx;

                var box4 = new BoxView
                {
                    TranslationX = work < -100 || (work < 100 && work > 0) ? bcx : box3.TranslationX,
                    TranslationY = bhy - 20,
                    WidthRequest = Math.Abs(box3.TranslationX - bcx),
                    HeightRequest = 2,
                    BackgroundColor = Color.Aqua,
                };

                var box5 = new BoxView
                {
                    TranslationX = bcx,
                    TranslationY = bhy - 20,
                    WidthRequest = 2,
                    HeightRequest = 20,
                    BackgroundColor = Color.Yellow,
                };

                Main.Children.Add(box1);
                Main.Children.Add(box2);
                Main.Children.Add(box3);
                Main.Children.Add(box4);
                Main.Children.Add(box5);

                BoxArray[0] = box1;
                BoxArray[1] = box2;
                BoxArray[2] = box3;
                BoxArray[3] = box4;
                BoxArray[4] = box5;
            }
        }

        private void Line2(View a, View b, bool dir = true)
        {
            /* Aインスタンスの右端または、左端のX, Y座標取得 */
            var aex = a.TranslationX + (dir ? a.Width : 0);
            var aey = a.TranslationY + a.Height / 2;

            /* Bインスタンスの中心のX座標取得 */
            var bcx = b.TranslationX + b.Width / 2;
 
            /* Bインスタンスの頭のY座標を取得 */
            var bhy = b.TranslationY;

            if (bhy - aey > 20)
            {
                var work = aex - bcx;

                var box1 = new BoxView
                {
                    TranslationX = aex + (work > -1 ? 0 : work),
                    TranslationY = aey,
                    WidthRequest = Math.Abs(work),
                    HeightRequest = 2,
                    BackgroundColor = Color.Cyan,
                };

                var box2 = new BoxView
                {
                    BackgroundColor = Color.Red,
                    IsVisible = false,
                };

                var box3 = new BoxView
                {
                    BackgroundColor = Color.Yellow,
                    IsVisible = false,
                };

                var box4 = new BoxView
                {                    
                    TranslationX = bcx,
                    TranslationY = aey,
                    WidthRequest = 2,
                    HeightRequest = bhy - aey,
                    BackgroundColor = Color.Green,
                };

                BoxArray2[0] = box1;
                BoxArray2[1] = box2;
                BoxArray2[2] = box3;
                BoxArray2[3] = box4;
            }
            else
            {
                /* Aインスタンスの中心のX座標を取計算 */
                var acx = a.TranslationX + a.Width / 2;

                /* 両インスタンスの領域の差分の絶対値を計算 */
                var abs = Math.Abs(bcx - acx) - a.Width / 2 - b.Width / 2;

                /* box1の幅を計算 */
                var width = abs > 40 ? abs / 2 : abs + b.Width / 2 + 20;

                var box1 = new BoxView
                {
                    TranslationY = aey,
                    WidthRequest = width,
                    HeightRequest = 2,
                    BackgroundColor = Color.Cyan,
                };

                var work = a.TranslationX + a.Width / 2;

                var box2 = new BoxView
                {
                    BackgroundColor = Color.Red,
                };

                var box3 = new BoxView
                {
                    BackgroundColor = Color.Yellow,
                    IsVisible = false,
                };

                var box4 = new BoxView
                {
                    TranslationX = bcx,
                    TranslationY = aey,
                    WidthRequest = 2,
                    HeightRequest = bhy - aey,
                    BackgroundColor = Color.Green,
                };

                BoxArray2[0] = box1;
                BoxArray2[1] = box2;
                BoxArray2[2] = box3;
                BoxArray2[3] = box4;
            }
        }

        private void Line7(BoxView a, BoxView b)
        {
            var ap = a.TranslationY + a.Height;

            var bp = b.TranslationY;
            
            if (bp - ap > 20)
            {
                var both = (bp - ap) / 2;

                var ac = a.TranslationX + a.Width / 2;
                var bc = b.TranslationX + b.Width / 2;

                BoxArray[1].IsVisible = false;
                BoxArray[3].IsVisible = false;

                if (bc - ac != 0)
                {
                    var box1 = BoxArray[0];
                    box1.TranslationX = ac;
                    box1.TranslationY = ap;
                    box1.WidthRequest = 2;
                    box1.HeightRequest = both;
                    box1.IsVisible = true;

                    var box2 = BoxArray[2];
                    box2.TranslationX = bc - ac > 0 ? ac : bc;
                    box2.TranslationY = ap + both;
                    box2.WidthRequest = Math.Abs(bc - ac);
                    box2.HeightRequest = 2;
                    box2.IsVisible = true;

                    var box3 = BoxArray[4];
                    box3.TranslationX = bc;
                    box3.TranslationY = bp - both;
                    box3.WidthRequest = 2;
                    box3.HeightRequest = both;
                    box3.IsVisible = true;
                    
                }
                else
                {
                    var box = BoxArray[0];
                    box.TranslationX = ac;
                    box.TranslationY = ap;
                    box.WidthRequest = 2;
                    box.HeightRequest = bp - ap;

                    BoxArray[1].IsVisible = false;
                    BoxArray[2].IsVisible = false;
                    BoxArray[3].IsVisible = false;
                    BoxArray[4].IsVisible = false;
                }
            }
            else
            {

            }
        }

        private void OnClick(object sender, EventArgs args)
        {
            Box1.TranslationY = 400;

            Line1(Box1, Box2);
        }

        private void OnClick2(object sender, EventArgs args)
        {
            Box1.TranslationY = Count++ % 2 == 0 ? 100 : 200 ;

            Line7(Box1, Box2);
        }
    }
}