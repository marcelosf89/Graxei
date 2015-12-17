(function () {

    var app = angular.module('pesquisa-produtos', []);

    app.controller('Pesquisar', function () {

        var controller = this;

        controller.produtos = {
            lista: {
                id: 111,
                codigo: '999',
                minhaDescricao: 'Leié',
                descricaoOriginal: 'Laiá',
                idEndereco: 200,
                idMeuProduto: 100,
                preco: 20.33
            },
            total: { valor: 10 },
            atual: { valor: 10 }
        };
    });

})();