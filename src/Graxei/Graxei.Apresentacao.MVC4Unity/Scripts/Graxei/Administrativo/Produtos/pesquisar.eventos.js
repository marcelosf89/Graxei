$('button[doc-type=paginar]').on("click", function () {
    var paginaAtualLista = $(this).html();
    if (alteracaoPendente(paginaAtualLista)) {
        var painelMensagem = getPainelMensagem();
        painelMensagem.empty();
        painelMensagem.append(getHtmlAvisoAlteracaoPendente(paginaAtualLista));
        document.getElementById('salvar-pendencias').addEventListener("click", salvarPrecos);
        document.getElementById('descartar-pendencias').addEventListener("click", cliqueDescartarPendencias);
        painelMensagem.addClass("alert alert-warning");
        painelMensagem.show();
        window.scrollTo(0, 0);
        return;
    }
    paginar(paginaAtualLista);
});

$("input[doc-type=check-meu-produto]").on("click", function () {
    var id = $(this).parents("tr").attr("id-prod");
    var tr = getTr(id);
    tr.toggleClass("success");
});

$("input[doc-type=valor-meu-produto]").on("keyup", function () {
    var divMensagem = $('#msg-produtos-atualizar')
    divMensagem.empty();
    divMensagem.removeClass();
    var valor = $(this).val();
    if (valor == null || valor < 0) {
        valor = 0;
    }
    var id = $(this).parents("tr").attr("id-prod");
    var tr = getTr(id);
    if (valor <= 0) {
        tr.removeClass("success");
    } else {
        tr.addClass("success");
    }
});

function cliqueDescartarPendencias() {
    var paginaAtual = $(this).data("pagina-atual");
    paginar(paginaAtual);
}

function getJsonProduto() {
    var produtos = {};
    produtos.IdLoja = $("#loja-chave").val();
    produtos.DescricaoProduto = $("#descricao-produto").val();
    produtos.MeusProdutos = $("#meus-produtos-chave").prop('checked');
    produtos.TotalElementosLista = $("#total-elementos").val();
    produtos.PaginaAtualLista = $(this).html();
    return produtos;
}

