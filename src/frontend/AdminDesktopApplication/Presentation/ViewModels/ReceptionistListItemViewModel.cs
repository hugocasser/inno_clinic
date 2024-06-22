using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Models;

namespace Presentation.ViewModels;

[QueryProperty(nameof(ReceptionistListItem),"ReceptionistListItem")]
public partial class ReceptionistListItemViewModel : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<ReceptionistListItem> _receptionistListItems = new(new[]
    {
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John  Williams",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Jeff Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "David Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Normal Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John  Williams",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Jeff Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "David Smith",
            Photo = "empty_profile_photo.png",
        },
        new ReceptionistListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Normal Smith",
            Photo = "empty_profile_photo.png",
        }
    });

    [RelayCommand]
    private void Search(string filter)
    {
        FilterList(filter);
    }

    private void FilterList(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return;
        }

        ReceptionistListItems = new ObservableCollection<ReceptionistListItem>(
            ReceptionistListItems.Where(doctor =>
                doctor.FullName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
    }
}