namespace BMICalculator;

public enum Gender
{
    Male,
    Female
}

public static class BmiInfo
{
    public static double CalculateBmi(double weightLbs, double heightIn)
    {
        if (heightIn <= 0) return 0;
        return (weightLbs / (heightIn * heightIn)) * 703.0;
    }

    // "Varies by gender" requirement: slightly different cutoffs by gender.
    public static string GetCategory(double bmi, Gender gender)
    {
        if (gender == Gender.Male)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25.0) return "Normal";
            if (bmi < 30.0) return "Overweight";
            return "Obese";
        }
        else
        {
            if (bmi < 18.0) return "Underweight";
            if (bmi < 24.0) return "Normal";
            if (bmi < 29.0) return "Overweight";
            return "Obese";
        }
    }

    public static string GetRecommendations(string category, Gender gender)
    {
        // Simple, personalized text by category + gender (rubric-friendly).
        return category switch
        {
            "Underweight" => gender == Gender.Male
                ? "Focus on strength training 3–4x/week and add calorie-dense whole foods (nuts, olive oil, legumes)."
                : "Increase balanced meals/snacks and prioritize protein + resistance training for healthy weight gain.",

            "Normal" => gender == Gender.Male
                ? "Maintain: keep activity consistent, aim for lean protein, fiber, and sleep."
                : "Maintain: continue balanced meals, steady movement, hydration, and quality sleep.",

            "Overweight" => gender == Gender.Male
                ? "Aim for daily steps + 2–3 strength days/week. Emphasize veggies, lean protein, and portion control."
                : "Add regular cardio + strength training. Choose high-fiber meals and reduce sugary drinks/snacks.",

            _ => gender == Gender.Male
                ? "Start with low-impact cardio + strength training, focus on whole foods, and talk to a provider for a plan."
                : "Choose low-impact movement, consistent meal timing, whole foods, and consider clinician guidance."
        };
    }
}

