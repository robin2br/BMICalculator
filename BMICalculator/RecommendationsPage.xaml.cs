namespace BMICalculator;

public partial class RecommendationsPage : ContentPage
{
    public RecommendationsPage(double bmi, string category, Gender gender)
    {
        InitializeComponent();

        HeaderLabel.Text = $"BMI: {bmi} | {gender} | Category: {category}";
        RecoLabel.Text = BmiInfo.GetRecommendations(category, gender);
    }

    private async void BackToResult_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // back to Page 2
    }

    private async void BackToInput_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(); // back to Page 1
    }
}
