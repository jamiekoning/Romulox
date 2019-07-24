var PlatformDto = {
    id: '',
    name: '',
    platformType: '',
    
    init(platform) {
      if (typeof platform.id === 'string' &&
          typeof platform.name === 'string' &&
          typeof platform.platformType === 'string') {

          this.id = platform.id;
          this.name = platform.name;
          this.platformType = platform.platformType;
      }
  }  
};

export { PlatformDto };