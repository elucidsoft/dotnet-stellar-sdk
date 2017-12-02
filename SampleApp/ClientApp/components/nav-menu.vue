<template>
    <nav class="navbar navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="http://www.stellar.org" class="navbar-brand" style="font-size: 28px; color: #08b5e5; font-weight: 700; line-height: 1.1; font-family: Clear Sans, sans-serif">stellar</a>
                <span class="navbar-brand" style="font-size: 28px; font-family: Clear Sans, sans-serif; border-left: 1px solid black; padding-top: 0px; padding-bottom: 0px; margin-top: 15px; padding-left: 1px; margin-left: 1px; height: 25px"> </span>
                <a asp-page="/Index" class="navbar-brand" style="font-size: 18px; font-family: Clear Sans, sans-serif; font-weight: normal; line-height: 1.9">Laboratory Sample App</a>

            </div>
            <div class="navbar-collapse collapse text-right">
                <div class="btn-group">
                    <a v-on:click="changeNetwork('testnet')" class="btn" v-bind:class="networkChangeTestnetClass">testnet</a>
                    <a v-on:click="changeNetwork('public')" class="btn" v-bind:class="networkChangePublicClass">public</a>
                </div>
                <br />
                <div style="font-size: 15.75px">
                    <p>
                        https://horizon-testnet.stellar.org
                    </p>
                </div>
            </div>
        </div>
    </nav>
</template>

<script>
    import { routes } from '../routes'
    import { mapActions, mapState } from 'vuex'

    export default {
        data() {
           
            return {
                routes
            }
        },
        computed: {
            ...mapState({
                currentNetwork: state => state.network
            }),
            networkChangeTestnetClass: function () {
                return this.getButtonClass('testnet')
            },
            networkChangePublicClass: function () {
                return this.getButtonClass('public')
            }
        },
        methods: {
            ...mapActions(['setNetwork']),
            changeNetwork: function (newNetwork) {
                this.setNetwork({ network: newNetwork })
            },
            getButtonClass: function (button)
            {
                return {
                    'btn-info': this.currentNetwork === button,
                    'btn-default': this.currentNetwork != button
                }
            }
        }
    }
</script>
<!--<script>
    import { mapActions, mapState } from 'vuex'
    export default {
        data() {
            return {
                autoCount: 0,
            }
        },
        computed: {
            ...mapState({
                currentCount: state => state.counter
            })
        },
        methods: {
            ...mapActions(['setCounter']),
            incrementCounter: function () {
                var counter = this.currentCount + 1;
                this.setCounter({ counter: counter });
            },
            resetCounter: function () {
                this.setCounter({ counter: 0 });
                this.autoCount = 0;
            }
        },
        created() {
            setInterval(() => {
                this.autoCount += 1
            }, 1000)
        }
    }
</script>-->

<style>
</style>
