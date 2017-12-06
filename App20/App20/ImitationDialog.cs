using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App20
{
    /// <summary>
    /// カスタム
    /// </summary>
    public class ImitationDialog
    {
        /// <summary>
        /// ダイアログ表示時の影の部分
        /// </summary>
        public BoxView Shadow { get; set; }

        /// <summary>
        /// ダイアログ本体
        /// </summary>
        public RelativeLayout Dialog{ get; set; }
       
        /// <summary>
        /// ダイアログ表示アニメーションの座標軸
        /// </summary>
        private int AxisFlag;

        /// <summary>
        /// ダイアログ表示アニメーションの方向
        /// </summary>
        private int DirectionFlag;
        
        /// <summary>
        /// ダイアログをY軸方向で動かすアニメーション
        /// </summary>
        /// <param name="y">
        /// ダイアログを表示させるY座標
        /// </param>
        /// <param name="up">
        /// ダイアログ、上から出るか下から出るか
        /// </param>
        public async void ShowUp(double y, bool up = true)
        {
            if (AxisFlag != 0)
            {
                Hide();
            }

            // ダイアログを指定座標まで移動
            var rc = Dialog.Bounds;
            rc.Y = y - 50;
            await Dialog.LayoutTo(rc, 0);

            rc = Shadow.Bounds;
            rc.Y = 0;
            await Shadow.LayoutTo(rc, 0);

            // アニメーション
            for (int cnt = 0, i = up ? 1 : -1; cnt < 5; cnt += i)
            {
                Dialog.Opacity += 0.2;

                Shadow.Opacity += 0.1;

                rc = Dialog.Bounds;
                rc.Y -= 10;
                await Dialog.LayoutTo(rc, 100);
            }

            AxisFlag = 1;
            DirectionFlag = 1;
        }

        /// <summary>
        /// ダイアログをX軸方向で
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="slide">
        /// 
        /// </param>
        public async void ShowSlide(double x, bool slide = true)
        {
            if (AxisFlag != 0)
            {
                Hide();
            }

            // ダイアログを指定座標まで移動
            var rc = Dialog.Bounds;
            rc.X = x;
            await Dialog.LayoutTo(rc, 0);

            // アニメーション
            for (int cnt = 0, i = slide ? 1 : -1; cnt < 5; cnt += i)
            {
                Dialog.Opacity += 0.2;

                Shadow.Opacity += 0.1;

                rc = Dialog.Bounds;
                rc.X -= 10;
                await Dialog.LayoutTo(rc, 100);
            }

            AxisFlag = 2;
            DirectionFlag = 2;
        }

        /// <summary>
        /// ダイアログを隠すアニメーション
        /// </summary>
        public async void Hide()
        {
            var rc = Dialog.Bounds;

            // 隠すアニメーション
            for (var cnt = 0; cnt < 5; cnt++)
            {
                Dialog.Opacity -= 0.2;

                Shadow.Opacity -= 0.1;

                if (AxisFlag == 1)
                {
                    rc.Y += 10;
                }
                else if(AxisFlag == 2)
                {
                    rc.X += 10;
                }

                await Dialog.LayoutTo(rc, 100);
            }

            AxisFlag = 0;
            DirectionFlag = 0;

            // 影をなくす
            rc = Shadow.Bounds;
            rc.Y = rc.Height;
            await Shadow.LayoutTo(rc, 0);
        }
    }
}
