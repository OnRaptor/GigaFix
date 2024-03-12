using System;
using System.Collections;
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
    public ObservableCollection<Application> Orders { get; set; } = new ();
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

    private void ApplyFilter()
    {
        if (Filter == "Не выбрано" || string.IsNullOrEmpty(Filter))
            return;
        if (Orders.Count == 0)
            Orders.AddRange(_cachedOrders);
        var ord = Orders
            .ToList().Where(x => x.WorkStatus != null && x.WorkStatus.Equals(Filter, StringComparison.OrdinalIgnoreCase));
        Orders.Clear();
        Orders.Add(ord);
    }

    private void ApplySearch()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;
        if (Orders.Count == 0)
            Orders.AddRange(_cachedOrders);
        var ord = Orders
            .ToList()
            .Where(x => (x.Number + " " + x.Description).Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        Orders.Clear();
        Orders.AddRange(ord);
    }
    async partial void OnSearchTextChanged(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            Orders.Clear();
            Orders.AddRange(_cachedOrders);
            ApplyFilter();
            return;
        }
        
        ApplyFilter();
        ApplySearch();
    }

    async partial void OnFilterChanged(string? text)
    {
        if (text != "Не выбрано")
        {
            ApplyFilter();
        }
        else
        {
            Orders.Clear();
            Orders.AddRange(_cachedOrders);
            ApplySearch();
        }
    }
    public override Action? OnNavigate =>
        async () =>
        {
            Orders.Clear();
            Orders.AddRange(new ObservableCollection<Application>(await _applicationsService.GetApplications()));
            _cachedOrders = Orders.ToList();
        };

    public void ShowEditDialog(int applicationId)
    {
        var vm = App.GetRequiredService<EditOrderViewModel>();
        vm.Init(applicationId);
        SukiHost.ShowDialog(vm, allowBackgroundClose:true);
    }
}