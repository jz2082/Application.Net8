using System.Windows.Input;

using StockApp.Net8.MVVM;
using StockService.Net8.Models;
using StockService.Net8.Services;
using StockApp.Net8.Commands;
using StockAppApiData.Net8.Services;

namespace StockApp.Net8.ViewModels;

public class MajorIndexViewModel(IMajorIndexService majorIndexService) : ViewModelBase
{
    private readonly IMajorIndexService _majorIndexService = majorIndexService;

    private MajorIndex _dowJones;
    private MajorIndex _nasdaq;
    private MajorIndex _sp500;

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

    private void LoadMajorIndexes()
    {
        _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
        {
            if (task.Exception == null)
            {
                DowJones = task.Result;
            }
        });
        _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
        {
            if (task.Exception == null)
            {
                Nasdaq = task.Result;
            }
        });
        _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
        {
            if (task.Exception == null)
            {
                SP500 = task.Result;
            }
        }); 
    }

    public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
    {
        MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);

        majorIndexViewModel.LoadMajorIndexes();

        return majorIndexViewModel;
    }
}
