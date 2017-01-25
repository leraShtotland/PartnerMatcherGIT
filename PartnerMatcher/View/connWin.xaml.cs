using PartnerMatcher.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    /// Interaction logic for connWin.xaml
    /// </summary>
    public partial class connWin : Window
    {
        BusLogic busLogic;
        public string usr;
        public string userMail; 
        public bool conf;
        public connWin(BusLogic busLogic)
        {
            InitializeComponent();
            conf = false;
            this.busLogic = busLogic;
        }


        private void connect_Click(object sender, RoutedEventArgs e)
        {
            {
                if (passBox.Password == "" || mailTxt.Text == "")
                {
                    System.Windows.MessageBox.Show("All fields are mandatory", "Error");

                }


                else
                {

                    //check if the mail exists and the password correct
                    if( !busLogic.checkPassword(mailTxt.Text.ToString().Trim(), passBox.Password))
                    {
                        System.Windows.MessageBox.Show("Mail or password are  incortrect", "Error");
                     
                    }
                    else
                    {
                        userMail = mailTxt.Text.ToString().Trim(); 
                        usr = busLogic.GetName(mailTxt.Text.ToString().Trim());
                        conf = true;
                                Close();

                    }


                }
            }

        }
    }


}
