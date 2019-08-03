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
                v-bind:key="platform.id"
        >
            <v-flex>
                <v-card
                    v-bind:to="{ name: 'PlatformDetail', params: { platformId: platform.id } }"
                >
                    <v-card-text class="headline">
                        {{ platform.name }}
                    </v-card-text>
                </v-card>
            </v-flex>
        </v-layout>

        <v-layout>
            <v-btn outlined color="blue"
                   v-bind:to="{ name: 'PlatformAdd' }"
                    
            >
                Add Platform
            </v-btn>
        </v-layout>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";
    import { PlatformDto } from "../models/PlatformDto";
    
    export default {
        name: 'PlatformList',
        data: () => ({
            platforms: []
        }),
        methods: {
            initPlatforms() {
                ApiService.get('/platforms').then(function (platforms) {
                    this.createPlatforms(platforms);
                }.bind(this));
            },
            createPlatforms(platforms) {
                for (let platform of platforms) {

                    let dto = Object.create(PlatformDto);
                    dto.init(platform);

                    this.platforms.push(dto);
                }
            }
            
        },
        created() {
            this.initPlatforms();
        }
    }
</script>

<style scoped>
    .v-btn {
        margin-left: 10px;
    }
</style>