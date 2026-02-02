$(function () {

    $('#txt_email').on('blur', function () {
        var email = $('#txt_email').val().trim();
        if (!CheckForValidEmail(email)) return;
        CheckForDuplicateEmails(email);
    })

    $('#btn_save').on('click', function () {
        if (!Validate()) return;
        SaveContact();
    })

    $('#btn_update').on('click', function () {
        if (!Validate()) return;
        UpdateContact();
    })

    $('.delete').on('click', function () {
        debugger;

        var contactId = $(this)
            .closest('tr')
            .find('#hid_ContactId')
            .val();

        fetch('Contact/DeleteContact',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(contactId)
            })
            .then(res => res.json())
            .then(data => {
                alert(data.message);
                location.reload();
            });
    })

    $('.update').on('click', function () {
        var contactId = $(this)
            .closest('tr')
            .find('#hid_ContactId')
            .val();

        $('#hid_ContactId_1').val(contactId);

        fetch('Contact/GetContactById',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(contactId)
            })
            .then(res => res.json())
            .then(data => {
                $('#txt_name').val(data.name);
                if (data.gender == 'Male')
                    $('#rdo_male').prop('checked', true);
                else if (data.gender == 'Female')
                    $('#rdo_female').prop('checked', true);
                $('#txt_email').val(data.email);
                $('#txt_address').val(data.address);
                $('#txt_state').val(data.state);
                $('#drp_select').val(data.country);
                $('#txt_zip').val(data.zip);
                $('#txt_phone').val(data.phone);

                $('#btn_update').show();
                $('#btn_save').hide();
            });
    })
})

function CheckForValidEmail(email) {

    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        alert('Please enter a valid email.');
        return false;
    }
    return true
}

function CheckForDuplicateEmails(email) {
    fetch('Contact/CheckForDuplicateEmails',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(email)
        })
        .then(res => res.json())
        .then(data => alert(data.message));
}
function Validate() {

    if ($('#txt_name').val().trim() == '') {
        alert('Please enter Name');
        return false;
    }
    if ($('input[name="genderOptions"]:checked').val() == null) {
        alert('Please choose Gender');
        return false;
    }
    if ($('#txt_email').val().trim() == '') {
        alert('Please enter Email');
        return false;
    }
    if ($('#drp_select :selected').text() == '--Select--') {
        alert('Please choose a Country');
        return false;
    }
    return true;
}
function SaveContact() {
    var gender = $('input[name="genderOptions"]:checked').val();

    var contact = {
        Name: $('#txt_name').val().trim(),
        Gender: gender,
        Email: $('#txt_email').val().trim(),
        Address: $('#txt_address').val().trim(),
        State: $('#txt_state').val().trim(),
        Zip: $('#txt_zip').val().trim(),
        Country: $('#drp_select :selected').text(),
        Phone: $('#txt_phone').val().trim(),
    }

    fetch('Contact/SaveNewContact',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(contact)
        })
        .then(res => res.json())
        .then(data => {
            alert(data.message);
            location.reload();
        });
}

function UpdateContact() {
    var gender = $('input[name="genderOptions"]:checked').val();

    var contact = {
        ContactId: $('#hid_ContactId_1').val(),
        Name: $('#txt_name').val().trim(),
        Gender: gender,
        Email: $('#txt_email').val().trim(),
        Address: $('#txt_address').val().trim(),
        State: $('#txt_state').val().trim(),
        Zip: $('#txt_zip').val().trim(),
        Country: $('#drp_select :selected').text(),
        Phone: $('#txt_phone').val().trim(),
    }

    fetch('Contact/UpdateContact',
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(contact)
        })
        .then(res => res.json())
        .then(data => {
            alert(data.message);
            location.reload();
        });
}