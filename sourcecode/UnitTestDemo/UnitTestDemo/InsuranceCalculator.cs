using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo;

public static class InsuranceCalculator
{
    public static double CalculatePremium(int age, int accidents)
    {
        if (age < 18)
            throw new ArgumentException("Age must be 18 or older.", nameof(age));
        if (accidents < 0)
            throw new ArgumentException("Accident count cannot be negative.", nameof(accidents));

        double basePremium = 500;
        if (age < 25)
            basePremium *= 2;                  // young drivers pay more
        else if (age > 70)
            basePremium *= 1.5;                // senior drivers surcharge
        else if (age >= 50 && age <= 70)
            basePremium *= 0.8;                // experienced middle-age discount

        if (accidents == 0)
            basePremium -= 200;                // safe driver discount
        else
            basePremium += 100;

        // Minimum premium enforcement
        if (basePremium < 300)
            basePremium = 300;

        return basePremium;
    }
}
