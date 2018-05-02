<template>
    <div>
        <Overview :IsResponsive="IsResponsive"></Overview>
        <!--<Edit-Page></Edit-Page>-->
    </div>
</template>
<script>
    import Login from './Login.vue'
    import Overview from './Overview.vue'
    import EditPage from './EditPage.vue'

    export default {
        components: {
            Login,
            Overview,
            EditPage
        },
        data() {
            return {
                IsResponsive: true,
                IsEdit: true,
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
                var self = this;
                //console.log(document.documentElement.clientWidth);
                var w = document.documentElement.clientWidth;
                if (w > 751) {
                    self.IsResponsive = true;
                } else {
                    self.IsResponsive = false;
                }
            },
        }
    }
</script>