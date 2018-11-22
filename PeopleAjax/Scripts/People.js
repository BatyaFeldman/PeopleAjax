$(() => {

    clearAndPopulateTable();

    $("#add-person").on('click', function () {
       const person={
             firstName: $("#first-name").val(),
             lastName: $("#last-name").val(),
             age: $("#age").val()
        };     

        $.post('/Home/AddPerson', person , function () {
            clearAndPopulateTable();

           
        });
        $("#first-name").val("");
        $("#last-name").val("");
        $("#age").val("");
    })

    $('body').on('click', '.btn-info', function () {
        const id = $(this).data('personid');
        const firstName = $(this).data('firstname');
        const lastName = $(this).data('lastname');
        const age = $(this).data('age');

        $("#id").val(id);
        $("#modalFirstName").val(firstName);
        $("#modalLastName").val(lastName);
        $("#modalPersonAge").val(age);

        $("#editModal").modal();

    })

    $('body').on('click', '.btn-primary', function () {
        const person = {
            id: $("#id").val(),
            firstName: $("#modalFirstName").val(),
            lastName: $("#modalLastName").val(),
            age: $("#modalPersonAge").val()
        }
        $.post('/Home/EditPerson', person, function () {
            clearAndPopulateTable();
        });

        $('#editModal').modal('hide');

    });

    $('body').on('click', '.btn-danger', function () {

        const id = $(this).data('personid');

        $.post('/Home/DeletePerson', { personid: id } , function () {
            clearAndPopulateTable();
        });
       
    });

    function clearAndPopulateTable() {
        $('.table tr:gt(0)').remove();
        $.get('/Home/GetPeople', function (people) {
            people.forEach(function (person) {
                $('.table').append(`<tr>
                    <td>${person.FirstName}</td >
                    <td>${person.LastName}</td>
                    <td>${person.Age}</td>
                    <td>
                         <button class="btn btn-info" data-personid="${person.Id}" data-firstname="${person.FirstName}" data-lastname="${person.LastName}" data-age="${person.Age}">Edit Person</button>
                         <button class="btn btn-danger" data-personid="${person.Id}">Delete Person</button>
                    </td>
                                 </tr>`)

            })

        })
    }
});

