$(function () {
    let getAll = async (msg) => {
        try {
            $('#status').text('Finding Employee Information...');
            let response = await fetch('api/employees/');
            if (!response.ok)
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let data = await response.json();
            buildStudentList(data);
            (msg === '') ?
                $('#status').text('employees Loaded') : $('#status').text(`${msg} - employees Loaded`);

            response = await fetch('api/departments/');
            if (!response.ok)
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let deps = await response.json();
            localStorage.setItem('alldepartments', JSON.stringify(deps));
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    const loadDepartmentDDL = (stu) => {
        html = '';
        $('#Department').empty();
        let alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartments.map(div => html += `<option value="${div.Id}">${div.Name}</option>`);
        $('#Department').append(html);
        $('#Department').val(stu);

    }

    let clearModalFields = () => {
        $('#title').val('');
        $('#firstname').val('');
        $('#TextBoxLastname').val('');
        $('#phone').val('');
        $('#email').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    }

    $('#studentList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'studentList' || Id === '') {
            Id = e.target.id;
        }
        if (Id != 'status' && Id != 'heading') {
            let data = JSON.parse(localStorage.getItem('allstudents'));
            clearModalFields();
            data.map(student => {
                if (student.Id === parseInt(Id)) {
                    $('#title').val(student.Title);
                    $('#firstname').val(student.Firstname);               
                    $('#phone').val(student.phoneno);
                    $('#email').val(student.Email);
                    $('#TextBoxLastname').val(student.Lastname);
                    localStorage.setItem('Id', student.Id);
                    localStorage.setItem('DepartmentId', student.DepartmentId);
                    localStorage.setItem('Timer', student.Timer);
                    $('#modalstatus').text('update data');
                    loadDepartmentDDL(student.DepartmentId.toString());
                    $('#theModal').modal('toggle');
                }
            });
        } else {
            return false;//ignore if they clicked on heading or status
        }
    });
    $('#updatebutton').click(async (e) => {
        try {
            //set up a new client side instance of Student
            stu = new Object();
            //populate the properties
            stu.Title = $('#title').val();
            stu.Firstname = $('#firstname').val();
            stu.Lastname = $('#TextBoxLastname').val();
            stu.phoneno = $('#phone').val();
            stu.Email = $('#email').val();
            // we stored these 3 earlier
            stu.Id = localStorage.getItem('Id');
            stu.DepartmentId = $('#Department').val();
            stu.Timer = localStorage.getItem('Timer');
            //send the updated back to the server asynchronously using PUT
            let response = await fetch('api/employees', {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(stu)
            });
            if (response.ok) {
                let data = await response.json();
                $('#status').text(data);
            } else {
                $('#status').text(`${response.status}, text - ${response.statusText}`);
            }
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    });

    let buildStudentList = (data) => {
        $('#studentList').empty();
        div = $(`<div style="background-color:lightslategrey;font-family:Arial;font-size:larger" class="list-group-item text-white  row d-flex" id="status">Employee Info</div>
                <div class="list-group-item row d-flex text-center" id = "heading" >
                <div style="font-weight:bold" class="col-4 h4">Title</div>
                <div style="font-weight:bold"class="col-4 h4">First</div>
                <div style="font-weight:bold"class="col-4 h4">Last</div>
                </div>`);
        div.appendTo($('#studentList'));
        localStorage.setItem('allstudents', JSON.stringify(data));
        data.map(stu => {
            btn = $(`<button class="list-group-item row d-flex" id="${stu.Id}">`);
            btn.html(`<div style="background-color:#C8C8C8" class="col-4" id="studenttitle${stu.Id}">${stu.Title}</div>
                      <div style="background-color:#C8C8C8" class ="col-4" id="studentfname${stu.Id}">${stu.Firstname}</div>
                      <div style="background-color:#C8C8C8" class="col-4" id="studentlastname${stu.Id}">${stu.Lastname}</div>`);
            btn.appendTo($('#studentList'));
        });
    }

    getAll('');
});