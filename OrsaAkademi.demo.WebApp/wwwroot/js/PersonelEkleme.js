
const fotografyuklemealani = new FormData();
var okulformData = new FormData();
function Kaydet() {
 
    var password = $("#Sifre").val();
    var password2 = $("#TekrarSifre").val();
    var telefon = $("#PersonelTelefon").val();
    var telefonDeseni = /^\d{11}$/;
    var email = $("#PersonelEmail").val();
    var emailDeseni = /^[a-zA-Z0-9_\-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if ($("#PersonelAd").val() == "" || $("#PersonelSoyad").val() == "" || $("#PersonelTelefon").val() == "" || $("#PersonelEmail").val() == "" || $("#PersonelDogumTarihi").val() == "" || $("#Sifre").val() == "" || $("#TekrarSifre").val() == "") {
        alert("Lütfen eklemek istediğiniz değerleri boş bırakmayınız")
        return;
    }

    if (!emailDeseni.test(email)) {
        alert("Lütfen geçerli bir e-posta adresi girin.");
        return;
    }
    //} if (!telefonDeseni.test(telefon)) {
    //    alert("Lütfen geçerli bir telefon  girin.");
    //    return;
    //}
    if (password2 != password) {
        alert("Şifreleriniz Uyuşmuyor");
        return;

    }
    if (UploadedImages.length > 4) {
        alert("4 tane medyadan fazla yükleyemezsiniz");
        return;
    }

    var model = {
        Ad: $("#PersonelAd").val(),
        Soyad: $("#PersonelSoyad").val(),
        Telefon: $("#PersonelTelefon").val(),
        Email: $("#PersonelEmail").val(),
        DogumTarihi: $("#PersonelDogumTarihi").val(),
        Sifre: $("#Sifre").val(),
    }
    var tabloidal;
    $.ajax({
        url: "/modelvalid/validatemodel",
        type: "POST",
        dataType: "json",
        data: model,
        success: function (validationResult) {
            if (validationResult.success) {

                $.ajax({
                    url: "/PersonelEkleme/PersonelEkleme2",
                    type: "POST",
                    dataType: "json",
                    data: model,
                    success: function (data) {
                        tabloidal = data.id;
                       
                        if (UploadedImages.length > 0) {
                            $.each(UploadedImages, function (index, file) {

                                fotografyuklemealani.append(`file_${index}`, file);

                            });
                            fotografyuklemealani.append(`PersonelId`, data.id);
                        }

                            $.ajax({
                                url: "/PersonelEkleme/PersonelFotografiKaydet",
                                type: 'POST',
                                data: fotografyuklemealani,
                                processData: false,
                                contentType: false,
                                success: function (photoIds) {
                                    if (allImages != null) {
                                        var personelid = tabloidal;
                                        $('.form-school').each(function (index, element) {
                                            var okulAd = $(element).find('[id^="okul_"]').val();
                                            var mezuniyetYili = $(element).find('[id^="mezuniyet_"]').val();
                                          
                                            okulformData.append('okul_' + (index + 1), okulAd);
                                            okulformData.append('mezuniyet_' + (index + 1), mezuniyetYili);
                                            var customDiv = $(element).find('[id^="customDiv_"]');
                                            if (customDiv.length > 0) {
                                                var customDivId = customDiv.attr('id');
                                                var customDivNumber = customDivId.match(/\d+$/)[0];  
                                                okulformData.append('customDivNumber_' + (index + 1), customDivNumber);
                                            }

                                            var cloneId = $(element).attr('id');
                                         

                                            if (allImages[cloneId]) {
                                                allImages[cloneId].forEach(function (image, imageIndex) {
                                                   
                                                    okulformData.append('okulResim_' + (index + 1) , image.file);
                                                });
                                            }
                                        });
                                        okulformData.append('personelId', personelid);

                                  
                                       
                                    $.ajax({
                                        url: "/TekrarliAlanin/tekrarlialanveriyolla",
                                        type: 'POST',
                                        contentType: 'application/json',
                                        data: okulformData,
                                        processData: false,
                                        contentType: false,
                                        success: function (schoolmedyasi) {

                                            console.log("personel ekleme başarılı");
                                            window.location.href = 'https://localhost:44354/PersonelEkleme/PersonelListesi';


                                        }
                                    });
                                    }


                                }


                            });
                   
                    
                   

                    }

                });

            }
            else {
                alert(validationResult.errors);
            }
        }
    });
}
