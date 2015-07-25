(function () {
    var app = angular.module('endereco', ['ngMessages']);

    app.controller('EnderecoController',  ['$http', function ($http) {

        this.estados = {};

        this.endereco = { telefones: [""] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        this.init = function(estadosServer, loja){
            this.estados = estadosServer;
            this.idLoja = loja;
        }

        this.salvar = function (argumento) {
            argumento.idLoja = this.idLoja;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: argumento }).
                success(function (statusOperacao) {
                    this.operacao = statusOperacao;
                });
        }

        this.adicionarTelefone = function () {
            this.endereco.telefones.push([""]);
        }

        this.removerTelefone = function (index) {
            this.endereco.telefones.splice(index, 1);
        }

    }]);
})();