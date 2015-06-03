function SuccessEndereco(result) {
    var msg = $('#msgSucesso').val();
    var idLoja = $('#luk').val();
    if (msg != '') {
        $("#modalEnderecos").modal('hide');

        openL();
        $.ajax({
            url: $('#endLista').val(),
            type: 'GET',
            data: { idLoja: idLoja },
            success: function (result) {
                $('#fm-content').html(result);
            },
            complete: function () { closeL(); }
        });
    }
}

$("#IdEstado").on("change", (function () {
    var estado = $("#IdEstado").val();
    if (estado != 0) {
        var url = $("#estadoSelec").val();
        $.post(url, { idEstado: estado },
            function () {
            });
    }
})
);

$("#IdCidade").on("blur", function () {
    var estado = $("#IdEstado").val();
    var nomeCidade = $("#IdCidade").val();
    if (estado > 0 && nomeCidade.length > 0) {
        var url = $("#cidadeSelec").val();
        $.post(url, { idEstado: estado, cidade: nomeCidade },
            function () {

            });
    }
});

$("#IdBairro").on("blur", function () {
    var idEstado = $("#IdEstado").val();
    var nomeCidade = $("#IdCidade").val();
    var nomeBairro = $("#IdBairro").val();
    if (idEstado > 0 && nomeCidade.length > 0 && nomeBairro.length > 0) {
        var url = '@Url.Action("BairroSelecionado", "Enderecos")';
        $.post(url, { estado: idEstado, cidade: nomeCidade, bairro: nomeBairro },
            function () {
            });
    }
});

var autoCidade = $("#autoCidade").val();
var autoBairro = $("#autoBairro").val();
var autoLogradouro = $("#autoLogradouro").val();
var cidades = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: autoCidade + '?term='+$('#IdCidade').val(),
    remote: {
        url: autoCidade + '?term=%QUERY',
        filter: function (response) {
            // Map the remote source JSON array to a JavaScript object array
            return $.map(response, function (val) {
                return {value: val };
            });
        }
    }
});

var bairros = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: autoBairro + '?term='+$('#IdBairro').val(),
    remote: {
        url: autoBairro + '?term=%QUERY',
        filter: function (response) {
            // Map the remote source JSON array to a JavaScript object array
            return $.map(response, function (val) {
                return {value: val };
            });
        }
    }
});

var logradouros = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: autoLogradouro +'?term='+$('#IdLogradouro').val(),
    remote: {
        url: autoLogradouro + '?term=%QUERY',
        filter: function (response) {
            // Map the remote source JSON array to a JavaScript object array
            return $.map(response, function (val) {
                return {value: val };
            });
        }
    }
});

cidades.initialize();
bairros.initialize();
logradouros.initialize();

$('#IdCidade').typeahead(null, {
    name: 'cidades',
    displayKey: 'value',
    source: cidades.ttAdapter()
});

$('#IdBairro').typeahead(null, {
    name: 'bairros',
    displayKey: 'value',
    source: bairros.ttAdapter()
});

$('#IdLogradouro').typeahead(null, {
    name: 'logradouros',
    displayKey: 'value',
    source: logradouros.ttAdapter()
});