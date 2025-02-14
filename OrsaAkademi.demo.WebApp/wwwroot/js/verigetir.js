
var id;
var id2;
window.silinenpersonelfotograflari = [];
window.silinenokulfotograflari = [];
var allImages = {};
var cloneCounts = {};
var baseId = 1000;

function initializeDropdown() {
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
}

initializeDropdown();

function clearCustomDivInputs(form, okulBilgisiId) {
    var customDiv = form.querySelector('#customDiv_' + okulBilgisiId);
    if (customDiv) {
        var inputs = customDiv.querySelectorAll('input');
        inputs.forEach(function (input) {
            input.value = '';
        });

        var relatedInputs = form.querySelectorAll('input, select');
        relatedInputs.forEach(function (input) {
            if (input.id.includes('_1')) {
                if (input.type === 'select-one') {
                    input.selectedIndex = 0;
                } else {
                    input.value = '';
                }
            }
        });

        var fileInputs = form.querySelectorAll('.veritabanischoolfileinput');
        fileInputs.forEach(function (input) {
            input.value = '';
        });

        var imageContainers = form.querySelectorAll('.image-container');
        imageContainers.forEach(function (container) {
            container.innerHTML = '';
        });
    }
}

$(function () {
    var url = window.location.href;
    var parts = url.split('/');
    id = parts[parts.length - 1];
    id2 = parseInt(id);

    $.ajax({
        url: "/PersonelDuzenleme/GuncellemeGetir",
        type: "GET",
        dataType: "json",
        data: { id: id2 },
        success: function (data) {
          

            var datalar = data.personellers;
            var fotograflar = data.medyalar;

            $("#PersonelAd").val(datalar.ad);
            $("#PersonelSoyad").val(datalar.soyad);
            $("#PersonelTelefon").val(datalar.telefon);
            $("#PersonelEmail").val(datalar.email);
            $("#Sifre").val(datalar.sifre);
            var datetime = new Date(datalar.dogumTarihi);
            var formatDate = datetime.toISOString().slice(0, 10);
            $("#PersonelDogumTarihi").val(formatDate);
         

            const previewArea = $('#previewArea');
            $.each(fotograflar, function (index, photo) {
                var imageid = photo.id;
                var photoData = {
                    ids: imageid,
                    medyaUrl: '/MedyaKutuphanesi/' + photo.medyaUrl,
                    names: photo.medyaAdi
                };
                var containerDiv = $('<div></div>').addClass('image-container col-12');
                const newImage = new Image();
                newImage.id = imageid;
                newImage.src = photoData.medyaUrl;
                newImage.alt = "Yüklenen Fotoğraf";
                newImage.style.maxWidth = "100%";
                newImage.style.maxHeight = "100%";
                newImage.style.width = "100%";
                newImage.style.height = "100%";

                const deletedatabasebutton = document.createElement('button');
                deletedatabasebutton.style.position = 'absolute';
                deletedatabasebutton.innerHTML = 'X';
                deletedatabasebutton.className = 'btn btn-danger rounded-circle d-inline-block';
                deletedatabasebutton.style.transform = 'translate(-100%,-0%)';
                deletedatabasebutton.style.fontSize = '0.5rem';

                deletedatabasebutton.addEventListener('click', function () {
                    var imageIdToDelete = $(this).siblings('img').attr('id');
                    silinenpersonelfotograflari.push(imageIdToDelete);
                    $(this).closest('.image-container').remove();
                    document.getElementById("photoUpload").value = "";
                });

                containerDiv.append(newImage);
                containerDiv.append(deletedatabasebutton);
                previewArea.append(containerDiv);
            });

            var okulbilgileri = data.okulbilgileri;
            var original = document.getElementById('veritabani_1');

            if (okulbilgileri.length > 0) {
                fillOkulBilgisi(original, okulbilgileri[0].okulBilgileri, okulbilgileri[0].okulMedyasi, okulbilgileri[0].okulBilgileri.sirano);
            } else {
                updateToYenialan(original);
            }

            for (var i = 1; i < okulbilgileri.length; i++) {
                var clonedOriginal = original.cloneNode(true);
                var newId = "veritabani_" + (i + 1);
                clonedOriginal.id = newId;
                updateVeritabaniIDs(clonedOriginal, newId);
                fillOkulBilgisi(clonedOriginal, okulbilgileri[i].okulBilgileri, okulbilgileri[i].okulMedyasi, okulbilgileri[i].okulBilgileri.sirano);
                document.getElementById('form-container').appendChild(clonedOriginal);
            }

            function fillOkulBilgisi(container, okulBilgisi, okulMedyasi, sirano) {
                var okulSelect = container.querySelector('[id^="veritabaniokul_"], [id^="yenialanokul_"]');
                if (!okulSelect) {
                    console.log("herhangi bir okul bulunamadı");
                    return;
                }

                fillDropdown($(okulSelect), okulBilgisi.okulid);

                var mezuniyetInput = container.querySelector('[id^="veritabaniokulmezuniyet_"], [id^="yenialanmezuniyet_"]');
                if (mezuniyetInput) {
                    mezuniyetInput.value = okulBilgisi.mezunolduguyil;
                }

               
                if (container.id.startsWith("veritabani_")) {
                    var okulBilgiDiv = container.querySelector('.okulBilgiId');
                    if (!okulBilgiDiv) {
                        okulBilgiDiv = document.createElement('div');
                        okulBilgiDiv.id = "okulBilgiId_" + container.id.split('_')[1];
                        okulBilgiDiv.style.display = "none";
                        okulBilgiDiv.className = "okulBilgiId";
                        okulBilgiDiv.textContent = okulBilgisi.id;
                        container.appendChild(okulBilgiDiv);
                    } else {
                        okulBilgiDiv.textContent = okulBilgisi.id;
                    }
                }

                var previews = container.querySelector('.veritabanischoolarea, .yenialanschoolarea');
                if (previews) {
                    previews.innerHTML = '';
                    okulMedyasi.forEach(function (media) {
                        var img = document.createElement('img');
                        img.src = '/MedyaKutuphanesi/' + media.medyaUrl;
                        img.alt = media.id;
                        img.style.maxWidth = "100%";
                        img.style.maxHeight = "100%";
                        img.style.width = "100%";
                        img.style.height = "100%";
                        var veritabanifotograf = $('<div></div>').addClass('image-container col-6');
                        const deletedatabaseimageokulbutton = document.createElement('button');
                        deletedatabaseimageokulbutton.style.position = 'absolute';
                        deletedatabaseimageokulbutton.innerHTML = 'X';
                        deletedatabaseimageokulbutton.className = 'btn btn-danger rounded-circle d-inline-block';
                        deletedatabaseimageokulbutton.style.transform = 'translate(-100%,-0%)';
                        deletedatabaseimageokulbutton.style.fontSize = '0.5rem';

                        veritabanifotograf.append(img);
                        veritabanifotograf.append(deletedatabaseimageokulbutton);
                        $(previews).append(veritabanifotograf);

                        deletedatabaseimageokulbutton.addEventListener('click', function () {
                            var imageIdToDelete = $(this).siblings('img').attr('alt');
                            silinenokulfotograflari.push(imageIdToDelete);
                            $(this).closest('.image-container').remove();
                            document.getElementById("photoUpload").value = "";
                        });
                    });
                }

                var customDiv = container.querySelector('[id^="customDiv_"]');
                if (!customDiv) {
                    customDiv = document.createElement('div');
                    customDiv.style.visibility = "hidden";
                    container.appendChild(customDiv);
                }

                customDiv.id = "customDiv_" + sirano;
                customDiv.textContent = "Custom Div " + sirano;

                var existingDeleteButton = container.querySelector('.btn-danger.btn-sm');
                if (existingDeleteButton) {
                    existingDeleteButton.remove();
                }

                var deleteButton = document.createElement('button');
                deleteButton.setAttribute('type', 'button');
                deleteButton.setAttribute('class', 'btn btn-danger btn-sm');
                deleteButton.textContent = '-';
                deleteButton.addEventListener('click', function () {
                    deleteVeritabaniForm(container, okulBilgisi.sirano);
                });
                container.querySelector('.col-1').appendChild(deleteButton);
            }

            function updateVeritabaniIDs(clone, newId) {
                var count = newId.split('_')[1];
                clone.querySelector('[id^="veritabaniokul_"]').id = "veritabaniokul_" + count;
                clone.querySelector('[id^="veritabaniokulmezuniyet_"]').id = "veritabaniokulmezuniyet_" + count;
                clone.querySelector('.veritabanischoolfileinput').id = "fileInput_" + count;
                clone.querySelector('.veritabanischoolarea').id = "schoolarea_" + count;
                var fileInputButton = clone.querySelector('.veritabanischoolfileinput + input[type="button"]');
                if (fileInputButton) fileInputButton.setAttribute('onclick', 'document.querySelector("#fileInput_' + count + '").click();');
            }

            function updateToYenialan(original) {
                var newId = "yenialan_1";
                original.id = newId;
                updateCloneIDs(original, newId);
                clearCloneImageArea(original);
                addDeleteButton(original, true);
                updateCustomDiv(original, newId, baseId);
                initializeCounter(newId);
            }

            function deleteVeritabaniForm(form, okulBilgisiId) {
                if (confirm("Bu kaydı silmek istediğinizden emin misiniz?")) {
                    var okulBilgiDiv = form.querySelector('.okulBilgiId');
                    if (okulBilgiDiv) {
                        okulBilgisiIds = okulBilgiDiv.textContent;
                    }


                    $.ajax({
                        url: "/OkulBilgisiSil",
                        type: "PUT",
                        data: { id: okulBilgisiIds },
                        success: function () {
                            if (okulBilgisiId == 1000 || form.id === "yenialan_1" || form.id ==="veritabani_1") {
                                clearCustomDivInputs(form, okulBilgisiId);
                            }
                            else {
                                form.remove();
                                updateIDs();
                            }
                        },
                        error: function () {
                            alert("Silme işlemi başarısız oldu.");
                        }
                    });
                } else {
                    alert("Silme işlemi iptal edildi.");
                }
            }

            document.addEventListener('change', function (event) {
                if (event.target && event.target.classList.contains('veritabanischoolfileinput')) {
                    gelenbilgilerdegisiklik(event.target);
                }
            });

            function gelenbilgilerdegisiklik(input) {
                const schoolarea = input.closest('.form-school').querySelector('.veritabanischoolarea, .yenialanschoolarea');
                const files = input.files;
                const cloneId = input.closest('.form-school').id;

                if (!allImages[cloneId]) {
                    allImages[cloneId] = [];
                    initializeCounter(cloneId);
                }

                [...files].forEach(file => {
                    if (file.type.match('image/jpeg') || file.type.match('image/png') || file.type.match('image/jpg')) {
                        const reader = new FileReader();

                        reader.addEventListener('load', function () {
                            const imageData = this.result;
                            const newImage = new Image();
                            const imageId = getNewImageId(cloneId);

                            newImage.src = imageData;
                            newImage.id = imageId;
                            newImage.alt = "Yüklenen Fotoğraf";
                            newImage.style.maxWidth = "100%";
                            newImage.style.maxHeight = "100%";
                            newImage.style.width = "100%";
                            newImage.style.height = "100%";

                            const containerDiv = document.createElement('div');
                            containerDiv.className = 'image-container col-4 mb-2';
                            containerDiv.id = 'container_' + imageId;

                            const deleteButton = document.createElement('button');
                            deleteButton.innerHTML = 'X';
                            deleteButton.className = 'btn btn-danger rounded-circle d-inline-block';
                            deleteButton.style.position = 'absolute';
                            deleteButton.style.transform = 'translate(-100%, -0%)';
                            deleteButton.style.fontSize = '0.5rem';

                            deleteButton.addEventListener('click', function () {
                                const index = allImages[cloneId].findIndex(img => img.id === imageId);
                                if (index !== -1) {
                                    allImages[cloneId].splice(index, 1);
                                    const container = document.getElementById('container_' + imageId);
                                    if (container) {
                                        schoolarea.removeChild(container);
                                    }
                                }
                            });

                            containerDiv.appendChild(newImage);
                            containerDiv.appendChild(deleteButton);
                            schoolarea.appendChild(containerDiv);

                            allImages[cloneId].push({ id: imageId, file });
                        });

                        reader.readAsDataURL(file);
                    }
                });
            }
        }
    });
});

function cloneForm(button) {
    var original = document.querySelector('.form-school').cloneNode(true);

    var forms = document.querySelectorAll('.form-school');
    var newId = generateNewId(forms);
    var sirano = calculateNewSirano(forms, button);
    updateCloneIDs(original, newId);
    clearCloneImageArea(original);
    insertCloneAtPosition(button, original, newId);
    addDeleteButton(original);
    updateCustomDiv(original, newId, sirano);
    initializeCounter(newId);
    fillDropdown($(original).find('[id^="okul_"], [id^="yenialanokul_"]'));
  

   
    if (newId.startsWith("veritabani_")) {
        var okulBilgiDiv = document.createElement('div');
        okulBilgiDiv.id = "okulBilgiId_" + newId.split('_')[1];
        okulBilgiDiv.style.display = "none";
        okulBilgiDiv.className = "okulBilgiId";
        original.appendChild(okulBilgiDiv);
    } else {
 
        var okulBilgiDiv = original.querySelector('.okulBilgiId'); 
        if (okulBilgiDiv) {
            okulBilgiDiv.remove();
        }
    }
}
 
function generateNewId(forms) {
    var newIdNumber = 1;
    var existingIds = Array.from(forms).map(form => form.id.startsWith('yenialan_') ? parseInt(form.id.split('_')[1]) : 0).filter(id => id > 0).sort((a, b) => a - b);

    while (existingIds.includes(newIdNumber)) {
        newIdNumber++;
    }

    return "yenialan_" + newIdNumber;
}

function calculateNewSirano(forms, button) {
    var prevForm = button.closest('.form-school');
    var prevSirano = parseInt(prevForm.querySelector('[id^="customDiv_"]').id.split('_')[1]);
    var nextForm = prevForm.nextElementSibling;
    var nextSirano = nextForm ? parseInt(nextForm.querySelector('[id^="customDiv_"]').id.split('_')[1]) : (prevSirano + 1000);
    return nextForm ? Math.floor((prevSirano + nextSirano) / 2) : prevSirano + 1000;
}

function updateCloneIDs(clone, newId) {
    clone.id = newId;
    var count = newId.split('_')[1];
    var okulSelect = clone.querySelector('[id^="veritabaniokul_"], [id^="yenialanokul_"]');
    var mezuniyetInput = clone.querySelector('[id^="veritabaniokulmezuniyet_"], [id^="yenialanmezuniyet_"]');
    var fileInput = clone.querySelector('.veritabanischoolfileinput');
    var schoolArea = clone.querySelector('.yenialanschoolarea');

    if (okulSelect) okulSelect.id = "yenialanokul_" + count;
    if (mezuniyetInput) mezuniyetInput.id = "yenialanmezuniyet_" + count;
    if (fileInput) fileInput.id = "fileInput_" + count;
    if (schoolArea) schoolArea.id = "schoolarea_" + count;
     fillDropdown($('#yeniokulalan_1'));
    var fileInputButton = clone.querySelector('.veritabanischoolfileinput + input[type="button"]');
    if (fileInputButton) fileInputButton.setAttribute('onclick', 'document.querySelector("#fileInput_' + count + '").click();');
    var originalolan = document.querySelector('[id^="yenialanokul_1"]');
    if (originalolan && originalolan.options.length <= 1) {
        fillDropdown($(originalolan));
    }
}

function fillDropdown(dropdown, selectedId = "") {
    dropdown.empty();
    dropdown.append('<option value="">Okul Seçin</option>');
    $.each(dropdownData, function (key, value) {
        var option = new Option(value.okuladi, value.id);
        if (value.id === selectedId) {
            option.selected = true;
        }
        dropdown.append(option);
    });
   
}

function updateVeritabaniIDs(clone, newId) {
    var count = newId.split('_')[1];
    clone.querySelector('[id^="okul_"]').id = "veritabaniokul_" + count;
    clone.querySelector('[id^="veritabaniokulmezuniyet_"]').id = "veritabaniokulmezuniyet_" + count;
    clone.querySelector('.veritabanischoolfileinput').id = "fileInput_" + count;
    clone.querySelector('.veritabanischoolarea').id = "schoolarea_" + count;
    var fileInput = clone.querySelector('.veritabanischoolfileinput');
    fileInput.id = "fileInput_" + count;
    var fileInputButton = clone.querySelector('.veritabanischoolfileinput + input[type="button"]');
    fileInputButton.setAttribute('onclick', 'document.querySelector("#fileInput_' + count + '").click();');
}

function clearCloneImageArea(clone) {
    var cloneImageArea = clone.querySelector('.veritabanischoolarea, .yenialanschoolarea');
    cloneImageArea.className = 'yenialanschoolarea';
    cloneImageArea.innerHTML = "";
}

function insertCloneAtPosition(button, clone, newId) {
    var parentForm = button.closest('.form-school');
    parentForm.parentNode.insertBefore(clone, parentForm.nextSibling);
}

function addDeleteButton(clone, isInitial = false) {
    var existingDeleteButton = clone.querySelector('.btn-danger.btn-sm');
    if (existingDeleteButton) {
        existingDeleteButton.remove();
    }

    var deleteButton = document.createElement('button');
    deleteButton.setAttribute('type', 'button');
    deleteButton.setAttribute('class', 'btn btn-danger btn-sm');
    deleteButton.textContent = '-';

    if (isInitial) {
        deleteButton.addEventListener('click', function () {
            clearCustomDivInputs(clone, 1000);
        });
    } else {
        deleteButton.setAttribute('onclick', 'deleteForm(this)');
    }

    clone.querySelector('.col-1').appendChild(deleteButton);
}

function updateCustomDiv(clone, newId, sirano) {
    var customDiv = clone.querySelector('[id^="customDiv_"]');
    if (!customDiv) {
        customDiv = document.createElement('div');
        customDiv.style.visibility = "hidden";
        clone.appendChild(customDiv);
    }

    customDiv.id = "customDiv_" + sirano;
    customDiv.textContent = "Custom Div " + sirano;
}

function deleteForm(button) {
    var formToDelete = button.closest('.form-school');
    var cloneId = formToDelete.id;
    delete allImages[cloneId];
    formToDelete.remove();
    updateIDs();
}

function updateIDs() {
    var forms = document.querySelectorAll('.form-school');
    var newAllImages = {};

    forms.forEach((form, index) => {
        var oldId = form.id;
        if (oldId.startsWith("yenialan_")) {
            var newCount = index + 1;
            var newId = "yenialan_" + newCount;
            var customDivId = calculateNewCustomDivId(forms, newCount - 1);

            updateCustomDivId(form, customDivId);
            updateCloneIDs(form, newId);
            if (allImages[oldId]) {
                newAllImages[newId] = allImages[oldId];
                delete allImages[oldId];
            }
        } else {
            newAllImages[oldId] = allImages[oldId];
        }
    });

    allImages = newAllImages;
}

function calculateNewCustomDivId(forms, index) {
    var prevCustomDivId = index > 0 ? parseInt(forms[index - 1].querySelector('[id^="customDiv_"]').id.split('_')[1]) : baseId;
    var nextCustomDivId = (index + 1 < forms.length) ? parseInt(forms[index + 1].querySelector('[id^="customDiv_"]').id.split('_')[1]) : (prevCustomDivId + 1000);
    return Math.floor((prevCustomDivId + nextCustomDivId) / 2);
}

function updateCustomDivId(form, customDivId) {
    var customDiv = form.querySelector('[id^="customDiv_"]');
    if (customDiv) {
        customDiv.id = "customDiv_" + customDivId;
        customDiv.textContent = "Custom Div " + customDivId;
    }
}

function clearForm(formId) {
    var form = document.getElementById(formId);
    if (form) {
        form.querySelectorAll('select, input').forEach(element => {
            if (element.tagName === 'SELECT') {
                element.selectedIndex = 0;
            } else if (element.type === 'file') {
                element.value = '';
            } else if (element.type !== 'button' && !element.classList.contains('veritabanischoolfileinput')) {
                element.value = '';
            }
        });
        clearCloneImageArea(form);

        delete allImages[formId];
    }
}

function initializeCounter(id) {
    cloneCounts[id] = 1;
}

function incrementCounter(cloneId) {
    if (!cloneCounts[cloneId]) {
        cloneCounts[cloneId] = 1;
    }

    if (!allImages[cloneId] || allImages[cloneId].length === 0) {
        return 'schoolImage_1';
    }

    const lastImageId = allImages[cloneId][allImages[cloneId].length - 1].id;
    const lastIndex = parseInt(lastImageId.replace('schoolImage_', ''));
    return 'schoolImage_' + (lastIndex + 1);
}

function getNewImageId(cloneId) {
    const newId = incrementGlobalCounter();
    cloneCounts[cloneId] = parseInt(newId.replace('schoolImage_', ''));
    return newId;
}

function incrementGlobalCounter() {
    let highestId = 0;
    Object.keys(allImages).forEach(cloneId => {
        if (allImages[cloneId]) {
            allImages[cloneId].forEach(image => {
                const idNumber = parseInt(image.id.replace('schoolImage_', ''));
                if (idNumber > highestId) {
                    highestId = idNumber;
                }
            });
        }
    });
    return 'schoolImage_' + (highestId + 1);
}
