$('#submit-btn').on('click', function () {

    Swal.fire({
        title: "از انجام عملیات اطمینان دارید؟",
        text: "بعد از حذف، قادر به بازیابی اطلاعات نخواهید بود!",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "خیر",
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله"
    }).then((result) => {
        if (result.isConfirmed) {
            $('#form-delete').submit();
        }
    });
});

$('#pay-gateway').on('click', function () {
    swal.fire({
        title: "هشدار",
        text: "درگاه پرداخت در حال توسعه میباشد لطفا از گزینه های دیگر استفاده کنید",
        icon: "warning"
    });
});
