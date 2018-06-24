var your_site_key = '<%= ConfigurationManager.AppSettings["SiteKey"]%>';
var renderRecaptcha = function () {
    grecaptcha.render('ReCaptchContainer', {
        'sitekey': your_site_key,
        'callback': reCaptchaCallback,
        theme: 'light', //light or dark    
        type: 'image',// image or audio    
        size: 'normal'//normal or compact    
    });
};

var reCaptchaCallback = function (response) {
    if (response !== '') {
        jQuery('#lblMessage').css('color', 'green').html('Success');
    }
};

jQuery('button[type="button"]').click(function (e) {
    var message = 'Please checck the checkbox';
    if (typeof (grecaptcha) != 'undefined') {
        var response = grecaptcha.getResponse();
        (response.length === 0) ? (message = 'Captcha verification failed') : (message = 'Success!');
    }
    jQuery('#lblMessage').html(message);
    jQuery('#lblMessage').css('color', (message.toLowerCase() == 'success!') ? "green" : "red");
});  