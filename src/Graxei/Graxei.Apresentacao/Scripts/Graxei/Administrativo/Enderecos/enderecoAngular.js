(function () {
    var app = angular.module('endereco', ['ngMessages', 'ui.bootstrap', 'enderecoServico', 'cacheEndereco']);

    app.controller('EnderecoController', function ($http, enderecos, cache) {

        var controller = this;

        this.endereco = { telefones: [{ numero: "" }] };

        this.operacao = { ok: false };

        this.idLoja = 0;

        this.getData = function (loja, endereco) {
            controller.init(loja, endereco, function (response) {
                controller.endereco = enderecos.transformarParaObjetoView(response);
            });
        }

        this.init = function (loja, endereco, funcaoCallback) {
            controller.endereco.idLoja = loja;
            if (endereco !== undefined && endereco !== 0) {
                enderecos.get(loja, endereco, funcaoCallback);
            }
        }

        this.salvar = function () {
            var modelo = controller.endereco;
            enderecos.salvar(modelo, function (response) {
                controller.operacao = response;
                controller.operacao.renderizar = true;
            });
        }

        this.alterarEstado = function (estadoSelecionado) {
            cache.alterarEstado(estadoSelecionado);
        }

        this.alterarCidade = function (enderecoForm) {
            var estado = controller.endereco.idEstado;
            var cidadeSelecionada = controller.endereco.cidade;

            cache.alterarCidade(estado, cidadeSelecionada);
        }

        this.alterarBairro = function (enderecoForm) {
            var endereco = controller.endereco;
            var estado = endereco.idEstado;
            var cidadeSelecionada = endereco.cidade;
            var bairroSelecionado = endereco.bairro;

            cache.alterarBairro(estado, cidadeSelecionada, bairroSelecionado);
        }

        this.getCidades = function (valor) {
            return cache.getCidades(valor);
        }

        this.getBairros = function (valor) {
            return cache.getBairros(valor);
        }

        this.getLogradouros = function (valor) {
            return cache.getLogradouros(valor);
        }

        this.adicionarTelefone = function () {
            this.endereco.telefones.push({ numero: "" });
        }

        this.removerTelefone = function (index) {
            this.endereco.telefones.splice(index, 1);
        }

    });

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