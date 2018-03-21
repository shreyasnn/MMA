(function (app) {
	var timer = 0;
	var delay = 200;
	var prevent = false;
	app.Observerlistcomponent = ng.core 
	.Component({
		selector: 'my-observer',
		templateUrl: 'Home/ObserverList'
	})
	.Class({
		constructor: [app.HeroesService, ng.router.Router,function Observerlistcomponent(heroesService,router) {
			this.router = router;
			this.heroesService = heroesService;
			this.getObserver = function () {
				heroesService.getObserver().subscribe(response => this.Assignobser(response),
								error => console.log(error));
			};
			this.head = this.heroesService.Languagelist[1];
			this.new = this.heroesService.Languagelist[17];
			this.update = this.heroesService.Languagelist[18];
			this.delete = this.heroesService.Languagelist[19];
			this.cancel = this.heroesService.Languagelist[20];
			this.company = this.heroesService.Languagelist[21];
			this.firstname = this.heroesService.Languagelist[22];
			this.surname = this.heroesService.Languagelist[23];
			this.warning = this.heroesService.Languagelist[24];
			this.warndata = this.heroesService.Languagelist[25];
			this.yes = this.heroesService.Languagelist[26];
			this.no = this.heroesService.Languagelist[27];
		}]
	});

	app.Observerlistcomponent.prototype.Assignobser = function (obserlist) {
		var obserlist1 = [{ id: '', name: '' }]; 
		for (var i = 0; i < obserlist[0].length; i++) {        
			obserlist1[i] = [{ id: String(obserlist[0][i]), name: String(obserlist[1][i]) }];   
		}
		this.obser = obserlist1;
	};
	app.Observerlistcomponent.prototype.ngOnInit = function () {
		this.getObserver();
	};
	app.Observerlistcomponent.prototype.goToArea = function (obsid, obsname) {
		timer = setTimeout(() => {
			if (!prevent) {
				this.heroesService.Observerid = obsid;
				//this.obsname = obsname.split(',');
				//this.heroesService.Observername = this.obsname[0];
				this.heroesService.Observername = obsname;
				var link = ['/area'];
				this.router.navigate(link);
			}
			prevent = false;
		}, delay);
	 };   
	app.Observerlistcomponent.prototype.goToEdit = function (id, name, Edit) {
		 clearTimeout(timer);
		 prevent = true;
		 this.id = id;
		 var name = name.split(',');
		 this.vorname = name[1];
		 this.name = name[0];
		 Edit.show();
	};
	app.Observerlistcomponent.prototype.goToUpdate = function (id, name, vorname, Edit) {
		if(name!= "" && vorname!= "" ){
		this.heroesService.editobserverdata(id,name, vorname, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
		Edit.close();
		}
	};
	app.Observerlistcomponent.prototype.goToDelete = function (id, name, vorname, confirm1, Edit) {
		this.heroesService.editobserverdata(id, name, vorname, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
		Edit.close();
		confirm1.close();
	};
	app.Observerlistcomponent.prototype.Assignstatus = function (status) {
		this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
		//this.ngOnInit().subscribe(this.zone.run());
		this.ngOnInit();
	};
})(window.app || (window.app = {}));