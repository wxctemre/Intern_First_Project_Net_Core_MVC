 
$(function () {
    $.ajax({
        url: "/PersonelEkleme/PersonelListeleme",
        type: "GET",
        dataType: "json",
        success: function (data) {
     
            $("#dataTable").DataTable({
                data: data,
                columns: [
                    { "data": 'ad' },
                    { "data": 'soyad' },
                    { "data": 'telefon' },
                    { "data": 'email' },
                    {
                        "data": 'dogumTarihi',
                        "render": function (data, type, row) {
                            var tarih = new Date(data);
                            var gun = tarih.getDate().toString().padStart(2, '0');
                            var ay = (tarih.getMonth() + 1).toString().padStart(2, '0');
                            var yil = tarih.getFullYear();
                            var bugun = new Date();
                            var yas = bugun.getFullYear() - tarih.getFullYear();
                            var ayfark = bugun.getMonth() - tarih.getMonth();
                            if (ayfark < 0 || (ayfark === 0 && bugun.getDate() < tarih.getDate())) {
                                yas--;
                            }
                            return gun + "-" + ay + "-" + yil + "-" + "<br>" + "Yaşı:" + yas;
                        }
                    },
                    
                        {
                            "data": 'medyaUrl', "render": function (data, type, row) {
                                if (row.medyaUrl) {
                                    return '<img src="/MedyaKutuphanesi/' + row.medyaUrl + '"class="medya-img">';
                                }
                                else {
                                    return '';
                                }
                            }



                    },

                    { "data": 'okuladi' },
                    { "data": 'mezunyili' },


                    {
                        "data": 'OkulMedyaUrl', "render": function (data, type, row) {
                            if (row.okulMedyaUrl) {
                                return '<img src="/MedyaKutuphanesi/' + row.okulMedyaUrl + '"class="medya-img">';
                            }
                            else {
                                return '';
                            }
                        }



                    },
                    {
                        "data": 'id', "render": function (data, type, row) {
                            return '<a type="button"class="btn btn-warning btn-sm mx-2" href="/PersonelDuzenleme/PersonelDuzenlemeSayfasi/' + row.id + '">Düzenle</a> <a type="button"class="btn btn-danger btn-sm mx-2" onclick="Sil(' + row.id + ') ">Sil</a> '
                        }
                    }

                ]
            });

        }

    });




});
function Sil(ids) {
    $.ajax({
        url: "/PersonelEkleme/PersonelSil",
        type: "DELETE",
        dataType: "json",
        data: { id: ids },
        success: function () {
            if (true) {
                alert("Silme işlemi başarılı!");
                location.reload();
            }
       
            else{
                alert("Silme işlemi başarısız!");
                location.reload();
            }

        },
        error: function () {
            alert("Silme işlemi sırasında bir hata oluştu!");

        }
    });
}