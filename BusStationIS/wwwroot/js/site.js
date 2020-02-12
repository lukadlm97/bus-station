// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.






$(document).ready(function () {
	$('.navbar-fostrap').click(function () {
		$('.nav-fostrap').toggleClass('visible');
		$('body').toggleClass('cover-bg');
	});
});

//Delete event handler.
$("body").on("click", "#cityIndexTable .Delete", function () {
    if (confirm("Do you want to delete this row?")) {
        var row = $(this).closest("tr");
        var customerId = row.find("span").html();
        $.ajax({
            type: "POST",
            url: "/Carrier/Delete",
            data: '{id: ' + carrierId + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if ($("#cityIndexTable tr").length > 2) {
                    row.remove();
                } else {
                    row.find(".Delete").hide();
                    row.find("span").html('&nbsp;');
                }
            }
        });
    }
});
