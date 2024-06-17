using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.ViewModels;

namespace Presentation.Pages.Profiles.Patients;

public partial class PatientViewAsDoctor : ContentPage
{
    private readonly PatientListItemViewModel _patientListItemViewModel;
    public PatientViewAsDoctor(PatientListItemViewModel patientListItemViewModel)
    {
        InitializeComponent();

        BindingContext = _patientListItemViewModel = patientListItemViewModel;
    }
}