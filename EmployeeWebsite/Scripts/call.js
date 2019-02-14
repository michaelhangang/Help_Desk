$(function () {  //call.js
    //Validate  input value
    $("#callModalForm").validate({
        rules: {
            Problem: {  required: true },
            Employee: {  required: true },
            Technician: { required: true },
            TextNotes: { maxlength: 250, required: true },
        },
        errorElement: "div",
        messages: {
            Problem: {
                required: "select Problem."
            },
            Employee: {
                required: "select Employee."
            },
            Technician: {
                required: "select Technician."
            },
            TextNotes: {
                required: "required 1-250 chars.", maxlength: "required 1-250 chars."
            },
          
        }
    });

    //GetAll calls Information
    const getAll = async (msg) => {
        try {
            $('#callList').html('<h3>Finding call Information, please wait..</h3>');
            let response = await fetch('api/calls/');
            if (!response.ok)  //or check for response.status
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let data = await response.json();
            buildCallList(data, true);
            (msg === '') ?  // are we appending to an existing message
                $('#status').text('Calls Loaded') : $('#status').text(`${msg} - Calls Loaded`);

            //get all problems
            response = await fetch('api/problems/');
            if (!response.ok)
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let pros = await response.json();
            localStorage.setItem('allproblems', JSON.stringify(pros));

            //get all employees
            response = await fetch('api/employees/');
            if (!response.ok)
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let emps = await response.json();
            localStorage.setItem('allemployees', JSON.stringify(emps));
        } catch (error) {
            $('#status').text(error.message);
        }
    }// getAll

    //Filter the stored JSON based on srch contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allcalls'));
        let filteredData = allData.filter((emp) => ~emp.EmployeeName.indexOf($('#srch').val()));
        buildCallList(filteredData, false);
    }

    //Setup Default value For Update 
    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('Update');
        $('#modaltitle').html('<h4>Add/Change Call Information</h4>');
        $('#deletebutton').show();
        clearModalFields();
        data.map(cal => {
            if (cal.Id === parseInt(Id)) {
                    loadProblemsDDL(cal.ProblemId.toString());
                    loadEmployeeDDL(cal.EmployeeId.toString());
                    loadTechnicianDDL(cal.TechId.toString());
                    $('#labelDateOpened').text(formatDate(cal.DateOpened));
                    $('#dateOpened').val(formatDate(cal.DateOpened));
                    $('#TextNotes').val(cal.Notes);
                    localStorage.setItem('Id', cal.Id);
                    localStorage.setItem('Timer', cal.Timer);
                    $('#modalstatus').text('Update data');
                
                if (!cal.OpenStatus) {
                    $('#Problem').prop('disabled',true);
                    $('#Employee').prop('disabled', true);
                    $('#Technician').prop('disabled', true);
                    $('#TextNotes').attr('readonly', 'readonly');
                    $('#checkbox').prop('checked', true);
                    $('#labelDateClosed').text(formatDate(cal.DateClosed));
                    $('#actionbutton').hide();
                    $('#checkbox').prop('disabled', true);

                }
            }//if 
        });//data map
        $('#theModal').modal('toggle');
    }// setupForUpdate

    //Setup Default value For Add 
    const setupForAdd = () => {
        $('#actionbutton').val('Add');
        $('#deletebutton').hide();
        $('#modaltitle').html('<h4>Add/Change Call Information</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('Add new call');
        clearModalFields();
        loadProblemsDDL(-1);
        loadEmployeeDDL(-1);
        loadTechnicianDDL(-1);
       
        $('#labelDateOpened').text(formatDate());
        $('#dateOpened').val(formatDate());
    }

   // Populate problem dropdown list
    const loadProblemsDDL = (proid) => {
        html = '';
        $('#Problem').empty();
        let allproblems = JSON.parse(localStorage.getItem('allproblems'));
        allproblems.map(pro => html += `<option value="${pro.Id}">${pro.Description}</option>`);
        $('#Problem').append(html);
        $('#Problem').val(proid);
    }

    // Populate emloyee dropdown list
    const loadEmployeeDDL = (empdiv) => {
        html = '';
        $('#Employee').empty();
        let allproblems = JSON.parse(localStorage.getItem('allemployees'));
        allproblems.map(emp => html += `<option value="${emp.Id}">${emp.Lastname}</option>`);
        $('#Employee').append(html);
        $('#Employee').val(empdiv);
    }

    
    // Populate technician dropdown list
    const loadTechnicianDDL = (empdiv) => {
        html = '';
        $('#Technician').empty();
        var alltechnicans = JSON.parse(localStorage.getItem('allemployees'));
        var technicans =[];
        alltechnicans.map(function (tech) {
            if (tech.IsTech) {
                technicans.push(tech);
            } 
        }
        );
        technicans.map(emp => html += `<option value="${emp.Id}">${emp.Lastname}</option>`);
        $('#Technician').append(html);
        $('#Technician').val(empdiv);
    }

    //Clear Modal Field
    const clearModalFields = () => {
        $('#Problem').val('');
        $('#Employee').val('');
        $('#Technician').val('');
        $('#labelDateOpened').text('');
        $('#labelDateClosed').text('');
        $('#TextNotes').val('');
        $('#checkbox').prop('checked',false);
        $('#Problem').prop('disabled', false);
        $('#Employee').prop('disabled', false);
        $('#Technician').prop('disabled', false);
        $('#TextNotes').attr('readonly', false);
        $('#actionbutton').show();
        $('#checkbox').prop('disabled', false);

        localStorage.removeItem('Id');
        localStorage.removeItem('Timer');

    }

    //Bulid calls list
    const buildCallList = (data, allcall) => {
        $('#callList').empty();
        div = $(`<div style="background-color:lightslategrey;font-family:Arial;font-size:larger" class="list-group-item text-white row d-flex" id="status">Call Info</div>
                <div class="list-group-item row d-flex text-center" id = "heading" >
                <div style="font-weight:bold" class="col-4 h4">Date</div>
                <div style="font-weight:bold" class="col-4 h4">For</div>
                <div style="font-weight:bold" class="col-4 h4">Problem</div>
                </div>`);
        div.appendTo($('#callList'));
        allcall ? localStorage.setItem('allcalls', JSON.stringify(data)) : null;
        btn = $('<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add call</div></button>');
        btn.appendTo($('#callList'));
        data.map(cal => {
            btn = $(`<button class="list-group-item row d-flex" id="${cal.Id}">`);
            btn.html(`<div style="background-color:#C8C8C8"class="col-4" id="Dateopened${cal.Id}">${formatDate(cal.DateOpened)}</div>
                      <div style="background-color:#C8C8C8"class ="col-4" id="employeename${cal.Id}">${cal.EmployeeName}</div>
                      <div style="background-color:#C8C8C8"class="col-4" id="ProblemDescription${cal.Id}">${cal.ProblemDescription}</div>`);
            btn.appendTo($('#callList'));
        });
    }

    //Update method
    const update = async () => {
        try {
            //set up a new client side instance of call
            cal = new Object();
            //populate the properties
            cal.EmployeeId = $('#Employee').val();
            cal.ProblemId = $('#Problem').val();
            cal.TechId = $('#Technician').val();
            cal.DateClosed = $('#dateClosed').val();
            cal.DateOpened = $('#dateOpened').val();
            cal.OpenStatus = $('#checkbox').is(':checked') ? false : true;
            cal.Notes = $('#TextNotes').val();
            // we stored these 3 earlier
            cal.Id = localStorage.getItem('Id');       
            cal.Timer = localStorage.getItem('Timer');
            //send the updated back to the server asynchronously using PUT
            let response = await fetch('api/calls', {
                method: 'Put',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(cal)
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

    //Add method
    const add = async () => {
        try {
            emp = new Object();
            //populate the properties
            emp.Id = -1;
            emp.EmployeeId = $('#Employee').val();
            emp.ProblemId = $('#Problem').val();
            emp.TechId = $('#Technician').val();
            emp.DateOpened = $('#dateOpened').val();
            emp.DateClosed = $('#dateClosed').val();

            emp.OpenStatus = $('#checkbox').is(':checked')?false:true;
            emp.Notes = $('#TextNotes').val();

            //send the call info to the server asynchronously using post
            let response = await fetch('api/calls', {
                method: 'POST',
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

    //Delete method
    let _delete = async () => {
        try {
            let response = await fetch(`api/calls/${localStorage
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

    //Set up functionality when update buttion is click
    $("#actionbutton").click((e) => {
        if ($("#callModalForm").valid()) {
            $("#actionbutton").val() === "Update" ? update() : add();
        }
        else {
            $("#modalstatus").text("Fix Errors");
            e.preventDefault;
        }
        // return false; // ignore click so modal remains
    });

    //Confimation modal
    $('[data-toggle=confirmation]').confirmation({ rootSeletor: '[data-toggle=confirmation]' });

    //Set up functionality when delete buttion is click
    $('#deletebutton').click(() => _delete());//if yes was chosen

 

    //What reaction is created when callList is clicked
    $('#callList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'callList' || Id === '') {
            Id = e.target.id;
        }
        if (Id != 'status' && Id != 'heading') {
            let data = JSON.parse(localStorage.getItem('allcalls'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false;//ignore if they clicked on heading or status
        }
    });

    //Search bar 
    $('#srch').keyup(filterData);

    //Getall method
    getAll('');

    //click check box
    $('#checkbox').click(() => {
        if ($('#checkbox').is(':checked') ){
            $("#labelDateClosed").text(formatDate());
            $("#dateClosed").val(formatDate());
        } else {
            $("#labelDateClosed").text('');
            $("#dateClosed").val('');
        }

    });

    //formatDate method
    const formatDate = (date) => {
        let d;
        (date === undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth()+1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min; // formatDate
    }
});

