﻿<template>
    <div id="login">
        <h1></h1>
        <div class="form-group">
            <input class="form-control" v-model="username" />
            <input class="form-control" v-model="password" type="password" />
            <button v-on:click="login" class="btn btn-success">Login</button>
        </div>
    </div>
</template>
<script>
    export default {
        data(){
            return {
                username: null,
                password: null,
            }
        },
        methods: {
            login: function () {
                self = this;
                $.ajax({
                    url: "/token",
                    method: 'POST',
                    contentType: "application/json",
                    data: {
                        username: self.username,
                        password: self.password,
                        grant_type: "password"
                    },
                    success: function (res) {
                        localStorage.setItem("accessToken", res.access_token);
                        //var headers = {};
                        //headers.Authorization = 'Bearer ' + res.access_token;
                        //fetch("/admin/home", {
                        //    headers: headers
                        //})
                        window.location.pathname = "/Admin/Home";
                    },
                    error: function (res) {
                        console.log(JSON.stringify(res));
                    }
                })
            }
        }
    }

</script>
