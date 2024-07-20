
$(document).ready(function () {

    LoadContacts();

});

function LoadContacts() {
    $('#loader').show();

    $.ajax({
        url: "http://localhost:5191/api/Contactbook/GetAllContacts",
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                // alert("Yeahh I ot the response.");

                response.data.forEach(function (contact) {
                    $('#contactbooklist tbody').append(`
                                 <tr>
                                    <td>${contact.contactId}</td>
                                    <td>${contact.name}</td>
                                    <td>${contact.email}</td>
                                    <td>${contact.phoneNumber}</td>
                                    <td>${contact.company}</td>
                                    <td>
                                            <a href="/CategoryAjax/Edit/${contact.contactId}"> Edit </a> |
                                            <a href="/CategoryAjax/Details/${contact.contactId}"> Details </a> |
                                            <a href="/CategoryAjax/Delete/${contact.contactId}"> Delete </a> |

                                     </td>
                                </tr>
                        `)
                });
            }
        },
        error: function (xhr, status, error) {
            // alert("Something went wrong, please try afer sometime.");

            //check if there is a responseText available

            if (xhr.responseText) {
                try {

                    //parse the responseText into javascriopt object
                    var errorResponse = JSON.parse(xhr.responseText);

                    //check the properties of the errorResponse object
                    if (errorResponse && errorResponse.message) {
                        // Display the error message to the user
                        // alert("Error : " + errorResponse.message);
                        $('#contactbooklist tbody').append(`
                                     <tr>
                                        <td colspan="4">No record found.</td>  
                                    </tr>
                            `)
                    } else {
                        // Display a generic error message
                        alert("An error occured. Please try again later.");

                    }

                } catch (parseError) {
                    console.error("Error parsing response." + parseError);
                    alert("An error occured. Please try again later.");
                }

            }
            else {
                // Display a generic error message if no responseText is available
                alert("An expected error occured. Please try again later.");
            }

        },
        complete: function () {
            $('#loader').hide();

        }


    });

}