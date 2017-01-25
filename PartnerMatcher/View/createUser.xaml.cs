using PartnerMatcher.Logic;
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

namespace PartnerMatcher.View
{
    /// <summary>
    /// Interaction logic for userAndPass.xaml
    /// </summary>
    public partial class userAndPass : Window
    {
        BusLogic busLogic;
        //string mail;
        public userAndPass(BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password == "" || mailTxt.Text == "")
            {
                System.Windows.MessageBox.Show("All fields are mandatory", "Error");

            }

            else if (busLogic.checkIfUseExist(mailTxt.Text))
            {
                System.Windows.MessageBox.Show("This e-mail is already in the system", "Error");

            }
            else
            {
                string mail = mailTxt.Text;
                string Pass = passBox.Password;
                AddProfileWin apw = new AddProfileWin(busLogic);
                apw.mail = mail;
                apw.pass = Pass;
                apw.ShowDialog();
                this.Close();
            }

        }

      

    }
}

