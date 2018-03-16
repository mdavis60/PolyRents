using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolyRents.views
{
    /// <summary>
    /// Interaction logic for AddEditRenterView.xaml
    /// </summary>
    public partial class AddEditRenterView : Window
    {
        public AddEditRenterView(Renter context = null)
        {
            if (context == null)
            {
                context = new Renter();
            }

            this.DataContext = context;

            InitializeComponent();
        }
    }
}
