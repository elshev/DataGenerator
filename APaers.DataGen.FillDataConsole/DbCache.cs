using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using APaers.DataGen.Entities;
using MongoDB.Bson;

namespace APaers.DataGen.FillDataConsole
{
    internal class DbCache
    {
        public List<FirstName> FirstNames { get; set; }
        public List<LastName> LastNames { get; set; }
        public List<Country> Countries { get; set; }
        public List<Region> Regions { get; set; }
        public List<City> Cities { get; set; }
        public List<RandomText> RandomTexts { get; set; }

        private const string sourceFolder = @"d:\_Proj\DataGenerator\NamesData\";

        public void FillFromFiles()
        {
            FirstNames = CreateNameCollection<FirstName>(@"FirstNames.csv");
            LastNames = CreateNameCollection<LastName>(@"LastNames.csv");
            Countries = CreateNameCollection<Country>(@"Countries.csv", FillCountry);
            FillRegionsAndCities();
            CheckAndClean();
            FillRandomText();
        }

        private string GetNewId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        private void FillCountry(Country country, string line)
        {
            if (line == null) 
                throw new ArgumentNullException(nameof(line));
            string[] items = line.Split(';');
            country.Name = items[0].Trim();
            country.MinZip = 100000;
            country.MaxZip = 999999;
            country.PhoneCode = items[1].Trim();
            string[] isoCodes = items[2].Split('/');
            country.IsoCode = isoCodes[0].Trim();
            country.IsoCode3 = isoCodes[1].Trim();
        }

        private List<TEntity> CreateNameCollection<TEntity>(string fileName, Action<TEntity, string> fillEntityAction = null)
            where TEntity : NamedEntityBase, new()
        {
            Console.WriteLine("Creating collection of type: {0}", typeof(TEntity));
            int count = 0;
            List<TEntity> names = new List<TEntity>();
            fileName = Path.Combine(sourceFolder, fileName);
            foreach (string line in File.ReadLines(fileName))
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }
                TEntity entity = new TEntity {Id = GetNewId()};
                if (fillEntityAction == null)
                    entity.Name = line;
                else
                    fillEntityAction(entity, line);
                if (!string.IsNullOrWhiteSpace(entity.Name))
                    names.Add(entity);
                count++;
            }
            Console.WriteLine("{0} count = {1}", typeof(TEntity), count);
            return names;
        }

        private Country FindCountryByIso3(string isoCode3)
        {
            List<Country> countryList = Countries.Where(c => c.IsoCode3 == isoCode3).ToList();
            if (countryList.Count > 1)
                throw new Exception("There are more than one country with ISO3 = " + isoCode3);
            Country result = countryList[0];
            return result;
        }

        private int StrToInt(string s)
        {
            return int.Parse(s.Replace(" ", ""));
        }

        private double StrToDouble(string s)
        {
            return double.Parse(s.Replace(" ", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }

        private void FillRegionsAndCities()
        {
            string fileName = Path.Combine(sourceFolder, "Regions and Cities.csv");

            string[] allLines = File.ReadAllLines(fileName);
            List<Region> regionList = new List<Region>();
            List<City> cityList = new List<City>();
            for (int i = 1; i < allLines.Length; i++)
            {
                string[] values = allLines[i].Split(',');
                Region newRegion = new Region {Id = GetNewId(), Name = values[8]};
                string isoCode3 = values[7];
                Country country = FindCountryByIso3(isoCode3);
                if (country == null)
                    throw new Exception("There is no country with ISO3 = " + isoCode3);
                newRegion.CountryId = country.Id;
                newRegion.Country = country;
                Region region = regionList.FirstOrDefault(r => r.CountryId == newRegion.CountryId && r.Name == newRegion.Name);
                if (region == null)
                {
                    regionList.Add(newRegion);
                    Console.WriteLine($"Country = {newRegion.Country.Name}; Region = {newRegion.Name}");
                    region = newRegion;
                }
                // City
                City newCity = new City
                {
                    Id = GetNewId(),
                    Name = values[1],
                    LocalName = values[0],
                    Population = StrToInt(values[4].Replace(".5", "")),
                    Latitude = StrToDouble(values[2]),
                    Longitude = StrToDouble(values[3]),
                    Region = region,
                    RegionId = region.Id,
                    Country = region.Country,
                    CountryId = region.Country.Id
                };
                cityList.Add(newCity);
            }
            Console.WriteLine();
            Console.WriteLine();

            Regions = regionList;
            Cities = cityList;
        }

        private void CheckAndClean()
        {
            // Cities with empty regions
            var cityWithEmptyRegion = Cities.FirstOrDefault(city => string.IsNullOrWhiteSpace(city.RegionId) || city.RegionId.All(ch => ch == '0'));
            if (cityWithEmptyRegion != null)
                throw new Exception($"There is '{cityWithEmptyRegion}' city with empty Region");

            // Countries with no regions
            for (int i = Countries.Count - 1; i >= 0; i--)
            {
                Country country = Countries[i];
                var region = Regions.FirstOrDefault(r => country.Id.Equals(r.CountryId));
                if (region == null)
                {
                    Countries.RemoveAt(i);
                    Console.WriteLine($"'{country.Name}' has no regions and was deleted");
                }
            }
        }

        private void FillRandomText()
        {
            string[] fileNames = { "LoremIpsum.txt" };

            List<RandomText> randomTexts = new List<RandomText>();
            foreach (string fileName in fileNames)
            {
                Console.WriteLine($"Process file: {fileName}");
                string filePath = Path.Combine(sourceFolder, fileName);
                string text = File.ReadAllText(filePath);
                RandomText randomText = new RandomText { Name = Path.GetFileNameWithoutExtension(fileName), Text = text };
                randomTexts.Add(randomText);
            }

            Console.WriteLine();
            Console.WriteLine();

            RandomTexts = randomTexts;
        }

    }
}
