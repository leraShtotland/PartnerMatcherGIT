using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
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
using PartnerMatcher.Logic;

namespace PartnerMatcher.View
{
    /// <summary>
    /// Interaction logic for AddProfileWin.xaml
    /// </summary>
    public partial class AddProfileWin : Window
    {
        BusLogic busLogic;
        int age;
        string name;
        bool? smoke;
        public string mail;
        public string pass;
        bool? kosher;
        bool? quiet;
        bool? animals;
        bool? play;
        string gender;
        string about;


        public AddProfileWin(BusLogic busLogic)
        {

            InitializeComponent();
            this.busLogic = busLogic;
            comboBoxGen.Items.Add("male");
            comboBoxGen.Items.Add("female");
        }


        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || ageTxt.Text == "" || comboBoxGen.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Name, age and Gender are mandatory", "Error");

            }
            else if (!int.TryParse(ageTxt.Text, out age))
            {
                System.Windows.MessageBox.Show("Enter a valid age in natural number", "Error");
            }
            else
            {
                about = textBoxAbout.Text;
                gender = comboBoxGen.SelectedValue.ToString();
                //get all matching rows
                name = nameTextBox.Text;
                smoke = checkBoxSmoke.IsChecked;
                kosher = checkBoxKosher.IsChecked;
                quiet = checkBoxquiet.IsChecked;
                animals = checkBoxAnimals.IsChecked;
                play = checkBoxPlay.IsChecked;

                bool added = busLogic.addUser(mail, pass, age, gender, smoke, name, kosher, quiet, animals, play, about);
                if (added)
                {
                    System.Windows.MessageBox.Show("User Created");
                    Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("There was problem to create the account, Try again", "Error");
                }
            }
        }


        private void comboBoxGen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gender = comboBoxGen.SelectedValue.ToString();
        }
    }
}




