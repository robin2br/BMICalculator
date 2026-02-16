namespace BMICalculator;

public partial class BmiResultPage : ContentPage
{
    private readonly double _weight;
    private readonly double _height;
    private readonly Gender _gender;

    private readonly double _bmi;
    private readonly string _category;

    public BmiResultPage(double weightLbs, double heightIn, Gender gender)
    {
        InitializeComponent();

        _weight = weightLbs;
        _height = heightIn;
        _gender = gender;

        _bmi = Math.Round(BmiInfo.CalculateBmi(_weight, _height), 1);
        _category = BmiInfo.GetCategory(_bmi, _gender);

        BmiValueLabel.Text = $"BMI: {_bmi}";
        CategoryLabel.Text = $"Category ({_gender}): {_category}";
        SummaryLabel.Text = $"Weight: {_weight} lbs | Height: {_height} in";
    }

    private async void GoToRecommendations_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecommendationsPage(_bmi, _category, _gender));
    }

    private async void BackToInput_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // back to Page 1
    }
}
