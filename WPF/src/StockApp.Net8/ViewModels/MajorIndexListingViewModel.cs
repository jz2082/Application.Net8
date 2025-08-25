using System.Windows.Input;

using StockApp.Net8.MVVM;
using StockService.Net8.Models;
using StockService.Net8.Services;
using StockApp.Net8.Commands;

namespace StockApp.Net8.ViewModels;

public class MajorIndexListingViewModel : ViewModelBase
{
    private MajorIndex _dowJones;
    public MajorIndex DowJones
    {
        get
        {
            return _dowJones;
        }
        set
        {
            _dowJones = value;
            OnPropertyChanged();
        }
    }

    private MajorIndex _nasdaq;
    public MajorIndex Nasdaq
    {
        get
        {
            return _nasdaq;
        }
        set
        {
            _nasdaq = value;
            OnPropertyChanged();
        }
    }

    private MajorIndex _sp500;
    public MajorIndex SP500
    {
        get
        {
            return _sp500;
        }
        set
        {
            _sp500 = value;
            OnPropertyChanged();
        }
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get
        {
            return _isLoading;
        }
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadMajorIndexesCommand { get; }

    public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
    {
        LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
    }

    public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
    {
        MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

        majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

        return majorIndexViewModel;
    }
}
