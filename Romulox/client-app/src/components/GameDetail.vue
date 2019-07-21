<template>
    <v-layout justify-center>
        <v-card max-width="600px" flat>
            <v-card-title>
                <v-flex>
                    <v-img :src="`${domain}${game.image}`"></v-img>
                </v-flex>
            </v-card-title>
            
            <v-card-text>
                <h4 id="name"> {{ game.name }}</h4>
                <div id="description">{{ game.description }}</div>
                <div id="developers">Developed by {{ game.developers }}</div>
                <div id="publishers">Published by {{ game.publishers }}</div>
            </v-card-text>
            
            <v-card-actions>
                <a 
                    class="v-btn v-btn--outline v-btn--large" download
                    v-bind:href="game.path"
                >
                    Download
                </a>

                <v-btn large outline color="blue" 
                       v-bind:to="{ name: 'PlatformDetail', params: { platformId: game.platformId } }" 
                       v-on:click=""
                >
                    Back
                </v-btn>
                
                <v-btn large outline color="green" 
                       v-bind:to="{ name: 'GameEdit', params: { platformId: game.platformId, gameId: game.id } }" 
                       v-on:click=""
                >
                    Edit
                </v-btn>
                
                <v-btn large outline color="red" 
                       v-bind:to="{ name: 'GameDelete', params: { platformId: game.platformId, gameId: game.id } }" 
                       v-on:click=""
                >
                    Remove
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-layout>
</template>

<script>
    import { PlatformsApiService } from "../services/PlatformsApiService";
    import { DomainService } from "../services/DomainService";

    export default {
        name: 'GameDetail',
        props: {
            platformId: String,
            gameId: String
        },
        data: function() {
            return {
                game: {}
            }
        },
        computed: {
            domain() {
                return DomainService.getCurrentDomain();
            } 
        },
        methods: {
            getGame: function() {
                PlatformsApiService.getGame(this.$route.params.platformId, this.$route.params.gameId)
                    .then(function(response) {
                        this.game = response.data;
                    }.bind(this));
            }
        },
        mounted() {
            this.getGame();
        }
    }
</script>

<style scoped>
    #description, #developers, #publishers {
        padding-top: 10px;
        padding-bottom: 10px;
    }
    
    #name {
        font-size: 32px;
    }
</style>