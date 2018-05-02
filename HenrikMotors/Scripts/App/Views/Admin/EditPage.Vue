<template>
    <div>
        <div class="tableheader">
            <h1 class="tabletitle">{{tableTitle}}</h1>
            <a class="tablebutton btn btn-success" v-on:click="save">
                <i class="fa fa-floppy-o" title="opslaan"></i>
                <span class="iconbutton">Opslaan</span>
            </a>
            <router-link to="/admin" class="tablebutton btn btn-warning">
                <i class="fa fa-ban" title="annuleren"></i>
                <span class="iconbutton">Annuleren</span>
            </router-link>
        </div>
        <section id="Edit" class="row">
            <div class="col-md-6 col-xl-3">
                <div class="form-group">
                    <label for="Merk" class="select-label">Merk:</label>
                    <select v-model="currentVoertuig.MerkId" ref="select" class="form-control selectpicker" data-live-search="true">
                        <option data-content="<span class='selectOption'><i class='fa fa-plus-circle'></i><span class='selectIcon''>Nieuw Merk</span></span>">0</option>
                        <option v-for="Merk in merken" v-bind:value="Merk.Id" id="Merk">
                            {{Merk.Naam}}
                        </option>
                    </select>
                    <input v-if="currentVoertuig.MerkId == 0" v-model="currentVoertuig.MerkNaam" placeholder="Typ hier uw nieuw merk in."
                           class="form-control" />
                </div>
                <div class="form-group">
                    <label for="Model">Model:</label>
                    <input v-model="currentVoertuig.Model" class="form-control" id="Model">
                </div>
                <div class="form-group">
                    <label for="Prijs">Prijs:</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">€</span>
                        </div>
                        <input v-model="currentVoertuig.Prijs" type="number" inputmode="numeric" class="form-control" step="0,01" id="Prijs">
                    </div>
                </div>
                <div class="form-group">
                    <label for="Kleur">Kleur:</label>
                    <input v-model="currentVoertuig.Kleur" class="form-control" id="Kleur">
                </div>
                <div class="form-group">
                    <label for="Bouwjaar">Bouwjaar:</label>
                    <input v-model="currentVoertuig.Bouwjaar" type="month" lang="nl-be" class="form-control" id="Bouwjaar">
                </div>
                <div class="form-group">
                    <label for="Ctype" class="select-label">Carrosserietype:</label>
                    <select v-model="currentVoertuig.CarrosserietypeId" ref="select" class="form-control selectpicker" data-live-search="true">
                        <option data-content="<span class='selectOption'><i class='fa fa-plus-circle'></i><span class='selectIcon''>Nieuw Type</span></span>">0</option>
                        <option v-for="Type in carrosserietypes" v-bind:value="Type.Id" id="Ctype">{{Type.Naam}}</option>
                    </select>
                    <input v-if="currentVoertuig.CarrosserietypeId == 0" v-model="currentVoertuig.CarrosserietypeNaam" placeholder="Typ hier uw nieuw type in."
                           class="form-control" />
                </div>
                <div class="form-group">
                    <label for="Deuren">Deuren:</label>
                    <input v-model="currentVoertuig.Deuren" class="form-control" id="Deuren">
                </div>
                <div class="form-group">
                    <label for="Plaatsen">Zitplaatsen:</label>
                    <input v-model="currentVoertuig.Plaatsen" class="form-control" id="Plaatsen">
                </div>

            </div>
            <div class="col-md-6 col-xl-3">
                <div class="form-group">
                    <label for="Kilometerstand">Kilometerstand:</label>
                    <input v-model="currentVoertuig.Kilometerstand" type="number" inputmode="numeric" class="form-control" id="Kilometerstand">
                </div>
                <div class="form-group">
                    <label for="Transmissie">Transmissie:</label>
                    <input v-model="currentVoertuig.Transmissie" class="form-control" id="Transmissie">
                </div>
                <div class="form-group">
                    <label for="Cilinders">Cilinders:</label>
                    <input v-model="currentVoertuig.Cilinders" class="form-control" id="Cilinders">
                </div>
                <div class="form-group">
                    <label for="Aandrijving">Aandrijving:</label>
                    <input v-model="currentVoertuig.Aandrijving" class="form-control" id="Aandrijving">
                </div>
                <div class="form-group">
                    <label for="Vermogen">Vermogen:</label>
                    <input v-model="currentVoertuig.Vermogen" class="form-control" id="Vermogen">
                </div>
                <div class="form-group">
                    <label for="Brandstof">Brandstof:</label>
                    <input v-model="currentVoertuig.Brandstof" class="form-control" id="Brandstof">
                </div>
                <div class="form-group">
                    <label for="Beschrijving">Beschrijving:</label>
                    <textarea id="beschrijving" class="form-control" rows="4"></textarea>
                </div>
            </div>
            <div class="col-md-12 col-xl-6">
                <div class="file-loading">
                    <label for="file">Foto uploaden:</label>
                    <input v-on:change="onFileChange" name="fotos[]" multiple id="files-upload" type="file">
                </div>

            </div>
            <div id="uitrustingen" class="row col-lg-12">
                <h3 class="col-12">Uitrusting:</h3>
                <div id="Veiligheid" class="col-md-6 col-xl-3">
                    <h5>Veiligheid</h5>
                    <div class="column1">
                        <label v-for="Uitrusting in filteredItems(1,1)" class="custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                    <div class="column2">
                        <label v-for="Uitrusting in filteredItems(2,1)" class=" custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                </div>
                <div id="Comfort" class="col-md-6 col-xl-3">
                    <h5>Comfort</h5>
                    <div class="column1">
                        <label v-for="Uitrusting in filteredItems(1,2)" class="custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                    <div class="column2">
                        <label v-for="Uitrusting in filteredItems(2,2)" class=" custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                </div>
                <div id="MultiMedia" class="col-md-6 col-xl-3">
                    <h5>MultiMedia</h5>
                    <div class="column1">
                        <label v-for="Uitrusting in filteredItems(1,3)" class="custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                    <div class="column2">
                        <label v-for="Uitrusting in filteredItems(2,3)" class=" custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                </div>
                <div id="Extra opties" class="col-md-6 col-xl-3">
                    <h5>Extra opties</h5>
                    <div class="column1">
                        <label v-for="Uitrusting in filteredItems(1,4)" class="custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                    <div class="column2">
                        <label v-for="Uitrusting in filteredItems(2,4)" class=" custom-control custom-checkbox">
                            <input type="checkbox" v-model="voertuigUitrusting" :value="Uitrusting.Id"
                                   class="custom-control-input" :id="Uitrusting.Naam">
                            <span class="custom-control-label">{{Uitrusting.Naam}}</span>
                        </label>
                    </div>
                    <div v-for="(uit,i) in newUitrusting">
                        <input type="checkbox" v-model="uit.Add" />
                        <input id="newuit" v-on:input="changeUitrustingen(uit)" v-model="uit.UitrustingNaam" />
                    </div>
                </div>
            </div>
            <div class="col-12 editbuttons">
                <router-link to="/admin" class="btn btn-warning">
                    <i class="fa fa-ban" title="annuleren"></i>
                    <span class="iconbutton">Annuleren</span>
                </router-link>
                <a class="btn btn-success" v-on:click="save">
                    <i class="fa fa-floppy-o" title="opslaan"></i>
                    <span class="iconbutton">Opsaan</span>
                </a>
            </div>
        </section>
    </div>
</template>
<script>
    import { store } from '../../store'
    import { mapGetters } from 'vuex'
    import { mapActions } from 'vuex'
    var apiURL = '/api/'
    export default {
        store,
        computed: {
            ...mapGetters(["currentVoertuig", "carrosserietypes", "merken", "uitrustingen"])
        },
        data() {
            return {
                tableTitle: "test",
                newUitrusting: [],
                voertuigUitrusting: this.$store.getters.voertuigUitrustingStore,
                IsEdit: false,
                IsNewVoertuig: false,
                image: null,
                info: [],
                fotolength: null,
                sortinfo: null,
                IsSort: false,
                count: -1,
            }
        },
        beforeRouteEnter(to, from, next) {
            //bug wanneer je niet van overview.vue komt.
            //console.log("from");
            //console.log(from);
            //console.log("to");
            //console.log(to);
            //if (from.path == "/") {

            //}
            if (to.params.id == "new") {
                store.dispatch("newVoertuig").then(() => { next(vm => vm.tableTitle = "Voertuig toevoegen") });    
            } else {
                store.dispatch('fetchVoertuigDetails', to.params.id).then(() => { next(vm => vm.tableTitle = "Voertuig bewerken") });
            }
           
        },

        mounted() {
                $('.selectpicker').selectpicker('refresh');
                this.fetchFotos();
                this.newUitrusting = [];
                this.count = -1;
                this.newUitrusting.push({ UitrustingId: 0 });
        },
        methods: {
            filterUitrustingen: function (CatId) {
                return this.uitrustingen.filter(u => u.Categorie === CatId);
            },
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                self.image = files;
                self.fotolength = self.image.length;
            },

            fetchFotos: function () {
                var fotos = this.currentVoertuig.FotoNaam;
                var path = [];
                this.info = [];
                if (fotos != null && fotos.length != 0 && fotos[0] != "") {
                    for (var i = 0; i < fotos.length; i++) {
                        path.push(`../../Content/images/Voertuig${this.currentVoertuig.Id}/${fotos[i]}.png`);
                        this.info.push({
                            caption: fotos[i], filename: `${fotos[i]}.png`, key: i + 1,
                            extra: { fileName: fotos[i], id: this.currentVoertuig.Id }
                        });
                    }

                }

                $('#files-upload').fileinput('destroy')
                    .fileinput({
                        theme: "explorer-fa",
                        language: "nl",
                        showUpload: false,
                        fileActionSettings: { 'showUpload': false },
                        uploadAsync: false,
                        initialPreview: path,
                        uploadUrl: `/api/voertuigen/PostFile/${this.fotolength}`,
                        initialPreviewAsData: true,
                        overwriteInitial: false,
                        initialPreviewFileType: 'image',
                        deleteUrl: "/api/voertuigen/DeleteFile",
                        browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i> ",
                        initialPreviewConfig: this.info,
                    }).on('filedeleted', function (event, key, jqXHR, data) {
                        var index = this.currentVoertuig.FotoNaam.indexOf(data.fileName);
                        if (index > -1) {
                            this.currentVoertuig.FotoNaam.pop();
                            var string = this.currentVoertuig.FotoNaam.join();
                            this.currentVoertuig.LijstFotos = string;
                            var stackinfo = $('#files-upload').fileinput('getPreview');
                            console.log(stackinfo.config);
                            this.sortinfo = stackinfo.config;
                            this.IsSort = false;
                            this.sortFotos();
                        }
                    }).on('filesorted', function (event, params) {
                        this.IsSort = true;
                        this.sortinfo = params.stack;
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
            filteredItems(column, catId) {
                //console.log("Wordt opgeroepen bij elke currentvoertuig element");
                const self = this;
                var uitrusting = self.uitrustingen.filter(u => u.Categorie === catId);
                const total = uitrusting.length;
                const gap = Math.ceil(total / 2);
                let top = (gap * column);
                const bottom = ((top - gap));
                top -= 1;
                return uitrusting.filter(item =>
                    uitrusting.indexOf(item) >= bottom
                    && uitrusting.indexOf(item) <= top,
                ); // Return the items for the given col
            },
            changeUitrustingen: function (u) {
                console.log(u);
                u.Add = true;
                if (u.UitrustingId - 1 <= this.count && u.Add == true) {
                    var cvu = {
                        Add: false,
                        UitrustingId: this.count,
                        UitrustingNaam: this.newUitrusting.UitrustingNaam,
                    }
                    this.newUitrusting.push(cvu);
                    this.count--;
                } else if (u.UitrustingNaam == "") { u.Add = false; }
            },
            addUitrustingenToVoertuig: function () {
                //this.currentVoertuig.VoertuigUitrusting = [];
                if (this.voertuigUitrusting.length != 0 && this.voertuigUitrusting != null) {
                    for (var i = 0; i < this.voertuigUitrusting.length; i++) {
                        var cvu = {
                            UitrustingId: this.voertuigUitrusting[i],
                            UitrustingNaam: "",
                            VoertuigId: this.currentVoertuig.Id
                        }
                        this.currentVoertuig.VoertuigUitrusting[i] = cvu;
                    }
                }

                for (var i = 0; i < this.newUitrusting.length; i++) {
                    if (this.newUitrusting[i].Add) {
                        var newcvu = {
                            UitrustingId: this.newUitrusting[i].UitrustingId,
                            UitrustingNaam: this.newUitrusting[i].UitrustingNaam,
                            VoertuigId: this.currentVoertuig.Id
                        }
                        this.currentVoertuig.VoertuigUitrusting.push(newcvu);
                    }
                }
            },
            EditMode: function (voertuig, IsNewVoertuig) {
                //var self = this;
                //self.IsNewVoertuig = IsNewVoertuig;
                //self.IsEdit = true;

                //self.newUitrusting = [];
                //self.count = -1;
                //self.newUitrusting.push({ UitrustingId: 0 });

                //if (!IsNewVoertuig) {
                //    self.fetchVoertuigDetails(voertuig)
                //    self.tableTitle = "Voertuig bewerken";
                //}
                //else {
                //    voertuig: null;
                //    self.tableTitle = "Voertuig toevoegen";
                //    self.voertuigUitrusting = [];
                //    self.currentVoertuig = {
                //        Id: 1, MerkId: 1, CarrosserietypeId: 1,
                //    };
                //}
            },
            save: function () {
                var self = this
                var ajaxHeader = new Headers();
                ajaxHeader.append("Content-Type", "application/json")
                ajaxHeader.append("Authorization", "Bearer " + localStorage.getItem("accessToken"))

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
                        store.dispatch('fetchvoertuigen');
                        store.dispatch('fetchcarrosserietypes');
                        store.dispatch('fetchMerken');
                        store.dispatch('fetchUitrustingen');
                        //themerken = self.merken.filter(merk => (merk.Id === res.MerkId))[0];
                        //self.currentVoertuig.MerkNaam = themerken.Naam;
                        //if (!self.IsNewVoertuig) {
                        //    theVoertuig = self.voertuigen.filter(voertuig => (voertuig.Id === self.currentVoertuig.Id))[0];
                        //    theVoertuig.MerkNaam = themerken.Naam;
                        //    theVoertuig.Model = self.currentVoertuig.Model;
                        //    theVoertuig.BouwjaarShort = res.BouwjaarShort;
                        //    theVoertuig.Prijs = res.Prijs.toLocaleString("de-DE");
                        //} else {
                        //    self.currentVoertuig.Id = res.Id;
                        //    self.voertuigen.push(self.currentVoertuig);
                        //}
                    }).catch(err => console.error('Fout: ' + err));
            },
           
        }

    }
</script>