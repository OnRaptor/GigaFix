using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using GigaFix.Data;
using GigaFix.Services;
using GigaFix.ViewModels.Dialogs;
using SukiUI.Controls;

namespace GigaFix.ViewModels.MainViews;

public partial class OrdersListViewModel : PageViewModel
{
    public override string Title => "Список заявок";
    public override string IconName => "mdi-list-box";
    public ObservableCollection<Application> Orders { get; set; } = new();
    [ObservableProperty] private string searchText;
    [ObservableProperty] private string filter;

    private readonly ApplicationsService _applicationsService;
    private List<Application> _cachedOrders;

    public OrdersListViewModel(
        ApplicationsService applicationsService
    )
    {
        _applicationsService = applicationsService;
    }

    private void ApplySort()
    {
        var t = new ObservableCollection<Application>(Orders.OrderByDescending(a => a.DateAddition));
        Orders.Clear();
        Orders.AddRange(t);
    }

    private void ApplyFilter()
    {
        if (Filter == "Не выбрано" || string.IsNullOrEmpty(Filter))
            return;
        var ord = Orders
            .ToList().Where(
                x => x.WorkStatus != null && x.WorkStatus.Equals(Filter, StringComparison.OrdinalIgnoreCase));
        Orders.Clear();
        Orders.Add(ord);
    }

    private void ApplySearch()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;
        var ord = Orders
            .ToList()
            .Where(x => (x.Number + " " + x.Description).Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        Orders.Clear();
        Orders.AddRange(ord);
    }

    partial void OnSearchTextChanged(string? text)
    {
        Orders.Clear();
        Orders.AddRange(_cachedOrders);
        if (string.IsNullOrWhiteSpace(text))
        {
            Orders.Clear();
            Orders.AddRange(_cachedOrders);
            ApplyFilter();
            return;
        }

        ApplyFilter();
        ApplySearch();
        ApplySort();
    }

    async partial void OnFilterChanged(string? text)
    {
        Orders.Clear();
        Orders.AddRange(_cachedOrders);
        if (text != "Не выбрано")
            ApplyFilter();
        else
            ApplySearch();
        ApplySort();
    }

    public override Action? OnNavigate =>
        async () =>
        {
            Orders.Clear();
            Orders.AddRange(new ObservableCollection<Application>(await _applicationsService.GetApplications()));
            ApplySort();
            _cachedOrders = Orders.ToList();
        };

    public void ShowEditDialog(int applicationId)
    {
        var vm = App.GetRequiredService<EditOrderViewModel>();
        vm.Init(applicationId, this);
        SukiHost.ShowDialog(vm, allowBackgroundClose: true);
    }
}