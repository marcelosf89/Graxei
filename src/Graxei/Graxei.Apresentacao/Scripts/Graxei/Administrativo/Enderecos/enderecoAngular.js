(function () {
    var app = angular.module('endereco', ['ngMessages']);

    app.controller('EnderecoController',  ['$http', function ($http) {

        this.estados = {};

        this.endereco = { novaLogicaTelefone: [{ numero: "" }] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        this.init = function(estadosServer, loja){
            this.estados = estadosServer;
            this.idLoja = loja;
        }

        this.salvar = function (modelo) {
            modelo.idLoja = this.idLoja;
            modelo.novaLogicaTelefone = this.endereco.novaLogicaTelefone;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: modelo }).
                success(function (statusOperacao) {
                    this.operacao = statusOperacao;
                });
        }

        this.adicionarTelefone = function () {
            this.endereco.novaLogicaTelefone.push({ numero: "" });
        }

        this.removerTelefone = function (index) {
            this.endereco.novaLogicaTelefone.splice(index, 1);
        }

    }]);
})();