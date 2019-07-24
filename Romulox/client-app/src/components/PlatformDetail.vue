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
                       v-bind:to="{ name: 'PlatformDelete', params: { platformId: this.platformId} }"
                       v-on:click=""
                >
                    Remove
                </v-btn>
        </v-layout>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";
    import { GameDto } from "../models/GameDto";
    import { DomainService } from "../services/DomainService";

    export default {
        name: "PlatformDetail",
        data: function() {
            return {
                games: [],
                refreshing: false,
                platformId: ''
            }
        },
        computed: {
            currentDomain: function() {
                console.log(DomainService.getCurrentDomain());
                return DomainService.getCurrentDomain();
            }
        },
        methods: {
            initGames() {
                ApiService.get(`/platforms/${this.platformId}/games`)
                    .then(function (games) {
                        this.createGames(games);
                    }.bind(this));
            },
            createGames(games) {
                for (let game of games) {
                    let gameDto = Object.create(GameDto);
                    gameDto.init(game);
                    
                    this.games.push(gameDto);
                }
            },
            refreshGames() {
                this.refreshing = true;
                
                ApiService.get(`/platforms/${this.platformId}/refresh`)
                    .then(function(response) {
                        //TODO add new games without resetting array
                        this.games = [];
                        this.initGames();
                        this.refreshing = false;
                    }.bind(this));
            },
            sliceGames(length) {
                let gameSlices = [];
                
                for (let i = 0; i <= this.games.length;) {
                    gameSlices.push(this.games.slice(i, i + length));
                    i += length;
                }
                
                return gameSlices;
                
            }
        },
        created() {
            this.platformId = this.$route.params.platformId;
            this.initGames();
        }
    }
</script>

<style scoped>
    #actions {
        padding-top: 20px;
    }
</style>