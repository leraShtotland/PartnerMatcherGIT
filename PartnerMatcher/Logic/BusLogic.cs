using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PartnerMatcher.Data;
using System.Data;
namespace PartnerMatcher.Logic
{

    //class for the business logic layer
    public class BusLogic
    {

        myData data;

        public BusLogic()
        {
            data = new myData();
        }

        //get the list of the areas
        public List<string> getAreas()
        {
            return data.getAreas();
        }

        //get the List of kinds
        public List<string> getKinds()
        {
            return data.getKinds();
        }

        //send system mail to a user
        public bool sendMailToUser(string mailAddr, string title, string content)
        {
            try
            {
                //send the mail to the user's mail               
                MailMessage mailMsg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mailMsg.From = new MailAddress("partnersmatcherapp@gmail.com");
                mailMsg.To.Add(mailAddr);
                mailMsg.Subject = title;
                mailMsg.Body = content;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("partnersmatcherapp@gmail.com", "p12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //send new user details to the data layer
        public bool addUser(string mail, string pass, int age, string gender, bool? smoke, string name, bool? kosher, bool? quiet, bool? animals, bool? play, string about)
        {
            if (data.AddUserToDB(mail, pass, age, gender, smoke, name, kosher, quiet, animals, play, about))
            {
                sendMailToUser(mail, name + ", Welcome To PartnersMatcher", "Hope you find your second part for your activity");
                return true;
            }

            else
            {
                return false;
            }
        }

        
        public bool applyRequest(string askerMail, int activityID, string chosenKind,int  adId,string  content, string AdvertiserMail)
        {
            sendMailToUser(askerMail, "Your request has been send", "Your request has been send to the partners in the activity and the Advertiser, wait for response.");
            sendMailToUser(AdvertiserMail, "You get request to join for one of actovities you advertise ", askerMail+ " asks to join for the activity. the content of the request is: " + content + ". all the partners in the activity got mail with request for ranking it. ");
           List<string> members= data.getMembersActivity(activityID);
            DateTime localDate = DateTime.Now;
            foreach (string item in members)
            {
                sendMailToUser(item, "someone request to join for one of your actovities", askerMail + " asks to join for one of your the activities. the content of the request is: " + content +". please rank his request. ");
            }
            int status = 1;
           return data.saveRequest(askerMail, localDate, activityID, chosenKind, adId, content, status); 
        }

        //noga
        public void AdvancedSearchDates(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string gender, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {

            data.AdvancedSearchDates(chosenArea, chosenKind, ref  dt, payed, minAge, maxAge, gender, smoke, kosher, quiet, animals, play);

        }

        //noga
        public void AdvancedSearchSport(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string type, int level, bool? smoke)
        {

            data.AdvancedSearchSport(chosenArea, chosenKind, ref dt, payed, minAge, maxAge, type, level, smoke);

        }

        //noga
        public void AdvancedSearchApartment(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, int roomsNum, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {

            data.AdvancedSearchApartment(chosenArea, chosenKind, ref dt, payed, minAge, maxAge, roomsNum, smoke, kosher, quiet, animals, play);

        }

        //noga
        public void AdvancedSearchTrips(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string kind, string country, bool? smoke, bool? kosher, bool? quiet)
        {

            data.AdvancedSearchTrips(chosenArea, chosenKind, ref dt, payed, minAge, maxAge, kind, country, smoke, kosher, quiet);

        }


        public bool checkIfUseExist(string mail)
        {
            return data.checkIfUserExist(mail);
        }

        public bool checkPassword(string mail, string password)
        {
            bool ans = data.checkPassword(mail, password);
            return ans;
        }

        public string GetName(string mail)
        {
            return data.getName(mail);
        }

        public List<string> GetKindFields(string kind)
        {
            List<string> kinds = data.getKinds();
            if (!kinds.Contains(kind))
                return null;
            List<string> fields = data.GetKindFields(kind);

            return fields;
        }

        public void find(string chosenArea, string chosenKind, ref DataTable dt, bool payed)
        {
            data.find(chosenArea, chosenKind, ref dt, payed);
        }

        internal Dictionary<int, string> GetUserActivities(string userMail)
        {
            return data.getMemberActivities(userMail);
        }

        public bool AddTripsAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool smoke, string country, DateTime date, string kindof, string content)
        {
            return data.AddTripsAd(userMail, area, activityId, minAge, maxAge, kosher, quiet, smoke, country, date, kindof, content);
        }

        public bool AddDatesAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool play, bool animals, bool smoke, string iWant, string gender, string about, string content)
        {
            return data.AddDatesAd(userMail, area, activityId, minAge, maxAge, kosher, quiet, play, animals, smoke, iWant, gender, about, content);
        }

        public bool AddSportsAd(string userMail, string area, int activityId, int minAge, int maxAge, bool smoke, int level, string type, string content)
        {
            return data.AddSportsAd(userMail, area, activityId, minAge, maxAge, smoke, level, type, content);
        }

        public bool AddEstaeteAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool animals, bool smoke, string address, int numofrooms, string content)
        {
            return data.AddEstaeteAd(userMail, area, activityId, minAge, maxAge, kosher, quiet, animals, smoke, address, numofrooms, content);
        }


        internal int CreateNewActivity(string area, string kind, string name, string head, string userMail, string info)
        {
            return data.CreateNewActivity(area, kind, name, head, userMail, info);
        }

        internal bool AddnewActivityMember(int actId, string userMail, bool isHead)
        {
            return data.AddnewActivityMember(actId, userMail, isHead);
        }
        
    }
}
