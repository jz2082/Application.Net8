using StockApp.Net8.MVVM;

namespace StockApp.Net8.ViewModels;

public class HomeViewModel : ViewModelBase
{
    //public AssetSummaryViewModel AssetSummaryViewModel { get; }
    //public MajorIndexListingViewModel MajorIndexListingViewModel { get; }

    //public HomeViewModel(AssetSummaryViewModel assetSummaryViewModel, MajorIndexListingViewModel majorIndexListingViewModel)
    //{
    //    AssetSummaryViewModel = assetSummaryViewModel;
    //    MajorIndexListingViewModel = majorIndexListingViewModel;
    //}

    

    //public override void Dispose()
    //{
    //    AssetSummaryViewModel.Dispose();
    //    MajorIndexListingViewModel.Dispose();

    //    base.Dispose();
    //}

    public MajorIndexViewModel MajorIndexViewModel { get; set; }

    public HomeViewModel(MajorIndexViewModel majorIndexViewModel)
    {
        MajorIndexViewModel = majorIndexViewModel;
    }
}
