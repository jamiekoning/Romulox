var DomainService = {
    developmentRoot: 'https://localhost:5001',
    productionRoot: 'https://localhost:5001',
    getCurrentDomain() {
        var nodeEnvironment = process.env.NODE_ENV;
        
        return (nodeEnvironment === 'development') ? this.developmentRoot : this.productionRoot;
    }
};

export { DomainService };