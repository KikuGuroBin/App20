using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App20
{
    public class MyAbsoluteLayout : AbsoluteLayout
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            System.Diagnostics.Debug.WriteLine("deg : MyAbsoluteLayout.OnSizeAllocated");
        }
    }
}
