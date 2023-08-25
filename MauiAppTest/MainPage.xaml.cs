
namespace MauiAppTest;

public partial class MainPage : ContentPage
{
    private const string Imc = "IMC";
    private const string IdealWeight = "peso ideal";
    
    private const string PlaceholderHeight = "altura";
    private const string PlaceholderWeight = "peso";
    private const string PlaceholderAge = "edad";

    private double _heightValue;
    private double _weightValue;
    private double _ageValue;
    
    
    public MainPage()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        HiddenMessageError();
    }
    
    private void CalculateImc(object sender, EventArgs e)
    {
        HiddenMessageError();
        
        if (CheckHeight(Imc) && CheckWeight(Imc))
        {
            _heightValue /= 100;
            var imc = Math.Round(_weightValue / (_heightValue * _heightValue), 2);
            ImcButton.Text = $"{imc} es tu IMC";
            SemanticScreenReader.Announce(ImcButton.Text);
        }
    }
    
    private void CalculateIdealWeight(object sender, EventArgs e)
    {
        HiddenMessageError();
        
        if (CheckHeight(IdealWeight) && CheckAge(IdealWeight))
        {
            var idealWeight = _heightValue - 100 - ((_heightValue - 150) / 4) + ((_ageValue - 20) / 4);
            IdealWeightButton.Text = $"{idealWeight} es tu peso ideal";
            SemanticScreenReader.Announce(IdealWeightButton.Text);
        }
    }

    private bool  CheckHeight(string function)
    {
        if (!double.TryParse(height.Text, out var result))
        {
            messageErrorHeight.Text = CalculateMessageError(PlaceholderHeight, function);
            messageErrorHeight.IsVisible = true;
            return false;
        }
        
        _heightValue = result;

        return true;
    }

    private bool CheckWeight(string function)
    {
        if (!double.TryParse(weight.Text, out var result))
        {
            messageErrorWeight.Text = CalculateMessageError(PlaceholderWeight, function);
            messageErrorWeight.IsVisible = true;
            return false;
        }

        _weightValue = result;
        
        return true;
    }
    
    private bool CheckAge(string function)
    {
        if (!double.TryParse(age.Text, out var result))
        {
            messageErrorAge.Text = CalculateMessageError(PlaceholderAge, function);
            messageErrorAge.IsVisible = true;
            return false;
        }
        
        _ageValue = result;
        
        return true;
    }
    
    private string CalculateMessageError(string placeholder, string function)
    {
        return $"El campo {placeholder}, es obligatorio para calcular el {function}.";
    }

    private void HiddenMessageError()
    {
        messageErrorWeight.IsVisible = false;
        messageErrorHeight.IsVisible = false;
        messageErrorAge.IsVisible = false;
    }
}
