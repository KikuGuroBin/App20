using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App20
{
    class MyBox : BoxView
    {
        public EventHandler<DrugEvent> Drug;
        
        public MyBox()
        {
            Drug = OnDrug;
        }

        public void OnDrug(object sender, DrugEvent args)
        {
            this.TranslateTo(TranslationX + args.X, TranslationY + args.Y);

            var rc = Bounds;
            rc.X += args.X;
            rc.Y += args.Y;
            //this.LayoutTo(rc);
        }
    }
}
