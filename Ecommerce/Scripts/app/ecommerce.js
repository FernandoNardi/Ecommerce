$(document).ready(function () {

    $('#confirmar').click(function () {
        if ($('#telefone_0').val() || $('#telefone_1').val() || $('#telefone_2').val()) {

            if ($('#telefone_0').val()) {
                if ($('#telefone_0').val().length < 13) {
                    alert("Telefone 1 inválido");
                    return false;
                }
            }
            if ($('#telefone_1').val()) {
                if ($('#telefone_1').val().length < 13) {
                    alert("Telefone 2 inválido");
                    return false;
                }
            }
            if ($('#telefone_2').val()) {
                if ($('#telefone_2').val().length < 13) {
                    alert("Telefone 3 inválido");
                    return false;
                }
            }
            return true;
        }
        else {
            alert("É obrigatório ter pelo menos um telefone.");
            return false;
        }
    });

    $('#adicionar-telefone').click(function () {
        var exibirAdicionar = 0;
        if ($('#div_telefone_1').hasClass('hide')) {
            $('#div_telefone_1').removeClass('hide');            
        }
        else if ($('#div_telefone_2').hasClass('hide')) {
            $('#div_telefone_2').removeClass('hide');            
        }
        else {
            alert("Só permitido cadastrar 3 telefones.")
        }
        if (!$('#div_telefone_1').hasClass('hide') && !$('#div_telefone_2').hasClass('hide')) {
            $(this).addClass('hide');
        }
    });

    $('#excluir_1').click(function () {
        $('#telefone_1').val('');
        $('#div_telefone_1').addClass('hide');
        $('#adicionar-telefone').removeClass('hide');
    });

    $('#excluir_2').click(function () {
        $('#telefone_2').val('');
        $('#div_telefone_2').addClass('hide');
        $('#adicionar-telefone').removeClass('hide');
    });

    $(".form-control-telefone").mask("(00) 0000-00009");
    $(".form-control-cpf").mask("999.999.999-99");

});

