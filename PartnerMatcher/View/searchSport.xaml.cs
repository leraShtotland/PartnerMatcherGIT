using PartnerMatcher.Logic;
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
using System.Windows.Shapes;

namespace PartnerMatcher.View
{

    /// <summary>
    /// Interaction logic for searchDateWin.xaml
    /// </summary>
    /// 

    public partial class searchSport : Window
    {

        BusLogic busLogic;
        public bool isSearch;
        public int mAge;
        public int maxiAge;
        public int _level;
        public bool? smoke;
        public string _type;


        public searchSport(BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {

            if ((!int.TryParse(minAge.Text, out mAge))&& minAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid min age in natural number", "Error");
            }

            else if ((!int.TryParse(maxAge.Text, out maxiAge))&& minAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid max age in natural number", "Error");
            }


            else if ((!int.TryParse(level.Text, out _level))&& minAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid level in natural number", "Error");
            }

            else
            {
                smoke = checkBoxSmoke.IsChecked;
                _type = type.Text;
                isSearch = true;
                Close();

            }


        }
    }
}
//}
