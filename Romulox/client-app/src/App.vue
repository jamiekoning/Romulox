<template>
  <v-app dark>
    <head>
      <link href='https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900|Material+Icons' rel="stylesheet">
    </head>
    
    <v-toolbar app fixed clipped-left>
      <v-toolbar-title>Romulox</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items >
        <v-btn :to="{ path: '/' }" flat>Platforms</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    
    <v-content>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-content>

  </v-app>
</template>

<script>

export default {
  name: 'app',
  data: function () {
    return {
      drawer: true
    }
  }
}
</script>

<style>

</style>
