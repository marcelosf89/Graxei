(function () {

    var app = angular.module('cacheEndereco', ['enderecoServico']);

    app.service('cache', function ($http) {

        this.alterarEstado = function (estadoSelecionado) {
            $http.get("/Administrativo/EnderecosAutoComplete/EstadoSelecionado",
                     { params: { idEstado: estadoSelecionado } }).then(function () {
                     });
        };

        this.alterarCidade = function (estado, cidadeSelecionada) {

            if (estado === undefined || cidadeSelecionada === undefined) {
                return;
            }

            return $http.get("/Administrativo/EnderecosAutoComplete/CidadeSelecionada",
                      { params: { idEstado: estado, cidade: cidadeSelecionada } }).then(function (response) {
                          return response.data;
                      });
        };

        this.alterarBairro = function (estado, cidadeSelecionada, bairroSelecionado) {

            if (estado === undefined || cidadeSelecionada === undefined || bairroSelecionado === undefined) {
                return;
            }

            return $http.get("/Administrativo/EnderecosAutoComplete/BairroSelecionado",
                      { params: { estado: estado, cidade: cidadeSelecionada, bairro: bairroSelecionado } })
                      .then(function (response) {
                          return response.data;
                      });
        };

        this.getCidades = function (valor) {
            return $http.get("/Administrativo/EnderecosAutoComplete/AutoCompleteCidade", { params: { term: valor } })
                        .then(function (response) {
                            return response.data;
                        });
        }

        this.getBairros = function (valor) {
            return $http.get("/Administrativo/EnderecosAutoComplete/AutoCompleteBairro", { params: { term: valor } })
                        .then(function (response) {
                            return response.data;
                        });
        }

        this.getLogradouros = function (valor) {
            return $http.get("/Administrativo/EnderecosAutoComplete/AutoCompleteLogradouro", { params: { term: valor } })
                        .then(function (response) {
                            return response.data;
                        });
        }

    });

})();