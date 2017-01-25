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
    /// Interaction logic for SerachDate.xaml
    /// </summary>
    public partial class SerachDate : Window
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
        public string gender;
        public SerachDate(BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
        }


        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {

            if ((!int.TryParse(minAge.Text, out mAge))&& minAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }

            else if ((!int.TryParse(maxAge.Text, out maxiAge))&& minAge.Text.Equals("") == false)
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }

            else
            {
                if (comboBoxGen.SelectedIndex<0)
                    System.Windows.MessageBox.Show("you should enter a gender");
                else { 
                gender = comboBoxGen.SelectedValue.ToString();
                int selectedIndex = comboBoxGen.SelectedIndex;
                if (selectedIndex == 0)
                {
                    gender = "male";

                }
                else gender = "women";
                //   Object selectedItem = comboBoxGen.SelectedItem;
                //  gender = selectedItem.ToString();
                //get all matching rows

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
}
