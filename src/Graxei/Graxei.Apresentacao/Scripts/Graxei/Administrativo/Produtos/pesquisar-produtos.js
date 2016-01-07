(function () {

    var app = angular.module('pesquisa-produtos', []);

    app.controller('PesquisarController', function ($http) {

        var controller = this;

        this.produtos = {};
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
            debugger;
            $http.post("/Administrativo/Produtos/PesquisarJson", { params: { model: pesquisaModel } })
                   .then(function (response) {

                       controller.produtos = response.data;
                   });

        }

        this.listaOriginal = this.produtos.lista;

        this.habilitarEdicao = function (itemLista, exibir) {
            debugger;
            if (exibir === undefined) {
                exibir = true;
            }
            itemLista.exibir = exibir;
        };

        this.desfazerEdicao = function (itemLista) {
            var listaOriginal = controller.listaOriginal;
            var id = itemLista.id;

            for (var i = 0; i < listaOriginal.length; i++) {
                if (listaOriginal[i]['id'] == id){
                    itemLista.minhaDescricao = listaOriginal[i].descricaoOriginal;
                }
            }

            itemLista.exibir = false;
        }
    });

})();