// model classes for BMI calculator
// GC

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMICalculator
{
    public enum BMICategory { Underweight, Normal, Overweight, Obese };

    public class BMI
    {
        const double UnderWeightUpperLimit = 18.4;              // inclusive upper limit
        const double NormalWeightUpperLimit = 24.9;
        const double OverWeightUpperLimit = 29.9;               // Obese from 30 +

        // conversion factors from imperial to metric
        const double PoundsToKgs = 0.453592;
        const double InchestoMetres = 0.0254;

        [Display(Name = "Age")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]                              
        public int Age { get; set; }

        [Display(Name = "Weight - Stones")]
        [Range(5, 50, ErrorMessage = "Stones must be between 5 and 50")]                              // max 50 stone
        public int WeightStones { get; set; }

        [Display(Name = "Pounds")]
        [Range(0, 13, ErrorMessage = "Pounds must be between 0 and 13")]                              // 14 lbs in a stone
        public int WeightPounds { get; set; }

        [Display(Name = "Height - Feet")]
        [Range(4, 7, ErrorMessage = "Feet must be between 4 and 7")]                               // max 7 feet
        public int HeightFeet { get; set; }

        [Display(Name = "Inches")]
        [Range(0, 11, ErrorMessage = "Inches must be between 0 and 11")]                              // 12 inches in a foot
        public int HeightInches { get; set; }

        // calculate BMI, display to 2 decimal places
        [Display(Name = "Your BMI is")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BMIValue
        {
            get
            {
                // bmi = weight in Kgs / height in metres squared

                double totalWeightInPounds = (WeightStones * 14) + WeightPounds;
                double totalHeightInInches = (HeightFeet * 12) + HeightInches;

                // do conversions to metric
                double totalWeightInKgs = totalWeightInPounds * PoundsToKgs;
                double totalHeightInMetres = totalHeightInInches * InchestoMetres;

                Console.WriteLine(totalWeightInKgs);
                Console.WriteLine(totalHeightInMetres);
                double bmi = totalWeightInKgs / (Math.Pow(totalHeightInMetres, 2));

                return bmi;
            }
        }
        //

        [Display(Name = "Daily calories intake for both genders is: \n")]
        [DisplayFormat(DataFormatString = "{0:F2}")]

        public Dictionary<string, double> Calories
        {
            get
            {
                double totalWeightInPounds = (WeightStones * 14) + WeightPounds;
                double totalHeightInInches = (HeightFeet * 12) + HeightInches;

                // do conversions to metric
                double totalWeightInKgs = totalWeightInPounds * PoundsToKgs;
                double totalHeightInMetres = totalHeightInInches * InchestoMetres;

                //For men:	BMR = 10 × weight(kg) + 6.25 × height(cm) - 5 × age(y) + 5
                //For women:	BMR = 10 × weight(kg) + 6.25 × height(cm) - 5 × age(y) - 161
                double MaleBMR = 10 * totalWeightInKgs + 6.25 * totalHeightInMetres * 100 - 5 * Age + 5;

                double FemaleBMR = 10 * totalWeightInKgs + 6.25 * totalHeightInMetres * 100 - 5 * Age - 161;

                double[] results = {MaleBMR, FemaleBMR};
                Dictionary<string, double> dictionary = new Dictionary<string, double>();
                dictionary.Add("Male", MaleBMR);
                dictionary.Add("Female", FemaleBMR);



                return dictionary;
            }
        }

        //

        

        // calculate BMI category 
        [Display(Name = "Your BMI Category is")]
        public BMICategory BMICategory
        {
            get
            {
                double bmi = this.BMIValue;

                // calculate BMI category based on upper limits
                if (bmi <= UnderWeightUpperLimit)
                {
                    return BMICategory.Underweight;
                }
                else if (bmi <= NormalWeightUpperLimit)
                {
                    return BMICategory.Normal;
                }
                else if (bmi <= OverWeightUpperLimit)
                {
                    return BMICategory.Overweight;
                }
                else
                {
                    return BMICategory.Obese;
                }
            }
        }
    }
}

