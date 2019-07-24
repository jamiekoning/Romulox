var GameDto = {
    id: '',
    name: '',
    platformId: '',
    releaseDate: Date.now(),
    developers: '',
    publishers: '',
    description: '',
    iconImage: '',
    image: '', 
    path: '',
    
    init(game) {
      this.id = game.id;
      this.name = game.name;
      this.platformId = game.platformId;
      this.releaseDate = game.releaseDate;
      this.developers = game.developers;
      this.publishers = game.publishers;
      this.description = game.description;
      this.iconImage = game.iconImage;
      this.image = game.image;
      this.path = game.path.slice(7); //slice 'wwwroot/' from beginning of path
    }  
};

export { GameDto }