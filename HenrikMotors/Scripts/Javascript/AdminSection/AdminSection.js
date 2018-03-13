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
        uitrustingen: null,
        voertuigUitrusting: [],
        newUitrusting: [],
        IsEdit: false,
        IsNewVoertuig: false,
        image: null,
        info: [],
        fotolength: null,
        sortinfo: null,
        IsSort: false,
        count: -1,
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
        this.fetchUitrustingen();
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
                    //self.Ischecked = [];
                    //var uitrustingenIds = [];
                    //var currentUitrustingIds = [];
                    self.voertuigUitrusting = [];
                    if (self.currentVoertuig.VoertuigUitrusting != 0 && self.currentVoertuig.VoertuigUitrusting != null) {
                        for (var i = 0; i < self.currentVoertuig.VoertuigUitrusting.length; i++) {
                            self.voertuigUitrusting[i] = self.currentVoertuig.VoertuigUitrusting[i].UitrustingId;
                        }
                    }
                    self.newUitrusting = [];
                    self.count = -1;
                    self.newUitrusting.push({ UitrustingId: 0 });

                    //for (var i = 0; i < self.currentVoertuig.VoertuigUitrusting.length; i++) {
                    //    currentUitrustingIds.push("" + self.currentVoertuig.VoertuigUitrusting[i].UitrustingId)
                    //}
                    //for (var i = 0; i < self.uitrustingen.length; i++) {
                    //    uitrustingenIds.push("" + self.uitrustingen[i].Id)
                    //}

                    //for (var i = 0; i < uitrustingenIds.length; i++) {
                    //    if (currentUitrustingIds.indexOf(uitrustingenIds[i]) !== -1) {
                    //        self.Ischecked.push(i);
                    //    } else
                    //        self.Ischecked.push(false);
                    //}

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
        fetchUitrustingen: function () {
            self = this;
            fetch(`${apiURL}uitrustingen`)
                .then(res => res.json())
                .then(function (res) {
                    self.uitrustingen = res;
                }).catch(err => console.error('Fout: ' + err));
        },
        onFileChange(e) {
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            self.image = files;
            self.fotolength = self.image.length;
        },

        fetchFotos: function () {
            var fotos = self.currentVoertuig.FotoNaam;
            var path = [];
            self.info = [];
            if (fotos != null && fotos.length != 0 && fotos[0] != "") {
                for (var i = 0; i < fotos.length; i++) {
                    path.push(`../../Content/images/Voertuig${self.currentVoertuig.Id}/${fotos[i]}.png`);
                    self.info.push({
                        caption: fotos[i], filename: `${fotos[i]}.png`, key: i + 1,
                        extra: { fileName: fotos[i], id: self.currentVoertuig.Id }
                    });
                }

            }

            $('#files-upload').fileinput('destroy')
                .fileinput({
                    theme: "explorer",
                    language: "nl",
                    showUpload: false,
                    fileActionSettings: { 'showUpload': false },
                    uploadAsync: false,
                    initialPreview: path,
                    uploadUrl: `/api/voertuigen/PostFile/${self.fotolength}`,
                    initialPreviewAsData: true,
                    overwriteInitial: false,
                    initialPreviewFileType: 'image',
                    deleteUrl: "/api/voertuigen/DeleteFile",
                    browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i> ",
                    initialPreviewConfig: self.info,
                }).on('filedeleted', function (event, key, jqXHR, data) {
                    var index = self.currentVoertuig.FotoNaam.indexOf(data.fileName);
                    if (index > -1) {
                        self.currentVoertuig.FotoNaam.pop();
                        var string = self.currentVoertuig.FotoNaam.join();
                        self.currentVoertuig.LijstFotos = string;
                        var stackinfo = $('#files-upload').fileinput('getPreview');
                        console.log(stackinfo.config);
                        self.sortinfo = stackinfo.config;
                        self.IsSort = false;
                        self.sortFotos();
                    }
                }).on('filesorted', function (event, params) {
                    self.IsSort = true;
                    self.sortinfo = params.stack;
                });


        },
        uploadFotos: function () {
            if (self.image != null && self.image.length != 0) {
                self.fotolength = self.image.length;
                $('#files-upload').fileinput('refresh', { uploadUrl: `/api/voertuigen/PostFile/${self.fotolength}` });
                $('#files-upload').fileinput('upload');
                $('#files-upload').on('filebatchuploadsuccess', function () {
                    self.IsEdit = false;
                });
            } else
                self.IsEdit = false;
        },
        sortFotos: function () {
            if (self.sortinfo != null) {
                var ajaxHeader = new Headers();
                ajaxHeader.append("Content-Type", "application/json")
                var ajaxConfig = {
                    method: 'POST',
                    body: JSON.stringify(self.sortinfo),
                    headers: ajaxHeader
                }
                fetch(`${apiURL}voertuigen/ChangeFileName`, ajaxConfig);
            }
        },

        changeUitrustingen: function (u) {
            console.log(u);
            u.Add = true;
            if (u.UitrustingId - 1 <= self.count && u.Add == true) {
                var cvu = {
                    Add: false,
                    UitrustingId: self.count,
                    UitrustingNaam: self.newUitrusting.UitrustingNaam,
                }
                self.newUitrusting.push(cvu);
                self.count--;
            } else if (u.UitrustingNaam == "") { u.Add = false; }
        },
        addUitrustingenToVoertuig: function () {
            self.currentVoertuig.VoertuigUitrusting = [];
            if (self.voertuigUitrusting.length != 0 && self.voertuigUitrusting != null) {
                for (var i = 0; i < self.voertuigUitrusting.length; i++) {
                    var cvu = {
                        UitrustingId: self.voertuigUitrusting[i],
                        UitrustingNaam: "",
                        VoertuigId: self.currentVoertuig.Id
                    }
                    self.currentVoertuig.VoertuigUitrusting[i] = cvu;
                }
            }

            for (var i = 0; i < self.newUitrusting.length; i++) {
                if (self.newUitrusting[i].Add) {
                    var newcvu = {
                        UitrustingId: self.newUitrusting[i].UitrustingId,
                        UitrustingNaam: self.newUitrusting[i].UitrustingNaam,
                        VoertuigId: self.currentVoertuig.Id
                    }
                    self.currentVoertuig.VoertuigUitrusting.push(newcvu);
                }
            }
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
            self.addUitrustingenToVoertuig();
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
                var myRequest = new Request(`${apiURL}voertuigen/PostVoertuig`, ajaxConfig)
            }
            //if (self.image != null) {
            //var formData = new FormData()
            //for (i = 0; i < self.image.length; i++) {
            //    formData.append(self.image[i].name, self.image[i], self.image[i].type)
            //}
            //fetch(`${apiURL}voertuigen/PostFile/${self.currentVoertuig.Id}/2`,
            //    {
            //        method: 'POST',
            //        body: formData
            //    });
            //$('#files-upload').fileinput('upload');
            //}

            fetch(myRequest)
                .then(res => res.json())
                .then(function (res) {
                    self.uploadFotos();
                    if (self.IsSort) {
                        self.sortFotos();
                    }
                    themerken = self.merken.filter(merk => (merk.Id === res.MerkId))[0];
                    self.currentVoertuig.MerkNaam = themerken.Naam;
                    if (!self.IsNewVoertuig) {
                        theVoertuig = self.voertuigen.filter(voertuig => (voertuig.Id === self.currentVoertuig.Id))[0];
                        theVoertuig.MerkNaam = themerken.Naam;
                        theVoertuig.Model = self.currentVoertuig.Model;
                        theVoertuig.Bouwjaar = self.currentVoertuig.Bouwjaar;
                        theVoertuig.Vermogen = self.currentVoertuig.Vermogen;
                    } else {
                        self.currentVoertuig.Id = res.Id;
                        self.voertuigen.push(self.currentVoertuig);
                    }
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