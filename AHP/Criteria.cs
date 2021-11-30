using System;
using System.Collections.Generic;
using System.Text;

namespace AHP
{
    enum CriteriaTypes
    {
        Number, rNumber, Preference
    }
    class Criteria
    {
        public string Name { get; set; }
        public CriteriaTypes Type { get; set; }
        public List<string[]> Preferences { get; set; }
        public int Priority { get; set; }
        public int CategoriesAmount { get; set; }
        public (double, double)[] Categories { get; set; }
        public Criteria(string name, CriteriaTypes type, int priority, int categoriesAmount, (double, double)[] categories = null, List<string[]> prefs = null)
        {
            Name = name;
            Type = type;
            Priority = priority;
            CategoriesAmount = categoriesAmount;
            Categories = categories;
            Preferences = prefs;
        }
    }
}
