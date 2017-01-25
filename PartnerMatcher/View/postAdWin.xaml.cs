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
    public partial class postAdWin : Window
    {
        BusLogic busLogic;
        public bool addreq;
        string userMail;
        string activityLine;
        string activityName; //[1]
        string activityArea; //[2]
        string activityKind; //[3]
        int activityId;
        string activityInfo; //[4]
        Dictionary<int, int> activitiesIdDics; // <index in listBox, acticityID>
        Dictionary<int, string> userActivities;
        int maxAge;
        int minAge;
        bool kosher = false;
        bool quiet = false;
        bool play = false;
        bool animals = false;
        bool smoke = false;
        string content;
        //estates
        string address;
        int numofrooms;
        //sport
        int level;
        string type;
        //trips
        string country;
        DateTime date;
        string kindof;
        //dates
        string gender;
        string about;
        string iWant;

        public postAdWin(BusLogic logic, string mail)
        {
            InitializeComponent();
            busLogic = logic;
            userMail = mail;
            activitiesIdDics = new Dictionary<int, int>();
            EnableContent(false);
            StartDateInput.DisplayDateStart = DateTime.Today;
            HideFileds();
            InitFileds();
            userActivities = busLogic.GetUserActivities(userMail);
            ShowActivities();

        }

        private void InitFileds()
        {
            maxAge = -1;
            minAge = -1;
            kosher = false;
            quiet = false;
            play = false;
            animals = false;
            smoke = false;
            content = "";
            //estates
            address = "";
            numofrooms = -1;
            //sport
            level = -1;
            type = "";
            //trips
            country = "";
            kindof = "";
            //dates
            gender = "";
            about = "";
            iWant = "";

        }

        private void ShowActivities()
        {
            List<string> list = new List<string>();
            int counter = 1;
            if (userActivities.Keys.Count == 0) //no activities
            {
                list.Add("You don't have any activities yet. please create one first :)");
            }
            else
            {
                list.Add("<Activity Name>\t<Area>\t<Kind>\t<CreationDate>\t<Additional Information>");
                foreach (int activityID in userActivities.Keys)
                {
                    list.Add(userActivities[activityID]);
                    activitiesIdDics.Add(counter, activityID);
                    counter++;
                }
            }
            ActivitiesListBox.ItemsSource = list;
        }

        private void HideFileds()
        {
            QuietInput.Visibility = Visibility.Hidden;
            KosherInput.Visibility = Visibility.Hidden;
            AnimalsInput.Visibility = Visibility.Hidden;
            PlayInput.Visibility = Visibility.Hidden;
            SmokeInput.Visibility = Visibility.Hidden;
            MinAgeInput.Visibility = Visibility.Hidden;
            MaxAgeInput.Visibility = Visibility.Hidden;
            numberofroomsInput.Visibility = Visibility.Hidden;
            LevelInput.Visibility = Visibility.Hidden;
            StartDateInput.Visibility = Visibility.Hidden;
            TypeInput.Visibility = Visibility.Hidden;
            CountryInput.Visibility = Visibility.Hidden;
            AddressInput.Visibility = Visibility.Hidden;
            KindTripInput.Visibility = Visibility.Hidden;
            GenderInput.Visibility = Visibility.Hidden;
            AboutInput.Visibility = Visibility.Hidden;
            IWantInput.Visibility = Visibility.Hidden;

            QuietInputLabel.Visibility = Visibility.Hidden;
            KosherInputLabel.Visibility = Visibility.Hidden;
            AnimalsInputLabel.Visibility = Visibility.Hidden;
            PlayInputLabel.Visibility = Visibility.Hidden;
            SmokeInputLabel.Visibility = Visibility.Hidden;
            MinAgeInputLabel.Visibility = Visibility.Hidden;
            MaxAgeInputLabel.Visibility = Visibility.Hidden;
            numberofroomsInputLabel.Visibility = Visibility.Hidden;
            LevelInputLabel.Visibility = Visibility.Hidden;
            StartDateInputLabel.Visibility = Visibility.Hidden;
            TypeInputLabel.Visibility = Visibility.Hidden;
            CountryInputLabel.Visibility = Visibility.Hidden;
            AddressInputLabel.Visibility = Visibility.Hidden;
            KindTripInputLabel.Visibility = Visibility.Hidden;
            GenderInputLabel.Visibility = Visibility.Hidden;
            AboutInputLabel.Visibility = Visibility.Hidden;
            IWantInputLabel.Visibility = Visibility.Hidden;



        }

        private void EnableContent(bool isEnable)
        {
            additionalInfoTextBox.IsEnabled = isEnable;
            kindTextBox.IsEnabled = isEnable;
            areaInfoTextBox.IsEnabled = isEnable;
        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            bool min = true, max = true, kind = false, posted=false;
            if (MinAgeInput.Text == null || (MinAgeInput.Text.ToString() == "") || minAge == -1)
            {
                MinAgeInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                min = false;
            }
            if (MaxAgeInput.Text == null || (MaxAgeInput.Text.ToString() == "") || maxAge == -1)
            {
                MaxAgeInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                max = false;
            }
            if (activityKind == "Trips")
            {
                kind = CheckTrips();
                if (kind && min && max)
                {
                    posted = busLogic.AddTripsAd(userMail, activityArea, activityId, minAge, maxAge, kosher, quiet, smoke, country, date, kindof, content);
                  
                }
            }
            else if (activityKind == "Dates")
            {
                kind = CheckDates();
                if (kind && min && max)
                {
                    posted = busLogic.AddDatesAd(userMail, activityArea, activityId, minAge, maxAge, kosher, quiet, play, animals, smoke, iWant, gender, about, content);
                  
                }
            }
            else if (activityKind == "Real_Estate")
            {
                kind = CheckRealEstate();
                if (kind && min && max)
                {
                    posted = busLogic.AddEstaeteAd(userMail, activityArea, activityId, minAge, maxAge, kosher, quiet, animals, smoke, address, numofrooms, content);
                
                }
            }
            else if (activityKind == "Sport")
            {
                kind = CheckSport();
                if (kind && min && max)
                {
                    posted = busLogic.AddSportsAd(userMail, activityArea, activityId, minAge, maxAge, smoke, level, type, content);
                   
                }
            }
            if (posted)
            {
                MessageBox.Show("Ad was posted!");
                Close();
            }
            else
                MessageBox.Show("An unknown error occurred, sorry! :(");

        }

        private void BlackAllLabels()
        {
            LevelInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            TypeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            numberofroomsInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            AddressInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            GenderInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            CountryInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            KindTripInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            StartDateInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            MinAgeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            MaxAgeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private bool CheckSport()
        {
            bool lvl = true, ty = true;
            if (LevelInput.Text == null || (LevelInput.Text.ToString() == "") || level == -1)
            {
                LevelInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                lvl = false;
            }
            if (TypeInput.Text == null || (TypeInput.Text.ToString() == "") || type == "")
            {
                TypeInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                ty = false;
            }
            return ty && lvl;
        }

        private bool CheckRealEstate()
        {
            bool rooms = true, add = true;
            if (numberofroomsInput.Text == null || (numberofroomsInput.Text.ToString() == "") || numofrooms == -1)
            {
                numberofroomsInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                rooms = false;
            }
            if (AddressInput.Text == null || (AddressInput.Text.ToString() == "") || address == "")
            {
                AddressInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                add = false;
            }
            return rooms && add;
        }

        private bool CheckDates()
        {
            bool gen = true;
            if ((GenderInput.SelectedItem == null) || GenderInput.SelectedItem.ToString() == "" || gender == "")
            {
                GenderInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                gen = false;
            }

            return gen;
        }

        private bool CheckTrips()
        {
            bool cont = true, dates = true, kind = true;
            if (CountryInput.Text == null || (CountryInput.Text.ToString() == "") || country == "")
            {
                CountryInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                cont = false;
            }
            if (KindTripInput.Text == null || (KindTripInput.Text.ToString() == "") || kindof == "")
            {
                KindTripInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                kind = false;
            }
            if (StartDateInput.SelectedDate == null)
            {
                StartDateInputLabel.Foreground = new SolidColorBrush(Colors.Red);
                dates = false;
            }
            return dates && cont && kind;
        }

        private void ShowRelevantFileds(List<string> kindFields)
        {
            for (int i = 0; i < kindFields.Count - 1; i++)//withput activityId
            {
                string currentFiled = kindFields[i];
                if (currentFiled == "quiet")
                {
                    QuietInput.Visibility = Visibility.Visible;
                    QuietInputLabel.Visibility = Visibility.Visible;

                }
                else if (currentFiled == "kosher")
                {
                    KosherInput.Visibility = Visibility.Visible;
                    KosherInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "animals")
                {
                    AnimalsInput.Visibility = Visibility.Visible;
                    AnimalsInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "play")
                {
                    PlayInput.Visibility = Visibility.Visible;
                    PlayInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "smoke")
                {
                    SmokeInput.Visibility = Visibility.Visible;
                    SmokeInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "minAge")
                {
                    MinAgeInput.Visibility = Visibility.Visible;
                    MinAgeInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "maxAge")
                {
                    MaxAgeInput.Visibility = Visibility.Visible;
                    MaxAgeInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "numberOfRooms")
                {
                    numberofroomsInput.Visibility = Visibility.Visible;
                    numberofroomsInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "level")
                {
                    LevelInput.Visibility = Visibility.Visible;
                    LevelInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "StartDate")
                {
                    StartDateInput.Visibility = Visibility.Visible;
                    StartDateInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "Type")
                {
                    TypeInput.Visibility = Visibility.Visible;
                    TypeInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "gender")
                {
                    GenderInput.Visibility = Visibility.Visible;
                    GenderInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "about")
                {
                    AboutInput.Visibility = Visibility.Visible;
                    AboutInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "Country")
                {
                    CountryInput.Visibility = Visibility.Visible;
                    CountryInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "Address")
                {
                    AddressInput.Visibility = Visibility.Visible;
                    AddressInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "KindOfTrip")
                {
                    KindTripInput.Visibility = Visibility.Visible;
                    KindTripInputLabel.Visibility = Visibility.Visible;
                }
                else if (currentFiled == "IWant")
                {
                    IWantInput.Visibility = Visibility.Visible;
                    IWantInputLabel.Visibility = Visibility.Visible;
                }

            }
        }

        private void activityButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActivitiesListBox.SelectedItem == null || ActivitiesListBox.SelectedIndex == 0)
                return;
            activityId = activitiesIdDics[ActivitiesListBox.SelectedIndex];
            activityLine = ActivitiesListBox.SelectedItem.ToString();
            string[] splitted = activityLine.Split('\t');
            activityName = splitted[0];
            activityArea = splitted[1];
            activityKind = splitted[2];
            activityInfo = splitted[3];
            HideFileds();
            BlackAllLabels();
            InitFileds();
            kindTextBox.Text = activityKind;
            areaInfoTextBox.Text = activityArea;
            additionalInfoTextBox.IsEnabled = true;
            List<string> kindFields = busLogic.GetKindFields(activityKind);
            ShowRelevantFileds(kindFields);

        }

        private void createActivityButton_Click(object sender, RoutedEventArgs e)
        {
            createActivity createAct = new createActivity(busLogic, userMail);
            createAct.ShowDialog();

            activityArea = createAct.actArea;
            activityKind = createAct.actKind;
            activityId = createAct.actId;
            HideFileds();
            BlackAllLabels();
            InitFileds();
            kindTextBox.Text = activityKind;
            areaInfoTextBox.Text = activityArea;
            additionalInfoTextBox.IsEnabled = true;
            List<string> kindFields = busLogic.GetKindFields(activityKind);
            ShowRelevantFileds(kindFields);
        }

        #region InputsEvents

        private void MinAgeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string min = MinAgeInput.Text;
            int num;
            if (Int32.TryParse(min, out num))
            {
                if(num < 0 || num > 100)
                {
                    MessageBox.Show("Age Cannot Be Smaller than 0 or Larger than 100!");
                    MinAgeInput.Clear();
                    return;
                }
                if (maxAge == -1 || maxAge >= num)
                {
                    MinAgeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
                    minAge = num;
                }
            }
            else
                minAge = -1;
        }

        private void MaxAgeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string max = MaxAgeInput.Text;
            int num;
            if (Int32.TryParse(max, out num))
            {
                if (num < 0 || num > 100)
                {
                    MessageBox.Show("Age Cannot Be Smaller than 0 or Larger than 100!");
                    MaxAgeInput.Clear();
                    return;
                }
                if (minAge == -1 || minAge <= num)
                {
                    MaxAgeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
                    maxAge = num;
                }
            }
            else
                maxAge = -1;
        }

        private void SmokeInput_Checked(object sender, RoutedEventArgs e)
        {
            smoke = true;
        }

        private void SmokeInput_Unchecked(object sender, RoutedEventArgs e)
        {
            smoke = false;
        }

        private void KosherInput_Unchecked(object sender, RoutedEventArgs e)
        {
            kosher = false;
        }

        private void KosherInput_Checked(object sender, RoutedEventArgs e)
        {
            kosher = true;
        }

        private void QuietInput_Checked(object sender, RoutedEventArgs e)
        {
            quiet = true;
        }

        private void QuietInput_Unchecked(object sender, RoutedEventArgs e)
        {
            quiet = false;
        }

        private void AnimalsInput_Unchecked(object sender, RoutedEventArgs e)
        {
            animals = false;
        }

        private void AnimalsInput_Checked(object sender, RoutedEventArgs e)
        {
            animals = true;
        }

        private void PlayInput_Checked(object sender, RoutedEventArgs e)
        {
            play = true;
        }

        private void PlayInput_Unchecked(object sender, RoutedEventArgs e)
        {
            play = false;
        }

        private void KindTripInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            kindof = KindTripInput.Text;
            KindTripInputLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void CountryInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            country = CountryInput.Text;
            CountryInputLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void StartDateInput_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (StartDateInput.SelectedDate != null)
            {
                date = (DateTime)StartDateInput.SelectedDate;
                StartDateInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void AddressInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            address = AddressInput.Text;
            AddressInputLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void numberofroomsInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string rooms = numberofroomsInput.Text;
            int num;
            if (Int32.TryParse(rooms, out num))
            {
                numberofroomsInputLabel.Foreground = new SolidColorBrush(Colors.Black);
                numofrooms = num;
            }
            else
                numofrooms = -1;
        }

        private void TypeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            type = TypeInput.Text;
            TypeInputLabel.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void LevelInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string lvl = LevelInput.Text;
            int num;
            if (Int32.TryParse(lvl, out num))
            {
                if (num < 1 || num > 5)
                {
                    MessageBox.Show("Level is between 1 and 5");
                    LevelInput.Clear();
                    return;
                }
                level = num;
                LevelInputLabel.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
                level = -1;
        }

        private void AboutInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            about = AboutInput.Text;
        }

        private void additionalInfoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            content = additionalInfoTextBox.Text;
        }

        private void IWantInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            iWant = IWantInput.Text;
        }


        private void GenderInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gender = GenderInput.Text;
        }

        #endregion
    }
}
