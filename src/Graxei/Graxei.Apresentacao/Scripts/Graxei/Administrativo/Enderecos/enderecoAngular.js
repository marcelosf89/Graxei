(function () {
    var app = angular.module('endereco', ['ngMessages', 'ui.bootstrap']);

    app.controller('EnderecoController', ['$http', function ($http) {

        var controller = this;

        this.endereco = { telefones: [{ numero: "" }] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        this.init = function (loja, endereco) {
            controller.idLoja = loja;
            debugger;
            if (endereco !== undefined) {
                var x =  $http.get("/Administrativo/Enderecos/Get",
                                          { params: { idLoja: loja, idEndereco: endereco } }).success(function (response) {
                                              return response.data;
                                          });
                controller.endereco = x;
            }
        }

        this.salvar = function (modelo) {
            modelo.idLoja = this.idLoja;
            modelo.telefones = this.endereco.telefones;
            debugger;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: modelo }).
                success(function (statusOperacao) {
                    controller.operacao = statusOperacao;
                    controller.operacao.renderizar = true;
                });
        }

        this.alterarEstado = function (estadoSelecionado) {
            $http.get("/Administrativo/EnderecosAutoComplete/EstadoSelecionado",
                      { params: { idEstado: estadoSelecionado } }).success(function () {
                      });
        }

        this.alterarCidade = function (enderecoForm) {
            var estado = enderecoForm.estado.$viewValue;
            var cidadeSelecionada = enderecoForm.cidade.$viewValue;

            if (estado === undefined || cidadeSelecionada === undefined) {
                return;
            }

            $http.get("/Administrativo/EnderecosAutoComplete/CidadeSelecionada",
                      { params: { idEstado: estado, cidade: cidadeSelecionada } }).success(function () {
                      });
        }

        this.alterarBairro = function (enderecoForm) {
            var estado = enderecoForm.estado.$viewValue;
            var cidadeSelecionada = enderecoForm.cidade.$viewValue;
            var bairroSelecionado = enderecoForm.bairro.$viewValue;

            if (estado === undefined || cidadeSelecionada === undefined || bairroSelecionado === undefined) {
                return;
            }

            $http.get("/Administrativo/EnderecosAutoComplete/BairroSelecionado",
                      { params: { estado: estado, cidade: cidadeSelecionada, bairro: bairroSelecionado } }).success(function () {
                      });
        }

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

        this.adicionarTelefone = function () {
            this.endereco.telefones.push({ numero: "" });
        }

        this.removerTelefone = function (index) {
            this.endereco.telefones.splice(index, 1);
        }

    }]);

    app.directive('focus', function ($timeout) {
        return {
            scope: {
                trigger: '@focus'
            },
            link: function (scope, element) {
                scope.$watch('trigger', function () {
                    $timeout(function () {
                        element[0].focus();
                    }, 500);
                });
            }
        };
    });

})();