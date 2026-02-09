using System;

namespace BMICalculator;

public partial class MainPage : ContentPage
{
    private enum GenderType { None, Male, Female }
    private GenderType _gender = GenderType.None;

    public MainPage()
    {
        InitializeComponent();

        // Show default slider values immediately
        HeightValueLabel.Text = ((int)HeightSlider.Value).ToString();
        WeightValueLabel.Text = ((int)WeightSlider.Value).ToString();

        UpdateGenderHighlight();
    }

    private void MaleTapped(object sender, TappedEventArgs e)
    {
        _gender = GenderType.Male;
        UpdateGenderHighlight();
    }

    private void FemaleTapped(object sender, TappedEventArgs e)
    {
        _gender = GenderType.Female;
        UpdateGenderHighlight();
    }

    private void UpdateGenderHighlight()
    {
        // Simple visual highlight: border + background
        MaleFrame.BorderColor = _gender == GenderType.Male ? Colors.Black : Colors.LightGray;
        MaleFrame.BackgroundColor = _gender == GenderType.Male ? Colors.LightGray : Colors.Transparent;

        FemaleFrame.BorderColor = _gender == GenderType.Female ? Colors.Black : Colors.LightGray;
        FemaleFrame.BackgroundColor = _gender == GenderType.Female ? Colors.LightGray : Colors.Transparent;
    }

    private void HeightChanged(object sender, ValueChangedEventArgs e)
    {
        HeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    private void WeightChanged(object sender, ValueChangedEventArgs e)
    {
        WeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    private async void CalculateClicked(object sender, EventArgs e)
    {
        if (_gender == GenderType.None)
        {
            await DisplayAlert("Missing Info", "Please select a gender.", "OK");
            return;
        }

        double height = HeightSlider.Value;  // inches
        double weight = WeightSlider.Value;  // pounds

        if (height <= 0)
        {
            await DisplayAlert("Invalid Height", "Height must be greater than 0.", "OK");
            return;
        }

        // BMI = (weight * 703) / (height^2)
        double bmi = (weight * 703.0) / (height * height);
        double bmiRounded = Math.Round(bmi, 1);

        string status = GetHealthStatus(_gender, bmi);
        string recs = GetRecommendations(status);

        string genderText = _gender == GenderType.Male ? "Male" : "Female";

        await DisplayAlert("Your calculated BMI results are:",
            $"Gender: {genderText}\n" +
            $"BMI: {bmiRounded}\n" +
            $"Health Status: {status}\n" +
            $"Recommendations:\n- {recs.Replace("\n", "\n- ")}",
            "Ok");
    }

    private static string GetHealthStatus(GenderType gender, double bmi)
    {
        if (gender == GenderType.Male)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal Weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
        else // Female
        {
            if (bmi < 18) return "Underweight";
            if (bmi < 24) return "Normal Weight";
            if (bmi < 29) return "Overweight";
            return "Obese";
        }
    }

    private static string GetRecommendations(string status)
    {
        // Match your rubric table wording (male & female same recommendations)
        return status switch
        {
            "Underweight" =>
                "Increase calorie intake with nutrient-rich foods (e.g., nuts, lean protein, whole grains).\n" +
                "Incorporate strength training to build muscle mass.\n" +
                "Consult a nutritionist if needed.",

            "Normal Weight" =>
                "Maintain a balanced diet with proteins, healthy fats, and fiber.\n" +
                "Stay physically active with at least 150 minutes of exercise per week.\n" +
                "Keep regular check-ups to monitor overall health.",

            "Overweight" =>
                "Reduce processed foods and focus on portion control.\n" +
                "Engage in regular aerobic exercises (e.g., jogging, swimming) and strength training.\n" +
                "Drink plenty of water and track your progress.",

            "Obese" =>
                "Consult a doctor for personalized guidance.\n" +
                "Start with low-impact exercises (e.g., walking, cycling).\n" +
                "Follow a structured weight-loss meal plan and consider behavioral therapy for lifestyle changes.\n" +
                "Avoid sugary drinks and maintain a consistent sleep schedule.",

            _ => "No recommendation available."
        };
    }
}

