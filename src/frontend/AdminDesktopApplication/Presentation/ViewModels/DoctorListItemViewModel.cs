using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Models;
using Presentation.Pages.Profiles.Doctors;

namespace Presentation.ViewModels;

[QueryProperty(nameof(DoctorListItem), "DoctorListItem")]
public partial class DoctorListItemViewModel : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<DoctorListItem> _doctorListItems = new(new[]
    {
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John  Williams",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Jeff Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available",
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "David Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Normal Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Miss Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "Ken Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        },
        new DoctorListItem()
        {
            Id = Guid.NewGuid(),
            FullName = "John Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png",
            Specialization = "General",
            Status = "Available"
        }
    });

    [RelayCommand]
    private void Search(string filter)
    {
        FilterList(filter);
    }
    
    [RelayCommand]
    private async Task GoToProfileCreation()
    {
        await Shell.Current.GoToAsync(nameof(DoctorCreationPage), true);
    }

    private void FilterList(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return;
        }

        DoctorListItems = new ObservableCollection<DoctorListItem>
            (DoctorListItems.Where(doctor => doctor.FullName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
    }
}