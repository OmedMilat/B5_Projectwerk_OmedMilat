/**
 * Vue CRUD Section
 **/
var apiURL = '/api/'
var app = new Vue({
    el: '#app',
    data: {
        message: 'Loading voertuigen...',
        voertuigen: null,
        currentVoertuig: null,
        carrosserietypes: null,
        merken: null,
        IsEdit: false,
        IsNewVoertuig: false,
        image: null,
    },
    updated: function () {
        if (self.IsEdit) {
            $('.selectpicker').selectpicker('refresh');
            self.fetchFotos();
        }
    },
    created: function () {
        self = this
        this.fetchvoertuig();
        this.fetchcarrosserietypes();
        this.fetchMerken();
        this.fetchFotos();
    },
    methods: {
        fetchvoertuig: function () {
            self = this
            fetch(`${apiURL}voertuigen`)
                .then(res => res.json())
                .then(function (voertuigen) {
                    self.voertuigen = voertuigen;
                    self.message = ""
                }).catch(err => console.error('Fout: ' + err));
        },
        fetchVoertuigDetails: function (voertuig) {
            self = this;
            fetch(`${apiURL}voertuigen/${voertuig.Id}`)
                .then(res => res.json())
                .then(function (res) {
                    self.currentVoertuig = res;
                }).catch(err => console.error('Fout: ' + err));
        },
        fetchcarrosserietypes: function () {
            self = this;
            fetch(`${apiURL}carrosserietypes`)
                .then(res => res.json())
                .then(function (res) {
                    self.carrosserietypes = res;
                }).catch(err => console.error('Fout: ' + err));
        },
        fetchMerken: function () {
            self = this;
            fetch(`${apiURL}merken`)
                .then(res => res.json())
                .then(function (res) {
                    self.merken = res;
                }).catch(err => console.error('Fout: ' + err));
        },
        onFileChange(e) {
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            self.image = files;
        },
        //createImage(file) {
        //    var image = new Image();
        //    var reader = new FileReader();
        //    var vm = this;

        //    reader.onload = (e) => {
        //        vm.image = e.target.result;
        //    };
        //    reader.readAsDataURL(file);
        //},

        fetchFotos: function () {
            var fotos = self.currentVoertuig.FotoNaam;
            var path = [];
            if (fotos != null) {
                for (var i = 0; i < fotos.length; i++) {
                    path.push(`../../Content/images/Voertuig${self.currentVoertuig.Id}/${fotos[i]}.png`);
                }
            }
            console.log(self.currentVoertuig);
            $('#files-upload').fileinput("destroy")
                .fileinput({
                    uploadAsync: false,
                    uploadUrl: `/api/voertuigen/PostFile/${self.currentVoertuig.Id}/${2}`,
                    initialPreview: path,
                    initialPreviewAsData: true,
                    overwriteInitial: false,
                });
        },
        EditMode: function (voertuig, IsNewVoertuig) {
            self = this;
            self.IsNewVoertuig = IsNewVoertuig;
            self.IsEdit = true;
            if (!IsNewVoertuig) {
                self.fetchVoertuigDetails(voertuig)
            }
            else {
                voertuig: null;
                self.currentVoertuig = {
                    Id: 1, MerkId: 1, CarrosserietypeId: 1,
                };
            }

        },
        save: function () {
            var self = this
            var ajaxHeader = new Headers();
            ajaxHeader.append("Content-Type", "application/json")
            var ajaxConfig = {
                method: 'PUT',
                body: JSON.stringify(self.currentVoertuig),
                headers: ajaxHeader
            }

            if (!self.IsNewVoertuig) {
                var myRequest = new Request(`${apiURL}voertuigen/${self.currentVoertuig.Id}`, ajaxConfig);
            }
            else {
                ajaxConfig.method = 'POST';
                var myRequest = new Request(`${apiURL}voertuigen/PostVoertuig`, ajaxConfig);
            }

            if (self.image != null) {
                var formData = new FormData()
                for (i = 0; i < self.image.length; i++) {
                    formData.append(self.image[i].name, self.image[i], self.image[i].type)
                }
                fetch(`${apiURL}voertuigen/PostFile/${self.currentVoertuig.Id}`,
                    {
                        method: 'POST',
                        body: formData
                    });
            }

            fetch(myRequest)
                .then(res => res.json())
                .then(function (res) {

                    themerken = self.merken.filter(merk => (merk.Id === res.MerkId))[0];
                    self.currentVoertuig.MerkNaam = themerken.Naam;

                    if (!self.IsNewVoertuig) {
                        theVoertuig = self.voertuigen.filter(voertuig => (voertuig.Id === self.currentVoertuig.Id))[0];
                        theVoertuig.MerkNaam = themerken.Naam;
                        theVoertuig.Model = self.currentVoertuig.Model;
                        theVoertuig.Bouwjaar = self.currentVoertuig.Bouwjaar;
                        theVoertuig.Vermogen = self.currentVoertuig.Vermogen;
                    } else {
                        //self.currentVoertuig.Id = res.Id;
                        self.voertuigen.push(self.currentVoertuig);
                    }
                    self.IsEdit = false;
                }).catch(err => console.error('Fout: ' + err));
        },
        Delete: function (currentVoertuig) {
            var self = this;

            var hr = new XMLHttpRequest();
            hr.open(`DELETE`, `${apiURL}voertuigen/${currentVoertuig.Id}`);
            hr.send();

            this.voertuigen.forEach(function (voertuig, i) {
                if (voertuig.Id == currentVoertuig.Id) {
                    self.voertuigen.splice(i, 1);
                    return;
                }
            })
        },
    }

});

/**
 * Bootstrap-select Section
 **/
//$(document).ready(function () {
//    $(document).on("click", ".edit", function () {
//        setTimeout(dynamicDiv, 200);
//    });
//    function dynamicDiv() {
//        $('.selectpicker').selectpicker();
//    }
//});

/**
 *  Section Bootstrap-File input
 **/
//$('#files-upload').fileinput({
//    'showUpload': false
//});