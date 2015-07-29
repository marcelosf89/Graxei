(function () {
    var app = angular.module('endereco', ['ngMessages']);

    app.controller('EnderecoController',  ['$http', function ($http) {

        this.endereco = { telefones: [{ numero: "" }] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        var controller = this;

        this.init = function(loja){
            this.idLoja = loja;
        }

        this.salvar = function (modelo) {
            modelo.idLoja = this.idLoja;
            modelo.telefones = this.endereco.telefones;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: modelo }).
                success(function (statusOperacao) {
                    controller.operacao = statusOperacao;
                    controller.operacao.renderizar = true;
                });
        }

        this.adicionarTelefone = function () {
            this.endereco.telefones.push({ numero: "" });
        }

        this.removerTelefone = function (index) {
            this.endereco.telefones.splice(index, 1);
        }

    }]);
})();