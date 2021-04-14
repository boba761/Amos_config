using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Amos.Controls
{
    class VariableTypeEditor : UITypeEditor
    {
        public override Object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            IWindowsFormsEditorService frmsvr = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (frmsvr == null)
                return null;

            NumericUpDown nmr = new NumericUpDown();

            nmr.Size = new Size(60, 120);
            nmr.Minimum = 0;
            nmr.Maximum = 20000;
            nmr.Increment = 1;
            nmr.DecimalPlaces = 0;
            nmr.Value = 100;
            frmsvr.DropDownControl(nmr);
            return Convert.ToInt32(nmr.Value);
            //return base.EditValue(context, provider, value);
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            
            //base.PaintValue(e);
        }

        //public override bool GetPaintValueSupported( ITypeDescriptorContext context )
        //{
        //    return true;
        //}

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }
    }
}
