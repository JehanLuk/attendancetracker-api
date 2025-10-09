function openModal(url, title) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            // Load the fetched HTML into the modal's body
            $('#myModal .modal-body').html(data);
            $('#myModal .modal-title').html(title);
            // Show the modal
            $('#myModal').modal('show');
        }
    });
}
//modal e o metodo que mosta as informaçao em uma caixa suspensa semelante a o alert so que e mais customizavel
//exeplo no html
//<button onclick="openModal('@Url.Action("Edit", "User")', 'Edit User')">
//    Edit
//</button>