//== Class Definition
var SnippetProfile = function () {
    var handleUpdateProfileFormSubmit = function () {
        $('#m_profile_update_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    UserName: {
                        required: true

                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = { 'FullName': $("#FullName").val(), 'UserName': $("#UserName").val(), 'Password': $("#Password").val(), 'Address': $("#Address").val(), 'RoleID': $('input[name=rbtn]:checked').val() };

            btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);

            form.ajaxSubmit({
                url: '/home/login',
                data: postData,
                success: function (response, status, xhr, $form) {


                }
            });
        });
    }
    var handleAddUserFormSubmit = function () {
        $('#m_add_user_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    FullName: {
                        required: true

                    },
                    UserName: {
                        required: true
                    },
                    Password: {
                        required: true
                    },
                    Address: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = $('this').serialize();

            form.ajaxSubmit({
                url: '/User/InsertUser',
                data: postData,
                success: function (response, status, xhr, $form) {

 


                }
            });
        });
    }
    //== Public Functions
    return {
        // public functions
        init: function () {
            handleUpdateProfileFormSubmit();
            handleAddUserFormSubmit();
        }
    };
}();

//== Class Initialization
jQuery(document).ready(function () {
    SnippetProfile.init();
});