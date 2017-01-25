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
    public partial class AddActivityMembers : Window
    {
        BusLogic busLogic;
        public HashSet<Tuple<string,bool>> members;

        public AddActivityMembers(BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
            members = new HashSet<Tuple<string, bool>>();
            isHead.IsChecked = false;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text != "")
            {
                string mail = nameTextBox.Text;
                if (!busLogic.checkIfUseExist(mail))
                {
                    System.Windows.MessageBox.Show("The e-mail " + mail + " does not exist in the system", "Error");
                }
                else
                {
                    bool head = (bool)isHead.IsChecked;
                    Tuple<string, bool> toAdd = new Tuple<string, bool>(mail, head);
                    members.Add(toAdd);
                    System.Windows.MessageBox.Show("The e-mail " + mail + " has added", "Add");
                }
            }
            nameTextBox.Text = "";
            isHead.IsChecked = false;
        }


        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}




