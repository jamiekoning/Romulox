<template>
    <v-container class="grid-list-xl">
        
        <v-layout v-for="games in sliceGames(5)">
            <v-flex xs2
                    v-for="game in games"
            >
                <v-card max-width="200px"
                        min-height="250px"
                        v-bind:to="{ name: 'GameDetail', params: {platformId: game.platformId, gameId: game.id} }"
                        v-on:click=""
                >
                    <v-img max-height="150px"
                           v-if="game.image"
                           v-bind:src="`${currentDomain}${game.image}`"
                    ></v-img>
                    
                    <v-card-title> {{ game.name }}</v-card-title>
                </v-card>
            </v-flex>
        </v-layout>
        
        <v-layout id="actions">
                <v-btn outline color="blue"
                       v-bind:disabled="refreshing"
                       v-on:click="refreshGames"
                >
                    Refresh Games
                </v-btn>

                <v-btn outline color="red"
                       v-bind:to="{ name: 'PlatformDelete', params: { platformId: platform.id} }"
                       v-on:click=""
                >
                    Remove
                </v-btn>
        </v-layout>
    </v-container>
</template>

<script>
    import { PlatformsApiService } from "../services/PlatformsApiService";
    import { PlatformDto } from "../models/PlatformDto";
    import { DomainService } from "../services/DomainService";

    export default {
        name: "PlatformDetail",
        data: function() {
            return {
                platform: {},
                games: [],
                refreshing: false
            }
        },
        computed: {
            currentDomain: function() {
                console.log(DomainService.getCurrentDomain());
                return DomainService.getCurrentDomain();
            }
        },
        methods: {
            getPlatform: function() {
                let platformId = this.$route.params.platformId;
                
                PlatformsApiService.getPlatform(platformId)
                    .then(function (response) {
                        let platformDto = Object.create(PlatformDto);
                        platformDto.init(response);
                        
                        this.platform = platformDto;
                    }.bind(this));
                
                PlatformsApiService.getGames(platformId)
                    .then(function (response) {
                        console.log(response);
                        this.games = response.data;
                    }.bind(this));
            },
            getGames() {
                let platformId = this.$route.params.platformId;
                PlatformsApiService.getGames(platformId)
                    .then(function (response) {
                       return response.data;
                    }.bind(this));
            },
            refreshGames() {
                this.refreshing = true;
                let platformId = this.$route.params.platformId;
                
                PlatformsApiService.refreshGames(platformId)
                    .then(function(response) {
                       this.getPlatform();
                       this.refreshing = false;
                    }.bind(this));
            },
            sliceGames(length) {
                let gameSets = [];
                
                for (let i = 0; i <= this.games.length;) {
                    gameSets.push(this.games.slice(i, i + length));
                    console.log('set', this.games.slice(i, i + length));
                    i += length;
                }
                
                return gameSets;
                
            }
        },
        created() {
            this.getPlatform();
        }
    }
</script>

<style scoped>
    #actions {
        padding-top: 20px;
    }
</style>