<template>
    <div>
        <h1>Weather forecast</h1>

        <p>This component demonstrates fetching data from the server.</p> <p v-if="lastUpdated">Last Updated: {{ lastUpdated }}</p>

        <p v-if="!forecasts"><em>Loading...</em></p>

        <table class="table" v-if="forecasts">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="forecast in forecasts" >
                    <td>{{ forecast.DateFormatted }}</td>
                    <td>{{ forecast.TemperatureC }}</td>
                    <td>{{ forecast.TemperatureF }}</td>
                    <td>{{ forecast.Summary }}</td>
                </tr>
            </tbody>
        </table>
        

    </div>
</template>

<script>
    export default {
        data() {
            return {
                connection: null,
                forecasts: null,
                lastUpdated: ''
            }
        },
        created: function () {
            this.connection = new this.$signalR.HubConnection('/weather');
        },
        mounted: function () {
            this.connection.start();

            this.connection.on('weather', data => {
                this.forecasts = data;
                this.lastUpdated = new Date().toLocaleString();
            });
        }
    }
</script>

<style>
</style>
