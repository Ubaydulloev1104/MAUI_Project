namespace MauiApp1;

public partial class LoadingView : ContentView
{
    // �������� ��� ���������� ����������
    public static readonly BindableProperty IsVisibleProperty =
        BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(LoadingView), false, propertyChanged: OnIsVisibleChanged);

    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    public LoadingView()
    {
        InitializeComponent();
    }

    // ��������� ��������� �������� IsVisible
    private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LoadingView loadingView)
        {
            loadingView.IsVisible = (bool)newValue;
        }
    }
}
