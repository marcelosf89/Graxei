(function () {

    var app = angular.module('pesquisa-produtos', []);

    app.controller('PesquisarController', function ($http) {

        var controller = this;

        this.produtos = {};

        this.exibirBotoes = false;

        this.listaOriginal = {};
        //    lista: [{
        //        id: 111,
        //        codigo: '999',
        //        minhaDescricao: 'Leié',
        //        descricaoOriginal: 'Laiá',
        //        idEndereco: 200,
        //        idMeuProduto: 100,
        //        preco: 20.33
        //    }
        //    ],
        //    total: { valor: 10 },
        //    atual: { valor: 10 }
        //};

        this.init = function (pesquisaModel) {
            controller.postData(pesquisaModel, function (response) {
                controller.produtos = response;
                controller.listaOriginal = controller.produtos;
            });
        }

        this.postData = function (pesquisaModel, funcaoCallback) {
            $http.post("/Administrativo/Produtos/PesquisarJson", { model: pesquisaModel })
                  .then(function (response) {
                      funcaoCallback(response.data);
                  });
        }

        this.habilitarEdicao = function (itemLista, exibir) {
            if (exibir === undefined) {
                exibir = true;
            }
            if (itemLista.minhaDescricao === null || itemLista.minhaDescricao.length === 0) {
                itemLista.minhaDescricao = itemLista.descricaoOriginal;
            }
            itemLista.exibir = exibir;
        };

        this.desfazerEdicao = function (itemLista) {
            var listaOriginal = controller.listaOriginal.lista;
            var id = itemLista.id;
            for (var i = 0; i < listaOriginal.length; i++) {
                if (listaOriginal[i]['id'] == id){
                    itemLista.minhaDescricao = listaOriginal[i].descricaoOriginal;
                    break;
                }
            }

            itemLista.exibir = false;

            controller.habilitarSalvar();
        }

        this.confirmarEdicao = function (itemLista) {

            debugger;
            itemLista.descricaoOriginal = itemLista.minhaDescricao;

            itemLista.exibir = false;

            controller.habilitarSalvar();
        }

        this.cancelarEdicao = function (itemLista) {
            
            itemLista.minhaDescricao = itemLista.descricaoOriginal;

            itemLista.exibir = false;

            controller.habilitarSalvar();
        }

        this.habilitarSalvar = function(){
            var listaParaEdicao = controller.produtos.lista;
            var listaOriginal = controller.listaOriginal.lista;

            controller.exibirBotoes = false;
            debugger;

            for (i = 0; i < listaParaEdicao.length; i++){
                var edicao = listaParaEdicao[i];
                for (j = 0; j < listaOriginal.length; j++) {
                    var original = listaOriginal[j];
                    if (edicao['id'] === original['id'] && (edicao['preco'] != original['preco'] || edicao['minhaDescricao'] || original['minhaDescricao'])){
                        controller.exibirBotoes = true;
                        return;
                    }
                }
            }
        }
    });

})();