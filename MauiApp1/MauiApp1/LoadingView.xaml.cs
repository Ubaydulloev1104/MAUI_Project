namespace MauiApp1;

public partial class LoadingView : ContentPage
{
    public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(LoadingView), false);
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }
    public LoadingView()
	{
		InitializeComponent();
        BindingContext = this;
    }
}