<template>
    <v-container>
        <v-layout>
            <h2> Editing {{ game.name }} </h2>
        </v-layout>
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
                <v-btn type="submit" color="green" outline 
                       v-bind:disabled="editing" 
                >
                    Edit Game
                </v-btn>
            </v-layout>
        </v-form>
    </v-container>
</template>

<script>
    import { PlatformsApiService } from "../services/PlatformsApiService";

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
            editing: false
        }),
        methods: {
            getGame: function() {
                PlatformsApiService.getGame(this.$route.params.platformId, this.$route.params.gameId)
                    .then(function(response) {
                        this.game = response.data;
                    }.bind(this));
            },
            gameSubmit() {
                this.editing = true;
                let platformId = this.$route.params.platformId;
                
                let gameId = this.$route.params.gameId;
                
                let data = {
                    name: this.name || this.game.name,
                    description: this.description || this.game.description,
                    developers: this.developers || this.game.developers,
                    publishers: this.publishers || this.game.publishers
                };
                
                PlatformsApiService.postGame(platformId, gameId, data)
                    .then(function (response) {
                        this.editing = false;
                        this.$router.push({ name: 'GameDetail', params: { platformId: this.game.platformId, gameId: this.game.id } });
                }.bind(this));
            }
        },
        created() {
            this.getGame();
        }
    }
</script>

<style scoped>

</style>