<template>
    <v-container>
        <v-card>
            <v-card-title>
                <h2 class="headline"> Add Platform </h2>
            </v-card-title>
            
            <v-card-text>
                <v-form @submit.prevent="platformSubmit">
                    <v-layout>
                        <v-text-field label="Platform Name" placeholder="Sega Genesis"
                                      v-model="platformName"
                        ></v-text-field>
                    </v-layout>
                    
                    <v-layout>
                        <v-text-field label="Platform Path" prefix="/wwwroot/Platforms/" placeholder="Genesis/"
                                      v-model="platformDirectory"
                        ></v-text-field>
                    </v-layout>
                    
                    <v-layout>
                        <v-select label="Platform Type" placeholder="Select Platform Type" 
                                  v-model="platformType" 
                                  v-bind:items="platformTypes"
                        ></v-select>
                    </v-layout>
                    
                    <v-layout>
                        <v-btn type="submit" outlined
                               v-bind:disabled="processing"
                        >
                            Add Platform
                        </v-btn>
                    </v-layout>
                </v-form>
            </v-card-text>
        </v-card>
    </v-container>
</template>

<script>
    import { ApiService } from "../services/ApiService";

    export default {
        name: 'PlatformAdd',
        data: () => ({
            platformName: '',
            platformDirectory: '',
            platformType: '',
            platformTypes: [],
            processing: false
        }),
        methods: {
            initPlatformTypes() {
                ApiService.get('/platforms/types')
                    .then(function(types) {
                        this.platformTypes = types;
                }.bind(this));
            },
            
            platformSubmit() {
                this.processing = true;
                
                ApiService.post('/platforms/', {
                    name: this.platformName,
                    path: 'wwwroot/Platforms/' + this.platformDirectory,
                    platformType: this.platformType
                }).then(function (response) {
                    this.processing = false;
                    this.$router.push({ name: 'PlatformList' });
                }.bind(this));
            }
        },
        created() {
            this.initPlatformTypes();
        }

    }
</script>

<style scoped>
 
</style>