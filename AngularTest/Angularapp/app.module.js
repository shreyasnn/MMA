//Angular Module JS contains all components,Modules,Service and Bootstraping component
(function (app) {
    app.AppModule = ng.core
    .NgModule({     
        imports: [
            ng.platformBrowser.BrowserModule,
            ng.forms.FormsModule, 
            routing, ng.http.HttpModule, ng.material.MaterialModule.forRoot(),
            ng.md2.Md2Module.forRoot()
        ],
        declarations: [
            app.nachbarcomponent,
            app.Herolistcomponent,
            app.Observerlistcomponent,
            app.Arealistcomponent,
            app.Timelistcomponent,
            app.Employeelistcomponent,
            app.SelectedEmployeelistcomponent,
            app.Employeeactivitieslistcomponent,
            app.Employeesubactivitieslistcomponent,
            app.Machinelistcomponent,
            app.Machinenamelistcomponent,
            app.Machinestatecomponent,
            app.Timeslotcomponent,
            app.Addentrycomponent,
            app.ReportComponent
        ],
        providers: [app.HeroesService],
        bootstrap: [app.nachbarcomponent]
    })
    .Class({
        constructor: function AppModule () {

        }
    })
})(window.app || (window.app = {}));