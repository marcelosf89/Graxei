(function () {
    var app = angular.module('endereco', ['ngMessages', 'ui.bootstrap']);

    app.controller('EnderecoController', ['$http', '$timeout', function ($http, $timeout) {

        var controller = this;

        this.endereco = { telefones: [{ numero: "" }] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        this.getData = function (loja, endereco) {
            controller.init(loja, endereco, function (response) {
                controller.endereco = response;
            });
        }

        this.init = function (loja, endereco, funcaoCallback) {
            controller.idLoja = loja;
            if (endereco !== undefined && endereco !== 0) {
                $http.get("/Administrativo/Enderecos/Get", { params: { idLoja: loja, idEndereco: endereco } })
                     .then(function (response) {
                    funcaoCallback(response.data)
                });
            }
        }

        this.salvar = function () {
            var modelo = controller.endereco;
            $http.post('/Administrativo/Enderecos/Salvar', { enderecoModel: modelo }).
                then(function (statusOperacao) {
                    controller.operacao = statusOperacao.data;
                    controller.operacao.renderizar = true;
                });
        }

        this.alterarEstado = function (estadoSelecionado) {
            $http.get("/Administrativo/EnderecosAutoComplete/EstadoSelecionado",
                      { params: { idEstado: estadoSelecionado } }).then(function () {
                      });
        }

        this.alterarCidade = function (enderecoForm) {
            var estado = enderecoForm.estado.$viewValue;
            var cidadeSelecionada = enderecoForm.cidade.$viewValue;

            if (estado === undefined || cidadeSelecionada === undefined) {
                return;
            }

            $http.get("/Administrativo/EnderecosAutoComplete/CidadeSelecionada",
                      { params: { idEstado: estado, cidade: cidadeSelecionada } }).then(function () {
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
                      { params: { estado: estado, cidade: cidadeSelecionada, bairro: bairroSelecionado } }).then(function () {
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