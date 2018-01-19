using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace App20
{
    /// <summary>
    /// 線を描画するためのレイアウト。
    /// 
    /// 線の描画は内部クラスで行っている。
    /// </summary>
    public class LineCanvas : AbsoluteLayout
    {
        /// <summary>
        /// 生成した線を管理するリスト。
        /// </summary>
        public List<LineCanvas.Line> Lines = new List<LineCanvas.Line>();

        /// <summary>
        /// しっぽから頭を繋ぐ線を描画するフロント。
        /// </summary>
        /// <param name="views">線でつなぎたいViewインスタンス</param>
        /// <returns>生成したLineCanvas.Lineインスタンス</returns>
        public LineCanvas.Line Tail(params View[] views)
        {
            var array = new BoxView[5];

            for (var i = 0; i < 5; i++)
            {
                var box = new BoxView
                {
                    BackgroundColor = Color.Black,
                    WidthRequest = 0,
                    HeightRequest = 0,
                };
                array[i] = box;

                this.Children.Add(box);
            }

            var line = views.Length == 2 ? new LineCanvas.Line(views[0], views[1], array) : new LineCanvas.Line(array);

            Lines.Add(line);

            return line;
        }

        /// <summary>
        /// 右端または左端から頭をつなぐ線を描画するフロント。
        /// </summary>
        /// <param name="views">線でつなぎたいViewインスタンス</param>
        /// <returns>生成したLineCanvas.Lineインスタンス</returns>
        public LineCanvas.Line Side(bool dir, params View[] views)
        {
            var array = new BoxView[4];

            for (var i = 0; i < 4; i++)
            {
                var box = new BoxView
                {
                    BackgroundColor = Color.Black,
                    WidthRequest = 0,
                    HeightRequest = 0,
                };
                array[i] = box;

                this.Children.Add(box);
            }

            var line = views.Length == 2 ? 
                new LineCanvas.Line(views[0], views[1], array): 
                new LineCanvas.Line(array);

            line.Direct = dir;

            Lines.Add(line);

            return line;
        }

        public LineCanvas.Line[] SearchLines(View view)
        {
            return Lines.Where(l => view == l.PreviousView || view == l.BehideView)
                        .Select(l => l)
                        .ToArray();
        }

        /// <summary>
        /// 線の描画を行う内部クラス。
        /// 
        /// 描画に関する詳細はdocument.htmlを参照。
        /// </summary>
        public class Line
        {
            /// <summary>
            /// 線の幅。
            /// </summary>
            private const int linePixcel = 2;

            /// <summary>
            /// インスタンスのマージン。
            /// </summary>
            private const int viewMargin = 20;

            /// <summary>
            /// 描画する線の出発点とつなげるViewインスタンス。
            /// </summary>
            public View PreviousView;

            /// <summary>
            /// 描画する線の末尾とつなげるViewインスタンス。
            /// </summary>
            public View BehideView;

            /// <summary>
            /// <seealso cref="DrawTail" />
            /// で描画したBoxViewを管理する配列。
            /// </summary>
            public BoxView[] TailLines = new BoxView[5];

            /// <summary>
            /// <seealso cref="DrawSide"/>
            /// で描画したBoxViewを管理する配列。
            /// </summary>
            public BoxView[] SideLines = new BoxView[4];

            /// <summary>
            /// <para><seealso cref="DrawTail" />か</para>
            /// <para><seealso cref="DrawSide"/></para>
            /// のどちらかで描画したかを判定する。
            /// </summary>
            public bool WhichDraw;

            /// <summary>
            /// 方向フラグ。<seealso cref="DrawSide" />で使用する。
            /// </summary>
            public bool Direct;

            /// <summary>
            /// コンストラクタ。
            /// 描画用BoxViewを代入する。
            /// </summary>
            /// <param name="args">線を表現するBoxViewインスタンス</param>
            public Line(BoxView[] args)
            {
                if (args.Length == 5)
                {
                    TailLines = args;
                    WhichDraw = true;
                }
                else if (args.Length == 4)
                {
                    SideLines = args;
                }
            }

            /// <summary>
            /// コンストラクタのオーバーロード。
            /// <para><seealso cref="PreviousView" />と</para>
            /// <para><seealso cref="BehideView" /></para>
            /// に引数を代入する。
            /// </summary>
            /// <param name="a">描画する線の出発点とつなげるViewインスタンス</param>
            /// <param name="b">描画する線の末尾とつなげるViewインスタンス</param>
            /// <param name="args">線を表現するBoxViewインスタンス</param>
            public Line(View a, View b, BoxView[] args) : this(args)
            {
                PreviousView = a;
                BehideView = b;
            }

            /// <summary>
            /// <para><seealso cref="Draw(View, View)" />のオーバーロード。</para>
            /// フィールドを使用して線を描画する。
            /// </summary>
            public void Draw()
            {
                Draw(PreviousView, BehideView);
            }

            /// <summary>
            /// 引数で指定した2つViewインスタンスを
            /// <para><seealso cref="DrawTail" /></para>
            /// <para><seealso cref="DrawSide"/></para>
            /// のどちらかで線を描画するためのフロント。
            /// </summary>
            /// <param name="a">描画する線の出発点とつなげるViewインスタンス</param>
            /// <param name="b">描画する線の末尾とつなげるViewインスタンス</param>
            public void Draw(View a, View b)
            {
                if (WhichDraw)
                {
                    DrawTail(a, b);
                }
                else
                {
                    DrawSide(a, b, Direct);
                }
            }

            /// <summary>
            /// しっぽから頭をつなぐ線を描画する。
            /// </summary>
            /// <param name="a">しっぽから線を描画するViewインスタンス。</param>
            /// <param name="b">頭に線をつなぐViewインスタンス。</param>
            public void DrawTail(View a, View b)
            {
                /* AインスタンスのしっぽのY座標を取得 */
                var aty = a.TranslationY + a.Height;

                /* Bインスタンスの頭のY座標を取得 */
                var bhy = b.TranslationY;

                /* 両インスタンスの中心のX座標を取得 */
                var acx = a.TranslationX + a.Width / 2;
                var bcx = b.TranslationX + b.Width / 2;

                /* 位置関係を判定 */
                if (bhy - aty > viewMargin)
                {
                    /* ======== パターンa,bの描画 ======== */

                    /* 両インスタンスの間隔を計算 */
                    var both = (bhy - aty) / 2;

                    var same = acx - bcx == 0;

                    /* 線の描画 */
                    var box1 = TailLines[0];
                    box1.TranslationX = acx - linePixcel / 2;
                    box1.TranslationY = aty;
                    box1.WidthRequest = linePixcel;
                    box1.IsVisible = true;

                    var box2 = TailLines[1];
                    box2.IsVisible = false;

                    var box3 = TailLines[2];

                    var box4 = TailLines[3];
                    box4.IsVisible = false;

                    var box5 = TailLines[4];

                    if (same)
                    {
                        box1.HeightRequest = bhy - aty - linePixcel / 2;

                        box3.IsVisible = false;

                        box5.IsVisible = false;
                    }
                    else
                    {
                        box1.HeightRequest = both - linePixcel / 2;

                        box3.TranslationX = Math.Min(acx, bcx) - linePixcel / 2;
                        box3.TranslationY = aty + both - linePixcel / 2;
                        box3.WidthRequest = Math.Abs(bcx - acx) + linePixcel;
                        box3.HeightRequest = linePixcel;
                        box3.IsVisible = true;

                        box5.TranslationX = bcx - linePixcel / 2;
                        box5.TranslationY = bhy - both + linePixcel / 2;
                        box5.WidthRequest = linePixcel;
                        box5.HeightRequest = both - linePixcel / 2;
                        box5.IsVisible = true;
                    }
                }
                else
                {
                    /* ======== c,dパターンの描画 ======== */

                    /* Bインスタンスの頭のX座標取得 */
                    var bhx = b.TranslationX;

                    /* 両インスタンスの間のX座標 */
                    var bothx = Math.Min(bcx, acx) + Math.Abs(bcx - acx) / 2;

                    /* 両インスタンスのしっぽの位置関係を計算 */
                    var diy = bhy + b.Height - aty;

                    var box1 = TailLines[0];
                    box1.TranslationX = acx - linePixcel / 2;
                    box1.TranslationY = aty;
                    box1.WidthRequest = linePixcel;
                    box1.HeightRequest = viewMargin;
                    box1.IsVisible = true;

                    /* 両インスタンスの領域の差分の絶対値を計算 */
                    var abs = Math.Abs(bcx - acx);

                    /* box2の幅を計算 */
                    var box2w = abs - a.Width / 2 - b.Width / 2 > viewMargin * 2 ? abs / 2 : abs + b.Width / 2 + viewMargin;

                    var box2 = TailLines[1];
                    box2.TranslationX = acx - (bcx - acx > 0 ? 0 : box2w);
                    box2.TranslationY = aty + box1.HeightRequest;
                    box2.WidthRequest = box2w;
                    box2.HeightRequest = linePixcel;
                    box2.IsVisible = true;

                    /* box3のX座標を計算 */
                    var box3x = acx + (bcx - acx > 0 ? box2w : -box2w);

                    var box3 = TailLines[2];
                    box3.TranslationX = box3x;
                    box3.TranslationY = bhy - viewMargin;
                    box3.WidthRequest = linePixcel;
                    box3.HeightRequest = aty - bhy + viewMargin * 2;
                    box3.IsVisible = true;

                    var work = bcx - acx;

                    /* 両インスタンスの領域の差分を計算 */
                    var region = abs - a.Width / 2 - b.Width / 2;

                    var box4 = TailLines[3];
                    box4.TranslationX = (region > viewMargin * 2 && work <= 0) || (region <= viewMargin * 2 && work > 0) ? bcx : box3x;
                    box4.TranslationY = bhy - viewMargin;
                    box4.WidthRequest = Math.Abs(bcx - box3x);
                    box4.HeightRequest = linePixcel;
                    box4.IsVisible = true;

                    var box5 = TailLines[4];
                    box5.TranslationX = bcx;
                    box5.TranslationY = bhy - viewMargin + linePixcel;
                    box5.WidthRequest = linePixcel;
                    box5.HeightRequest = viewMargin;
                    box5.IsVisible = true;
                }
            }

            /// <summary>
            /// 右端または左端から頭を繋ぐ線を描画する。
            /// </summary>
            /// <param name="a">右端またはに左端から線を描画するViewインスタンス。</param>
            /// <param name="b">頭に線をつなぐViewインスタンス。</param>
            /// <param name="dir">方向フラグ。</param>
            public void DrawSide(View a, View b, bool dir = true)
            {
                /* Aインスタンスの右端または、左端のX, Y座標取得 */
                var aex = a.TranslationX + (dir ? a.Width : 0);
                var aey = a.TranslationY + a.Height / 2;

                /* Bインスタンスの中心のX座標取得 */
                var bcx = b.TranslationX + b.Width / 2;

                /* Bインスタンスの頭のY座標を取得 */
                var bhy = b.TranslationY;

                if (bhy - aey > viewMargin)
                {
                    /* ======== e,fパターンの描画 ======== */

                    /* Aインスタンスの中心のX座標を計算 */
                    var acx = a.TranslationX + a.Width / 2;

                    var work = aex - bcx;

                    /* Aインスタンスの端とBインスタンスの中心のX座標の差分を計算 */
                    var border = aex + (dir ? 1 : -1) * viewMargin - bcx;

                    /* 両インスタンスの領域の差分を計算 */
                    var region = Math.Abs(bcx - aex) + (bcx - aex > 0 ? -1 : 1) * (b.Width / 2);

                    var box1 = SideLines[0];
                    box1.TranslationY = aey;
                    box1.HeightRequest = linePixcel;
                    box1.IsVisible = true;

                    var box2 = SideLines[1];

                    var box3 = SideLines[2];

                    var box4 = SideLines[3];
                    box4.TranslationX = bcx;
                    box4.WidthRequest = linePixcel;

                    if ((dir && border > 0) || (!dir && border <= 0))
                    {
                        box1.TranslationX = aex - (dir ? 0 : viewMargin);
                        box1.WidthRequest = viewMargin;

                        var both = (bhy - aey + a.Height / 2) / 2;

                        box2.TranslationX = aex + (dir ? 1 : -1) * viewMargin;
                        box2.TranslationY = aey;
                        box2.WidthRequest = linePixcel;
                        box2.HeightRequest = both;

                        var box3w = Math.Abs(bcx - aex) + viewMargin;

                        box3.TranslationX = bcx - (work > 0 ? 0 : box3w);
                        box3.TranslationY = aey + both;
                        box3.WidthRequest = Math.Abs(bcx - aex) + viewMargin;
                        box3.HeightRequest = linePixcel;

                        box4.TranslationY = aey + both;
                        box4.HeightRequest = bhy - (aey + both);
                    }
                    else
                    {
                        box1.TranslationX = aex - (dir ? 0 : work);
                        box1.WidthRequest = Math.Abs(work);

                        box2.IsVisible = false;

                        box3.IsVisible = false;

                        box4.TranslationY = aey;
                        box4.HeightRequest = bhy - aey;
                    }
                }
                else
                {
                    /* Aインスタンスの中心のX座標を取計算 */
                    var acx = a.TranslationX + a.Width / 2;

                    /* 両インスタンスの領域の差分の絶対値を計算 */
                    var abs = Math.Abs(bcx - acx);

                    /* box1の幅 */
                    var box1w = 0.0;

                    /* box1wの計算 */
                    if ((dir && bcx - acx <= 0) || (!dir && bcx - acx > 0))
                    {
                        box1w = viewMargin;
                    }
                    else
                    {
                        if (abs - a.Width / 2 - b.Width / 2 > viewMargin * 2)
                        {
                            box1w = abs / 2;
                        }
                        else
                        {
                            box1w = abs + b.Width / 2 + viewMargin;
                        }

                        box1w -= a.Width / 2;
                    }

                    var box1 = SideLines[0];
                    box1.TranslationX = aex - (dir ? 0 : box1w);
                    box1.TranslationY = aey;
                    box1.WidthRequest = box1w;
                    box1.HeightRequest = linePixcel;

                    /* box2のX座標を計算 */
                    var box2x = aex + (dir ? box1w : -box1w);

                    var box2 = SideLines[1];
                    box2.TranslationX = box2x;
                    box2.TranslationY = bhy - viewMargin;
                    box2.WidthRequest = linePixcel;
                    box2.HeightRequest = aey - bhy + viewMargin;
                    box2.IsVisible = true;

                    var work = bcx - acx;

                    /* 両インスタンスの領域の差分を計算 */
                    var region = Math.Abs(bcx - aex) + (bcx - aex > 0 ? -1 : 1) * (b.Width / 2);

                    var box3 = SideLines[2];
                    box3.TranslationX = (region > viewMargin * 2) ? bcx : box2x;
                    box3.TranslationY = bhy - viewMargin;
                    box3.WidthRequest = Math.Abs(bcx - box2x);
                    box3.HeightRequest = linePixcel;
                    box3.IsVisible = true;

                    var box4 = SideLines[3];
                    box4.TranslationX = bcx;
                    box4.TranslationY = bhy - viewMargin;
                    box4.WidthRequest = linePixcel;
                    box4.HeightRequest = viewMargin;
                }
            }
        }
    }
}
