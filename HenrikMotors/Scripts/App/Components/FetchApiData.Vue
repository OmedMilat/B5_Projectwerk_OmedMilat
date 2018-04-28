<template>

</template>
<script>
    var apiURL = '/api/'

    export default {
        data() {
            return {
                voertuigen: null,
                currentVoertuig: null,
                carrosserietypes: null,
                merken: null,
                uitrustingen: null,
                voertuigUitrusting: [],
            }
        },     
        render: function(h) {
            return h()
        },
        created() {
            var self = this
            this.fetchvoertuig();
            this.fetchcarrosserietypes();
            this.fetchMerken();
            this.fetchUitrustingen();
        },
        methods: {
            fetchvoertuig: function () {
                var self = this
                fetch(`${apiURL}voertuigen`)
                    .then(res => res.json())
                    .then(function (res) {        
                        for (var i = 0; i < res.length; i++) {
                            res[i].Prijs = res[i].Prijs.toLocaleString("de-DE");
                            res[i].Kilometerstand = res[i].Kilometerstand.toLocaleString("de-DE");
                            var d = new Date(res[i].Bouwjaar);
                            if (res[i].Bouwjaar)
                                res[i].Bouwjaar = ("0" + (d.getMonth() + 1)).slice(-2) + "-" + d.getFullYear();
                        }
                        self.voertuigen = res;
                        self.loading = "";
                        self.$store.state.voertuigen = res;
                    }).catch(err => console.error('Fout: ' + err));
            },
            fetchVoertuigDetails: function (voertuig) {
                var self = this;
                fetch(`${apiURL}voertuigen/${voertuig.Id}`)
                    .then(res => res.json())
                    .then(function (res) {
                        var date = new Date(res.Bouwjaar);
                        var month = ("0" + (date.getMonth() + 1)).slice(-2);
                        res.Bouwjaar = `${date.getFullYear()}-${month}`;

                        self.currentVoertuig = res;
                        self.voertuigUitrusting = [];
                        if (self.currentVoertuig.VoertuigUitrusting != 0 && self.currentVoertuig.VoertuigUitrusting != null) {
                            for (var i = 0; i < self.currentVoertuig.VoertuigUitrusting.length; i++) {
                                self.voertuigUitrusting[i] = self.currentVoertuig.VoertuigUitrusting[i].UitrustingId;
                            }
                        }
                    }).catch(err => console.error('Fout: ' + err));
            },
            fetchcarrosserietypes: function () {
                var self = this;
                fetch(`${apiURL}carrosserietypes`)
                    .then(res => res.json())
                    .then(function (res) {
                        self.carrosserietypes = res;
                    }).catch(err => console.error('Fout: ' + err));
            },
            fetchMerken: function () {
                var self = this;
                fetch(`${apiURL}merken`)
                    .then(res => res.json())
                    .then(function (res) {
                        self.merken = res;
                    }).catch(err => console.error('Fout: ' + err));
            },
            fetchUitrustingen: function () {
                var self = this;
                fetch(`${apiURL}uitrustingen`)
                    .then(res => res.json())
                    .then(function (res) {
                        self.uitrustingen = res;
                    }).catch(err => console.error('Fout: ' + err));
            },
        }
    }
</script>