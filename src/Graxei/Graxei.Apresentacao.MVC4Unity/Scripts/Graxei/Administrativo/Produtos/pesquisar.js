$('button[doc-type=paginar]').on("click", function () {
    var produtos = {};
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
    var currentForm = $("form[name='form-produtos-vendedor']");
    if (!currentForm.valid()) {
        $.each($("textarea"), function (index, element) {
            var divEdicao = $(element).parent("div");
            var divRotulo = divEdicao.siblings("div");
            if (!$(element).hasClass("input-validation-error")) {
                divEdicao.hide();
                divRotulo.show();
            } else {
                divEdicao.show();
                divRotulo.hide();
            }
        });
        return;
    } 

    var itens = [];
    $('#tabela-precos tbody tr').each(function () {
        var td = $('td', this);
        var preco = $('input[name="precoProduto"]', td).val();
        var idProd = $(this).attr('id-prod');
        var meuProd = $(this).attr('meu-prod');
        var descricao = $('textarea', td).val();
        var idEndereco = $('#unico-endereco');
        itens.push({
            IdProduto: idProd,
            IdMeuProduto: meuProd,
            MinhaDescricao: descricao,
            IdEndereco: idEndereco.val(),
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

function habilitarEdicao(element) {
    var parentDiv = $(element).parent("div");
    var divExibir = parentDiv.siblings("div");
    var label = parentDiv.children("label");
    var textArea = divExibir.children("textarea");
    if (textArea.val().trim().length == 0) {
        textArea.val(label.html());
    }
    divExibir.show();
    parentDiv.hide();
    textArea.focus();
}

function exibirPainelEdicao(parentDiv, divToShow) {
}

function tratarCliqueEdicao(element) {
    var textArea = $(element).siblings("textarea");
    var div = $(element).parent("div");
    var painelPrincipalDiv = $(div).siblings("div");
    var currentLabel = painelPrincipalDiv.children("label");
    var valorOriginal = currentLabel.data("original");
    var botaoAtual = $(element).html();
    switch (botaoAtual) {
        case "OK":
            var textArea = $(element).siblings("textarea")
            var validator = $("form[name='form-produtos-vendedor']").validate();
            if (!validator.element(textArea)) {
                return;
            }
            $(currentLabel).text($(textArea).val());
            break;
        case "Desfazer":
            currentLabel.text(valorOriginal);
        default:
            textArea.val("");
    }
    painelPrincipalDiv.show();
    div.hide();
}