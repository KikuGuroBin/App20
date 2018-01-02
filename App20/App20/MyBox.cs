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
            this.TranslateTo(TranslationX + args.X, TranslationY + args.Y);
        }
    }
}
