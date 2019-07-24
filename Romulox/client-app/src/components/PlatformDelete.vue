<template>
    <v-container>
        <v-card>
            <v-card-title id="cardTitle" class="headline">
                Are you sure you want to remove {{ platform.name }}?
            </v-card-title>

            <v-flex headline id="confirm">
                <v-btn color="red" 
                       v-on:click="deletePlatform"
                > 
                    Remove 
                </v-btn>
                
                <v-btn id="no" 
                       v-bind:to="{name: 'PlatformDetail', params: { platformId: platform.id } }" 
                       v-on:click=""
                >
                    Back 
                </v-btn>
            </v-flex>
        </v-card>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";
    import { PlatformDto } from "../models/PlatformDto";

    export default {
        name: 'PlatformDelete',
        data: () => ({
            platform: {},
            platformId: ''
        }),
        methods: {
            initPlatform() {
                ApiService.get(`/platforms/${this.platformId}/`)
                    .then(function(platform) {
                        this.platform = Object.create(PlatformDto);
                        this.platform.init(platform);
                    }.bind(this));
            },
            deletePlatform() {
                ApiService.delete(`/platforms/${this.platformId}/`)
                    .then(function(response) {
                        this.$router.push({ name: 'PlatformList' });
                    }.bind(this));
            }
        },
        created() {
            this.platformId = this.$route.params.platformId;

            this.initPlatform();
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