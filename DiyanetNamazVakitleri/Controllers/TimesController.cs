using DiyanetNamazVakitleri.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DiyanetNamazVakitleri.Controllers
{
    public class TimesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPrayerTimes(int? stateID)
        {
            if (stateID == null) return Json("State ID is null");
            Times tm = new Times();
            try
            {
                Uri url = new Uri("https://namazvakitleri.diyanet.gov.tr/tr-TR/" + stateID);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                string dt = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'ti-miladi')]").InnerText.Trim();
                tm.Date = dt+" " + DateTime.Now.Year;

                HtmlNodeCollection times = doc.DocumentNode.SelectSingleNode("//div[@id='today-pray-times-row']").SelectNodes("//div[contains(@class,'w3-col m2 s4')]");
                foreach (HtmlNode time in times)
                {
                    string vakitName = time.SelectSingleNode("div").Attributes["data-vakit-name"].Value;
                    switch (vakitName)
                    {
                        case "imsak":
                            tm.Imsak = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                        case "gunes":
                            tm.Gunes = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                        case "ogle":
                            tm.Ogle = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                        case "ikindi":
                            tm.Ikindi = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                        case "aksam":
                            tm.Aksam = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                        case "yatsi":
                            tm.Yatsi = time.SelectSingleNode("div").SelectSingleNode("div[contains(@class,'tpt-time')]").InnerText;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("An error occured");
            }
            return Json(tm);
        }

        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            List<Countries> countries = new List<Countries>();
            try
            {
                Uri url = new Uri("https://namazvakitleri.diyanet.gov.tr/");
                WebClient client = new WebClient();
                string html = client.DownloadString(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNodeCollection cs = doc.DocumentNode.SelectSingleNode("//select[@name='country']").SelectNodes("//option");
                foreach (HtmlNode ctry in cs)
                {
                    Countries ct = new Countries();
                    ct.Country = ctry.InnerText;
                    ct.ID = int.Parse(ctry.Attributes["value"].Value);
                    countries.Add(ct);
                }
            }
            catch (Exception)
            {
                return Json("An error occured");
            }
            return Json(countries);
        }
        [HttpGet]
        public IHttpActionResult GetCities(int? countryID)
        {
            if (countryID == null) return Json("Country ID is null");
            List<Cities> cities = new List<Cities>();
            try
            {
                Uri url = new Uri("https://namazvakitleri.diyanet.gov.tr/tr-TR/home/GetRegList?ChangeType=country&CountryId=" + countryID + "&Culture=tr-TR");
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);

                JObject obj = JObject.Parse(json);
                var stateList = obj["StateList"].Children();
                foreach (var item in stateList)
                {
                    Cities city = new Cities();
                    city.CityName = item.Value<string>("SehirAdi");
                    city.CountryID = (int)countryID;
                    city.ID = Convert.ToInt32(item.Value<string>("SehirID"));
                    cities.Add(city);
                }
            }
            catch (Exception)
            {
                return Json("An error occured");
            }
            return Json(cities);
        }

        [HttpGet]
        public IHttpActionResult GetDistricts(int? countryID, int? cityID)
        {
            if (countryID == null) return Json("Country ID is null");
            if (cityID == null) return Json("City ID is null");
            List<Districts> districts = new List<Districts>();
            try
            {
                Uri url = new Uri("https://namazvakitleri.diyanet.gov.tr/tr-TR/home/GetRegList?ChangeType=state&CountryId=" + countryID + "&Culture=tr-TR&StateId=" + cityID);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);

                JObject obj = JObject.Parse(json);
                var stateRegList = obj["StateRegionList"].Children();
                foreach (var item in stateRegList)
                {
                    Districts rg = new Districts();
                    rg.CityID = (int)cityID;
                    rg.CountryID = (int)countryID;
                    rg.ID = Convert.ToInt32(item.Value<string>("IlceID"));
                    rg.DistrictName = item.Value<string>("IlceAdi");
                    districts.Add(rg);
                }
            }
            catch (Exception)
            {
                return Json("An error occured");
            }
            return Json(districts);
        }
    }
}
