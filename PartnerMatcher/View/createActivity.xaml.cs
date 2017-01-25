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
    /// Interaction logic for createActivity.xaml
    /// </summary>
    public partial class createActivity : Window
    {
        BusLogic busLogic;
        string userMail;
        public int actId = -1;
        public string actArea = "", actKind = "";
        HashSet<Tuple<string, bool>> membersToAdd;

        public createActivity(BusLogic busLogic, string mail)
        {
            InitializeComponent();
            this.busLogic = busLogic;
            userMail = mail;
            membersToAdd = new HashSet<Tuple<string, bool>>();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (areaBox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Area field is mandatory!", "Error");
            }
            else if (kindBox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Kind field is mandatory!", "Error");
            }
            else if (activityName.Text == "")
            {
                System.Windows.MessageBox.Show("Activity name field is mandatory!", "Error");
            }
            else if (headActBox.Text == "")
            {
                System.Windows.MessageBox.Show("Head of Activity field is mandatory!", "Error");
            }
            else if (!busLogic.checkIfUseExist(headActBox.Text))
            {
                System.Windows.MessageBox.Show("This e-mail is not exist in the system", "Error");
            }
            else
            {
                actArea = areaBox.Text;
                actKind = kindBox.Text;
                string name = activityName.Text;
                string head = headActBox.Text;
                string info = additionalBox.Text;

                actId = busLogic.CreateNewActivity(actArea, actKind, name, head, userMail, info);
                if (actId > 0)
                    System.Windows.MessageBox.Show("The activity: '" + name + "' created successfully", "Yay!");

                if (head.Equals(userMail))
                    busLogic.AddnewActivityMember(actId, head, true);
                else
                {
                    busLogic.AddnewActivityMember(actId, head, true);
                    busLogic.AddnewActivityMember(actId, userMail, false);
                }

                if (membersToAdd.Count > 0)
                {
                    foreach (Tuple<string, bool> member in membersToAdd)
                    {
                        busLogic.AddnewActivityMember(actId, member.Item1, member.Item2);
                        //busLogic.sendMailToUser(membersToAdd[i].Item1, "You've been added to a new activity", "Hello, \nThe new activity: '" + name +
                        //     "' has been created and you are a member in it!.\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");
                    }
                }

                this.Close();

                if (!head.Equals(userMail))
                {
                    busLogic.sendMailToUser(userMail, "You've added to a new activity", "Hello, \nYou have created the new activity: '" + name +
                         "'the head of that activity is: '" + head + "'.\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");

                    busLogic.sendMailToUser(head, "You've been added to a new activity", "Hello, \nThe new activity: '" + name +
                        "' has been created and you are the head of it!\nYou've been added by the user: " + userMail + ".\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");

                }
                else if (head.Equals(userMail))
                    busLogic.sendMailToUser(userMail, "You've been added to a new activity", "Hello, \nYou have created the new activity: '" + name +
                         "' and you are the head of it!\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");

                if (membersToAdd.Count > 0)
                {
                    foreach (Tuple<string, bool> member in membersToAdd)
                    {
                        if (member.Item2 == false)
                        {
                            busLogic.sendMailToUser(member.Item1, "You've been added to a new activity", "Hello, \nThe new activity: '" + name +
                                 "' has been created and you are a member in it!\nYou've been added by the user: " + userMail + ".\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");
                        }
                        else
                        {
                            busLogic.sendMailToUser(member.Item1, "You've been added to a new activity", "Hello, \nThe new activity: '" + name +
                                 "' has been created and you are the head of it!\nYou've been added by the user: " + userMail + ".\n\nHope you will enjoy your new activity, \nPartnerMatcher Team.");
                        }
                    }
                }
            }

        }

        private void addMembersbutton_Click_1(object sender, RoutedEventArgs e)
        {
            AddActivityMembers aam = new AddActivityMembers(busLogic);
            aam.Show();
            membersToAdd = aam.members;
        }


    }
}

