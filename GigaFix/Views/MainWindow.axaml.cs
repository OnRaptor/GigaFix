using System;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SukiUI.Controls;
using SukiUI.MessageBox;

namespace GigaFix.Views;

public partial class MainWindow : SukiWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override async void OnOpened(EventArgs e)
    {
        var db = App.GetRequiredService<AppDbContext>().Database;
        var logger = App.GetRequiredService<ILogger<App>>();
        if (await db.CanConnectAsync())
            logger.LogInformation("Connected to database: " + db.GetDbConnection().DataSource);
        else
            MessageBox.Error(this,
                "Unable to connect to database", "Database connection failed");
        base.OnOpened(e);
    }
}