using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace InterviewDemo
{
    public partial class _Default : Page
    {
        static HttpClient client = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadJson();
        }
        public void LoadJson()
        {

            string filepath = Server.MapPath("~/data.json");
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                List<profile> items = JsonConvert.DeserializeObject<List<profile>>(json);

                //Question 1 
                var lstAge = (from x in items
                              where x.age > 50
                              select x).ToList();
                q1.InnerHtml += "<span class=\"bold\">" + lstAge.Count.ToString() + " total individuals are over the age of 50." + "</span>";
                //Question 2
                var lstLastActive = (from x in items
                                     orderby x.registered descending
                                     select x).First();
                q2.InnerHtml += "<span class=\"bold\">" + lstLastActive.name.first + " " + lstLastActive.name.last + " registered on " + lstLastActive.registered.ToShortDateString() + "</span>";
                //Question 3
                var lstFavFruit = (from x in items
                                   group x.favoriteFruit by x.favoriteFruit into g
                                   select new { Fruit = g.Key, CNT = g.Count() }).ToList();

                string list = string.Empty;
                for (int i = 0; i < lstFavFruit.Count; i++)
                {
                    list += lstFavFruit[i].Fruit + ":" + lstFavFruit[i].CNT.ToString() + " ";
                }
                q3.InnerHtml += "<span class=\"bold\">" + list + "</span>";
                //Question 4
                var lstCommonEyeColor = (from x in items
                                         group x.eyeColor by x.eyeColor into g
                                         orderby g.Count() descending
                                         select g).ToList();
                q4.InnerHtml += "<span class=\"bold\">" + lstCommonEyeColor[0].Key + ":" + lstCommonEyeColor[0].Count().ToString() + "</span>";
                //Question 5
                double balance = 0;
                foreach (profile p in items)
                {

                    var t = double.Parse(p.balance, NumberStyles.Currency);
                    balance += t;

                }
                q5.InnerHtml += "<span class=\"bold\">" + balance.ToString("C0") + "</span>";
                //Question 6
                var lstFullName = (from x in items
                                   where x.id == "5aabbca3e58dc67745d720b1"
                                   select x).First();


                string fullName = lstFullName.name.last + "," + lstFullName.name.first;

                q6.InnerHtml += "<span class=\"bold\">" + fullName + "</span>";

            }
        }
    }
    public class profile
    {


        public string favoriteFruit { get; set; }
        public string greeting { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public DateTime registered { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public fullname name { get; set; }
        public string eyeColor { get; set; }
        public int age { get; set; }
        public string balance { get; set; }
        public bool isActive { get; set; }
        public string id { get; set; }
    }

    public class fullname
    {

        public string last { get; set; }
        public string first { get; set; }

    }
}