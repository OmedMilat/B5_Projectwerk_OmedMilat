<template>
            <div class="card">
                <img :class="ImageClass" :src="imagepath" />
                <div class="card-body" v-bind:class="{padding: Orientation}">
                    <h3 class="card-title"><slot name="title"></slot></h3>
                    <h5 class="card-subtitle mb-2 text-muted"><slot name="subtitle"></slot></h5>
                    <h4 class="prijs">€ <slot name="prijs"></slot></h4>
                    <ul class="list-unstyled">
                        <li :class="{'col-6 col-lg-4':Orientation}">
                            <i class="far fa-calendar-alt iconbutton"></i>
                            <span><slot name="bouwjaar"></slot></span>
                        </li>
                        <li :class="{'col-6 col-lg-4':Orientation}">
                            <i class="fa fa-road iconbutton"></i>
                            <span><slot name="kilometerstand"></slot> km</span>

                        <li :class="{'col-6 col-lg-4':Orientation}">
                            <i class="fa fa-car iconbutton"></i>
                            <span><slot name="carrosserietype"></slot></span>
                        </li>
                        <li :class="{'col-6 col-lg-4':Orientation}">
                            <img src="/Content/images/Site/gas-station.svg" style="height:20px;" class="iconbutton" />
                            <span><slot name="brandstof"></slot></span>
                        </li>
                        <li v-if="Orientation" class="d-none d-lg-block"  v-bind:class="{'col-6 col-lg-4':Orientation}">
                            <i class="fa fa-bolt iconbutton"></i>
                            <span><slot name="vermogen"></slot></span>
                        </li>
                        <li v-if="Orientation" class="d-none d-lg-block"  v-bind:class="{'col-6 col-lg-4':Orientation}">
                            <i class="fa fa-couch iconbutton"></i>
                            <span><slot name="zitplaatsen"></slot></span>
                        </li>
                        
                        <!--<a href="#" class="btn btn-primary">Go somewhere</a>-->
                    </ul>
                </div>
        </div>
</template>
<script>
    export default {

        props: {
            horizontal: {
                type: Boolean,
                default: false,
                required: false,
            },
            imagepath: {
                required: false,
            }
        },

        data() {
            return {
                Orientation: this.horizontal,
            }
        },
        computed: {
            ImageClass: function () {
                return {
                    'card-img-overlay': this.Orientation,
                    'card-img-top': !this.Orientation
                }
            }
        },
        mounted() {
            this.$nextTick(function () {
                window.addEventListener('resize', this.responsive);
                this.responsive();
            })
        },
        methods: {
            responsive() {
                console.log(document.documentElement.clientWidth);
                var w = document.documentElement.clientWidth;
                if (this.horizontal == true){
                if (w > 750) {
                    this.Orientation = true;
                } else {
                    this.Orientation = false;
                    }
                }
            }
        },
    }

</script>
<style scoped>
    .padding {
        padding-left: 26em;
    }
</style>