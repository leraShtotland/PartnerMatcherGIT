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
    /// Interaction logic for requestWin.xaml
    /// </summary>
    public partial class requestWin : Window
    {

        BusLogic buslogbusLogic;
        public bool addreq;
        string content;
        string askerMail;
        int activityID;
        int adId;
        string chosenKind;
        string AdvertiserMail;
        public requestWin(BusLogic buslog, string usermail, int adId, string AdvertiserMail, int activityId, string chosenKind)
        {
            InitializeComponent();
            buslogbusLogic = buslog;
            askerMail = usermail;
            activityID = activityId;
            this.chosenKind = chosenKind;
            this.adId = adId;
            this.AdvertiserMail = AdvertiserMail;
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            content = textBoxreq.Text;

            buslogbusLogic.applyRequest(askerMail, activityID, chosenKind, adId, content, AdvertiserMail);
            addreq = true;
            Close();
        }
    }
}
