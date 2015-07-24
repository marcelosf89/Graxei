(function () {
    var app = angular.module('endereco', []);

    app.controller('EnderecoController',  ['$http', function ($http) {

        this.estados = {};

        this.endereco = {};

        this.operacao = { ok: false };

        this.salvar = function (argumento) {
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: argumento }).
                success(function (statusOperacao) {
                    this.operacao = statusOperacao;
                });
        }
    }]);
})();