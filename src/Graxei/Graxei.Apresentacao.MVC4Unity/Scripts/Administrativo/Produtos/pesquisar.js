$('button[doc-type=paginar]').on("click", function () {
    var produtos = {};
    console.log("foi btnpagina");
    produtos.IdLoja = $("#loja-chave").val();
    produtos.DescricaoProduto = $("#descricao-produto").val();
    produtos.MeusProdutos = $("#meus-produtos-chave").prop('checked');
    produtos.TotalElementosLista = $("#total-elementos").val();
    produtos.PaginaAtualLista = $(this).html();
    var url = document.getElementById("pesquisar-produtos-url").value;
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(produtos),
        dataType: "html",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $('#myContentProd').html(result);
        },
    });
});

$("input[doc-type=cp]").on("click", function () {
    var id = $(this).parents("tr").attr("id-prod");
    $("tr[id-prod=" + id + "]").toggleClass("success");
});

$("input[doc-type=ip]").on("keyup", function () {
    $('#msg-produtos-atualizar').empty();
    $('#msg-produtos-atualizar').removeClass();
    var valor = $(this).val();
    if (valor == null || valor < 0) {
        valor = 0;
    }
    var id = $(this).parents("tr").attr("id-prod");

    if (valor <= 0) {
        $("tr[id-prod=" + id + "]").removeClass("success");
    } else {
        $("tr[id-prod=" + id + "]").addClass("success");
    }
});

function habilitarBotaoSalvar(me) {
    var button = $('#salvarPrecos');
    button.addClass("invisible");
    $(".valor-produto").each(function () {
        if ($(this).val() != $(this).data("preco-original")) {
            button.removeClass("invisible");
            return false;
        }
    });
}

function salvarPrecos() {
    var itens = [];
    $('#tabela-precos tbody tr').each(function () {
        var td = $('td', this);
        var preco = $('input[name="precoProduto"]', td).val();
        var idProd = $(this).attr('id-prod');
        var meuProd = $(this).attr('meu-prod');
        var idEndereco = ("#")
        itens.push({
            IdProduto: idProd,
            IdMeuProduto: meuProd,
            IdEndereco: $("#enderecoAtual").val(),
            Preco: preco
        });
    });

    var json = { itens: itens };
    var rota = document.getElementById("salvar-produtos-url").value;
    $.ajax({
        url: rota,
        type: "POST",
        data: JSON.stringify(json),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            resultadoSalvar(result);
        }
    });

    function resultadoSalvar(args) {
        $('#msg-produtos-atualizar').empty();
        if (args.Sucesso) {
            $('#msg-produtos-atualizar').addClass("alert alert-success");
        } else {
            $('#msg-produtos-atualizar').addClass("alert alert-danger");
        }
        $('#msg-produtos-atualizar').html(args.Mensagem)
    }
}