//== Class Definition
var SnippetProfile = function () {
    var handleGetUserData = function () {
        $.ajax({
            async: true,
            url: '/User/GetEmployeeByUserID',
            type: 'POST',
            success: function (data) {
                
                var FullName = data.data["0"].FullName;
                var CompanyName = data.data["0"].CompanyName;
                var Occupation = data.data["0"].Occupation;
                var PhoneNo = data.data["0"].PhoneNo;
                var Address = data.data["0"].Address;
                var State = data.data["0"].State;
                var Postcode = data.data["0"].Postcode;
                var City = data.data["0"].City;
                var Linkedin = data.data["0"].Linkedin;
                var Facebook = data.data["0"].Facebook;
                var Twitter = data.data["0"].Twitter;
                var Instagram = data.data["0"].Instagram;

                $('#FullName').val(FullName);
                $('#ProfileName').text(FullName);
                $('#Occupation').val(Occupation);
                $('#CompanyName').val(CompanyName);
                $('#PhoneNo').val(PhoneNo);
                $('#Address').val(Address);
                $('#City').val(City);
                $('#State').val(State);
                $('#Postcode').val(Postcode);
                $('#Linkedin').val(Linkedin);
                $('#Facebook').val(Facebook);
                $('#Twitter').val(Twitter);
                $('#Instagrams').val(Instagram);

                //$("input[name=RoleID][value=" + Role + "]").prop('checked', true);
            }
        });
    }

    var handleUpdateProfileByUserID = function () {
        $('#m_profile_update_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');
            form.validate({
                rules: {
                    FullName: {
                        required: true

                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = $('this').serialize();

            form.ajaxSubmit({
                url: '/User/UpdateEmployeeByUserID',
                data: postData,
                success: function (data) {

                    if (data == "success") {
                        $("#FullName").focus();
                        $("#UpdateSuccess").show();
                        setTimeout(function () { $("#UpdateSuccess").hide(); }, 5000);
                    }

                }
            });
        });
    }
    //== Public Functions
    return {
        // public functions
        init: function () {
            handleGetUserData();
            handleUpdateProfileByUserID();
        }
    };
}();

//== Class Initialization
jQuery(document).ready(function () {
    SnippetProfile.init();
});