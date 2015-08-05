(function () {
    var app = angular.module('enderecoServico', []);

    app.service('enderecos', function ($http) {

        var servico = this;

        this.get = function (loja, endereco, funcaoCallback) {

            $http.get("/Administrativo/Enderecos/Get", { params: { idLoja: loja, idEndereco: endereco } })
                    .then(function (response) {
                        funcaoCallback(response.data)
                    });
        }

        this.salvar = function (modelo, funcaoCallback) {
            debugger;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: modelo })
                 .then(function (statusOperacao) {
                   funcaoCallback(statusOperacao.data);
               });
        }

        this.transformarParaObjetoView = function(endereco){
            if (endereco === undefined || endereco.idEstado === undefined){
                return endereco;
            }
            var retorno = endereco;
            retorno.idEstado = String(endereco.idEstado);
            return retorno;
        }

    });
})();