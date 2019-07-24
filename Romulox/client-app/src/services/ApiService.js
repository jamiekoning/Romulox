import axios from 'axios';

var ApiService = {
    http: axios.create({
        //TODO handle production URL
        baseURL: "https://localhost:5001/api/",
        headers: {'content-typer': 'application/json'}
    }),
    
    get(url, params) {
        return this.http.get(url, { params: params })
            .then(function (response) {
                let data = response.data;
                if (Array.isArray(data)) {
                    return Promise.resolve([...data]);
                }
                else {
                    return Promise.resolve({...data});
                }
            });
    },
    
    post(url, data) {
        return this.http.post(url, data);
    },
    
    delete(url) {
        return this.http.delete(url);
    }
    
};

export { ApiService }