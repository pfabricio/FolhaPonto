$(function () {

    $(".tempo").mask("99:99");

    $(".mdata").mask("99/99/9999");

    $(".data").datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        autoclose: true
    });

    $(".autocomplete").autocomplete({
        minLength: 2,
        source: function (request, response) {
            var url = $("#FuncionarioNome").attr("dataurl");
            $.getJSON(url, { term: request.term }, function (data) {
                response(data);
            });
        },
        select: function (event, ui) {
            $("input[id=FuncionarioId]").val(ui.item.id);
        },
        change: function (event, ui) {
            if (!ui.item) {
                $("input[id=FuncionarioId]").val("");
            }
        }
    });
})