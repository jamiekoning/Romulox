import axios from 'axios';
import { DomainService } from "./DomainService";

//TODO Implement Proper Error Handling
var ApiService = {
    base: DomainService.getCurrentDomain(),
    get(endpoint) {
        return axios.get(this.base + '/' + endpoint);
    },
    post(endpoint, data) {
        return axios.post(this.base + '/' + endpoint, data);
    },
    delete(endpoint) {
        return axios.delete(this.base + '/' + endpoint);
    }
};

var PlatformsApiService = {
    platformsEndpoint: 'api/platforms',
    gamesEndpoint: 'games',
    getPlatforms() {
        return this.get(this.platformsEndpoint);
    },
    getPlatform(platformId) {
        let endpoint = this.platformsEndpoint + '/' + platformId;
        return this.get(endpoint);
    },
    getGames(platformId) {
        let endpoint = this.platformsEndpoint + '/' + platformId + '/' + this.gamesEndpoint;
        return this.get(endpoint)
    },
    getGame(platformId, gameId)
    {
        let endpoint = this.platformsEndpoint + '/' + platformId + '/' + this.gamesEndpoint + '/' + gameId;
        return this.get(endpoint)
    },
    getPlatformTypes() {
        let endpoint = this.platformsEndpoint + '/types';
        return this.get(endpoint);
    },
    postPlatform(data) {
        return this.post(this.platformsEndpoint, data);
    },
    postGame(platformId, gameId, data) {
        let endpoint = this.platformsEndpoint + '/' + platformId + '/' + this.gamesEndpoint + '/' + gameId;
        return this.post(endpoint, data);
    },
    refreshGames(platformId) {
        let endpoint = this.platformsEndpoint + '/' + platformId + '/refresh';
        return this.get(endpoint);
    },
    deleteGame(platformId, gameId) {
        let endpoint = this.platformsEndpoint + '/' + platformId + '/' + this.gamesEndpoint + '/' + gameId;
        return this.delete(endpoint);
    },
    deletePlatform(platformId) {
        let endpoint = this.platformsEndpoint + '/' + platformId;
        return this.delete(endpoint);
    }
};

Object.setPrototypeOf(PlatformsApiService, ApiService);

export { PlatformsApiService };







