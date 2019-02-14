$(function () {  //employeeaddupdate.js
    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need vaild email format"
            }
        }
    });

    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");

    const getAll = async (msg) => {
        try {
            $('#employeeList').html('<h3>Finding Employee Information, please wait..</h3>');
            let response = await fetch('api/employees/');
            if (!response.ok)  //or check for response.status
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let data = await response.json();

           
            buildemployeeList(data,true);
            (msg === '') ?  // are we appending to an existing message
                $('#status').text('employees Loaded') : $('#status').text(`${msg} - employees Loaded`);

            response = await fetch('api/departments/');
            if (!response.ok)
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let deps = await response.json();
            localStorage.setItem('alldepartments', JSON.stringify(deps));

        } catch (error) {
            $('#status').text(error.message);
        }
    }// getAll

    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        let filteredData = allData.filter((emp) => ~emp.Lastname.indexOf($('#srch').val()));
        buildemployeeList(filteredData, false);
    }


    const loadDepartmentDDL = (emp) => {
        html = '';
        $('#Department').empty();
        let alldepartment = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartment.map(div => html += `<option value="${div.Id}">${div.Name}</option>`);
        $('#Department').append(html);
        $('#Department').val(emp);

    }

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update employees</h4>');
        clearModalFields();
      
        let validator = $('#EmployeeModalForm').validate();  //validation
        validator.resetForm();
       
        data.map(employee => {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.phoneno);
                $('#TextBoxEmail').val(employee.Email);
                $('#ImageHolder').html(`<img height="120" width="110" src="data:image/png;base64,${employee.StaffPicture64}"/>`);

                localStorage.setItem('Id', employee.Id);
                localStorage.setItem('DepartmentId', employee.DepartmentId);
                localStorage.setItem('Timer', employee.Timer);
                localStorage.setItem('Picture', employee.StaffPicture64);

                $('#modalstatus').text('update data');
                loadDepartmentDDL(employee.DepartmentId.toString());
                $('#theModal').modal('toggle');
                           
            }//if 
        });//data map
    }// setupForUpdate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add employee</h4>');     
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new employee');
        loadDepartmentDDL(-1);
        clearModalFields();
        let validator = $('#EmployeeModalForm').validate();  //validation
        validator.resetForm();
    }

    const clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        $('#ImageHolder').html('');

        localStorage.removeItem('Id');
        localStorage.removeItem('DivisionId');
        localStorage.removeItem('Timer');
        localStorage.removeItem('Picture');

    }

    const buildemployeeList = (data, allemployees) => {
        $('#employeeList').empty();
        div = $(`<div style="background-color:lightslategrey;font-family:Arial;font-size:larger" class="list-group-item text-white  row d-flex" id="status">employee Info</div>
                <div class="list-group-item row d-flex text-center" id = "heading" >
                <div style="font-weight:bold" class="col-4 h4">Title</div>
                <div style="font-weight:bold" class="col-4 h4">First</div>
                <div style="font-weight:bold" class="col-4 h4">Last</div>
                </div>`);
        div.appendTo($('#employeeList'));
        allemployees ? localStorage.setItem('allemployees', JSON.stringify(data)) : null;
        btn = $('<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add employee</div></button>');
        btn.appendTo($('#employeeList'));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.Id}">`);
            btn.html(`<div style="background-color:#C8C8C8" class="col-4" id="employeetitle${emp.Id}">${emp.Title}</div>
                      <div style="background-color:#C8C8C8" class ="col-4" id="employeefname${emp.Id}">${emp.Firstname}</div>
                      <div style="background-color:#C8C8C8" class="col-4" id="employeelastname${emp.Id}">${emp.Lastname}</div>`);
            btn.appendTo($('#employeeList'));
        });
    }

    const update = async () => {
        try {
            //set up a new client side instance of employee
            emp = new Object();
            //populate the properties
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            // we stored these 3 earlier
            emp.Id = localStorage.getItem('Id');
            emp.DepartmentId = $('#Department').val();
            emp.Timer = localStorage.getItem('Timer');
            localStorage.getItem('Picture') ? emp.StaffPicture64 = localStorage.getItem('Picture') : null;

            //send the updated back to the server asynchronously using PUT
            let response = await fetch('api/employees', {
                method: 'Put',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(emp)
            });
            if (response.ok) {
                let data = await response.json();
               getAll(data);
            } else {
                $('#status').text(`${response.status}, text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }//update

    $('input:file').change(() => {
        const reader = new FileReader();
        const file = $('#fileUpload')[0].files[0];
        file ? reader.readAsBinaryString(file) : null;
        reader.onload = (readerEvt) => {
            //get binary data then convert to encoded string 
            const binaryString = reader.result;
            const encodedString = btoa(binaryString);
            localStorage.setItem('Picture', encodedString);
        }
    });

    const add = async () => {
        try {
            emp = new Object();
            //populate the properties
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.DepartmentId = $('#Department').val();
            localStorage.getItem('Picture') ? emp.StaffPicture64 = localStorage.getItem('Picture') : null;
            emp.Id = -1;
            //send the employee info to the server asynchronously using post
            let response = await fetch('api/employees', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) {
                let data = await response.json();
                getAll(data);

            } else {
                $('#status').text(`${response.status},Text-${response.statusText}`);
            }
            $('#theModal').modal('toggle');
        }
        catch (error) {
            $('#status').text(error.message);
        }
    }//add

    $("#actionbutton").click((e) => {
        if ($("#EmployeeModalForm").valid()) {
            $("#actionbutton").val() === "update" ? update() : add();
        } else {
            $("#modalstatus").text("Fix Errors");
            e.preventDefault;
        }
    });

    $('[data-toggle=confirmation]').confirmation({ rootSeletor: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete());//if yes was chosen

    let _delete = async () => {
        try {
            let response = await fetch(`api/employees/${localStorage
                .getItem('Id')}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                });
            if (response.ok)//or check for reponse.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status},Text-${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    $('#employeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;

        if (Id === 'employeeList' || Id === '') {
            Id = e.target.id;
        }
        if (Id != 'status' && Id != 'heading') {
            let data = JSON.parse(localStorage.getItem('allemployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false;//ignore if they clicked on heading or status
        }
    });

    $('#srch').keyup(filterData);

    getAll('');
   
});