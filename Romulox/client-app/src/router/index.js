import Vue from 'vue';
import Router from 'vue-router';

import PlatformAdd from '../components/PlatformAdd';
import PlatformDelete from "../components/PlatformDelete";
import PlatformDetail from '../components/PlatformDetail';
import PlatformList from "../components/PlatformList";

import GameDelete from "../components/GameDelete";
import GameDetail from '../components/GameDetail';
import GameEdit from '../components/GameEdit'

Vue.use(Router);

export default new Router({
   mode: 'history',
   routes: [
       {
           name: 'PlatformList',
           path: '/',
           component: PlatformList,
           meta: {
               title: 'Platforms - Romulox'
           }
       },
       {
           name: 'PlatformAdd',
           path: '/platforms/add',
           component: PlatformAdd,
           meta: {
               title: "Add Platform - Romulox"
           }

       },
       {
           name: 'PlatformDetail',
           path: '/platforms/:platformId',
           component: PlatformDetail,
           meta: {
               title: "Platform Details - Romulox"
           }
       },
       {
           name: 'GameDetail',
           path: '/platforms/:platformId/games/:gameId',
           component: GameDetail,
           meta: {
               title: 'Game Details - Romulox'
           }
       },
       {
           name: 'GameEdit',
           path: '/platforms/:platformId/games/:gameId/edit',
           component: GameEdit,
           meta: {
               title: 'Edit Game - Romulox'
           }
       },
       {
           name: 'GameDelete',
           path: '/platforms/:platformId/games/:gameId/delete',
           component: GameDelete,
           meta: {
               title: 'Remove Game - Romulox'
           }
       },
       {
           name: 'PlatformDelete',
           path: '/platforms/:platformId/delete',
           component: PlatformDelete,
           meta: {
               title: 'Remove Platform - Romulox'
           }
       }
       
   ] 
});


