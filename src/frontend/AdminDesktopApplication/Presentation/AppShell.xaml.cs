namespace Presentation;

public partial class AppShell
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