<template>
    <div class="row">
        <div class="col-md-7">
            <div class="head row">
                <div class="col-md-9" id="name">
                    <h3>{{currentVoertuig.MerkNaam}}</h3>
                    <div class="border-name" />
                    <h5>{{currentVoertuig.Model}}</h5>
                </div>
                <div class="col-md-3" id="price">
                    <h4>€{{currentVoertuig.Prijs}}</h4>
                </div>
            </div>
            
            <div>
                <img :src="'/Content/images/Voertuig'+currentVoertuig.Id+'/1_'+currentVoertuig.ArtikelNummer+'.png'" />
            </div>
            <div>
                <ul>
                    <li v-for="foto in currentVoertuig.FotoNaam" class="list-unstyled">
                        <img :src="'/Content/images/Voertuig'+currentVoertuig.Id+'/'+foto+'.png'" />
                    </li>
                </ul>
            </div>
        </div>
        <div class="col-md-4" id="specificaties">
            <h2>Specificaties</h2>
            <div>
                <dl>
                    <dt>
                        Merk
                    </dt>
                    <dd>
                        {{currentVoertuig.MerkNaam}}
                    </dd>
                    <dt>
                        Model
                    </dt>
                    <dd>
                        {{currentVoertuig.Model}}
                    </dd>
                    <dt>
                        Carrosserietype
                    </dt>
                    <dd>
                        {{currentVoertuig.CarrosserietypeNaam}}
                    </dd>
                    <dt>
                        Bouwjaar
                    </dt>
                    <dd>
                        {{currentVoertuig.BouwjaarShort}}
                    </dd>
                    <dt>
                        Kilometerstand
                    </dt>
                    <dd>
                        {{currentVoertuig.Kilometerstand}}
                    </dd>
                    <dt>
                        Kleur
                    </dt>
                    <dd>
                        {{currentVoertuig.Kleur}}
                    </dd>
                    <dt>
                        Transmissie
                    </dt>
                    <dd>
                        {{currentVoertuig.Transmissie}}
                    </dd>
                    <dt>
                        Cilinders
                    </dt>
                    <dd>
                        {{currentVoertuig.Cilinders}}
                    </dd>
                    <dt>
                        Aandrijving
                    </dt>
                    <dd>
                        {{currentVoertuig.Aandrijving}}
                    </dd>
                    <dt>
                        Vermogen
                    </dt>
                    <dd>
                        {{currentVoertuig.Vermogen}}
                    </dd>
                    <dt>
                        Brandstof
                    </dt>
                    <dd>
                        {{currentVoertuig.Brandstof}}
                    </dd>
                    <dt>
                        Deuren
                    </dt>
                    <dd>
                        {{currentVoertuig.Deuren}}
                    </dd>
                    <dt>
                        Zitplaatsen
                    </dt>
                    <dd>
                        {{currentVoertuig.Zitplaatsen}}
                    </dd>
                    

                </dl>
            </div>
        </div>
    </div>
</template>
<script>
    import { store } from '../store'
    import { mapGetters } from 'vuex'
    import { mapActions } from 'vuex'

    export default {
        store,
        computed: {
            ...mapGetters(["currentVoertuig", "carrosserietypes", "merken", "uitrustingen"])
        },
        beforeRouteEnter(to, from, next) {
            store.dispatch('fetchVoertuigDetails', to.params.id);
            next();
        },
    }
</script>
<style scoped>
    .row {
        margin-left: 0px;
        margin-right: 0px;
    }

    .head {
        padding-top: 7px;
        padding-bottom: 7px;
        padding-left: 15px;
        padding-right: 15px;
        border: solid 1px #E4E3E3;
        border-left-color: #fc7776;
        border-left-style: solid;
        border-left-width: 0.1rem;
        margin-bottom: 1rem;
    }

    #price > h4 {
        margin-top: 1.7rem;
        margin-bottom: 1.7rem;
        text-align: end;
    }

    .border-name {
        border: 0.7px solid #ff8181 !important;
        margin-top: 0.5rem;
        margin-bottom: 0.5rem;
        width: 15%;
    }

    div > img {
        height: 100%;
        width: 100%;
        object-fit: cover;
    }

    ul > li > img {
        width: 100%;
    }

    ul > li {
        width: 84px;
        float: left;
        display: block;
    }
    #specificaties {
        padding-top: 7px;
        padding-bottom: 7px;
        border: solid 1px #E4E3E3;
    }
</style>