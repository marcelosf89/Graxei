function habilitarBotaoSalvar() {
    var button = $('#salvarPrecos');
    button.addClass("invisible");
    $(".valor-produto").each(function () {
        if ($(this).val() != $(this).data("preco-original")) {
            button.removeClass("invisible");
            return false;
        }
    });

    $(".descricao-produto").each(function () {
        var tr = $(this).parents("tr");
        var textArea = $(tr).find("textarea");
        var label = $(tr).find("label");
        var valorAreaTexto = textArea.val();
        var valorOriginal = $(this).data("original");
        ////console.log("valorOriginal: " + valorOriginal + "  ///  valorAreaTexto: " + valorAreaTexto);
        if (valorAreaTexto != '' && valorOriginal != valorAreaTexto) {
            var inputValor = $(tr).find("input[type='number']");
            if (inputValor.val() > 0) {
                button.removeClass("invisible");
                return false;
            }
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
}

function resultadoSalvar(args) {
    var elemento = getPainelMensagem();
    elemento.empty();
    elemento.removeClass();
    if (args.Sucesso) {
        elemento.addClass("alert alert-success");
        atualizarNovosProdutos(args);
    } else {
        elemento.addClass("alert alert-danger");
    }
    elemento.html(args.Mensagem)
}

function atualizarNovosProdutos(objeto) {
    var lista = objeto.ProdutosIncluidos;
    for (i = 0; i < lista.length; i++) {
        var elementoAtual = lista[i];
        var tr = getTr(elementoAtual.IdProduto);
        $(tr).attr("meu-prod", elementoAtual.IdMeuProduto);
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
    habilitarBotaoSalvar();
}

function getTr(idProd) {
    return $("tr[id-prod=" + idProd + "]");
}

function alteracaoPendente() {
    var button = $('#salvarPrecos');
    return !button.hasClass("invisible");
}

function getPainelMensagem() {
    return $('#msg-produtos-atualizar');
}

function getHtmlAvisoAlteracaoPendente(paginaAtual) {
    var buttons = '  <button type="button" class="btn btn-success" id="salvar-pendencias">Sim</button> <button type="button" class="btn btn-danger" data-pagina-atual="' + paginaAtual + '" id="descartar-pendencias">Não</button>';
    return 'Há alterações pendentes. Salvar agora?' + buttons;  
}

function paginar(numeroPagina) {
    //var produtos = getJsonProduto();
    var produtos = {};
    produtos.IdLoja = $("#loja-chave").val();
    produtos.DescricaoProduto = $("#descricao-produto").val();
    produtos.MeusProdutos = $("#meus-produtos-chave").prop('checked');
    produtos.TotalElementosLista = $("#total-elementos").val();
    produtos.PaginaAtualLista = numeroPagina;
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
    window.scrollTo(0, 0);
}