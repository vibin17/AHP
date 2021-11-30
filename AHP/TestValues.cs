using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AHP
{
    class TestValues
    {
        private List<Criteria> Criterias { get; set; } = new List<Criteria>()
            {
                new Criteria("Brand", CriteriaTypes.Preference, 4, 2, null, new List<string[]>() { new string[] {"Apple", "Samsung" } }),
                new Criteria("Year", CriteriaTypes.Number, 4, 3),
                new Criteria("Price", CriteriaTypes.rNumber, 1, 4, new (double, double)[] { (0, 15000), (15000, 30000), (30000, 60000), (60000, 200000) }),
                new Criteria("OS", CriteriaTypes.Preference, 3, 2, null, new List<string[]>() { new string[] {"iOS"} }),
                new Criteria("Antutu", CriteriaTypes.Number, 1, 4, new (double, double)[] { (0, 350000), (350000, 550000), (550000, 800000), (800000, 1000000) }),
                new Criteria("Storage", CriteriaTypes.Number, 3, 4, new (double, double)[] { (64, 64), (128, 128), (256, 256), (512, 512) }),
                new Criteria("Battery", CriteriaTypes.Number, 4, 4, new (double, double)[] { (80, 90), (90, 100), (100, 120), (120, 150) }),
                new Criteria("Photo", CriteriaTypes.Number, 2, 5),
                new Criteria("Screen", CriteriaTypes.Number, 1, 5)
            };
        public List<Alternative> Alternatives { get; } = new List<Alternative>()
        {
                new Alternative("iPhone 13", new Dictionary<string, string>()
                {
                        { "Brand", "Apple" }, {"Year", "2021"},
                        { "Price", "90000" }, {"OS", "iOS" },
                        { "Antutu", "843813" }, {"Storage", "256" },
                        { "Battery", "89"}, {"Photo", "5"}, { "Screen", "4"}
                }),
                new Alternative("Samsung S21", new Dictionary<string, string>()
                {
                        {"Brand", "Samsung"}, {"Year", "2021"},
                        { "Price", "113000" }, {"OS", "Android"},
                        { "Antutu", "774302" }, {"Storage", "512" },
                        { "Battery", "93" }, {"Photo", "5" }, {"Screen", "5" }
                }),
                new Alternative("Oneplus 9",  new Dictionary<string, string>()
                {
                        {"Brand", "Oneplus"}, {"Year", "2021"},
                        { "Price", "39000" }, {"OS", "Android"},
                        { "Antutu", "765843" }, {"Storage", "128" },
                        { "Battery", "87" }, {"Photo", "4" }, {"Screen", "4" }
                }),
                    new Alternative("Oneplus 9 Pro", new Dictionary<string, string>()
                {
                        {"Brand", "Oneplus"}, {"Year", "2021"},
                        { "Price", "60400" }, {"OS", "Android"},
                        { "Antutu", "788255" }, {"Storage", "256" },
                        { "Battery", "87" }, {"Photo", "4" }, {"Screen", "5" }
                }),
                    new Alternative("Pixel 6 Pro", new Dictionary<string, string>()
                {
                        {"Brand", "Oneplus"}, {"Year", "2021"},
                        { "Price", "100000" }, {"OS", "Android"},
                        { "Antutu", "720000" }, {"Storage", "128" },
                        { "Battery", "84" }, {"Photo", "3" }, {"Screen", "4" }
                }),
                    new Alternative("iPhone 11", new Dictionary<string, string>()
                {
                        {"Brand", "Apple"}, {"Year", "2019"},
                        { "Price", "55000" }, {"OS", "iOS"},
                        { "Antutu", "507467" }, {"Storage", "128" },
                        { "Battery", "94" }, {"Photo", "3" }, {"Screen", "3" }
                }),
                    new Alternative("Samsung A52", new Dictionary<string, string>()
                {
                        {"Brand", "Apple"}, {"Year", "2021"},
                        { "Price", "28000" }, {"OS", "Android"},
                        { "Antutu", "259353" }, {"Storage", "128" },
                        { "Battery", "111" }, {"Photo", "2" }, {"Screen", "4" }
                }),
                    new Alternative("Xiaomi Mi 11 Lite", new Dictionary<string, string>()
                {
                        {"Brand", "Xiaomi"}, {"Year", "2021"},
                        { "Price", "28000" }, {"OS", "Android"},
                        { "Antutu", "524958" }, {"Storage", "128" },
                        { "Battery", "100" }, {"Photo", "2" }, {"Screen", "2" }
                }),
                    new Alternative("Sony Xperia 1 III", new Dictionary<string, string>()
                {
                        {"Brand", "Sony"}, {"Year", "2021"},
                        { "Price", "85000" }, {"OS", "Android"},
                        { "Antutu", "732567" }, {"Storage", "256" },
                        { "Battery", "82" }, {"Photo", "4" }, {"Screen", "4" }
                }),
                    new Alternative("Xiaomi Redmi Note 9", new Dictionary<string, string>()
                {
                        {"Brand", "Xiaomi"}, {"Year", "2020"},
                        { "Price", "13000" }, {"OS", "Android"},
                        { "Antutu", "337265" }, {"Storage", "64" },
                        { "Battery", "125" }, {"Photo", "1" }, {"Screen", "1" }
                })
        };
        public double[,] GetAlternativesRated()
        {
            double[,] alternativesCritValues = new double[Criterias.Count, Alternatives.Count];
            for (int i = 0; i < Criterias.Count; i++)
            {
                if (Criterias[i].Type == CriteriaTypes.Number || Criterias[i].Type == CriteriaTypes.rNumber)
                {
                    var max = Alternatives.Max(x => Convert.ToDouble(x.Criterias[Criterias[i].Name]));
                    var min = Alternatives.Min(x => Convert.ToDouble(x.Criterias[Criterias[i].Name]));
                    var categoriesAmount = Criterias[i].CategoriesAmount;
                    var categories = new (double, double)[categoriesAmount];
                    if (Criterias[i].Categories != null)
                    {
                        categories = Criterias[i].Categories;
                    } else
                    {
                        var diff = (max - min) / (float)categoriesAmount;
                        var cur = min;
                        for (int k = 0; k < categoriesAmount; k++)
                        {
                            categories[k] = (cur, cur + diff);
                            cur += diff;
                        }
                    }
                    if (Criterias[i].Type == CriteriaTypes.Number)
                    {
                        categories = categories.Reverse().ToArray();
                    }
                    for (int j = 0; j < Alternatives.Count; j++)
                    {
                        for (int k = 0; k < categoriesAmount; k++)
                        {
                            if (Convert.ToDouble(Alternatives[j].Criterias[Criterias[i].Name]) >= categories[k].Item1 &&
                                Convert.ToDouble(Alternatives[j].Criterias[Criterias[i].Name]) <= categories[k].Item2)
                            {
                                alternativesCritValues[i, j] = k + 1;
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < Alternatives.Count; j++)
                    {
                        alternativesCritValues[i, j] = Criterias[i].Preferences.Count + 1;
                        for (int k = 0; k < Criterias[i].Preferences.Count; k++)
                        {
                            if (Criterias[i].Preferences[k].Contains(Alternatives[j].Criterias[Criterias[i].Name])) {
                                alternativesCritValues[i, j] = k + 1;
                            }
                        }
                    }
                }
            }
            return alternativesCritValues;
        }
        public double[] GetCrits()
        {
            var critsPriorities = new double[Criterias.Count];
            for (int i = 0; i < Criterias.Count; i++)
            {
                critsPriorities[i] = Criterias[i].Priority;
            }
            return critsPriorities;
        }
        public double[,] GetItems()
        {
            var n = GetCrits().Length;
            double[,] itemsCritPriorities = new double[n, Alternatives.Count];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < Alternatives.Count; j++)
                {
                    itemsCritPriorities[i, j] = Convert.ToDouble(Alternatives[j].Criterias[Criterias[i].Name]);
                }
            }
            return itemsCritPriorities;
        }
    }
}
