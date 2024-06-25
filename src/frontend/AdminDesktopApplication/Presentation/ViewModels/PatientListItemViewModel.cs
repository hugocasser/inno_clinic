using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.Models;
using Presentation.Pages.Profiles.Patients;

namespace Presentation.ViewModels;

[QueryProperty(nameof(PatientListItem), "PatientListItem")]
public partial class PatientListItemViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<PatientListItem> _patientListItems = new(new[]
    {
        new PatientListItem()
        {
            FullName = "John Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "John  Williams",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "Jeff Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "David Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "Normal Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "Miss Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "Ken Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "John Fred Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "John Ken Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "John Ken Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
        },
        new PatientListItem()
        {
            FullName = "John Ken Smith",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Photo = "empty_profile_photo.png"
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
        await Shell.Current.GoToAsync(nameof(PatientCreationPage), true);
    }

    private void FilterList(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
        {
            return;
        }

        PatientListItems = new ObservableCollection<PatientListItem>(PatientListItems
            .Where(patient => patient.FullName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)));
    }
}