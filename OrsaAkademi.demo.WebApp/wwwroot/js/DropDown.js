window.dropdownData = [];
$(function () {

    $.ajax({
        url: '/OkullariGetir',
        type: 'GET',
        dataType: 'json',
        success: function (Data) {
            dropdownData = Data;
            var dropdown = $('#okul_1');
            dropdown.empty();
            dropdown.append('<option value="">Okul Seçin</option>');  
            $.each(Data, function (key, value) {
                dropdown.append('<option value="' + value.id + '">' + value.okuladi + '</option>');
            });

        },


    });






});
