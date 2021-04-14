using System;
using System.Drawing;
using System.Windows.Forms;
using Calculations.Variables;
using Amos.Forms;

namespace Amos.Views
{
    public partial class DashboardView : BaseView
    {
        private static DashboardView _self;

        private DashboardView()
        {
            InitializeComponent();
            toolStrip.BackColor = Color.FromArgb( 188, 199, 216 );
            propertyGrid.BackColor = Color.FromArgb( 222, 225, 231 );
            propertyGrid.CategoryForeColor = Color.FromArgb( 110, 133, 164 );
            propertyGrid.HelpBackColor = Color.FromArgb( 222, 225, 231 );
            propertyGrid.LineColor = Color.FromArgb( 240, 240, 240 );
        }

        public static DashboardView Instance()
        {
            if (_self == null)
                _self = new DashboardView();
            return _self;
        }

        public override Document Document
        {
            set 
            {
                try
                {
                    base.Document = value;
                    if ( base.Document == null )
                        propertyGrid.SelectedObject = null;
                    else
                        propertyGrid.SelectedObject = Document.Dashboard;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public override void Refresh( )
        {
            propertyGrid.Refresh( );
        }

        private void OnCustomize(object sender, EventArgs ar)
        {
            ( new DashboardCustomizeForm( Document ) ).ShowDialog();
            propertyGrid.Refresh( );
        }

        private void OnVariableEditor(object sender, EventArgs ar)
        {
            (new VariableEditorForm(Program.MainForm.Document)).ShowDialog();
            propertyGrid.Refresh( );
        }

        private void OnValueMode(object sender, EventArgs ar)
        {
            if (valueMode.Checked)
                return;
            valueMode.Checked = true;
            formulaMode.Checked = false;
            Variable.IsShowExpression = false;
            propertyGrid.Refresh();
        }

        private void OnFormulaMode(object sender, EventArgs ar)
        {
            if (formulaMode.Checked)
                return;
            formulaMode.Checked = true;
            valueMode.Checked = false;
            Variable.IsShowExpression = true;
            propertyGrid.Refresh();
        }

        private void OnCategorized( object sender, EventArgs e )
        {
            categorized.Checked = true;
            alphabetical.Checked = false;
            propertyGrid.PropertySort = PropertySort.Categorized;
        }

        private void OnAlphabetical( object sender, EventArgs e )
        {
            categorized.Checked = false;
            alphabetical.Checked = true;
            propertyGrid.PropertySort = PropertySort.Alphabetical;
        }
    }
}
