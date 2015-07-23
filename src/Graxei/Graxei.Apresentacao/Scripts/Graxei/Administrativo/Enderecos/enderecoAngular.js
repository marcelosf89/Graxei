(function () {
    var app = angular.module('endereco', []);

    app.controller('EnderecoController', ['$http', function ($http) {

        this.endereco = {};

        this.operacao = { ok: false };

        function salvar(endereco) {
            $http.post('/Administrativo/Endereco/Salvar', { enderecoModel: endereco}).
                success(function(statusOperacao){
                    this.operacao = statusOperacao;
                });
        }
    }]);
});