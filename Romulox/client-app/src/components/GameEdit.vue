<template>
    <v-container>
        <v-card>
            <v-card-title>
                <h2> Editing {{ game.name }} </h2>
            </v-card-title>
            
            <v-card-text>
                <v-form @submit.prevent="gameSubmit">
                    <v-layout>
                        <v-text-field label="Title" 
                                      v-model="name" 
                                      v-bind:placeholder="game.name"
                        ></v-text-field>
                    </v-layout>
        
                    <v-layout>
                        <v-text-field label="Description" 
                                      v-model="description" 
                                      v-bind:placeholder="game.description"
                        ></v-text-field>
                    </v-layout>
        
                    <v-layout>
                        <v-text-field label="Developers" 
                                      v-model="developers" 
                                      v-bind:placeholder="game.developers"
                        ></v-text-field>
                    </v-layout>
        
                    <v-layout>
                        <v-text-field label="Publishers" 
                                      v-model="publishers" 
                                      v-bind:placeholder="game.publishers"
                        ></v-text-field>
                    </v-layout>
        
                    <v-layout>
                        <v-btn type="submit" color="green" outlined
                               v-bind:disabled="editing" 
                        >
                            Edit Game
                        </v-btn>
                    </v-layout>
                </v-form>
            </v-card-text>
        </v-card>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";
    import { GameDto } from "../models/GameDto";

    export default {
        name: 'GameEdit',
        data: () => ({
            game: {},
            
            name: '',
            releaseDate: Date,
            developers: '',
            publishers: '', 
            description: '',
            response: '',
            editing: false,
            
            platformId: '',
            gameId: ''
        }),
        methods: {
            initGame: function() {
                ApiService.get(`/platforms/${this.platformId}/games/${this.gameId}`)
                    .then(function(game) {
                        this.game = Object.create(GameDto);
                        this.game.init(game);
                    }.bind(this));
            },
            gameSubmit() {
                this.editing = true;
                
                let data = {
                    name: this.name || this.game.name,
                    description: this.description || this.game.description,
                    developers: this.developers || this.game.developers,
                    publishers: this.publishers || this.game.publishers
                };
                
                ApiService.post(`/platforms/${this.platformId}/games/${this.gameId}/`, data)
                    .then(function (response) {
                        this.editing = false;
                        this.$router.push({ name: 'GameDetail', params: { platformId: this.platformId, gameId: this.gameId } });
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

</style>