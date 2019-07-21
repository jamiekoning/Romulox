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
                           v-on:click=""
                    > 
                        Back 
                    </v-btn>
                </v-flex>
            </v-card>
    </v-container>
</template>

<script>
    import { PlatformsApiService } from "../services/PlatformsApiService";

    export default {
        name: 'GameDelete',
        data: () => ({
            game: {}
        }),
        methods: {
            deleteGame() {
                PlatformsApiService.deleteGame(this.$route.params.platformId, this.$route.params.gameId)
                    .then(function(response) {
                        this.$router.push({ name: 'PlatformDetail', params: { platformId: this.game.platformId }});
                    }.bind(this)); 
            }
        },
        mounted() {
            PlatformsApiService.getGame(this.$route.params.platformId, this.$route.params.gameId)
                .then(function(response) {
                    this.game = response.data;
                }.bind(this));
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