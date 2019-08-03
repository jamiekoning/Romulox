<template>
    <v-container>
            <v-card>
                <v-card-title id="cardTitle" class="headline">
                    Are you sure you want to remove {{ game.name }}?
                </v-card-title>
    
                <v-flex id="confirm" headline>
                    
                    <v-btn color="red" 
                           v-on:click="deleteGame" 
                    > 
                        Remove 
                    </v-btn>
                    
                    <v-btn id="no" 
                           v-bind:to="{name: 'PlatformDetail', params: { platformId: game.platformId } }"
                    > 
                        Back 
                    </v-btn>
                </v-flex>
            </v-card>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";
    import { GameDto } from "../models/GameDto";

    export default {
        name: 'GameDelete',
        data: () => ({
            game: {}
        }),
        methods: {
            initGame() {
                ApiService.get(`/platforms/${this.platformId}/games/${this.gameId}`)
                    .then(function(game) {
                        this.game = Object.create(GameDto);
                        this.game.init(game);
                    }.bind(this));
            },
            deleteGame() {
                ApiService.delete(`/platforms/${this.platformId}/games/${this.gameId}/`)
                    .then(function(response) {
                        this.$router.push({ name: 'PlatformDetail', params: { platformId: this.platformId }});
                    }.bind(this)); 
            }
        },
        created() {
            this.platformId = this.$route.params.platformId;
            this.gameId = this.$route.params.gameId;

            this.initGame();
        }
    }
</script>

<style scoped>
    #confirm {
        padding-bottom: 20px;
    }
    
    #cardTitle {
        padding-left: 30px;
    }
    
    #confirm {
        padding-left: 20px;
    }
    
    #no {
        margin-left: 25px;
    }
</style>