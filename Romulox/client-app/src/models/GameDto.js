var GameDto = {
  init(response) {
      let responseData = response.data;

      this.id = responseData.id;
      this.name = responseData.name;
      this.platformId = responseData.platformId;
      this.releaseDate = responseData.releaseDate;
      this.developers = responseData.developers;
      this.publishers = responseData.publishers;
      this.description = responseData.description;
      this.iconImage = responseData.iconImage;
      this.image = responseData.image;
      
  }  
};