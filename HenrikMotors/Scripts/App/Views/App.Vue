<template>
    <div class="row">
        <div class="col-md-6 col-xl-4 cards col-xxl-3" v-for="(voertuig,i) in voertuigen" :key="i">
            <Card :imagepath="'/Content/images/Voertuig'+voertuig.Id+'/1_'+voertuig.ArtikelNummer+'.png'">
                <template slot="title">
                    {{voertuig.MerkNaam}}
                </template>
                <template slot="subtitle">
                    {{voertuig.Model}}
                </template>
                <template slot="prijs">
                    {{voertuig.Prijs}}
                </template>
                <template slot="bouwjaar">
                    {{voertuig.BouwjaarShort}}
                </template>
                <template slot="kilometerstand">
                    {{voertuig.Kilometerstand}}
                </template>
                <template slot="carrosserietype">
                    {{voertuig.CarrosserietypeNaam}}
                </template>
                <template slot="brandstof">
                    {{voertuig.Brandstof}}
                </template>
            </Card>
        </div>
    </div>
</template>
<script>
    import Card from '../Components/Card.vue'
    import { mapGetters } from 'vuex'
    export default {
        computed: {
            ...mapGetters(['voertuigen'])
        },
        components: {
            Card
        }
    }

</script>