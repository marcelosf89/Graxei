$(document).on('click', '#nao-excluir', function () {
    var td = $(this).closest("td");
    slideSpanUp(td);
});

$(document).on('click', '#sim-excluir', function () {
    var td = $(this).closest("td");
    var id = td.data('id-endereco');
    $.get('Administrativo/Enderecos/Excluir',
          { idEndereco: id },
          function (result) {
            var mensagem = $('.mensagem-endereco');
            if (result.ok === true) {
                mensagem.addClass('alert alert-success');
                var tr = td.closest('tr');
                tr.remove();
            } else {
                mensagem.addClass('alert alert-danger');
                slideSpanUp(td);
            }
            mensagem.html(result.mensagem);
            window.scrollTo(0, 0);
        }
    );
});

$('.excluir-endereco').on('click', function () {
    var td = $(this).closest("td");
    var span = td.children('span');
    if (span.length == 0) {
        span = '<span style="display: none">Excluir endereco?<button type="button" id="sim-excluir" class="btn btn-danger">Sim</button><button type="button" id="nao-excluir" class="btn btn-default">Não</button></span>';
        td.append(span);
        span = td.find("span");
    }
    span.slideToggle();
});

function registrarAngular() {
    angular.bootstrap(document.getElementById('appEnderecos'), ['endereco']);
}

function slideSpanUp(td) {
    var span = td.children('span');
    span.slideUp();
}