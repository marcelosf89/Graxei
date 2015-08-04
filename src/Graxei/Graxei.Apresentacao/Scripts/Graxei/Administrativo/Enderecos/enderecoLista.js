function excluirEndereco(id, idLoja) {
    var value = confirm('Deseja realmente apagar o endereço?');
    if (!value) {
        return false;
    }
    openL();
    $.ajax({
        url: '@Url.Action("Excluir", "Enderecos")',
        type: 'POST',
        data: { idEndereco: id, idLoja: idLoja },
        success: function (result) {
            $('#fm-content').html(result);
        },
        complete: function () { closeL(); }
    });
}

function registrarAngular() {
    angular.bootstrap(document.getElementById('appEnderecos'), ['endereco']);
}