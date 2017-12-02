<template>
    <div>
        <nav-menu params="route: route"></nav-menu>

        <hr style="border-top: 1px solid lightgray" />
        <br />
        <div class="container body-content">
            <ul class="nav nav-pills">
                <li role="presentation" class="active"><a href="#">Introduction</a></li>
                <li role="presentation" class="inactive"><a href="#">Account Creator</a></li>
                <li role="presentation" class="inactive"><a href="#">Endpoint Explorer</a></li>
                <li role="presentation" class="inactive"><a href="#">Transaction Builder</a></li>
                <li role="presentation" class="inactive"><a href="#">Transaction Signer</a></li>
                <li role="presentation" class="inactive"><a href="#">XDR Viewer</a></li>
            </ul>
        </div>
        <router-view></router-view>
        <hr style="border-top: 1px solid lightgray" />
        <div>
            <footer class="container body-content">
                <p>&copy; 2017 - Laboratory SampleApp</p>
            </footer>
        </div>

    </div>

</template>

<script>
    import Vue from 'vue'
    import CounterExample from './counter-example'
    import FetchData from './fetch-data'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'

    Vue.component('counter-example', CounterExample);
    Vue.component('fetch-data', FetchData);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);

    export default {
        data() {
            return {
            }
        }
    }
</script>

<style>
</style>
