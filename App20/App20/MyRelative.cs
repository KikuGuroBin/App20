using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App20
{
    public class MyRelative : RelativeLayout
    {
        public bool Druging;

        public EventHandler<DrugEvent> Touch;

        public MyRelative()
        {
            Touch = OnTouch;
        }

        public void OnTouch(object sender, DrugEvent args)
        {
            switch (args.EventFlag)
            {
                /* タッチした時 */
                case 1:
                    Druging = true;
                    
                    break;

                /* ドラッグした時 */
                case 2:
                    break;

                /* 指を離した時 */
                case 3:
                    Druging = false;

                    break;
            }
        }
    }
}
