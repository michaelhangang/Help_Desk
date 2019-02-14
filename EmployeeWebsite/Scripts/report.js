$(function () {
    $('#employeebuttun').click(async (e) => {
        try {
            $('#lblstatus').text('generating report on server - please wait...');
            let response = await fetch('api/employeereport');
            if (!response.ok)//check for response.status
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let data = await response.json();
            (data === 'employee report generated')
                ? window.open('PDF/Employee.pdf') :
                $('#lblstatus').text('problem generating report');
        } catch (error) {
            $('#lblstatus').text(error.message);
        }
    });

    $('#callsbutton').click(async (e) => {
        try {
            $('#lblstatus').text('generating report on server - please wait...');
            let response = await fetch('api/callreport');
            if (!response.ok)//check for response.status
                throw new Error(`Status - ${response.status},Text-${response.statusText}`);
            let data = await response.json();
            (data === 'call report generated')
                ? window.open('PDF/Call.pdf') :
                $('#lblstatus').text('problem generating report');
        } catch (error) {
            $('#lblstatus').text(error.message);
        }
    });
});//jQuery