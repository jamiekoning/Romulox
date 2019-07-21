var PlatformDto = {
  init(response) {
      let responseData = response.data;
      
      this.id = responseData.id;
      this.name = responseData.name;
      this.platformType = responseData.platformType;
  }  
};

export { PlatformDto };