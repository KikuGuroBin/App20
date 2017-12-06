using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App20
{
    class MyBox : BoxView
    {
        public void Drug(object sender, DrugEvent args)
        {
            var rc = this.Bounds;

            rc.X += args.X;
            rc.Y += args.Y;

            this.LayoutTo(rc);
        }
    }
}
