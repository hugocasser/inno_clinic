namespace Presentation;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        if (Window != null)
        {
            Window.MaximumHeight = 800;
            Window.MaximumWidth = 500;
        }
    }
}