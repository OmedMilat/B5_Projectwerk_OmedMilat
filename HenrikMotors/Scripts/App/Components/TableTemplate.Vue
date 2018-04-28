<template>
    <div>


        <!--<a class="btn btn-danger" v-if="!IsEdit" v-on:click="Logoff">
            <i class="fa fa-off" title="LoggOff"></i>
            <span class="iconbutton">Afmelden</span>
        </a>-->
        <div class="tableheader">
            <h1 class="tabletitle"><slot name="title"></slot></h1>
            <slot name="table-button"></slot>
            <!--<slot name="table-button-2"></slot>
            <slot name="table-button-3"></slot>-->
        </div>
        <table v-bind:class="{'table table-hover': IsResponsive}">
            <!--v-if="!IsEdit"-->
            <thead>
                <tr>
                    <th v-for="th in column">{{th}}</th>
                </tr>
            </thead>
            <tbody>
                <tr class="tableRow" v-for="(length,i) in column">
                    <th scope="row" class="artikelNr"></th>
                    <td><slot :name="'td'+(i+1)" >{{i}}</slot></td>
                    <td><slot :name="'td'+(i+1)">{{i}}</slot></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
    export default {
        props: {
            column:{},
        },
        data() {
            return {
                
            }
        }
    }
</script>