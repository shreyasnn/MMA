//This JS contains all the routing path and its corresponding component
const appRoutes = [{
    path: "intro",
    component: app.Herolistcomponent
},
{
    path: "introEN",
    component: app.Herolistcomponent
},
{
    path: "introHG",
    component: app.Herolistcomponent
},
{
    path:'',
    redirectTo: '/intro',
    pathMatch:'full'
},

{
    path:'obs',
    component: app.Observerlistcomponent
},
{
    path:'area',
    component: app.Arealistcomponent  
},
{
    path: 'time',
    component: app.Timelistcomponent
},
{
    path: 'employee/:area',
    component: app.Timelistcomponent,
},
{
    path: 'employee',
    component: app.Timelistcomponent,
},
{
    path: 'employee1/:area',
    component: app.Employeelistcomponent,
},
{
    path: 'employee2',
    component: app.SelectedEmployeelistcomponent,
},
{
    path: 'employee3',
    component: app.Employeeactivitieslistcomponent,
},
{
    path: 'employee4/:activity',
    component: app.Employeesubactivitieslistcomponent,
},
{
    path: 'machine',
    component: app.Timelistcomponent
},
{
    path: 'machine1',
    component: app.Machinelistcomponent
},
{
    path: 'machine2/:line',
    component: app.Machinenamelistcomponent
},
{
    path: 'machine3',
    component: app.Machinestatecomponent
},
{
    path: 'timeslot',
    component: app.Timeslotcomponent
},
{
    path: 'addentry/:selected',
    component: app.Addentrycomponent
},
{
    path: 'report',
    component: app.ReportComponent
},
{
    path: '**',
    redirectTo: '/intro'
}
];
const routing = ng.router.RouterModule.forRoot(appRoutes);