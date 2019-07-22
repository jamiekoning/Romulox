<template>
    <v-container>
        <v-layout>
            <h2 class="headline"> Add Platform </h2>
        </v-layout>
        <v-form @submit.prevent="platformSubmit">
            <v-layout>
                <v-text-field label="Platform Name" placeholder="Sega Genesis"
                              v-model="platformName"
                ></v-text-field>
            </v-layout>
            
            <v-layout>
                <v-text-field label="Platform Path" prefix="wwwroot/" placeholder="Platforms/Genesis/"
                              v-model="platformDirectory"
                ></v-text-field>
            </v-layout>

            <v-layout>
                <v-text-field label="No-Intro Dat File Path (Optional)" prefix="wwwroot/" placeholder="Platforms/Genesis/Gen.dat"
                              v-model="noIntroDatFilePath"
                ></v-text-field>
            </v-layout>
            
            <v-layout>
                <v-select label="Platform Type" placeholder="Select Platform Type" 
                          v-model="platformType" 
                          v-bind:items="platformTypes"
                ></v-select>
            </v-layout>
            
            <v-layout>
                <v-btn type="submit" outline 
                       v-bind:disabled="processing"
                >
                    Add Platform
                </v-btn>
            </v-layout>
        </v-form>
    </v-container>
</template>

<script>
    import { PlatformsApiService } from "../services/PlatformsApiService";
    
    export default {
        name: 'PlatformAdd',
        data: () => ({
            platformName: '',
            platformDirectory: '',
            noIntroDatFilePath: '',
            platformType: '',
            platformTypes: [],
            processing: false
        }),
        methods: {
            getPlatformTypes() {
                PlatformsApiService.getPlatformTypes()
                    .then(function(response) {
                        this.platformTypes = response.data;
                }.bind(this));
            },
            platformSubmit() {
                this.processing = true;
                
                PlatformsApiService.postPlatform({
                    name: this.platformName,
                    path: 'wwwroot/' + this.platformDirectory,
                    platformType: this.platformType,
                    noIntroDatFilePath: this.noIntroDatFilePath == null ? null : 'wwwroot/' + this.noIntroDatFilePath
                }).then(function (response) {
                    this.processing = false;
                    this.$router.push({ name: 'PlatformList' });
                }.bind(this));
            }
        },
        created() {
            this.getPlatformTypes();
        }

    }
</script>

<style scoped>
 
</style>