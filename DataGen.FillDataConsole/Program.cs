using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MongoDB.Driver;
using System.Globalization;
using DataGen.Entities;

namespace DataGen.FillDataConsole
{
    class Program
    {
        private const string ConnectionString = "mongodb://localhost";
        private static readonly MongoClient client = new MongoClient(ConnectionString);
        private static readonly IMongoDatabase db = client.GetDatabase("DataGenTest");
        private const string countryCollectionName = "Countries";

        private static void Main(string[] args)
        {
            //FillCountries();
            FillRegions();
            //FillFirstNames();
            //FillLastNames();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void FillLastNames()
        {
            const string lastNamesFileName = @"d:\_Vadim\DataGen\NamesData\LastNames.csv";
            const string lastNameCollectionName = "LastNames";

            string[] allLines = File.ReadAllLines(lastNamesFileName);
            List<LastName> list = new List<LastName>();
            string previous = null;
            for (int i = 1; i < allLines.Length; i++)
            {
                string name = allLines[i];
                LastName lastName = new LastName();
                if (name == previous) continue;
                lastName.Name = name;
                list.Add(lastName);
                previous = name;
                Console.WriteLine(string.Format("Name = {0}", lastName.Name));
            }
            Console.WriteLine();
            Console.WriteLine();

            db.DropCollection(lastNameCollectionName);
            IMongoCollection<LastName> lastNameCollection = db.GetCollection<LastName>(lastNameCollectionName);
            lastNameCollection.InsertMany(list);
        }

        private static void FillFirstNames()
        {
            const string firstNamesFileName = @"d:\_Vadim\DataGen\NamesData\FirstNames.csv";
            const string firstNameCollectionName = "FirstNames";

            string[] allLines = File.ReadAllLines(firstNamesFileName);
            List<FirstName> list = new List<FirstName>();
            string previous = null;
            for (int i = 1; i < allLines.Length; i++)
            {
                string name = allLines[i];
                FirstName firstName = new FirstName();
                if (name == previous) continue;
                firstName.Name = name;
                list.Add(firstName);
                previous = name;
                Console.WriteLine(string.Format("Name = {0}", firstName.Name));
            }
            Console.WriteLine();
            Console.WriteLine();

            db.DropCollection(firstNameCollectionName);
            IMongoCollection<FirstName> firstNameCollection = db.GetCollection<FirstName>(firstNameCollectionName);
            firstNameCollection.InsertMany(list);
        }

        private static Country FindCountryByIso3(string isoCode3)
        {
            IMongoCollection<Country> countryCollection = db.GetCollection<Country>(countryCollectionName);
            var countryList = countryCollection.Find(c => c.IsoCode3 == isoCode3);
            if (countryList.Count() > 1)
                throw new Exception("There are more than one country with ISO3 = " + isoCode3);
            Country result = countryList.FirstOrDefault();
            return result;
        }

        private static int StrToInt(string s)
        {
            return int.Parse(s.Replace(" ", ""));
        }

        private static double StrToDouble(string s)
        {
            return double.Parse(s.Replace(" ", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }

        private static void FillRegions()
        {
            const string regionsFileName = @"d:\_Vadim\DataGen\NamesData\Regions and Cities.csv";
            const string regionCollectionName = "Regions";
            const string cityCollectionName = "Cities";

            db.DropCollection(cityCollectionName);
            db.DropCollection(regionCollectionName);
            IMongoCollection<Region> regionCollection = db.GetCollection<Region>(regionCollectionName);

            string[] allLines = File.ReadAllLines(regionsFileName);
            List<Region> regionList = new List<Region>();
            List<City> cityList = new List<City>();
            for (int i = 1; i < allLines.Length; i++)
            {
                string[] values = allLines[i].Split(',');
                Region newRegion = new Region();
                newRegion.Name = values[8];
                string isoCode3 = values[7];
                Country country = FindCountryByIso3(isoCode3);
                if (country == null)
                    throw new Exception("There is no country with ISO3 = " + isoCode3);
                newRegion.CountryId = country.Id;
                newRegion.Country = country;
                bool isFound = false;
                for (int j = 0; j < regionList.Count; j++)
                {
                    Region r = regionList[j];
                    if (r.CountryId == newRegion.CountryId && r.Name == newRegion.Name)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    regionList.Add(newRegion);
                    regionCollection.InsertOne(newRegion);
                    Console.WriteLine(string.Format("Country = {0}; Name = {1}", newRegion.Country.Name, newRegion.Name));
                }
                // City
                City newCity = new City
                {
                    Name = values[1],
                    NativeName = values[0],
                    Population = StrToInt(values[4].Replace(".5", "")),
                    Latitude = StrToDouble(values[2]),
                    Longitude = StrToDouble(values[3]),
                    Region = newRegion,
                    RegionId = newRegion.Id,
                    Country = newRegion.Country,
                    CountryId = newRegion.Country.Id
                };
                cityList.Add(newCity);
            }
            Console.WriteLine();
            Console.WriteLine();

            IMongoCollection<City> cityCollection = db.GetCollection<City>(cityCollectionName);
            cityCollection.InsertMany(cityList);

        }

        private static void FillCountries()
        {
            const string countriesFileName = @"d:\_Vadim\DataGen\NamesData\Countries.csv";
            const int countryNameIndex = 0;
            string[] allLines = File.ReadAllLines(countriesFileName);
            List<Country> list = new List<Country>();
            for (int i = 1; i < allLines.Length; i++)
            {
                string[] values = allLines[i].Split(';');
                string countryName = values[countryNameIndex];
                Country country = new Country();
                country.Name = countryName;
                country.PhoneCode = values[1];
                string[] isoCodes = values[2].Split('/');
                country.IsoCode2 = isoCodes[0].Trim();
                country.IsoCode3 = isoCodes[1].Trim();
                country.Population = StrToInt(values[3]);
                list.Add(country);
                Console.WriteLine(string.Format("Name = {0}; Phone code = {1}; ISO2 = {2}; ISO3 = {3}; Population = {4}",
                    country.Name, country.PhoneCode, country.IsoCode2, country.IsoCode3, country.Population));
            }
            Console.WriteLine();
            Console.WriteLine();

           db.DropCollection(countryCollectionName);
            IMongoCollection<Country> countryCollection = db.GetCollection<Country>(countryCollectionName);
            countryCollection.InsertMany(list);
        }
    }
}