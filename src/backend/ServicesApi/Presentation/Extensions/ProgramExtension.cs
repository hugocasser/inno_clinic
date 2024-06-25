        builder
            .Services
            .AddDataLayer()
            .AddBusinessLayer();
        app.MapServicesEndpoints();
        app.MapSpecializationsEndpoints();
