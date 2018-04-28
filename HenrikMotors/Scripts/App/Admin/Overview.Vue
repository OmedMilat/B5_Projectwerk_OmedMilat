<template>
    <section>
        <!--<a class="btn btn-danger" v-if="!IsEdit" v-on:click="Logoff">
            <i class="fa fa-off" title="LoggOff"></i>
            <span class="iconbutton">Afmelden</span>
        </a>-->
        <div class="tableheader">
            <h1 class="tabletitle">Voertuigen</h1>
            <a class="tablebutton btn btn-success" v-if="!IsEdit" v-on:click="EditMode(voertuig,true)">
                <i class="fa fa-plus-circle" title="toevoegen"></i>
                <span class="iconbutton">Voertuig Toevoegen</span>
            </a>
        </div>
        <table v-bind:class="{'table table-hover' : IsResponsive}">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Foto</th>
                    <th>Merk</th>
                    <th>Model</th>
                    <th>Bouwjaar</th>
                    <th>Vermogen</th>
                    <th>Acties</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="voertuig in voertuigen" class="tableRow">
                    <th scope="row" class="artikelNr">{{voertuig.ArtikelNummer}}</th>
                    <td class="TableFoto"><img :src="'/Content/images/Voertuig'+voertuig.Id+'/1_'+voertuig.ArtikelNummer+'.png'" /></td>
                    <td data-title="Merk:">{{voertuig.MerkNaam}}</td>
                    <td data-title="Model:">{{voertuig.Model}}</td>
                    <td data-title="Bouwjaar:">{{voertuig.BouwjaarShort}}</td>
                    <td data-title="Prijs:">€ {{voertuig.Prijs}}</td>
                    <td class="tableIcons">
                        <!--<router-link to="/editpage">--><a class="edit" v-on:click="EditMode(voertuig,false) "><i class="fa fa-cog" title="bewerken"></i></a><!--</router-link>-->
                        <a class="remove" v-on:click="Delete(voertuig)"><i class="fa fa-trash-o" title="verwijderen"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </section>
</template>
<script>
    import { mapGetters } from 'vuex'


    export default {

        computed: {
            ...mapGetters(["test", "voertuigen"])
        },
        props: {
            IsResponsive: {
                type: Boolean,
            }
        },
        data() {
            return {

            }
        },
        created() {
        },
        methods: {
            EditMode: function (voertuig, IsNewVoertuig) {

                if (!IsNewVoertuig) {
                    this.$router.push(`/editpage/${voertuig.Id}`);
                }
                else {
                    this.$router.push("/editpage/new");
                }
                
            },
            Delete: function (currentVoertuig) {
                var self = this;

                var hr = new XMLHttpRequest();
                hr.open(`DELETE`, `/api/voertuigen/${currentVoertuig.Id}`);
                hr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('accessToken'))
                hr.send();

                this.voertuigen.forEach(function (voertuig, i) {
                    if (voertuig.Id == currentVoertuig.Id) {
                        self.voertuigen.splice(i, 1);
                        return;
                    }
                })
            },
        }
    }

</script>