(function (app) {
    app.ViewReportcomponent = ng.core
	.Component({
	    selector: 'my-report',
	    templateUrl: 'Home/Viewreport',
	    styleUrls: ['Content/report.css']
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, function ViewReportcomponent(heroesService, route) {
	        this.reportparam = route.snapshot.params.reportparam;
	        this.reportlist = heroesService.reportlist;
	        if (this.reportparam == "employee") {
	            this.activity = heroesService.selectedreport.pop();
	            this.ort = heroesService.selectedreport.pop();
	            this.function = heroesService.selectedreport.pop();
	        }
	    else{
	            this.state = heroesService.selectedreport.pop();
	            this.machine = heroesService.selectedreport.pop();
	        }      
	        this.shift = heroesService.selectedreport.pop();
	        this.area = heroesService.selectedreport.pop();
	        this.observer = heroesService.selectedreport.pop();
	        this.date = heroesService.selectedreport.pop();
	    }]
	});
    app.ViewReportcomponent.prototype.printPDF = function () {
        window.print();
    };
    app.ViewReportcomponent.prototype.fnExcelReport = function () {
        //var blob = new Blob([document.getElementById('Table').innerHTML], {
        //    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        //});
        //saveAs(blob, "Report.xlsx")
        //};
        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById('Table'); // id of table

        for (j = 0 ; j < tab.rows.length ; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";
        }
        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "Report");
        }
        else                 //other browser not tested on IE 11
            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
        return (sa);
    };

})(window.app || (window.app = {}));