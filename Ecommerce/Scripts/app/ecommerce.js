$(document).ready(function () {

    $('#confirmar').click(function () {
        if ($('#ddd_0').val().trim().length > 0 && $('#telefone_0').val().trim().length > 0 ||
            $('#ddd_1').val().trim().length > 0 && $('#telefone_1').val().trim().length > 0 ||
            $('#ddd_2').val().trim().length > 0 && $('#telefone_2').val().trim().length > 0) {            
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
        $('#ddd_1, #numero_1').val('');        
        $('#div_telefone_1').addClass('hide');
        $('#adicionar-telefone').removeClass('hide');
    });

    $('#excluir_2').click(function () {
        $('#ddd_2, #numero_2').val('');        
        $('#div_telefone_2').addClass('hide');
        $('#adicionar-telefone').removeClass('hide');
    });

    //var form = $('#frm').closest("form");
    //$.ajax({
    //    type: 'POST',
    //    url: '/Cliente/Cadastrar',
    //    data: form.serialize(),
    //    success: function (response) {
    //        alert("Cliente deletado com sucesso!");
    //    },
    //});

});

