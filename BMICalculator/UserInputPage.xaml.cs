namespace BMICalculator;

public partial class UserInputPage : ContentPage
{
    private Gender? _selectedGender = null;

    public UserInputPage()
    {
        InitializeComponent();

        // tap gestures
        var maleTap = new TapGestureRecognizer();
        maleTap.Tapped += (_, __) => SelectGender(Gender.Male);
        MaleFrame.GestureRecognizers.Add(maleTap);

        var femaleTap = new TapGestureRecognizer();
        femaleTap.Tapped += (_, __) => SelectGender(Gender.Female);
        FemaleFrame.GestureRecognizers.Add(femaleTap);

        // initial label values
        UpdateWeightLabel(WeightSlider.Value);
        UpdateHeightLabel(HeightSlider.Value);
    }

    private void SelectGender(Gender gender)
    {
        _selectedGender = gender;

        // simple visual highlight
        MaleFrame.BorderColor = (gender == Gender.Male) ? Colors.Green : Colors.Gray;
        FemaleFrame.BorderColor = (gender == Gender.Female) ? Colors.Green : Colors.Gray;

        StatusLabel.Text = "";
    }

    private void WeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        => UpdateWeightLabel(e.NewValue);

    private void HeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        => UpdateHeightLabel(e.NewValue);

    private void UpdateWeightLabel(double value)
    {
        int lbs = (int)Math.Round(value);
        WeightLabel.Text = $"Weight: {lbs} lbs";
    }

    private void UpdateHeightLabel(double value)
    {
        int inches = (int)Math.Round(value);
        HeightLabel.Text = $"Height: {inches} in";
    }

    private async void GoToResult_Clicked(object sender, EventArgs e)
    {
        if (_selectedGender is null)
        {
            StatusLabel.Text = "Please select a gender by tapping an image.";
            return;
        }

        double weight = Math.Round(WeightSlider.Value);
        double height = Math.Round(HeightSlider.Value);

        var resultPage = new BmiResultPage(weight, height, _selectedGender.Value);
        await Navigation.PushAsync(resultPage);
    }
}
