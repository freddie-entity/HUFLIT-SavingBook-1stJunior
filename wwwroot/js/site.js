﻿$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});



showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
};

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#partial-view").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $.notify('Submit successfully', { globalPosition: 'top center', className: 'success' });
                }
                else 
                    $("#form-modal .modal-body").html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })

    } catch (e) {
        console.log(e);
    }
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record?')) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $("#partial-view").html(res.html);                    
                    $.notify('Delete successfully', { globalPosition: 'top center', className: 'success' });
                        
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (e) {
            console.log(e);
        }
    }

    return false;
}