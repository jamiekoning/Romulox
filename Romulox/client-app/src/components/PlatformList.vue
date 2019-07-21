<template>
    <v-container class="grid-list-xl">
        <v-layout justify-center 
                  v-if="platforms.length === 0"
        >
            <p>
                Welcome to Romulox! Add a platform to get started.
            </p>
        </v-layout>
        
        <v-layout 
                v-else
                v-for="platform in platforms"
        >
            <v-flex>
                <v-card
                    v-bind:to="{ name: 'PlatformDetail', params: { platformId: platform.id } }"
                    v-on:click=""
                >
                    <v-card-text class="headline">
                        {{ platform.name }}
                    </v-card-text>
                </v-card>
            </v-flex>
        </v-layout>

        <v-layout>
            <v-btn outline color="blue"
                   v-bind:to="{ name: 'PlatformAdd' }"
                   v-on:click=""
                    
            >
                Add Platform
            </v-btn>
        </v-layout>
    </v-container>
</template>

<script>
    import {PlatformsApiService} from "../services/PlatformsApiService";

    export default {
        name: 'PlatformList',
        data: () => ({
            platforms: []
        }),
        mounted() {
            PlatformsApiService.getPlatforms().then(function (response) {
                if (this.platforms !== response.data) {
                    this.platforms = response.data;
                }
            }.bind(this));
        }
    }
</script>

<style scoped>
    
</style>