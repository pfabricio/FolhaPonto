jQuery(function ($) {
    $(".Tempo").mask("99:99");

    $(".Cpf").mask("999.999.999-99");

    $(".Numero").keypress(function (e) {
        if (String.fromCharCode(e.keyCode).match(/[^0-9]/g))
            return false;
    });

    $(".data").datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true
    });
});