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
    /// Interaction logic for SearchReal_Estate.xaml
    /// </summary>
    public partial class SearchReal_Estate : Window
    {

        BusLogic busLogic;
        public bool isSearch;
        public int mAge;
        public int maxiAge;
        public bool? smoke;
        public bool? kosher;
        public bool? quiet;
        public bool? animals;
        public bool? play;


        public int roomsNumber;
        public SearchReal_Estate(BusLogic busLogic)
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

            else if ((!int.TryParse(maxAge.Text, out maxiAge))&& maxAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid max age in natural number", "Error");
            }


            else if ((!int.TryParse(roomsNum.Text, out roomsNumber))&& roomsNum.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid rooms Number in natural number", "Error");
            }


            else
            {



                smoke = checkBoxSmoke.IsChecked;
                kosher = checkBoxKosher.IsChecked;
                quiet = checkBoxquiet.IsChecked;
                animals = checkBoxAnimals.IsChecked;
                play = checkBoxPlay.IsChecked;
                isSearch = true;
                Close();

            }
        }
    }
}
