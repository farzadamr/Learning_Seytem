function isNameValid(type) {
    if (type === "first") {
        var firstname = $('#firstname').val();
        return firstname.length > 2 && firstname.length < 15;
    }
    if (type === "last") {
        var lastname = $('#lastname').val();
        return lastname.length > 2 && lastname.length < 15;
    }
    return null;
}
function isPhoneValid(phoneNumber) {
    var phonePattern = /^[0-9]{10,11}$/;
    return phoneNumber && phonePattern.test(phoneNumber);
}
function isEmailValid(email) {
    var emailPattern = /^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,6}$/;
    return email && emailPattern.test(email);
}
function isPasswordValid(password) {
    return password && password.trim().length > 6;
}
function isImageValid(image) {
    return image && image.trim().length > 0;
}
$(document).ready(function () {
    $('#firstname').on('input focus', function () {
        if (!isNameValid("first")) {
            $('#firstname-error').html("لطفا نام را به درستی وارد کنید");
        } else {
            $('#firstname-error').html("");
        }
    });

    $('#lastname').on('input focus', function () {
        if (!isNameValid("last")) {
            $('#lastname-error').html("لطفا نام خانوادگی را به درستی وارد کنید");
        } else {
            $('#lastname-error').html("");
        }
    });
    $('#phonenumber').on('input focus', function () {
        if (!isPhoneValid($('#phonenumber').val())) {
            $('#phonenumber-error').html("شماره تلفن را به درستی وارد کنید");
        }
        else {
            $('#phonenumber-error').html("");
        }
    });
    $('#email').on('input focus', function () {
        if (!isEmailValid($('#email').val())) {
            $('#email-error').html("پست الکترونیک را به درستی وارد کنید");
        }
        else {
            $('#email-error').html("");
        }
    });
    $('#password').on('input focus', function () {
        if (!isPasswordValid($('#password').val())) {
            $('#password-error').html("کلمه عبور را به درستی وارد کنید");
        }
        else {
            $('#password-error').html("");
        }
    });
    $('#person-profile').on('input focus', function () {
        if (!isImageValid($('#person-profile').val())) {
            $('#image-error').html("تصویر را به درستی وارد کنید");
        }
        else {
            $('#image-error').html("");
        }
    });

});
function addPersonValidation() {
    if (
        isNameValid("first") &&
        isNameValid("last") &&
        isPhoneValid($('#phonenumber').val()) &&
        isEmailValid($('#email').val()) &&
        isPasswordValid($('#password').val()) &&
        isImageValid($('#person-profile').val())
    ) {
        $('#personForm').submit();
    }
    else {
        swal.fire({
            title: "اخطار",
            text: "ابتدا خطا های فرم را بررسی کنید",
            icon: "warning"
        });
    }
}



